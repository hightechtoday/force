using System;
using Force.Examples.Data;
using Force.Examples.Domain.Features;
using MediatR;

namespace Force.Tests.InfrastructureTests
{
    public abstract class TestsWithServiceProviderBase
    {
        private readonly IServiceProvider _serviceProvider;

        protected IServiceProvider GetServiceProvider()
        {
            return _serviceProvider;
        }

        protected TestsWithServiceProviderBase()
        {
            _serviceProvider = Services.BuildServiceProvider(
                sp => new ExampleDbContext(),
                typeof(ProductController).Assembly);
        }
    }
}