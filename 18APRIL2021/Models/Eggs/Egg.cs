using Easter.Models.Eggs.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easter.Models.Eggs
{
    public class Egg : IEgg
    {
        private string name;
        private int energyRequierd;
        public Egg(string name, int energyRequired)
        {
            Name = name;
            EnergyRequired = energyRequired;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Egg name cannot be null or empty.");
                }
                name = value;
            }
        }

        public int EnergyRequired
        {
            get => energyRequierd;
            private set 
            {
                if(value < 0)
                {
                    value = 0;
                }
                energyRequierd = value;
            }
        } // vuzmojna proverka za <0

        public void GetColored()
        {
            EnergyRequired -= 10;
            if (EnergyRequired < 0)
            {
                EnergyRequired = 0;
            }
        }

        public bool IsDone()
        {
            return EnergyRequired == 0;
        }
    }
}
