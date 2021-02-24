using System.Threading.Tasks;
using Force.Cqrs;

namespace Force.Examples.Domain.Features
{
    public class UpdateProductAsyncHandler: 
        ICommandHandler<UpdateProductAsync, Task>,
        ICommandHandler<UpdateProductAsyncContext, Task>
    {
        public Task Handle(UpdateProductAsync input)
        {
            // TODO: write logic here;
            return Task.CompletedTask;
        }

        public Task Handle(UpdateProductAsyncContext input)
        {
            // TODO: write logic here;
            return Task.CompletedTask;
        }
    }
}