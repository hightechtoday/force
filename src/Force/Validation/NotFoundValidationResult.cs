using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Force.Validation
{
    public class NotFoundValidationResult: ValidationResult
    {
        protected NotFoundValidationResult(ValidationResult validationResult) : base(validationResult)
        {
        }

        public NotFoundValidationResult(string errorMessage) : base(errorMessage)
        {
        }

        public NotFoundValidationResult(string errorMessage, IEnumerable<string> memberNames) : 
            base(errorMessage, memberNames)
        {
        }
    }
}