using System;
using System.Collections.Generic;

namespace Avalanche.Data
{
    public partial class Wettkampf
    {
        public Wettkampf()
        {
            Snowboarders = new HashSet<Snowboarder>();
        }

        public long Rowid { get; set; }
        public string? Name { get; set; }
        public byte[]? Jahr { get; set; }
        public string? Sponsor { get; set; }
        public string? Berg { get; set; }
        public double? Preisgeld { get; set; }

        public virtual Berg? BergNavigation { get; set; }
        public virtual Sponsor? SponsorNavigation { get; set; }

        public virtual ICollection<Snowboarder> Snowboarders { get; set; }
    }
}
