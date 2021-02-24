using System;
using System.Collections.Generic;
using System.Linq;
using Force.Cqrs;
using Force.Ddd;
using Force.Extensions;
using Force.Linq.Pagination;

namespace Force.Examples
{
    public abstract class GetEnumerableQueryHandlerBase<TKey, TQuery, TEntity, TListItem> :
        GetEnumerableBase<TQuery, TEntity, TListItem>,
        IQueryHandler<TQuery, IEnumerable<TListItem>>
        where TQuery : class, IQuery<IEnumerable<TListItem>>
        where TListItem : IHasId<TKey>
        where TKey : IEquatable<TKey>
    {
        protected GetEnumerableQueryHandlerBase(IQueryable<TEntity> queryable) : base(queryable)
        {
        }

        public IEnumerable<TListItem> Handle(TQuery input) => MapFilterAndSort(input).PipeTo(p => Fetch(p, input));

        protected virtual IEnumerable<TListItem> Fetch(IOrderedQueryable<TListItem> sorted, TQuery query) =>
            query switch
            {
                IPaging paging => sorted.ToPagedEnumerable(paging),
                _ => sorted.ToList()
            };
    }
}