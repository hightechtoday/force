using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Force.Validation;

namespace Force.Examples.Domain.Features
{
    public class UpdateProductValidator: IAsyncValidator<UpdateProductAsyncContext>
    {
        public Task<IEnumerable<ValidationResult>> ValidateAsync(UpdateProductAsyncContext obj)
        {
            return Task.FromResult<IEnumerable<ValidationResult>>(new[]
            {
                new ValidationResult("Something is wrong")
            });
        }
    }
}