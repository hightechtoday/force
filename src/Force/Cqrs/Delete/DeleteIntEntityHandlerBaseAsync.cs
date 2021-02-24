using System.Threading.Tasks;
using Force.Ddd;

namespace Force.Cqrs.Delete
{
    public class DeleteIntEntityHandlerBaseAsync<TEntity, TCommand> : DeleteEntityHandlerBaseAsync<int, TEntity, TCommand>
        where TCommand : ICommand<Task>, IHasId<int>
        where TEntity : class, IHasId<int>
    {
    }
}