using System.ComponentModel.DataAnnotations;

namespace Force.Ddd.Domain
{
    public class IntHasNameBase: IntHasIdBase
    {
        [Required]
        public string Name { get; set; }

        public override string ToString() => Name;
    }
}