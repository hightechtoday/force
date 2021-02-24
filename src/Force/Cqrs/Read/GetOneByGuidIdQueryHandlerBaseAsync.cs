using System;
using System.Linq;
using System.Threading.Tasks;
using Force.Ddd;

namespace Force.Cqrs.Read
{
    public abstract class GetOneByGuidIdQueryHandlerBaseAsync<TQuery, TEntity, TDto> :
        GetOneByIdQueryHandlerBaseAsync<Guid, TQuery, TEntity, TDto>
        where TQuery : IQuery<Task<TDto>>, IHasId<Guid>
        where TDto : class, IHasId<Guid>
    {
        protected GetOneByGuidIdQueryHandlerBaseAsync(IQueryable<TEntity> queryable, IServiceProvider serviceProvider) :
            base(queryable, serviceProvider)
        {
        }
    }
}