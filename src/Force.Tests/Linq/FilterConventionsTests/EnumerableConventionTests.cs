using System.Linq;
using Force.Cqrs;
using Force.Linq;
using Force.Tests.Infrastructure.Context;
using Xunit;

namespace Force.Tests.Linq.FilterConventionsTests
{
    public class EnumerableConventionTests : DbContextFixtureTestsBase
    {
        public EnumerableConventionTests(DbContextFixture dbContextFixture) : base(dbContextFixture)
        {
        }

        [Fact]
        public void Test_Filter()
        {
            var products = DbContext
                .Products
                .Select(x => new ProductListItem()
                {
                    Name = x.Name,
                    CategoryName = x.Category.Name
                });
            var product = products.FirstOrDefault();
            Assert.NotNull(product);
            var categoryName = product.CategoryName;
            
            var predicate = new ProductListItemFilter()
            {
                CategoryName = new []{categoryName}
            };

            var actualFilteredProducts = products
                .Filter(predicate)
                .ToList();

            Assert.All(actualFilteredProducts, x => Assert.Equal(categoryName, x.CategoryName));
        }
    }

    public class ProductListItemFilter : FilterQuery<ProductListItem>
    {
        public string[] CategoryName { get; set; }
    }
}