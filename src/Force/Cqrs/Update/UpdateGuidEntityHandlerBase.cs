using System;
using Force.Ddd;

namespace Force.Cqrs.Update
{
    public abstract class UpdateGuidEntityHandlerBase<TEntity, TCommand>: 
        UpdateEntityHandlerBase<Guid, TEntity, TCommand> 
        where TEntity : class, IHasId<Guid>
        where TCommand : ICommand, IHasId<Guid>
    {
    }
}