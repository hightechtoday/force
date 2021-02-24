﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Force.Helpers
{
    public interface IQueryableHelper
    {
        Task<T> FirstOrDefaultAsync<T>(IQueryable<T> queryable, Func<T, bool> predicate);
        
        Task<List<T>> ToListAsync<T>(IQueryable<T> queryable);
    }
}