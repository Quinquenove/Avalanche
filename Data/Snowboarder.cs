using System;
using System.Collections.Generic;

namespace Avalanche.Data
{
    public partial class Snowboarder
    {
        public Snowboarder()
        {
            Profis = new HashSet<Profi>();
        }

        public string Mitgliedsnummer { get; set; } = null!;
        public string? Vorname { get; set; }
        public string? Nachname { get; set; }
        public string? Kuenstlername { get; set; }
        public byte[]? Geburtstag { get; set; }
        public string? HausBerg { get; set; }

        public virtual Berg? HausBergNavigation { get; set; }
        public virtual ICollection<Profi> Profis { get; set; }
    }
}
