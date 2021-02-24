using System;
using System.Threading.Tasks;
using Force.Ccc;

namespace Force.Workflow
{
    public interface IAsyncWorkflowStep<TRequest, TResult>
    {
        Task<Result<TResult, FailureInfo>> ProcessAsync(
            TRequest request,
            Func<TRequest, Task<Result<TResult, FailureInfo>>> next);
    }
}