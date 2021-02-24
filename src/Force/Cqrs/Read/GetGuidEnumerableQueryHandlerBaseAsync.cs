using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Force.Ddd;

namespace Force.Cqrs.Read
{
    public abstract class GetGuidEnumerableQueryHandlerBaseAsync<TQuery, TEntity, TListItem> :
        GetEnumerableQueryHandlerBaseAsync<Guid, TQuery, TEntity, TListItem>
        where TQuery : class, IQuery<Task<IEnumerable<TListItem>>>
        where TListItem : IHasId<Guid>
    {
        protected GetGuidEnumerableQueryHandlerBaseAsync(IQueryable<TEntity> queryable,
            IServiceProvider serviceProvider) : base(queryable, serviceProvider)
        {
        }
    }
}