using System.ComponentModel.DataAnnotations;

namespace Avalanche.Models
{
    public class TrickViewModel
    {
        public long? Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Beschreibung")]
        public string Beschreibung { get; set; }
    }
}
