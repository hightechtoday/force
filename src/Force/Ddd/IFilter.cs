﻿using System.Linq;

 namespace Force.Ddd
{
    public interface IFilter<TQueryable, TPredicate>
    {
        IQueryable<TQueryable> Filter(IQueryable<TQueryable> queryable, TPredicate predicate);
    }
}