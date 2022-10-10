using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Avalanche.Models
{
    public class ProfiViewModel
    {
        [Display(Name = "Lizenznummer")]
        public string Lizenznummer { get; set; }
        [Display(Name = "Weltcuppunkte")]
        public long? Weltcuppunkte { get; set; }
        [Display(Name = "Mitgliedsnummer")]
        public string Mitgliedsnummer { get; set; }
        [Display(Name = "Best Trick")]
        public string BestTrick { get; set; }

        public IEnumerable<SelectListItem> TrickList { get; set; }
    }
}
