using System;
using System.Collections.Generic;

namespace motoGP.Models
{
    public partial class Championship
    {
        public Championship()
        {
            Races = new HashSet<Race>();
        }

        public long Year { get; set; }
        public string? Champion { get; set; }
        public long? ChampionNumber { get; set; }

        public virtual ICollection<Race> Races { get; set; }
    }
}
