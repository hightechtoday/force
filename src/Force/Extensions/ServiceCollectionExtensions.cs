using System;
using Force.Ccc;
using Force.Validation;
using Force.Workflow;

namespace Force.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static HandlerWorkflow<TRequest, TResponse> CreateDefaultWorkflow<TRequest, TResponse>(
            this IServiceProvider sp)
        {
            var validator = (IValidator<TRequest>) sp.GetService(typeof(IValidator<TRequest>));
            var uow = (IUnitOfWork) sp.GetService(typeof(IUnitOfWork));
            
            return validator == null
                ? new HandlerWorkflow<TRequest, TResponse>(new UnitOfWorkWorkflowStep<TRequest, TResponse>(uow))
                : new HandlerWorkflow<TRequest, TResponse>(
                    new ValidateWorkflowStep<TRequest, TResponse>(validator),
                    new UnitOfWorkWorkflowStep<TRequest, TResponse>(uow));
        }

        public static AsyncHandlerWorkflow<TRequest, TResponse> CreateDefaultWorkflowAsync<TRequest, TResponse>(
            this IServiceProvider sp)
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