using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Force.Cqrs;
using Force.Examples.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Force.Examples.Domain.Features
{
    public class GetProductListAsyncQueryHandler: IQueryHandler<GetProductListAsync, Task<IEnumerable<ProductListItem>>>
    {
        private readonly IQueryable<Product> _products;

        public GetProductListAsyncQueryHandler(IQueryable<Product> products)
        {
            _products = products;
        }

        public async Task<IEnumerable<ProductListItem>> Handle(GetProductListAsync input) =>
            await _products
                .Select(ProductListItem.Map)
                .ToListAsync();
    }
}