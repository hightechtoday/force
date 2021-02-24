using System.Linq;
using Force.Cqrs;
using Force.Examples.Domain.Entities;

namespace Force.Examples.Domain.Features
{
    public class MassIncreasePriceHandler: ICommandHandler<MassIncreasePrice, int>
    {
        private readonly IQueryable<Product> _products;

        public MassIncreasePriceHandler(IQueryable<Product> products)
        {
            _products = products;
        }

        public int Handle(MassIncreasePrice command) =>
            _products
                .Where(Product.Specs.IsForSale)
                .MassIncreasePrice(command);
    }
}