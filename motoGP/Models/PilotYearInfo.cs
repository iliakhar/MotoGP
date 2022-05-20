using System;
using System.Collections.Generic;

namespace motoGP.Models
{
    public partial class PilotYearInfo
    {
        public long? PilotId { get; set; }
        public long ResId { get; set; }
        public long? Year { get; set; }
        public long? Points { get; set; }
        public long? Starts { get; set; }
        public long? Poles { get; set; }
        public long? Wins { get; set; }
        public long? Podiums { get; set; }
        public long? FastestLaps { get; set; }
        public long? Laps { get; set; }

        public virtual Pilot? Pilot { get; set; }
    }
}
