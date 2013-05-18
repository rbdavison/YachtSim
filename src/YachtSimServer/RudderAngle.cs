using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Geospatial;

namespace YachtSim
{
    public class RudderAngle : Angle
    {
        /// <summary>Initializes a new instance of the RudderAngle class.</summary>
        /// <param name="angle">The angle of the rudder.</param>
        /// <exception cref="ArgumentNullException">angle is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// angle is greater than 90 degrees or less than 0 degrees.
        /// </exception>
        public RudderAngle()
            : this(new Angle(0))
        {
        }

        public RudderAngle(Angle angle)
            : base((angle ?? new Angle(0)).Radians) // Prevent null reference access
        {
            if (angle == null)
            {
                throw new ArgumentNullException("angle");
            }
            ValidateRange("SailAngle", angle.TotalDegrees, -90, 90);
        }

        private RudderAngle(double radians)
            : base(radians)
        {
        }

        /// <summary>Creates a new Latitude from an angle in degrees.</summary>
        /// <param name="degrees">The angle of the latitude in degrees.</param>
        /// <returns>A new Latitude representing the specified value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// degrees is greater than 90 or less than -90.
        /// </exception>
        public static new RudderAngle FromDegrees(double degrees)
        {
            ValidateRange("RudderAngle", degrees, -90, 90);
            return new RudderAngle(Angle.FromDegrees(degrees).Radians);
        }

        /// <summary>Creates a new Latitude from an amount in radians.</summary>
        /// <param name="radians">The angle of the latitude in radians.</param>
        /// <returns>A new Latitude representing the specified value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// radians is greater than PI/2 or less than -PI/2.
        /// </exception>
        public static new RudderAngle FromRadians(double radians)
        {
            ValidateRange("RudderAngle", radians, Angle.FromDegrees(-90).Radians, Angle.FromDegrees(90).Radians);
            return new RudderAngle(radians);
        }

        public override string ToString(string format, IFormatProvider formatProvider)
        {
            return "RudderAngle: " + this.TotalDegrees.ToString("D1");
        }
    }
}