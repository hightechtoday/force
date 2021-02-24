using System;
using System.Threading.Tasks;
using Force.Ccc;

namespace Force.Workflow
{
    public interface IAsyncWorkflow<in T, TResult>
    {
        Task<Result<TResult, FailureInfo>> ProcessAsync(T request, IServiceProvider sp);
    }
}