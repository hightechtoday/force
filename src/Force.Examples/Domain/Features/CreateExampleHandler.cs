using Force.Cqrs.Create;
using Force.Examples.Domain.Entities;

namespace Force.Examples.Domain.Features
{
    public class CreateExampleHandler: CreateIntEntityHandlerBase<CreateProduct, Product>
    {
        protected override Product CreateNewEntity(CreateProduct input) =>
            new Product(input);
    }
}