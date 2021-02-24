using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Force.Workflow
{
    public class FailureInfo
    {
        public FailureInfo(FailureType type, string message = null)
        {
            Type = type;
            Message = message;
        }

        public static FailureInfo Invalid(string message) => 
            new FailureInfo(FailureType.Invalid, message);
        
        public static FailureInfo Invalid(IEnumerable<ValidationResult> errors) => 
            new ValidationFailureInfo(FailureType.Invalid, errors);
        
        public static FailureInfo Unauthorized(string message) => 
            new FailureInfo(FailureType.Unauthorized, message);

        public static FailureInfo ConfigurationError(string message) => 
            new FailureInfo(FailureType.ConfigurationError, message);

        public static FailureInfo Other(string message) => 
            new FailureInfo(FailureType.Other, message);
        
        public FailureType Type { get;} 
        
        public string Message { get; }
    }
}