using System.Linq;
using Force.Ddd;

namespace Force.Cqrs.Read
{
    public abstract class GetOneByIntIdQueryHandlerBase<TQuery, TEntity, TDto> :
        GetOneByIdQueryHandlerBase<int, TQuery, TEntity, TDto>
        where TQuery : IQuery<TDto>, IHasId<int>
        where TDto : class, IHasId<int>
    {
        protected GetOneByIntIdQueryHandlerBase(IQueryable<TEntity> queryable) : base(queryable)
        {
        }
    }
}