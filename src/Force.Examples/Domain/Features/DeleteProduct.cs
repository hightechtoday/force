using Force.Cqrs;
using Force.Examples.AspNetCore;

namespace Force.Examples.Domain.Features
{
    public class DeleteProduct: IdRequestBase<int>, ICommand
    {
    }
}