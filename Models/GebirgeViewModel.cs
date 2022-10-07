using System.ComponentModel.DataAnnotations;

namespace Avalanche.Models
{
    public class GebirgeViewModel
    {
        public long? Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
