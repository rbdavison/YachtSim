using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Geospatial;

namespace YachtSim
{
    public class SailAngle : Angle
    {
        /// <summary>Initializes a new instance of the SailAngle class.</summary>
        /// <param name="angle">The angle of the sail.</param>
        /// <exception cref="ArgumentNullException">angle is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// angle is greater than 0 degrees or less than 90 degrees.
        /// </exception>
        public SailAngle()
            : this(new Angle(0))
        {
        }

        public SailAngle(Angle angle)
            : base((angle ?? new Angle(0)).Radians) // Prevent null reference access
        {
            if (angle == null)
            {
                throw new ArgumentNullException("angle");
            }
            ValidateRange("SailAngle", angle.TotalDegrees, 0, 90);
        }

        private SailAngle(double radians)
            : base(radians)
        {
        }

        /// <summary>Creates a new Latitude from an angle in degrees.</summary>
        /// <param name="degrees">The angle of the latitude in degrees.</param>
        /// <returns>A new Latitude representing the specified value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// degrees is greater than 0 or less than 90.
        /// </exception>
        public static new SailAngle FromDegrees(double degrees)
        {
            ValidateRange("SailAngle", degrees, 0, 90);
            return new SailAngle(degrees * Math.PI / 180);
        }

        /// <summary>Creates a new Latitude from an amount in radians.</summary>
        /// <param name="radians">The angle of the latitude in radians.</param>
        /// <returns>A new Latitude representing the specified value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// radians is greater than PI/4 or less than -PI/4.
        /// </exception>
        public static new SailAngle FromRadians(double radians)
        {
            ValidateRange("SailAngle", radians, 0, 90);
            return new SailAngle(radians);
        }

        public override string ToString(string format, IFormatProvider formatProvider)
        {
            return "SailAngle: " + this.TotalDegrees.ToString("D1");
        }
    }
}