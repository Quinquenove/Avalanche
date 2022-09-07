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
        public DateTime Geburtstag { get; set; }
        [Display(Name = "Haus Berg")]
        public BergViewModel HausBerg { get; set; }
        [Display(Name = "Mitgliedsnummer")]
        public string Mitgliednummer { get; set; }
    }
}
