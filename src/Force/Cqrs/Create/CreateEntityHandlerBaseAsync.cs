using System;
using System.Threading.Tasks;
using Force.Ccc;
using Force.Ddd;
using Force.Workflow;

namespace Force.Cqrs.Create
{
    public abstract class CreateEntityHandlerBaseAsync<TKey, TCommand, TEntity> :
        IHasUnitOfWork,
        ICommandHandler<TCommand, Task<TKey>>
        where TEntity : class, IHasId<TKey>
        where TKey : IEquatable<TKey>
        where TCommand : ICommand<Task<TKey>>
    {
        private IUnitOfWork _uow;

        IUnitOfWork IHasUnitOfWork.UnitOfWork
        {
            get => _uow;
            set => _uow = value;
        }

        protected abstract Task<TEntity> CreateNewEntityAsync(TCommand input);

        public async Task<TKey> Handle(TCommand input)
        {
            var entity = await CreateNewEntityAsync(input);
            _uow.Add(entity);
            _uow.Commit();
            return entity.Id;
        }
    }
}