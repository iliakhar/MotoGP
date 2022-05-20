using System;
using System.Collections.Generic;

namespace motoGP.Models
{
    public partial class TeamYearInfo
    {
        public long? TeamId { get; set; }
        public long TeamYearInfoId { get; set; }
        public long? Year { get; set; }
        public string? BikeName { get; set; }
        public long? NumberOfParticipants { get; set; }
        public long? Points { get; set; }

        public virtual Team? Team { get; set; }
    }
}
