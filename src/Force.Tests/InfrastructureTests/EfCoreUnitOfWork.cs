using System;
using System.Collections.Generic;
using System.Linq;
using Force.Ccc;
using Force.Cqrs;
using Force.Ddd.DomainEvents;
using Microsoft.EntityFrameworkCore;

namespace Force.Tests.InfrastructureTests
{
    public class EfCoreUnitOfWork: UnitOfWorkBase
    {
        private readonly DbContext _dbContext;

        public EfCoreUnitOfWork(
            IHandler<IEnumerable<IDomainEvent>> domainEventDispatcher, 
            DbContext dbContext): 
            base(domainEventDispatcher)
        {
            _dbContext = dbContext;
        }

        public override void Dispose()
        {
            _dbContext.Dispose();
        }

        public override void Add<TEntity>(TEntity entity)
        {
            _dbContext.Add(entity);
        }

        public override void Remove<TEntity>(TEntity entity)
        {
            _dbContext.Remove(entity);
        }

        /*public override void Rollback()
        {
            throw new System.NotImplementedException();
        }*/

        public override TEntity Find<TEntity>(params object[] id) 
        {
            return (TEntity)_dbContext.Find(typeof(TEntity), id);
        }

        public override IUnitOfWorkTransaction BeginTransaction()
        {
            throw new NotImplementedException();
        }

        protected override void DoCommit()
        {
            _dbContext.SaveChanges();
        }

        protected override IEnumerable<IDomainEvent> GetDomainEvents() =>
            _dbContext.ChangeTracker
                .Entries<IHasDomainEvents>()
                .SelectMany(x => x.Entity.GetDomainEvents());

        public override void Update<TEntity>(TEntity entity)
        {
            _dbContext.Update(entity);
        }
    }
}