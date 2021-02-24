using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Force.Ccc;
using Force.Cqrs;
using Force.Ddd;
using Force.Ddd.DomainEvents;
using Force.OperationContext;
using Force.Reflection;
using Force.Validation;
using Force.Workflow;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Force.Tests.InfrastructureTests
{
    public static class Services
    {
        private static readonly Type[] TargetInterfaces =
        {
            typeof(IHandler<,>),
            typeof(ICommandHandler<>),
            typeof(ICommandHandler<,>),
            typeof(IFilter<,>),
            typeof(ISorter<,>),
            typeof(IQueryHandler<,>),
            typeof(IValidator<>),
            typeof(IAsyncValidator<>),
            typeof(IAsyncOperationContextFactory<,>)
        };

        public static IServiceProvider BuildServiceProvider<T>(
            Func<IServiceProvider, T> dbContextFactory,
            params Assembly[] assemblies)
            where T : DbContext
        {
            var sc = new ServiceCollection();
            sc.AddDbContextAndQueryables(dbContextFactory);
            foreach (var assembly in assemblies)
            {
                sc.AddModule(assembly);
                sc.AddMediatR(assembly);
            }

            return sc.BuildServiceProvider();
        }

        public static void AddDbContextAndQueryables<T>(
            this IServiceCollection sc,
            Func<IServiceProvider, T> dbContextFactory)
            where T : DbContext
        {
            sc.AddScoped<DbContext>(dbContextFactory);
            var dbSets = Type<T>
                .PublicProperties
                .Values
                .Where(x => x.PropertyType.IsGenericType
                            && x.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                .ToList();

            foreach (var dbSet in dbSets)
            {
                var entityType = dbSet.PropertyType.GetGenericArguments()[0];
                var qt = typeof(IQueryable<>).MakeGenericType(entityType);
                sc.AddScoped(
                    qt,
                    typeof(QueryableFactory<>).MakeGenericType(entityType));
            }
        }

        public static void AddModule(this IServiceCollection services, Assembly assembly)
        {
            AddInfrastructure(services);

            var impls = new Dictionary<Type, Type>();
            var ctxs = new List<Type>();

            foreach (var type in assembly.GetTypes())
            {
                if (!type.IsTypeDefinition || type.IsAbstract || type.IsGenericType)
                {
                    continue;
                }

                foreach (var inf in type
                    .GetInterfaces()
                    .Where(x => x.IsGenericType))
                {
                    var gtd = inf.GetGenericTypeDefinition();

                    if (TargetInterfaces.Contains(gtd))
                    {
                        impls[inf] = type;
                    }

                    if (typeof(IOperationContext<>) == gtd)
                    {
                        ctxs.Add(type);
                    }
                }
            }

            foreach (var kv in impls)
            {
                services.AddScoped(kv.Key, kv.Value);
            }

            foreach (var ctx in ctxs)
            {
                var inf = ctx
                    .GetInterfaces()
                    .First(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IOperationContext<>));

                var requestType = inf.GenericTypeArguments[0];
                var funcType = typeof(Func<,>).MakeGenericType(requestType, ctx);

                var factoryType = typeof(OperationContextFactory<,>).MakeGenericType(requestType, ctx);

                services.AddScoped(factoryType);

                services.AddScoped(funcType, sp => ((dynamic) sp.GetService(factoryType)).BuildFunc(sp));
            }
        }

        private static void AddInfrastructure(IServiceCollection services)
        {
            services.TryAddScoped<IHandler<IEnumerable<IDomainEvent>>, DomainEventDispatcher>();
            services.TryAddScoped<IUnitOfWork, EfCoreUnitOfWork>();
            services.TryAddScoped(typeof(IWorkflow<,>), typeof(HandlerWorkflowFactory<,>));
            services.TryAddScoped(typeof(IAsyncWorkflow<,>), typeof(HandlerAsyncWorkflowFactory<,>));
            services.TryAddScoped(typeof(IValidator<>), typeof(DataAnnotationValidator<>));
            services.TryAddScoped(typeof(IAsyncValidator<>), typeof(DataAnnotationValidator<>));
        }
    }
}