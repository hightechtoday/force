using System.Threading;
using System.Threading.Tasks;
using Force.Cqrs;
using MediatR;

namespace Force.Tests.InfrastructureTests.Mocks
{
    public class CommandMockHandler: ICommandHandler<CommandMock>, IRequestHandler<CommandMock>
    {
        public void Handle(CommandMock input)
        {
            
        }

        public Task<Unit> Handle(CommandMock request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Unit.Value);
        }
    }
}