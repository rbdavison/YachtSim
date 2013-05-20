using System;
using System.Collections.Generic;
using System.Text;
using YachtSim;
using Geospatial;
using System.Windows;
using System.Threading;

namespace Tracksail
{
    public class Simulator : IYacht, IDisposable
    {
        #region IYacht implementation

        public Simulator()
        {
            this.Heading = new Angle(0);
            this.Heeling = new Angle(0);
            this.MaximumPointingAngle = Angle.FromDegrees(30);
            this.MilliSecondsBetweenUpdates = Constants.MILLISECONDS_BETWEEN_YACHT_UPDATES;
            this.KeepAlive = true;
            this.RudderAngle = new RudderAngle();
            this.SailAngle = SailAngle.FromDegrees(0);
            this.SailAverageCord = 0.3;
            this.SailConstant = 1.0;
            this.SailFootHeight = 0.15;
            this.SailHeight = 1.15;
            this.Waypoints = new List<Waypoint>();
            this.Waypoints.Add(new Waypoint(0, Location.Parse("0 0")));
            this.Location = Waypoints[0].Location;
            this.processorThread = new Thread(new ThreadStart(Processor));
            this.processorThread.SetApartmentState(ApartmentState.STA);
            this.processorThread.Start();
            // Wait for the thread to start up
            while (!processorThread.IsAlive) ;
        }

        public void Dispose()
        {
            this.KeepAlive = false;
            if (processorThread != null)
            {
                processorThread.Join(1000);
                while (processorThread.IsAlive) ;
            }
        }

        public Angle Heading { get; private set; }
        public Angle Heeling { get; private set; }
        public int Id { get; set; }
        public Location Location { get; private set; }
        public Angle MaximumPointingAngle { get; set; }
        public string Name { get; set; }
        public RudderAngle RudderAngle { get; private set; }
        public SailAngle SailAngle { get; private set; }
        public double SailAverageCord { get; set; }
        public double SailConstant { get; set; }
        public Angle SailDirection { get; private set; }
        public double SailFootHeight { get; set; }
        public double SailHeight { get; set; }
        public SailTypes SailType { get; set; }

        public void SetLocation(Location location)
        {
            this.Location = location;
        }

        public void SetRudderAngle(RudderAngle rudderAngle)
        {
            this.RudderAngle = rudderAngle;
        }

        public void SetSailAngle(SailAngle sailAngle)
        {
            this.SailAngle = sailAngle;
        }

        public void SetSimServer(YachtSimServer sailSim)
        {
            this.YachtSimServer = sailSim;
        }

        public string SimulatorName { get { return "TrackSail"; } }
        public SimVersions SimulatorVersion { get { return SimVersions.V1; } }
        public List<Waypoint> Waypoints { get; set; }

        #endregion

        #region Private simiulator stuff

        public Angle ApparentWindAngle { get; set; }
        public double ApparentWindVelocity { get; set; }
        private Angle currentWindAngle = Angle.FromDegrees(0);
        private double currentWindVelocity = 0;
        private bool KeepAlive { get; set; }
        private DateTime LastUpdateTime { get; set; }
        private int MilliSecondsBetweenUpdates { get; set; }
        private Thread processorThread;
        private Vector position = new Vector(0, 0);
        private YachtSimServer YachtSimServer { get; set; }
        private double previouslyVelocity = 0;

        public void Processor()
        {
            LastUpdateTime = DateTime.Now;
            while (this.KeepAlive)
            {
                UpdateYacht();
                Thread.Sleep(MilliSecondsBetweenUpdates);
            }
        }

        private void UpdateYacht()
        {
            DateTime updateTime = DateTime.Now;
            UpdateApparentWind();
            UpdateSailDirection();
            UpdateHeading();
            // TODO: Update the heeling of the yacht due to wind

            UpdateLocation(updateTime);
            this.LastUpdateTime = updateTime;
        }

        private void UpdateHeading()
        {
            this.Heading = Angle.FromDegrees(this.Heading.TotalDegrees + this.RudderAngle.TotalDegrees * (Constants.HEADING_CHANGE_PER_DEGREE_PER_SECOND / (1000 / Constants.MILLISECONDS_BETWEEN_YACHT_UPDATES)));
            if (this.Heading.TotalDegrees > 360)
            {
                this.Heading = Angle.FromDegrees(this.Heading.TotalDegrees - 360);
            }
            else if (this.Heading.TotalDegrees < 0)
            {
                this.Heading = Angle.FromDegrees(this.Heading.TotalDegrees + 360);
            }
            // TODO: Add changes in direction of the yacht due to waves and wind
        }

        private void UpdateLocation(DateTime updateTime)
        {
            double totalSeconds = updateTime.Subtract(this.LastUpdateTime).TotalSeconds;

            // Get the Boats movement
            double velocity = GetThrust() * totalSeconds;
            if (velocity < 0)
            {
                velocity = velocity * -1.0;
            }
            Point boatMovement = new Point(Math.Cos(this.Heading.Radians) * velocity, Math.Sin(this.Heading.Radians) * velocity);

            // Now add leeway
            double windVelocity = this.ApparentWindVelocity * 0.05 * totalSeconds;
            Point leewayMovement = new Point(Math.Cos(this.ApparentWindAngle.Radians) * windVelocity,
                                    Math.Sin(this.ApparentWindAngle.Radians) * windVelocity);
            // Add the two together
            position = new Vector(boatMovement.X - leewayMovement.X, boatMovement.Y - leewayMovement.Y);

            //position = new Vector(point.X, point.Y);
            //Point leewayPos = Vector.Add(position, point);
            double distance = position.Length;
            position.Normalize();
            if (!double.IsNaN(distance) && position.X != 0 && !double.IsNaN(position.X))
            {
                Vector north = new Vector(1.0, 0.0);
                Angle movementDirection = Angle.FromDegrees(Vector.AngleBetween(north, position));
                if (movementDirection.TotalDegrees < 0)
                {
                    movementDirection = Angle.FromDegrees(movementDirection.TotalDegrees + 360);
                }
                this.Location = this.Location.GetPoint(distance, movementDirection);
            }
            previouslyVelocity = velocity;
        }

        private double GetThrust()
        {
            double px = Math.Cos(Angle.FromDegrees(this.SailDirection.TotalDegrees + 90).Radians);
            double py = Math.Sin(Angle.FromDegrees(this.SailDirection.TotalDegrees + 90).Radians);

            double wx = Math.Cos(this.ApparentWindAngle.Radians);
            double wy = Math.Sin(this.ApparentWindAngle.Radians);

            double pt = px * wx + py * wy;

            double thrust = this.ApparentWindVelocity * Math.Abs(pt);
            return thrust;
        }

        private void UpdateApparentWind()
        {
            if (YachtSimServer != null && YachtSimServer.Wind != null)
            {
                this.currentWindAngle = YachtSimServer.Wind.Direction;
                this.currentWindVelocity = YachtSimServer.Wind.Velocity;
            }
            this.ApparentWindAngle = this.currentWindAngle;
            this.ApparentWindVelocity = this.currentWindVelocity;
            // A = SquareRoot(W^2 + V^2 + 2WV cos alpha)
            // A = Apparent wind
            // V = Velocity of the boat over the ground
            // W = True wind velocity
            // alpha = the pointing angle of the wind. 0 = upwind, 180 = downwind
            //double alpha = (Heading.TotalDegrees - this.currentWindAngle.TotalDegrees + 360) % 360;
            //this.ApparentWindVelocity = Math.Sqrt(this.currentWindVelocity * this.currentWindVelocity +
            //                                this.previouslyVelocity * this.previouslyVelocity +
            //                                2 * this.currentWindVelocity * this.previouslyVelocity * Math.Cos(Math.PI * alpha / 180.0));

            // beta = arccos((W cos alpha + V) / A)
            // A = Apparent wind
            // V = Velocity of the boat over the ground
            // W = True wind velocity
            // alpha = the pointing angle of the wind. 0 = upwind, 180 = downwind
            // beta = apparent wind angle
            //this.ApparentWindAngle = Angle.FromDegrees(Math.Acos((this.currentWindVelocity * Math.Cos(Math.PI * alpha / 180)) /
            //                                                     this.ApparentWindVelocity));
        }

        private void UpdateSailDirection()
        {
            double sailDir = (this.ApparentWindAngle.TotalDegrees + 180.0 - this.Heading.TotalDegrees + 360) % 360;
            double starboardTack = (180 - SailAngle.TotalDegrees + 360) % 360;
            double portTack = (180 + SailAngle.TotalDegrees) % 360;
            // TODO: need to account for the current sail direction in here also...
            // I.e. if they sail angle is set to 90 degrees and the yacht is on a port tack the sail
            // isn't going to swing to the port when the wind is coming from 181 degrees.
            // In reality it won't switch until the angle wind is coming from 271 degrees.
            if (sailDir > portTack)
            {
                sailDir = portTack;
            }
            if (sailDir < starboardTack)
            {
                sailDir = starboardTack;
            }

            this.SailDirection = Angle.FromDegrees((sailDir + this.Heading.TotalDegrees) % 360);
        }

        #endregion

    }
}
