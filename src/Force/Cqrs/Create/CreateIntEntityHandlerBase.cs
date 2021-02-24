using Force.Ddd;

namespace Force.Cqrs.Create
{
    public abstract class CreateIntEntityHandlerBase<TCommand, TEntity> :
        CreateEntityHandlerBase<int, TCommand, TEntity>
        where TEntity : class, IHasId<int>
        where TCommand : ICommand<int>
    {
    }
}