using System;
using System.Linq;
using System.Threading.Tasks;
using Force.Ddd;

namespace Force.Cqrs.Read
{
    public abstract class GetOneByLongIdQueryHandlerBaseAsync<TQuery, TEntity, TDto> :
        GetOneByIdQueryHandlerBaseAsync<long, TQuery, TEntity, TDto>
        where TQuery : IQuery<Task<TDto>>, IHasId<long>
        where TDto : class, IHasId<long>
    {
        protected GetOneByLongIdQueryHandlerBaseAsync(IQueryable<TEntity> queryable, IServiceProvider serviceProvider) :
            base(queryable, serviceProvider)
        {
        }
    }
}