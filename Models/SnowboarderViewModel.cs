using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Avalanche.Models
{
    public class SnowboarderViewModel
    {
        [Display(Name ="Nachname")]
        public string Nachname { get; set; }
        [Display(Name = "Vorname")]
        public string Vorname { get; set; }
        [Display(Name = "Künstlername")]
        public string Kuenstlername { get; set; }
        [Display(Name = "Geburtstag")]
        [DataType(DataType.Date)]
        public DateTime? Geburtstag { get; set; }
        [Display(Name = "Haus Berg")]
        public string HausBerg { get; set; }
        [Display(Name = "Mitgliedsnummer")]
        public string Mitgliedsnummer { get; set; }
        public List<SelectListItem> BergList { get; set; }
    }
}
