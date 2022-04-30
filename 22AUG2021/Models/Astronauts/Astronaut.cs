using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Bags;
using SpaceStation.Models.Bags.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceStation.Models.Astronauts
{
    public abstract class Astronaut : IAstronaut
    {
        private string name;
        private double oxygen;

        protected Astronaut(string name, double oxygen)
        {
            Name = name;
            Oxygen = oxygen;
            Bag = new Backpack();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Astronaut name cannot be null or empty.");
                }
                name = value;
            }
        }

        public double Oxygen
        {
            get => oxygen;
            protected set
            { 
                if(value < 0)
                {
                    throw new ArgumentException("Cannot create Astronaut with negative oxygen!");
                }
                oxygen = value; 
            }
        }

        public virtual bool CanBreath => Oxygen >= 10;

        public IBag Bag { get; private set; }

        public virtual void Breath()
        {
            if (CanBreath)
            {
                Oxygen -= 10;
            }
        }
    }
}
