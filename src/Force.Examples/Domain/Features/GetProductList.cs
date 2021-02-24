using System.Collections.Generic;
using Force.Cqrs;

namespace Force.Examples.Domain.Features
{
    public class GetProductList: IQuery<IEnumerable<ProductListItem>>
    {
        public string Name { get; set; }
    }
}