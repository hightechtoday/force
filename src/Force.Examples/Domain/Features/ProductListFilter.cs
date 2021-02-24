using System.Linq;
using Force.Ddd;

namespace Force.Examples.Domain.Features
{
    public class ProductListFilter: IFilter<ProductListItem, GetProductList>
    {
        public IQueryable<ProductListItem> Filter(IQueryable<ProductListItem> queryable, GetProductList predicate) =>
            queryable.Where(x => x.Name == predicate.Name + "/Very Complex Logic");
    }
}