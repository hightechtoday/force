using Force.Ddd;

namespace Force.Cqrs.Create
{
    public abstract class CreateStringEntityHandlerBase<TCommand, TEntity>: 
        CreateEntityHandlerBase<string, TCommand, TEntity>
        where TEntity: class, IHasId<string>
        where TCommand : ICommand<string>
    {
    }
}