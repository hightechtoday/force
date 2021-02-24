using System;
using System.Linq;
using Force.Ddd;
using Force.Extensions;
using Force.Linq;
using Force.Workflow;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Force.Examples
{
    public abstract class GetEnumerableBase<TQuery, TEntity, TListItem> : IHasServiceProvider
        where TQuery : class
    {
        private IQueryable<TEntity> _queryable;

        private IServiceProvider _serviceProvider;

        IServiceProvider IHasServiceProvider.ServiceProvider
        {
            get => _serviceProvider;
            set => _serviceProvider = value;
        }

        protected GetEnumerableBase(IQueryable<TEntity> queryable)
        {
            _queryable = queryable;
        }

        protected IOrderedQueryable<TListItem> MapFilterAndSort(TQuery query) =>
            EntityFilter(query)
                .PipeTo(q => Map(_queryable, query))
                .PipeTo(q => Filter(q, query))
                .PipeTo(q => Sort(q, query));

        protected virtual IQueryable<TListItem> Map(IQueryable<TEntity> queryable, TQuery query)
            => queryable.ProjectToType<TListItem>();

        private IQueryable<TListItem> Filter(IQueryable<TListItem> listItems, TQuery query)
        {
            listItems = query switch
            {
                IFilter<TListItem> filter => filter.Filter(listItems),
                _ => listItems
            };

            var predicateFilter = _serviceProvider?.GetService<IFilter<TListItem, TQuery>>();
            if (predicateFilter != null) listItems = predicateFilter.Filter(listItems, query);

            return listItems;
        }

        private IOrderedQueryable<TListItem> Sort(IQueryable<TListItem> listItems, TQuery query)
        {
            listItems = query switch
            {
                ISorter<TListItem> sorter => listItems.Sort(sorter),
                _ => listItems.OrderBy(x => 0)
            };

            var sort = _serviceProvider?.GetService<ISorter<TListItem, TQuery>>();
            if (sort != null) listItems = sort.Sort(listItems, query);

            return (IOrderedQueryable<TListItem>) listItems;
        }

        private TQuery EntityFilter(TQuery query)
        {
            var filter = _serviceProvider?.GetService<IFilter<TEntity>>();
            if (filter != null) _queryable = filter.Filter(_queryable);

            var queryFilter = _serviceProvider?.GetService<IFilter<TEntity, TQuery>>();
            if (queryFilter != null) _queryable = queryFilter.Filter(_queryable, query);

            return query;
        }
    }
}