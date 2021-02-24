using Force.Cqrs;
using MediatR;

namespace Force.Tests.InfrastructureTests.Mocks
{
    public class CommandMock: ICommand, IRequest<Unit>
    {
        
    }
}