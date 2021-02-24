using System;
using System.Threading.Tasks;
using Force.Ccc;
using Force.Cqrs;

namespace Force.Workflow
{
    public class UnitOfWorkWorkflowStep<TRequest, TReturn>: 
        IWorkflowStep<TRequest, TReturn>,
        IAsyncWorkflowStep<TRequest, TReturn>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkWorkflowStep(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<Result<TReturn, FailureInfo>> ProcessAsync(TRequest request, Func<TRequest, Task<Result<TReturn, FailureInfo>>> next)
        {
            var res = await next(request);
            if (!res.IsFaulted)
            {
                Dispatch((dynamic)request);
            }

            return res;
        }
        
        public Result<TReturn, FailureInfo> Process(TRequest request, Func<TRequest, Result<TReturn, FailureInfo>> next)
        {
            var res = next(request);
            if (!res.IsFaulted)
            {
                Dispatch((dynamic)request);
            }

            return res;
        }

        void Dispatch<T>(ICommand<T> command)
        {
            _unitOfWork.Commit();
        }
        
        void Dispatch(ICommand command)
        {
            _unitOfWork.Commit();
        }
        
        void Dispatch(object command)
        {
        }
    }
}