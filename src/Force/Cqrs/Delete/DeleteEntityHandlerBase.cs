using System;
using Force.Ccc;
using Force.Ddd;
using Force.Workflow;

namespace Force.Cqrs.Delete
{
    public class DeleteEntityHandlerBase<TKey, TEntity, TCommand> :
        IHasUnitOfWork,
        ICommandHandler<TCommand>
        where TEntity : class, IHasId<TKey>
        where TKey : IEquatable<TKey>
        where TCommand : ICommand, IHasId<TKey>
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

        public void Handle(TCommand input)
        {
            Delete(input);
            _uow.Commit();
        }

        protected virtual void Delete(TCommand input)
        {
            _uow.Remove(GetEntity(input.Id));
        }
    }
}