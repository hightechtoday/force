using System.Threading.Tasks;
using Force.Ddd;

namespace Force.Cqrs.Create
{
    public abstract class CreateStringEntityHandlerBaseAsync<TCommand, TEntity> :
        CreateEntityHandlerBaseAsync<string, TCommand, TEntity>
        where TEntity : class, IHasId<string>
        where TCommand : ICommand<Task<string>>
    {
    }
}