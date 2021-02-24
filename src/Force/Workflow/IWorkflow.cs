using System;
using Force.Ccc;

namespace Force.Workflow
{
    public interface IWorkflow<in T, TResult>
    {
        Result<TResult, FailureInfo> Process(T request, IServiceProvider sp);
    }
}