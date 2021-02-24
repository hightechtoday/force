using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Force.Validation
{
    public interface IAsyncValidator<in T>
    {
        Task<IEnumerable<ValidationResult>> ValidateAsync(T obj);
    }
}