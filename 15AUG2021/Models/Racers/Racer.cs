using CarRacing.Models.Cars.Contracts;
using CarRacing.Models.Racers.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRacing.Models.Racers
{
    public abstract class Racer : IRacer
    {
        
        private string username;
        private string racingBehavior;
        private int drivingExperience;
        private ICar car;

        protected Racer(string username, string racingBehavior, int drivingExperience, ICar car)
        {
            Username = username;
            RacingBehavior = racingBehavior;
            DrivingExperience = drivingExperience;
            Car = car;
        }

        public string Username
        {
            get => username;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Username cannot be null or empty.");
                }
                else
                {
                    username = value;
                }
            }
        }

        public string RacingBehavior
        {
            get => racingBehavior;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Racing behavior cannot be null or empty.");
                }
                else
                {
                    racingBehavior = value;
                }
            }
        }

        public int DrivingExperience
        {
            get => drivingExperience;
            protected set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException("Racer driving experience must be between 0 and 100.");
                }
                else
                {
                    drivingExperience = value;
                }
            }
        }

        public ICar Car
        {
            get => car;
            private set
            {
                if(value == null)
                {
                    throw new ArgumentException("Car cannot be null or empty.");
                }
                else
                {
                    car = value;
                }
            }
        }

        public bool IsAvailable()
        {
         if(Car.FuelAvailable >= car.FuelConsumptionPerRace)
            {
                return true;
            }
            return false;
        }

        public virtual void Race()
        {
            car.Drive();

        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{this.GetType().Name}: {Username}");
            sb.AppendLine($"--Driving behavior: {racingBehavior}");
            sb.AppendLine($"--Driving experience: {drivingExperience}");
            sb.AppendLine($"--Car: {Car.Make} {Car.Model} ({Car.VIN})");
            return sb.ToString().TrimEnd();
        }
    }
}
