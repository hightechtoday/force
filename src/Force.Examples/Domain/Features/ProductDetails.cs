using System;
using System.Linq.Expressions;
using Force.Ddd;
using Force.Examples.Domain.Entities;

namespace Force.Examples.Domain.Features
{
    public class ProductDetails: HasIdBase
    {
        public static readonly Expression<Func<Product, ProductDetails>> Map = x => new ProductDetails
        {
            Id = x.Id,
            Name = x.Name,
            Price = x.Price,
            DiscountPercent = x.DiscountPercent
        };
        
        public string Name { get; set; }
        
        public double Price { get; set; }
        
        public int DiscountPercent { get; set; }

        public override string ToString() => DiscountPercent > 0
            ? $"{Name} ${Price} Sale: ${DiscountPercent}%!"
            : $"{Name} ${Price}";
    }
}