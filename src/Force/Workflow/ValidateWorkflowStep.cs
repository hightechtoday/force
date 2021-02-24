using System;
using System.Linq;
using System.Threading.Tasks;
using Force.Ccc;
using Force.Validation;

namespace Force.Workflow
{
    public class ValidateWorkflowStep<TRequest, TReturn>: IWorkflowStep<TRequest, TReturn>
    {
        private readonly IValidator<TRequest> _validator;

        public ValidateWorkflowStep(IValidator<TRequest> validator)
        {
            _validator = validator;
        }
        
        public Result<TReturn, FailureInfo> Process(TRequest request, Func<TRequest, Result<TReturn, FailureInfo>> next)
        {
            var errors = _validator
                .Validate(request)
                .ToList();
            
            return errors.IsValid() 
                ? next(request) 
                : FailureInfo.Invalid(errors);
        }
    }
    
    public class ValidateWorkflowAsyncStep<TRequest, TReturn>: IAsyncWorkflowStep<TRequest, TReturn>
    {
        private readonly IAsyncValidator<TRequest> _asyncValidator;

        public ValidateWorkflowAsyncStep(IAsyncValidator<TRequest> asyncValidator)
        {
            _asyncValidator = asyncValidator;
        }

        public async Task<Result<TReturn, FailureInfo>> ProcessAsync(
            TRequest request, 
            Func<TRequest, Task<Result<TReturn, FailureInfo>>> next)
        {
            var errors = await _asyncValidator.ValidateAsync(request);
            if (errors.IsValid())
            {
                return await next(request);
            }

            return FailureInfo.Invalid(errors);
        }
    }
}