using System.Threading.Tasks;
using Force.Ddd;

namespace Force.Cqrs.Create
{
    public abstract class CreateLongEntityHandlerBaseAsync<TCommand, TEntity> :
        CreateEntityHandlerBaseAsync<long, TCommand, TEntity>
        where TEntity : class, IHasId<long>
        where TCommand : ICommand<Task<long>>
    {
    }
}