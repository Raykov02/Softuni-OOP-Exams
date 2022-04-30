using Gym.Models.Athletes.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.Models
{
    public abstract class Athlete : IAthlete
    {
        private string fullName;
        private string motivation;
        private int numberOfMedals;
        private int stamina;

        protected Athlete(string fullName, string motivation, int stamina, int numberOfMedals)
        {
            FullName = fullName;
            Motivation = motivation;
            Stamina = stamina;
            NumberOfMedals = numberOfMedals;
        }

        public string FullName
        {
            get => fullName;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Athlete name cannot be null or empty.");
                }
                fullName = value;
            }
        }

        public string Motivation
        {
            get => motivation;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("The motivation cannot be null or empty.");
                }
                motivation = value;
            }
        }

        public int Stamina { get => stamina; protected set => stamina = value; }

        public int NumberOfMedals
        {
            get => numberOfMedals;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Athlete's number of medals cannot be below 0.");
                }
                numberOfMedals = value;
            }
        }

        public abstract void Exercise();

        public override string ToString()
        {
            return this.FullName;
        }

    }

}
