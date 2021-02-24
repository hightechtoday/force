using System;
using System.Collections.Generic;
using System.Linq;
using Force.Cqrs;
using Force.Ddd.DomainEvents;

namespace Force.Ddd
{
    public class DomainEventDispatcher: IHandler<IEnumerable<IDomainEvent>>
    {
        private readonly IServiceProvider _serviceProvider;

        public DomainEventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Handle(IEnumerable<IDomainEvent> input)
        {
            var grouped = input.GroupBy(x => x.GetType());
            foreach (var eventGroup in grouped)
            {
                var handler = _serviceProvider.GetService(GetHandlerType(eventGroup.Key));
                if (handler != null)
                {
                    foreach (dynamic domainEvent in eventGroup)
                    {
                        DispatchSingle(handler, domainEvent);
                    }
                }

         
                handler = _serviceProvider.GetService(GetHandlerType(typeof(IEnumerable<>).MakeGenericType(eventGroup.Key)));
                DispatchMultiple((dynamic)handler, eventGroup, (dynamic)eventGroup.First());
            }
        }

        private void DispatchSingle<T>(IHandler<T> handler, T value)
        {
            handler.Handle(value);
        }
        
        private void DispatchMultiple<T>(
            IHandler<IEnumerable<T>> handler,
            IEnumerable<IDomainEvent> values,
            T dlrHelper /*Type Inference*/)
        {
            handler?.Handle(values.Cast<T>());
        }
        
        private Type GetHandlerType(Type type) => typeof(IHandler<>).MakeGenericType(type);
    }
}