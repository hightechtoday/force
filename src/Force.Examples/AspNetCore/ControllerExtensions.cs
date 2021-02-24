using System;
using System.Threading.Tasks;
using Force.Ccc;
using Force.Cqrs;
using Force.Workflow;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Force.Examples.AspNetCore
{
    public static class ControllerExtensions
    {
        #region Process

        public static ActionResultBuilder<object> Process(this ControllerBase controller, ICommand command)
        {
            if (command == null) return NullRequest<object>(controller.HttpContext);
            
            var w = MakeGenericServiceType(
                controller,
                command,
                typeof(IWorkflow<,>),
                typeof(object));

            return RunWorkflow(controller, w, (dynamic) command, default(object));
        }
        
        public static ActionResultBuilder<T> Process<T>(this ControllerBase controller, ICommand<T> command)
        {
            if (command == null) return NullRequest<T>(controller.HttpContext);

            var w = MakeGenericServiceType(
                controller, 
                command, 
                typeof(IWorkflow<,>),
                typeof(T));

            return RunWorkflow(controller, w, (dynamic) command, default(T));
        }

        public static ActionResultBuilder<T> Process<T>(
            this ControllerBase controller,
            IQuery<T> query)
        {
            if (query == null) return NullRequest<T>(controller.HttpContext);

            var w = MakeGenericServiceType(
                controller, 
                query, 
                typeof(IWorkflow<,>),
                typeof(T));

            return RunWorkflow(controller, w, (dynamic) query, default(T));
        }
        
        #endregion
        
        #region ProcessAsync

        public static Task<ActionResultBuilder<T>> ProcessAsync<T>(
            this ControllerBase controller, 
            ICommand<Task<T>> command)
        {
            if (command == null) return Task.FromResult(NullRequest<T>(controller.HttpContext));

            var w = MakeGenericServiceType(
                controller, 
                command, 
                typeof(IAsyncWorkflow<,>),
                typeof(T));
            
            return RunWorkflowAsync(controller, w, (dynamic) command, default(T));
        }
        
        public static async Task<ActionResultBuilder<object>> ProcessAsync(
            this ControllerBase controller, 
            ICommand<Task> command)
        {
            if (command == null) return NullRequest<object>(controller.HttpContext);

            var w = MakeGenericServiceType(
                controller, 
                command, 
                typeof(IAsyncWorkflow<,>),
                typeof(object));

            return await RunWorkflowAsync(controller, w, (dynamic) command, default(object));
        }
        
        public static Task<ActionResultBuilder<T>> ProcessAsync<T>(
            this ControllerBase controller,
            IQuery<Task<T>> query)
        {
            if (query == null) return Task.FromResult(NullRequest<T>(controller.HttpContext));

            var w = MakeGenericServiceType(
                controller, 
                query, 
                typeof(IAsyncWorkflow<,>),
                typeof(T));

            return RunWorkflowAsync(controller, w, (dynamic) query, default(T));
        }

        #endregion
        
        #region Private
        
        private static object MakeGenericServiceType(
            ControllerBase controller, 
            object command,
            Type serviceType,
            Type genericType) =>
            controller
                .HttpContext
                .RequestServices
                .GetService(serviceType.MakeGenericType(
                    command.GetType(), 
                    genericType));
        
        private static IActionResult NoWorkflowResult(this ControllerBase controller) => 
            controller.StatusCode(501, new
            {
                Message = "Workflow for this method is not registered"
            });
        
        internal static ActionResultBuilder<T> WorkflowNotFound<T>(HttpContext httpContext)
        {
            return new ActionResultBuilder<T>(
                new Result<T, FailureInfo>(new FailureInfo(
                FailureType.NotImplemented,
                "Workflow for this method is not registered")), 
                httpContext);
        }
        
        internal static ActionResultBuilder<T> NullRequest<T>(HttpContext httpContext)
        {
            return new ActionResultBuilder<T>(
                new Result<T, FailureInfo>(new FailureInfo(
                    FailureType.ConfigurationError,
                    "Workflows for null values are not supported")), 
                httpContext);
        }

        private static ActionResultBuilder<TResponse> RunWorkflow<TRequest, TResponse>(
            ControllerBase controller, 
            dynamic workflow,
            TRequest request,
            // for type inference only
            TResponse response)
        {
            if (typeof(Task).IsAssignableFrom(typeof(TResponse)))
            {
                throw new InvalidOperationException("Use ProcessAsync instead");
            }
            // MUST BE OBJECT!
            object result = workflow.Process(request, controller.HttpContext.RequestServices);
            
            // DON'T REMOVE IT! Removing this line will result in absolutely cryptic behavior
            var res = GetResult<TResponse>(result);
            return new ActionResultBuilder<TResponse>(res, controller.HttpContext);
        }
        
        private static async Task<ActionResultBuilder<TResponse>> RunWorkflowAsync<TRequest, TResponse>(
            ControllerBase controller, 
            dynamic workflow,
            TRequest request, 
            // for type inference only
            TResponse response)
        {
            // MUST BE OBJECT!
            object result = await workflow.ProcessAsync(
                request,
                controller.HttpContext.RequestServices);
            
            // DON'T REMOVE IT! Removing this line will result in absolutely cryptic behavior
            var res = GetResult<TResponse>(result);

            return new ActionResultBuilder<TResponse>(res, controller.HttpContext);
        }

        internal static Result<T, FailureInfo> GetResult<T>(dynamic result)
        {
            Result<T, FailureInfo> res;
            if (result is Result<object, FailureInfo> ores)
            {
                res = ores.Match(
                    x => new Result<T, FailureInfo>(default(T)), 
                    x => x);
            }
            else
            {
                res = result;
            }

            return res;
        }

        #endregion
    }
}