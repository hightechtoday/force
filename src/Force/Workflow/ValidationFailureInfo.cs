using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Force.Workflow
{
    public class ValidationFailureInfo: FailureInfo
    {
        public IEnumerable<ValidationResult> Results { get; }

        public ValidationFailureInfo(
            FailureType type, 
            IEnumerable<ValidationResult> results) : 
            base(type, GetMessage(results))
        {
            Results = results;
        }

        private static string GetMessage(IEnumerable<ValidationResult> results)
        {
            return "One or more validation errors occurred.";
        }
    }
}