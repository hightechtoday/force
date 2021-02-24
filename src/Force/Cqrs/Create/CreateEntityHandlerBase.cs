using System;
using Force.Ccc;
using Force.Ddd;
using Force.Workflow;

namespace Force.Cqrs.Create
{
    public abstract class CreateEntityHandlerBase<TKey, TCommand, TEntity> :
        IHasUnitOfWork,
        ICommandHandler<TCommand, TKey>
        where TEntity : class, IHasId<TKey>
        where TKey : IEquatable<TKey>
        where TCommand : ICommand<TKey>
    {
        private IUnitOfWork _uow;

        IUnitOfWork IHasUnitOfWork.UnitOfWork
        {
            get => _uow;
            set => _uow = value;
        }

        protected abstract TEntity CreateNewEntity(TCommand input);

        public TKey Handle(TCommand input)
        {
            var entity = CreateNewEntity(input);
            _uow.Add(entity);
            _uow.Commit();
            return entity.Id;
        }
    }
}