using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Geospatial;

namespace YachtSim
{
    public interface IYacht
    {
        /// <summary>
        /// The Apparent Wind Angle for the yacht.
        /// </summary>
        Angle ApparentWindAngle { get; set; }

        /// <summary>
        /// The Apparent Wind Velcity for the yacht.
        /// </summary>
        double ApparentWindVelocity { get; set; }

        /// <summary>
        /// Shutdown method. Simulator classes should implement IDisposable
        /// </summary>
        void Dispose();

        /// <summary>
        /// Returns the current heading of the yacht
        /// </summary>
        /// <returns></returns>
        Angle Heading { get; }

        /// <summary>
        /// Returns the current heeling angle
        /// </summary>
        /// <returns></returns>
        Angle Heeling { get; }

        /// <summary>
        /// This is the latitude and longitude of the yacht.
        /// </summary>
        /// <returns></returns>
        Location Location { get; }

        /// <summary>
        /// This is the ID for the yacht in the SailSim list.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// The Maximum angle at which the yacht can sail into the wind.
        /// Angles less than this into the wind are in the No-Go Zone
        /// </summary>
        Angle MaximumPointingAngle { get; set; }

        /// <summary>
        /// The name of the yacht that the simulator is processing.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// This is the angle that the rudder is. (Think proprioception)
        /// This is the angle of the rudder as the yacht reads it.
        /// </summary>
        RudderAngle RudderAngle { get; }

        /// <summary>
        /// The angle of the sail is. (Think proprioception)
        /// This is how lose or tight the sail is set as the yacht reads it.
        /// </summary>
        SailAngle SailAngle { get; }

        /// <summary>
        /// The average cord (width) of the sail
        /// </summary>
        double SailAverageCord { get; set; }

        /// <summary>
        /// The Aerodynamic Coefficient dimensionless.
        /// It is the sum of two percentages: the percentage of recovered energy on the leeward side +
        /// the percentage of the recovered energy on the windward side
        /// </summary>
        double SailConstant { get; set; }

        /// <summary>
        /// The height of the sail in metres
        /// </summary>
        double SailHeight { get; set; }

        /// <summary>
        /// The height of the foot of the sail above the sea in metres
        /// </summary>
        double SailFootHeight { get; set; }

        /// <summary>
        /// The type of sail that the yacht has
        /// </summary>
        SailTypes SailType { get; set; }

        /// <summary>
        /// Gets the angle of the sail on the yacht
        /// </summary>
        Angle SailDirection { get; }

        /// <summary>
        /// Sets the location for the yacht.
        /// This is usually set when first adding the yacht.
        /// </summary>
        /// <param name="location"></param>
        void SetLocation(Location location);

        /// <summary>
        /// Sets the angle of the rudder
        /// </summary>
        /// <param name="rudderAngle"></param>
        void SetRudderAngle(RudderAngle rudderAngle);

        /// <summary>
        /// For Cloth sails sets the desired maximum sail angle since these sails are able to freely to move from side to side.
        /// For Wing sails sets the angle of the sail which cannot move freely
        /// </summary>
        /// <param name="sailAngle"></param>
        void SetSailAngle(SailAngle sailAngle);

        /// <summary>
        /// Sets the link back to the Sail Simulator
        /// </summary>
        /// <param name="sailSim"></param>
        void SetSimServer(YachtSimServer sailSim);

        /// <summary>
        /// The name of the simulator that is being used.
        /// </summary>
        string SimulatorName { get; }

        /// <summary>
        /// The version for the simulator.
        /// </summary>
        SimVersions SimulatorVersion { get; }

        /// <summary>
        /// Gets and sets the list of waypoints
        /// </summary>
        List<Waypoint> Waypoints { get; set; }
    }
}
