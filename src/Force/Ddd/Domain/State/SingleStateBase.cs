﻿using System;

 namespace Force.Ddd.Domain.State
{
    public abstract class SingleStateBase<TEntity, TStatus>: 
        StateBase<TEntity, TStatus>
        where TEntity: class, IHasStatus<TStatus>
        where TStatus : Enum
    {
        protected SingleStateBase(TEntity entity) : base(entity)
        {
        }

        public override bool IsStatusEligible(TStatus status) => status?.Equals(EligibleStatus) == true;
        
        public abstract TStatus EligibleStatus { get; }
    }
}