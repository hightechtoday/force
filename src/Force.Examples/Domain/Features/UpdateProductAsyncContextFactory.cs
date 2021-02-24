using System.Threading.Tasks;
using Force.OperationContext;

namespace Force.Examples.Domain.Features
{
    public class UpdateProductAsyncContextFactory: 
        IAsyncOperationContextFactory<UpdateProductAsync, UpdateProductAsyncContext>
    {
        public Task<UpdateProductAsyncContext> BuildAsync(UpdateProductAsync request)
        {
            return Task.FromResult(new UpdateProductAsyncContext(request));
        }
    }
}