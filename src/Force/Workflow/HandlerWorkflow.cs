using System;
using Force.Ccc;
using Force.Cqrs;

namespace Force.Workflow
{
    public class HandlerWorkflow<TRequest, TResult> : IWorkflow<TRequest, TResult>
    {
        public readonly IWorkflowStep<TRequest, TResult>[] _steps;

        public HandlerWorkflow(params IWorkflowStep<TRequest, TResult>[] steps)
        {
            _steps = steps;
        }

        private Func<TRequest, Result<TResult, FailureInfo>> GetWorkflowDispatch(
            object request,
            IServiceProvider sp,
            IWorkflowStep<TRequest, TResult>[] steps) =>
            r => FailureInfo.ConfigurationError($"Workflow for type: \"{request.GetType()}\" is not supported");

        private Func<TRequest, Result<TResult, FailureInfo>> GetWorkflowDispatch(
            ICommand command,
            IServiceProvider sp,
            IWorkflowStep<TRequest, TResult>[] steps)
        {
            var ht = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
            var tf = GetVoidTerminalFunc(sp, ht);
            return GetWorkflowRecursive(tf, sp, steps, 0);
        }
        
        private Func<TRequest, Result<TResult, FailureInfo>> GetWorkflowDispatch<TReturn>(
            ICommand<TReturn> command,
            IServiceProvider sp,
            IWorkflowStep<TRequest, TResult>[] steps)
        {
            var ht = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
            var tf = GetTerminalFunc(sp, ht);
            return GetWorkflowRecursive(tf, sp, steps, 0);
        }

        private Func<TRequest, Result<TResult, FailureInfo>> GetWorkflowDispatch<TReturn>(
            IQuery<TReturn> query,
            IServiceProvider sp,
            IWorkflowStep<TRequest, TResult>[] steps)
        {
            var ht = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            var tf = GetTerminalFunc(sp, ht);
            return GetWorkflowRecursive(tf, sp, steps, 0);
        }

        private static Func<TRequest, Result<TResult, FailureInfo>> GetVoidTerminalFunc(
            IServiceProvider sp,
            Type ht)
        {
            var h = (IHandler<TRequest>) sp.GetService(ht);
            if (h == null)
            {
                return r => FailureInfo.ConfigurationError($"Type \"{ht}\" is not registered"); 
            }

            if (h is IHasServiceProvider sph)
            {
                sph.ServiceProvider = sp;
            }

            if (h is IHasUnitOfWork uowh)
            {
                uowh.UnitOfWork = (IUnitOfWork) sp.GetService(typeof(IUnitOfWork));
            }

            return r =>
            {
                h.Handle(r);
                return new Result<TResult, FailureInfo>(default(TResult));
            };
        }
        
        private static Func<TRequest, Result<TResult, FailureInfo>> GetTerminalFunc(
            IServiceProvider sp,
            Type ht)
        {
            var h = (IHandler<TRequest, TResult>) sp.GetService(ht);
            if (h == null)
            {
                return r => FailureInfo.ConfigurationError($"Type \"{ht}\" is not registered"); 
            }
            
            if (h is IHasServiceProvider sph)
            {
                sph.ServiceProvider = sp;
            }
            
            if (h is IHasUnitOfWork uowh)
            {
                uowh.UnitOfWork = (IUnitOfWork) sp.GetService(typeof(IUnitOfWork));
            }

            return r =>
            {
                TResult res = h.Handle(r);
                return new Result<TResult, FailureInfo>(res);
            };
        }

        public Func<TRequest, Result<TResult, FailureInfo>> GetWorkflow(
            object request,
            IServiceProvider sp,
            IWorkflowStep<TRequest, TResult>[] steps)
        {
            return GetWorkflowDispatch((dynamic) request, sp, steps);
        }

        private Func<TRequest, Result<TResult, FailureInfo>> GetWorkflowRecursive(
            Func<TRequest, Result<TResult, FailureInfo>> terminalFunc,
            IServiceProvider sp,
            IWorkflowStep<TRequest, TResult>[] steps,
            int index)
        {
            if (index < steps.Length - 1)
            {
                Result<TResult, FailureInfo> NewTerminalFunc(TRequest x) => steps[index].Process(x, terminalFunc);
                return GetWorkflowRecursive(NewTerminalFunc, sp, steps, index + 1);
            }

            return terminalFunc;
        }

        public Result<TResult, FailureInfo> Process(TRequest request, IServiceProvider sp) =>
            GetWorkflow(request, sp, _steps)(request);
    }
}