using System.Threading.Tasks;
using Force.Ddd;

namespace Force.Cqrs.Update
{
    public abstract class UpdateLongEntityHandlerBaseAsync<TEntity, TCommand> :
        UpdateEntityHandlerBaseAsync<long, TEntity, TCommand>
        where TEntity : class, IHasId<long>
        where TCommand : ICommand<Task>, IHasId<long>
    {
    }
}