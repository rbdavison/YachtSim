using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Geospatial;

namespace YachtSim
{
    public class Waypoint
    {
        public Location Location { get; set; }
        public int Id { get; set; }

        public Waypoint(int id, Location location)
        {
            Id = id;
            Location = location;
        }
    }
}
