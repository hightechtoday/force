using System;
using Force.Ddd;

namespace Force.Cqrs.Create
{
    public abstract class CreateGuidEntityHandlerBase<TCommand, TEntity> :
        CreateEntityHandlerBase<Guid, TCommand, TEntity>
        where TEntity : class, IHasId<Guid>
        where TCommand : ICommand<Guid>
    {
    }
}