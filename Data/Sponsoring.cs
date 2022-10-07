using System;
using System.Collections.Generic;

namespace Avalanche.Data
{
    public partial class Sponsoring
    {
        public string Snowboarder { get; set; } = null!;
        public long Sponsor { get; set; }
        public long? Vertragsart { get; set; }

        public virtual Snowboarder SnowboarderNavigation { get; set; } = null!;
        public virtual Sponsor SponsorNavigation { get; set; } = null!;
        public virtual Vertragsart? VertragsartNavigation { get; set; }
    }
}
