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

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public long Jahr { get; set; }
        public long? SponsorId { get; set; }
        public long? BergId { get; set; }
        public double? Preisgeld { get; set; }

        public virtual Berg? Berg { get; set; }
        public virtual Sponsor? Sponsor { get; set; }

        public virtual ICollection<Snowboarder> Snowboarders { get; set; }
    }
}
