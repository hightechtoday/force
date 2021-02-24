using System;
using System.Threading.Tasks;
using Force.Ddd;

namespace Force.Cqrs.Delete
{
    public class DeleteGuidEntityHandlerBaseAsync<TEntity, TCommand> : DeleteEntityHandlerBaseAsync<Guid, TEntity, TCommand>
        where TCommand : ICommand<Task>, IHasId<Guid>
        where TEntity : class, IHasId<Guid>
    {
    }
}