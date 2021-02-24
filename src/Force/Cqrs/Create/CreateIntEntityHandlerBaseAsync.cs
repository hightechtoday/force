using System.Threading.Tasks;
using Force.Ddd;

namespace Force.Cqrs.Create
{
    public abstract class CreateIntEntityHandlerBaseAsync<TCommand, TEntity> :
        CreateEntityHandlerBaseAsync<int, TCommand, TEntity>
        where TEntity : class, IHasId<int>
        where TCommand : ICommand<Task<int>>
    {
    }
}