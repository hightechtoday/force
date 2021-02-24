using System;
using System.Linq;
using System.Threading.Tasks;
using Force.Ddd;

namespace Force.Cqrs.Read
{
    public abstract class GetOneByIntIdQueryHandlerBaseAsync<TQuery, TEntity, TDto> :
        GetOneByIdQueryHandlerBaseAsync<int, TQuery, TEntity, TDto>
        where TQuery : IQuery<Task<TDto>>, IHasId<int>
        where TDto : class, IHasId<int>
    {
        protected GetOneByIntIdQueryHandlerBaseAsync(IQueryable<TEntity> queryable, IServiceProvider serviceProvider) :
            base(queryable, serviceProvider)
        {
        }
    }
}