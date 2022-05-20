using System;
using System.Collections.Generic;

namespace motoGP.Models
{
    public partial class Race
    {
        public Race()
        {
            PilotRaceRes = new HashSet<PilotRaceRe>();
        }
        public Race(int id)
        {
            RaceId = id;
        }

        public long RaceId { get; set; }
        public long? Year { get; set; }
        public long? RaceNumber { get; set; }
        public string? Date { get; set; }
        public string? Track { get; set; }
        public long? Starters { get; set; }
        public long? Laps { get; set; }
        public string? Winner { get; set; }
        public string? FastestLap { get; set; }
        public string? PointsLeader { get; set; }

        public virtual Championship? YearNavigation { get; set; }
        public virtual ICollection<PilotRaceRe> PilotRaceRes { get; set; }
    }
}
