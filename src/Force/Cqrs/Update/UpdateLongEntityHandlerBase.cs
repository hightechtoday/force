using Force.Ddd;

namespace Force.Cqrs.Update
{
    public abstract class UpdateLongEntityHandlerBase<TEntity, TCommand>: 
        UpdateEntityHandlerBase<long, TEntity, TCommand> 
        where TEntity : class, IHasId<long>
        where TCommand : ICommand, IHasId<long>
    {
    }
}