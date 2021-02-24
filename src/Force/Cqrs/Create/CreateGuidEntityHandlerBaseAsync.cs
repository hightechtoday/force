using System;
using System.Threading.Tasks;
using Force.Ddd;

namespace Force.Cqrs.Create
{
    public abstract class CreateGuidEntityHandlerBaseAsync<TCommand, TEntity> :
        CreateEntityHandlerBaseAsync<Guid, TCommand, TEntity>
        where TEntity : class, IHasId<Guid>
        where TCommand : ICommand<Task<Guid>>
    {
    }
}