using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Force.Cqrs;

namespace Force.Examples.Domain.Features
{
    public class CreateProductAsync: ICommand<Task<int>>
    {
        // get and set both are public and there is no constructor
        // because this is a DTO (not an Entity) so that
        // it should only be used on the boundaries of the app
        // e.g. serialization/deserialization to/from JSON
        [Required]
        public string Name { get; set; } 
        
        [Range(1, double.MaxValue)]
        public double Price { get; set; }
    }
}