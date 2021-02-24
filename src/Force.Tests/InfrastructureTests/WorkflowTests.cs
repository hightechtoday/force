using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Force.Examples.Domain.Features;
using Force.Workflow;
using Xunit;
using static System.String;

namespace Force.Tests.InfrastructureTests
{
    public class WorkflowTests : TestsWithServiceProviderBase
    {
        public IWorkflow<TIn, TOut> GetWorkflow<TIn, TOut>()
        {
            return (IWorkflow<TIn, TOut>) GetServiceProvider().GetService(typeof(IWorkflow<TIn, TOut>));
        }

        public IAsyncWorkflow<TIn, TOut> GetAsyncWorkflow<TIn, TOut>()
        {
            return (IAsyncWorkflow<TIn, TOut>) GetServiceProvider().GetService(typeof(IAsyncWorkflow<TIn, TOut>));
        }

        private int CreateNewEntity(string name = null)
        {
            var wf = GetWorkflow<CreateProduct, int>();
            var res = wf.Process(
                new CreateProduct
                {
                    Name = name ?? CreatedText,
                    Price = 100
                },
                GetServiceProvider());

            var match = res.Match(x => x, x => 0);
            return match;
        }

        private static string CreatedText => "Created";

        [Fact]
        public void Create()
        {
            var entityId = CreateNewEntity();
            Assert.NotEqual(0, entityId);
        }

        [Fact]
        public async Task CreateAsync()
        {
            var wf = GetAsyncWorkflow<CreateProductAsync, int>();
            var res = await wf.ProcessAsync(
                new CreateProductAsync
                {
                    Name = "Created Async",
                    Price = 200
                },
                GetServiceProvider());

            var match = res.Match(x => x, x => 0);
            Assert.NotEqual(0, match);
        }

        [Fact]
        public void Update()
        {
            var entityId = CreateNewEntity();
            // https://stackoverflow.com/questions/11318973/void-in-c-sharp-generics
            var wf = GetWorkflow<UpdateProduct, object>();
            var res = wf.Process(
                new UpdateProduct
                {
                    Id = entityId,
                    Name = "Updated"
                },
                GetServiceProvider());

            var match = res.Match(x => true, x => false);
            Assert.True(match);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            var entityId = CreateNewEntity();
            // https://stackoverflow.com/questions/11318973/void-in-c-sharp-generics
            var wf = GetAsyncWorkflow<UpdateProductAsync, object>();
            var res = await wf.ProcessAsync(
                new UpdateProductAsync
                {
                    Id = entityId,
                    Name = "Updated"
                },
                GetServiceProvider());

            var match = res.Match(x => true, x => false);
            Assert.True(match);
        }

        [Fact]
        public async Task UpdateAsyncWithValidator()
        {
            var entityId = CreateNewEntity();
            // https://stackoverflow.com/questions/11318973/void-in-c-sharp-generics
            var wf = GetAsyncWorkflow<UpdateProductAsyncContext, object>();
            var res = await wf.ProcessAsync(new UpdateProductAsyncContext(
                    new UpdateProductAsync
                    {
                        Id = entityId,
                        Name = "Updated"
                    }),
                GetServiceProvider());

            var match = res.Match(x => x, x => x);
            Assert.IsType<FailureInfo>(match);
        }


        [Fact]
        public void Delete()
        {
            var entityId = CreateNewEntity();
            var wf = GetWorkflow<DeleteProduct, object>();
            var res = wf.Process(
                new DeleteProduct
                {
                    Id = entityId
                },
                GetServiceProvider());

            var match = res.Match(x => true, x => false);
            Assert.True(match);
        }

        [Fact]
        public void GetOneById()
        {
            var entityId = CreateNewEntity();
            var wf = GetWorkflow<GetProductDetailsById, ProductDetails>();
            var res = wf.Process(
                new GetProductDetailsById
                {
                    Id = entityId
                },
                GetServiceProvider());
            var match = res.Match(x => x.Name, x => Empty);
            Assert.Equal(CreatedText, match);
        }

        [Fact]
        public void GetEnumerable()
        {
            var expectedText = CreatedText + "/Very Complex Logic";
            CreateNewEntity(expectedText);
            var wf = GetWorkflow<GetProductList, IEnumerable<ProductListItem>>();
            var res = wf.Process(
                new GetProductList
                {
                    Name = CreatedText
                },
                GetServiceProvider());

            var match = res.Match(x => x.First().Name, x => Empty);
            Assert.Equal(expectedText, match);
        }

        [Fact]
        public async Task GetEnumerableAsync()
        {
            CreateNewEntity(CreatedText);
            var wf = GetAsyncWorkflow<GetProductListAsync, IEnumerable<ProductListItem>>();
            var res = await wf.ProcessAsync(
                new GetProductListAsync(),
                GetServiceProvider());

            var match = res.Match(x => x.Count(), x => -1);
            Assert.Equal(1, match);
        }


        [Fact]
        public void ProcessCommand()
        {
            CreateNewEntity();
            CreateNewEntity();
            CreateNewEntity();

            var wf = GetWorkflow<MassIncreasePrice, int>();
            // doesn't work with in-memory :(
            Assert.Throws<ArgumentException>(() =>
            {
                var res = wf.Process(
                    new MassIncreasePrice
                    {
                        Price = 100
                    },
                    GetServiceProvider());
            });
        }
    }
}