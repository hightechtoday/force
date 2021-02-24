using System.Linq;
using Force.Cqrs.Read;
using Force.Examples.Domain.Entities;

namespace Force.Examples.Domain.Features
{
    public class GetProductDetailsQueryHandler :
        GetOneByIntIdQueryHandlerBase<GetProductDetailsById, Product, ProductDetails>
    {
        public GetProductDetailsQueryHandler(IQueryable<Product> queryable) : base(queryable)
        {
        }

        protected override IQueryable<ProductDetails> Map(IQueryable<Product> queryable, GetProductDetailsById query) =>
            queryable.Select(ProductDetails.Map);
    }
}