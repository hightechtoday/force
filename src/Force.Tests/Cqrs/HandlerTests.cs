using Force.Cqrs;
using Force.Extensions;
using Xunit;

namespace Force.Tests.Cqrs
{
    public class HandlerTests
    {
        [Fact]
        public void PipeTo()
        {
            var handler = new IntToStringHandler();
            var res = handler
                .Handle(1)
                .PipeTo(x => x + "!");
            
            Assert.Equal("1!", res);
        }
        
        [Fact]
        public void ToFunc()
        {
            var handler = new IntToStringHandler();
            var res = handler
                .Handle(1)
                .PipeTo(x => x + "!");
            
            Assert.Equal("1!", res);
        }
    }
}