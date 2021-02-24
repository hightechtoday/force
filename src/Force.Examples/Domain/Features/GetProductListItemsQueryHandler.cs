using System.Linq;
using Force.Examples.Domain.Entities;

namespace Force.Examples.Domain.Features
{
    public class GetProductListItemsQueryHandler :
        GetIntEnumerableQueryHandlerBase<GetProductList, Product, ProductListItem>
    {
        public GetProductListItemsQueryHandler(IQueryable<Product> queryable) : base(queryable)
        {
        }

        protected override IQueryable<ProductListItem> Map(IQueryable<Product> queryable, GetProductList query) =>
            queryable.Select(ProductListItem.Map);
    }
}