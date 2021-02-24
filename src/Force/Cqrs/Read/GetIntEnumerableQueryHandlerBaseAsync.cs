using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Force.Ddd;

namespace Force.Cqrs.Read
{
    public abstract class GetIntEnumerableQueryHandlerBaseAsync<TQuery, TEntity, TListItem> :
        GetEnumerableQueryHandlerBaseAsync<int, TQuery, TEntity, TListItem>
        where TQuery : class, IQuery<Task<IEnumerable<TListItem>>>
        where TListItem : IHasId<int>
    {
        protected GetIntEnumerableQueryHandlerBaseAsync(IQueryable<TEntity> queryable, IServiceProvider serviceProvider)
            : base(queryable, serviceProvider)
        {
        }
    }
}