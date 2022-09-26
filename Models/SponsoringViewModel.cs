using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Avalanche.Models
{
    public class SponsoringViewModel
    {
        [Display(Name = "Mitgliedsnummer des Snowboarders")]
        public string Mitgliedsnummer { get; set; }
        [Display(Name = "Sponsor")]
        public string Sponsor { get; set; }
        [Display(Name = "Vertragsart")]
        public string Vertragsart { get; set; }
        public List<SelectListItem> SponsorList { get; set; }
        public List<SelectListItem> VertragsartList { get; set; }
    }
}
