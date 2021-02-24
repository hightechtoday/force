using System;
using System.Threading.Tasks;
using Force.Ccc;
using Force.Extensions;

namespace Force.Workflow
{
    public class HandlerWorkflowFactory<TRequest, TResult>: IWorkflow<TRequest, TResult>
    {
        public Result<TResult, FailureInfo> Process(TRequest request, IServiceProvider sp)
        {
            var wf = sp.CreateDefaultWorkflow<TRequest, TResult>();
            return wf.Process(request, sp);
        }
    }
    
    public class HandlerAsyncWorkflowFactory<TRequest, TResult>: IAsyncWorkflow<TRequest, TResult>
    {
        public Task<Result<TResult, FailureInfo>> ProcessAsync(TRequest request, IServiceProvider sp)
        {
            var wf = sp.CreateDefaultWorkflowAsync<TRequest, TResult>();
            return wf.ProcessAsync(request, sp);
        }
    }
}