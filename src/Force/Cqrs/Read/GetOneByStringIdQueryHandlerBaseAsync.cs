using System;
using System.Linq;
using System.Threading.Tasks;
using Force.Ddd;

namespace Force.Cqrs.Read
{
    public abstract class GetOneByStringIdQueryHandlerBaseAsync<TQuery, TEntity, TDto> :
        GetOneByIdQueryHandlerBaseAsync<string, TQuery, TEntity, TDto>
        where TQuery : IQuery<Task<TDto>>, IHasId<string>
        where TDto : class, IHasId<string>
    {
        protected GetOneByStringIdQueryHandlerBaseAsync(IQueryable<TEntity> queryable, IServiceProvider serviceProvider)
            : base(queryable, serviceProvider)
        {
        }
    }
}