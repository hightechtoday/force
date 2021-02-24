using System.Threading.Tasks;
using Force.Ddd;

namespace Force.Cqrs.Delete
{
    public class DeleteStringEntityHandlerBaseAsync<TEntity, TCommand> : DeleteEntityHandlerBaseAsync<string, TEntity, TCommand>
        where TCommand : ICommand<Task>, IHasId<string>
        where TEntity : class, IHasId<string>
    {
    }
}