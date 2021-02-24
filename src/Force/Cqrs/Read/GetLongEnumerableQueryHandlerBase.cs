using System.Collections.Generic;
using System.Linq;
using Force.Ddd;

namespace Force.Cqrs.Read
{
    public abstract class GetLongEnumerableQueryHandlerBase<TQuery, TEntity, TListItem> :
        GetEnumerableQueryHandlerBase<long, TQuery, TEntity, TListItem>
        where TQuery : class, IQuery<IEnumerable<TListItem>>
        where TListItem : IHasId<long>
    {
        protected GetLongEnumerableQueryHandlerBase(IQueryable<TEntity> queryable) : base(queryable)
        {
        }
    }
}