using System;
using Force.Ddd;

namespace Force.Cqrs.Delete
{
    public class DeleteGuidEntityHandlerBase<TEntity, TCommand> : DeleteEntityHandlerBase<Guid, TEntity, TCommand>
        where TCommand : ICommand, IHasId<Guid>
        where TEntity : class, IHasId<Guid>
    {
    }
}