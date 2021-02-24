﻿using System;

 namespace Force.Ddd.Domain.State
{
    public abstract class StateBase<TEntity, TStatus>
        where TEntity: class, IHasStatus<TStatus>
        where TStatus : Enum
    {
        public abstract bool IsStatusEligible(TStatus status);
        
        protected StateBase(TEntity entity)
        {
            Entity = entity
                     ?? throw new ArgumentNullException(nameof(entity));

            // ReSharper disable once VirtualMemberCallInConstructor
            if (!IsStatusEligible(Entity.Status))
            {
                throw new ArgumentException($"Can't create state {GetType().Name} " +
                                            $"because the entity status \"{Entity.Status}\"is in not eligible", 
                    nameof(entity));
            }
        }

        protected TEntity Entity { get; }
        
        protected T As<T>()
            where T: StateBase<TEntity, TStatus>
            => (T)this;
    }
}