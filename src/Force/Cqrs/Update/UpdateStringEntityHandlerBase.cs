using Force.Ddd;

namespace Force.Cqrs.Update
{
    public abstract class UpdateStringEntityHandlerBase<TEntity, TCommand>: 
        UpdateEntityHandlerBase<string, TEntity, TCommand> 
        where TEntity : class, IHasId<string>
        where TCommand : ICommand, IHasId<string>
    {
    }
}