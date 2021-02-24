using System;
using System.Linq;
using System.Linq.Expressions;
using EFCore.BulkExtensions;
using Force.Examples.Domain.Features;

namespace Force.Examples.Domain.Entities
{
    public partial class Product
    {
        public static readonly ProductBatchUpdates BatchUpdates = new ProductBatchUpdates();
        public class ProductBatchUpdates
        {
            internal ProductBatchUpdates(){}
            
            public Expression<Func<Product, Product>> MassIncreasePrice(MassIncreasePrice command) =>
                x => new Product
                {
                    Name = x.Name,
                    Price = x.Price + command.Price
                };
        }
    }

    public static class ProductBatchUpdatesExtensions
    {
        public static int MassIncreasePrice(this IQueryable<Product> products, MassIncreasePrice command) =>
            products
                .BatchUpdate(Product.BatchUpdates.MassIncreasePrice(command));
    }
}