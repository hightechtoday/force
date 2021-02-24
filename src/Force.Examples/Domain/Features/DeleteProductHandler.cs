using Force.Cqrs.Delete;
using Force.Examples.Domain.Entities;

namespace Force.Examples.Domain.Features
{
    public class DeleteProductHandler: DeleteIntEntityHandlerBase<Product, DeleteProduct>
    {
    }
}