﻿using System.Linq;

 namespace Force.Ddd
{
    public interface IQueryableFactory<T>
    {
        IQueryable<T> Queryable { get; }
    }
}