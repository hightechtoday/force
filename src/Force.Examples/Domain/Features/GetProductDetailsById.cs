using Force.Cqrs;
using Force.Examples.AspNetCore;

namespace Force.Examples.Domain.Features
{
    public class GetProductDetailsById: IdRequestBase<int>, IQuery<ProductDetails>
    {
    }
}