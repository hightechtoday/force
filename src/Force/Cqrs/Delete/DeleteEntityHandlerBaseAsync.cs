using System;
using System.Threading.Tasks;
using Force.Ccc;
using Force.Ddd;
using Force.Workflow;

namespace Force.Cqrs.Delete
{
    public class DeleteEntityHandlerBaseAsync<TKey, TEntity, TCommand> :
        IHasUnitOfWork,
        ICommandHandler<TCommand, Task>
        where TEntity : class, IHasId<TKey>
        where TKey : IEquatable<TKey>
        where TCommand : ICommand<Task>, IHasId<TKey>
    {
        private IUnitOfWork _uow;

        IUnitOfWork IHasUnitOfWork.UnitOfWork
        {
            get => _uow;
            set => _uow = value;
        }

        private TEntity GetEntity(TKey id)
        {
            return _uow.Find<TEntity>(id);
        }

        public Task Handle(TCommand input)
        {
            Delete(input);
            _uow.Commit();
            return Task.CompletedTask;
        }

        protected virtual void Delete(TCommand input)
        {
            _uow.Remove(GetEntity(input.Id));
        }
    }
}