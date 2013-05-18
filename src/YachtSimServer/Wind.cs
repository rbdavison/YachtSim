using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Geospatial;

namespace YachtSim
{
    public class Wind
    {
        private Random rand = new Random();
        private int direction;
        private double velocity;
        private int setDirection;
        private double setVelocity;

        public Wind()
            : this(0, 5)
        {
        }

        public Wind(int direction, int velocity)
        {
            this.direction = direction;
            this.setDirection = direction;
            this.velocity = velocity;
            this.setVelocity = velocity;
        }

        public Angle Direction
        {
            get
            {
                UpdateDirection();
                return Angle.FromDegrees(direction);
            }
            set
            {
                setDirection = Convert.ToInt32(value.TotalDegrees);
            }
        }

        public double Velocity
        {
            get
            {
                UpdateVelocity();
                return velocity;
            }
            set
            {
                setVelocity = value;
            }
        }

        public int DirectionVariability { get; set; }

        private void UpdateDirection()
        {
            int directionMinimum = (setDirection - DirectionVariability) % 360;
            int directionMaximum = (setDirection + DirectionVariability) % 360;
            if (directionMinimum > directionMaximum)
            {
                directionMinimum -= 360;
            }
            direction = rand.Next(directionMinimum, directionMaximum);
            if (direction < 0)
            {
                direction += 360;
            }
            if (direction > 360)
            {
                direction -= 360;
            }
        }

        private void UpdateVelocity()
        {
            int minimumVelocity = Convert.ToInt16(setVelocity * 100.0 - (setVelocity * 10 * 0.3));
            int maximumVelocity = Convert.ToInt16(setVelocity * 100.0 + (setVelocity * 10 * 0.3));
            velocity = rand.Next(minimumVelocity, maximumVelocity) / 100.0;
        }
    }
}
