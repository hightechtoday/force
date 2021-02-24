using System.Collections.Generic;
using Force.Ddd.DomainEvents;

namespace Force.Cqrs
{
    public interface IDomainEventHandler<T>: IHandler<T>
        where T: IDomainEvent
    {
    }
    
    public interface IGroupDomainEventHandler<T>: IHandler<T>
        where T: IEnumerable<IDomainEvent>
    {
    }
}