using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Force.Ccc;
using Force.Reflection;
using Force.Validation;

namespace Force.Ddd
{
    public class DataAnnotationValidator<T>: IValidator<T>, IAsyncValidator<T>
    {
        private static readonly string[] EntityPropertyNames = Type<T>
            .PublicProperties
            .Where(x => !x.Value.PropertyType.IsValueType
                        && x.Key != "Request"
                        && x.Value.GetCustomAttributes().Any(y => y.GetType() == typeof(RequiredAttribute))
            )
            .Select(x => x.Key)
            .ToArray();
            
        public virtual IEnumerable<ValidationResult> Validate(T obj)
        {
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(obj, new ValidationContext(obj), results, true);
            var notFoundResults  = new List<string>();
            
            foreach (var result in results)
            {
                foreach (var mn in result.MemberNames)
                {
                    if (EntityPropertyNames.Contains(mn))
                    {
                        notFoundResults.Add(mn);
                    }
                }
            }

            if (notFoundResults.Any())
            {
                results.Add(new NotFoundValidationResult("One or more entities are not found", notFoundResults));
            }
            
            return results;
        }

        public virtual Task<IEnumerable<ValidationResult>> ValidateAsync(T obj) =>
            Task.FromResult(Validate(obj));
    }
}