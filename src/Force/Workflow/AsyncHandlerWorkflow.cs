using System;
using System.Threading.Tasks;
using Force.Ccc;
using Force.Cqrs;

namespace Force.Workflow
{
    public class AsyncHandlerWorkflow<TRequest, TResult>: IAsyncWorkflow<TRequest, TResult>
    {
        private readonly IAsyncWorkflowStep<TRequest, TResult>[] _steps;

        public AsyncHandlerWorkflow(params IAsyncWorkflowStep<TRequest, TResult>[] steps)
        {
            _steps = steps;
        }
        
        private Func<TRequest, Task<Result<TResult, FailureInfo>>> GetAsyncWorkflowDispatch(
            object request, 
            IServiceProvider sp, 
            IAsyncWorkflow<TRequest, TResult>[] steps) => 
            r => Task.FromResult(new Result<TResult, FailureInfo>(FailureInfo.ConfigurationError(
                $"Workflow for type: \"{request.GetType()}\" is not supported")));

        private Func<TRequest, Task<Result<TReturn, FailureInfo>>> GetAsyncWorkflowDispatch<TReturn>(
            ICommand<Task> command,
            IServiceProvider sp,
            IAsyncWorkflowStep<TRequest, TReturn>[] steps)
        {
            var ht = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(Task));
            var tf = GetAsyncVoidTerminalFunc<TReturn>(sp, ht);
            return GetWorkflowRecursive<TReturn>(tf, steps, 0);
        }
        
        private Func<TRequest, Task<Result<TReturn, FailureInfo>>> GetAsyncWorkflowDispatch<TReturn>(
            ICommand<Task<TReturn>> command,
            IServiceProvider sp,
            IAsyncWorkflowStep<TRequest, TReturn>[] steps)
        {
            var ht = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(Task<TResult>));
            var tf = GetAsyncTerminalFunc<TReturn>(sp, ht);
            return GetWorkflowRecursive<TReturn>(tf, steps, 0);
        }
        
        private Func<TRequest, Task<Result<TReturn, FailureInfo>>> GetAsyncWorkflowDispatch<TReturn>(
            IQuery<Task<TReturn>> query,
            IServiceProvider sp,
            IAsyncWorkflowStep<TRequest, TReturn>[] steps)
        {
            var ht = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(Task<TReturn>));
            var tf = GetAsyncTerminalFunc<TReturn>(sp, ht);
            return GetWorkflowRecursive<TReturn>(tf, steps, 0);
        }
        
        private static Func<TRequest, Task<Result<TReturn, FailureInfo>>> GetAsyncTerminalFunc<TReturn>(
            IServiceProvider sp,
            Type ht)
        {
            dynamic h = sp.GetService(ht);
            if (h == null)
            {
                var failure = FailureInfo.ConfigurationError($"Type \"{ht}\" is not registered");
                return r =>  Task.FromResult(new Result<TReturn, FailureInfo>(failure));
            }
            
            if (h is IHasServiceProvider sph)
            {
                sph.ServiceProvider = sp;
            }
            
            if (h is IHasUnitOfWork uowh)
            {
                uowh.UnitOfWork = (IUnitOfWork) sp.GetService(typeof(IUnitOfWork));
            }

            return async r =>
            {
                TReturn res = await h.Handle(r);
                return new Result<TReturn, FailureInfo>(res);
            };
        }
        
        private static Func<TRequest, Task<Result<TReturn, FailureInfo>>> GetAsyncVoidTerminalFunc<TReturn>(
            IServiceProvider sp,
            Type ht)
        {
            dynamic h = sp.GetService(ht);
            if (h == null)
            {
                var failure = FailureInfo.ConfigurationError($"Type \"{ht}\" is not registered");
                return r =>  Task.FromResult(new Result<TReturn, FailureInfo>(failure));
            }
            
            if (h is IHasServiceProvider sph)
            {
                sph.ServiceProvider = sp;
            }
            
            if (h is IHasUnitOfWork uowh)
            {
                uowh.UnitOfWork = (IUnitOfWork) sp.GetService(typeof(IUnitOfWork));
            }

            return async r =>
            {
                await h.Handle(r);
                return new Result<TReturn, FailureInfo>(default(TReturn));
            };
        }
        
        private Func<TRequest, Task<Result<TResult, FailureInfo>>> GetAsyncWorkflow(
            object request,
            IServiceProvider sp,
            IAsyncWorkflowStep<TRequest, TResult>[] steps)
        {
            return GetAsyncWorkflowDispatch<TResult>((dynamic) request, sp, steps);
        }
        
        private Func<TRequest, Task<Result<TReturn, FailureInfo>>> GetWorkflowRecursive<TReturn>(
            Func<TRequest, Task<Result<TReturn, FailureInfo>>> terminalFunc,
            IAsyncWorkflowStep<TRequest, TReturn>[] steps,
            int index)
        {
            if (index < steps.Length - 1)
            {
                async Task<Result<TReturn, FailureInfo>> NewTerminalFunc(TRequest x) => 
                    await steps[index].ProcessAsync(x, terminalFunc);


                return GetWorkflowRecursive(NewTerminalFunc, steps, index + 1);
            }

            if (steps.Length > 0)
            {
                return async r =>
                {
                    var res = await steps[index].ProcessAsync(r, terminalFunc);
                    return res;
                };
            }

            return terminalFunc;
        }


        public async Task<Result<TResult, FailureInfo>> ProcessAsync(TRequest request, IServiceProvider sp)
        {
            var res = await GetAsyncWorkflow(request, sp, _steps)(request);
            return res;
        }

    }
}