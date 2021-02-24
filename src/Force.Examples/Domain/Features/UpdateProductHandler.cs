using Force.Cqrs.Update;
using Force.Examples.Domain.Entities;

namespace Force.Examples.Domain.Features
{
    public class UpdateProductHandler: UpdateIntEntityHandlerBase<Product, UpdateProduct>
    {
        protected override void UpdateEntity(Product entity, UpdateProduct command)
        {
            entity.Update(command);
        }
    }
}