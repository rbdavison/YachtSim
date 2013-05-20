using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Geospatial;
using System.IO;
using System.Windows.Forms;

namespace EyeInTheSky
{
    internal class YachtItem
    {
        internal YachtItem(string details)
        {
            FalconViewIconHandle = -1;
            IconSize = 32;
            Id = Convert.ToInt32(Regex.Replace(details, "=.*", ""));
            Text = Regex.Replace(details, ".*=", "");
            Waypoints = new List<Location>();
        }

        internal int Id { get; private set; }
        internal string Text { get; private set; }
        private Location location;
        internal Location Location
        {
            get { return location; }
            set
            {
                PreviousLocation = location;
                location = value;
            }
        }
        internal Location Waypoint { get; set; }
        internal List<Location> Waypoints { get; set; }
        internal float Heading { get; set; }
        internal int IconSize { get; set; }
        internal Location PreviousLocation { get; set; }
        internal float SailDirection { get; set; }
        internal float WindAngle { get; set; }
        internal bool NorthUp { get; set; }
        internal string IconFileName
        {
            get
            {
                string northTag = NorthUp ? "-NorthUp" : "";
                string fileName = Path.Combine(Application.StartupPath,
                                    Id.ToString(),
                                    Convert.ToInt32(Heading).ToString(),
                                    Convert.ToInt32(SailDirection).ToString(),
                                    Convert.ToInt32(WindAngle).ToString(),
                                    Id.ToString() +
                                    "-" + Convert.ToInt32(Heading).ToString() +
                                    "-" + Convert.ToInt32(SailDirection).ToString() +
                                    "-" + Convert.ToInt32(WindAngle).ToString() +
                                    "-" + Convert.ToInt32(IconSize).ToString() +
                                    northTag +
                                    ".ico");
                if (!Directory.Exists(Path.GetDirectoryName(fileName)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                }
                return fileName;
            }
        }
        internal int FalconViewIconHandle { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
