using System;
using System.Collections.Generic;

namespace Avalanche.Data
{
    public partial class Wettkampf
    {
        public string Name { get; set; } = null!;
        public byte[] Jahr { get; set; } = null!;
        public string? Sponsor { get; set; }
        public string? Berg { get; set; }
        public double? Preisgeld { get; set; }
        public int WettkampfId { get; set; }
    }
}
