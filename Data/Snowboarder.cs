using System;
using System.Collections.Generic;

namespace Avalanche.Data
{
    public partial class Snowboarder
    {
        public Snowboarder()
        {
            Sponsorings = new HashSet<Sponsoring>();
            Wettkampfs = new HashSet<Wettkampf>();
        }

        public string Mitgliedsnummer { get; set; } = null!;
        public string? Vorname { get; set; }
        public string? Nachname { get; set; }
        public string? Kuenstlername { get; set; }
        public DateTime? Geburtstag { get; set; }
        public string? HausBerg { get; set; }

        public virtual Berg? HausBergNavigation { get; set; }
        public virtual Profi MitgliedsnummerNavigation { get; set; } = null!;
        public virtual Profi Profi { get; set; } = null!;
        public virtual ICollection<Sponsoring> Sponsorings { get; set; }

        public virtual ICollection<Wettkampf> Wettkampfs { get; set; }
    }
}
