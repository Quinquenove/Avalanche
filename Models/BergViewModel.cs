using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Avalanche.Models
{
    public class BergViewModel
    {
        public long? Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Gebirge")]
        public string GebirgeId { get; set; }
        [Display(Name = "Schwierigkeit")]
        public string SchwierigkeitId { get; set; }

        public IEnumerable<SelectListItem> GebirgeListe { get; set; }
        public IEnumerable<SelectListItem> SchwierigkeitListe { get; set; }
    }
}
