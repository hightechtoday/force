using Force.Ddd;

namespace Force.Cqrs.Update
{
    public abstract class UpdateIntEntityHandlerBase<TEntity, TCommand>: 
        UpdateEntityHandlerBase<int, TEntity, TCommand> 
        where TEntity : class, IHasId<int>
        where TCommand : ICommand, IHasId<int>
    {
    }
}