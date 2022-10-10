using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Avalanche.Models
{
    public class WettkampfViewModel
    {
        public long Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; } = null!;
        [Display(Name = "Jahr")]
        public long Jahr { get; set; }
        [Display(Name = "Sponsor")]
        public string? Sponsor { get; set; }
        [Display(Name = "Berg")]
        public string? Berg { get; set; }
        [Display(Name = "Preisgeld")]
        public double? Preisgeld { get; set; }

        public IEnumerable<SelectListItem> SponsorList { get; set; }
        public IEnumerable<SelectListItem> BergList { get; set; }
    }
}
