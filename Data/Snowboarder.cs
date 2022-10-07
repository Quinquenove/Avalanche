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
        public string Nachname { get; set; } = null!;
        public string Vorname { get; set; } = null!;
        public string Kuenstlername { get; set; } = null!;
        public DateTime? Geburtstag { get; set; } = null!;
        public long? HausBergId { get; set; }

        public virtual Berg? HausBerg { get; set; }
        public virtual Profi? Profi { get; set; }
        public virtual ICollection<Sponsoring> Sponsorings { get; set; }

        public virtual ICollection<Wettkampf> Wettkampfs { get; set; }
    }
}
