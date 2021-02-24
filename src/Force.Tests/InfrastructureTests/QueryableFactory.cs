using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Force.Tests.InfrastructureTests
{
    internal class QueryableFactory<T>: IQueryable<T>
        where T : class
    {
        private readonly DbContext _dbContext;

        public QueryableFactory(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> Queryable => _dbContext.Set<T>();

        public IEnumerator<T> GetEnumerator() => Queryable.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()  => Queryable.GetEnumerator();

        public Type ElementType => Queryable.ElementType;
        
        public Expression Expression => Queryable.Expression;
        
        public IQueryProvider Provider => Queryable.Provider;
    }
}