using System;
using System.Threading.Tasks;
using Force.Ccc;
using Force.Validation;
using Force.Workflow;

namespace Force.Tests.InfrastructureTests
{
    public class HandlerWorkflowFactory<TRequest, TResult>: IWorkflow<TRequest, TResult>
    {
        public Result<TResult, FailureInfo> Process(TRequest request, IServiceProvider sp)
        {
            var wf = CreateDefaultWorkflow<TRequest, TResult>(sp);
            return wf.Process(request, sp);
        }
        
        public static HandlerWorkflow<TRequest, TResponse> CreateDefaultWorkflow<TRequest, TResponse>(
            IServiceProvider sp)
        {
            var validator = (IValidator<TRequest>) sp.GetService(typeof(IValidator<TRequest>));
            var uow = (IUnitOfWork) sp.GetService(typeof(IUnitOfWork));
            
            return validator == null
                ? new HandlerWorkflow<TRequest, TResponse>(new UnitOfWorkWorkflowStep<TRequest, TResponse>(uow))
                : new HandlerWorkflow<TRequest, TResponse>(
                    new ValidateWorkflowStep<TRequest, TResponse>(validator),
                    new UnitOfWorkWorkflowStep<TRequest, TResponse>(uow));
        }
    }
    
    public class HandlerAsyncWorkflowFactory<TRequest, TResult>: IAsyncWorkflow<TRequest, TResult>
    {
        public Task<Result<TResult, FailureInfo>> ProcessAsync(TRequest request, IServiceProvider sp)
        {
            var wf = CreateDefaultWorkflowAsync<TRequest, TResult>(sp);
            return wf.ProcessAsync(request, sp);
        }
        
        public static AsyncHandlerWorkflow<TRequest, TResponse> CreateDefaultWorkflowAsync<TRequest, TResponse>(
            IServiceProvider sp)
        {
            var validatorAsync = (IAsyncValidator<TRequest>) sp.GetService(typeof(IAsyncValidator<TRequest>));
            var uow = (IUnitOfWork) sp.GetService(typeof(IUnitOfWork));
            
            return validatorAsync == null
                ? new AsyncHandlerWorkflow<TRequest, TResponse>(new UnitOfWorkWorkflowStep<TRequest, TResponse>(uow))
                : new AsyncHandlerWorkflow<TRequest, TResponse>(
                    new UnitOfWorkWorkflowStep<TRequest, TResponse>(uow),
                    new ValidateWorkflowAsyncStep<TRequest, TResponse>(validatorAsync));
        }
    }
}