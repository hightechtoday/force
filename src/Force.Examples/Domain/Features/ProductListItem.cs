using System;
using System.Linq.Expressions;
using Force.Ddd;
using Force.Examples.Domain.Entities;

namespace Force.Examples.Domain.Features
{
    public class ProductListItem: HasIdBase
    {
        public static readonly Expression<Func<Product, ProductListItem>> Map = x => new ProductListItem
        {
            Id = x.Id,
            Name = x.Name,
            Price = x.Price
        };
        
        public string Name { get; set; }
        
        public double Price { get; set; }
    }
}