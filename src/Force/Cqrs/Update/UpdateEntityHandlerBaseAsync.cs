using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Force.Ccc;
using Force.Ddd;
using Force.Workflow;

namespace Force.Cqrs.Update
{
    public abstract class UpdateEntityHandlerBaseAsync<TKey, TEntity, TCommand> :
        IHasUnitOfWork,
        ICommandHandler<TCommand, Task>,
        IValidator<TCommand>
        where TEntity : class, IHasId<TKey>
        where TCommand : ICommand<Task>, IHasId<TKey>
        where TKey : IEquatable<TKey>
    {
        private IUnitOfWork _uow;

        IUnitOfWork IHasUnitOfWork.UnitOfWork
        {
            get => _uow;
            set => _uow = value;
        }

        protected abstract void UpdateEntity(TEntity entity, TCommand command);

        public Task Handle(TCommand input)
        {
            var entity = _uow.Find<TEntity>(input.Id);
            UpdateEntity(entity, input);
            _uow.Commit();
            return Task.CompletedTask;
        }

        public IEnumerable<ValidationResult> Validate(TCommand obj)
        {
            yield return ValidationResult.Success;
        }
    }
}