using System.Linq;

namespace Force.Cqrs.Read
{
    public abstract class GetOneByIdMapBase<TQuery, TEntity, TDto>
    {
        protected abstract IQueryable<TDto> Map(IQueryable<TEntity> queryable, TQuery query);
    }
}