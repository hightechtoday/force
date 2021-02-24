using System.Threading.Tasks;
using Force.Cqrs;
using Force.OperationContext;

namespace Force.Examples.Domain.Features
{
    public class UpdateProductAsyncContext: OperationContextBase<UpdateProductAsync>, ICommand<Task>
    {
        public UpdateProductAsyncContext(UpdateProductAsync request) : base(request)
        {
        }
    }
}