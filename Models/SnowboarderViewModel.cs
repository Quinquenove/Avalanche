namespace Avalanche.Models
{
    public class SnowboarderViewModel
    {
        public string Nachname { get; set; }
        public string Vorname { get; set; }
        public string Kuenstlername { get; set; }
        public DateTime Geburtstag { get; set; }
        public BergViewModel HausBerg { get; set; }
        public string Mitgliednummer { get; set; }
    }
}
