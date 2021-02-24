using System;
using Force.Ccc;

namespace Force.Workflow
{
    public interface IWorkflowStep<TRequest, TResult>
    {
        Result<TResult, FailureInfo> Process(
            TRequest request, 
            Func<TRequest, Result<TResult, FailureInfo>> next);
    }
}