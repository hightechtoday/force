using System;
using System.Linq;
using Force.Ddd;
using Force.Linq;

namespace Force.Cqrs.Read
{
    public abstract class GetOneByIdQueryHandlerBase<TKey, TQuery, TEntity, TDto> :
        GetOneByIdMapBase<TQuery, TEntity, TDto>,
        IQueryHandler<TQuery, TDto>
        where TQuery : IQuery<TDto>, IHasId<TKey>
        where TDto : class, IHasId<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly IQueryable<TEntity> _queryable;

        protected GetOneByIdQueryHandlerBase(IQueryable<TEntity> queryable)
        {
            _queryable = queryable;
        }

        public TDto Handle(TQuery input) => Map(_queryable, input).FirstOrDefaultById(input.Id);
    }
}