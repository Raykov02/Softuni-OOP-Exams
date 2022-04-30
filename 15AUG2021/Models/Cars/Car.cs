using CarRacing.Models.Cars.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRacing.Models.Cars
{
    public abstract class Car : ICar
    {
        //----------------------------------------------------------------FIELDS----------------------------------------------------------------

        private string make;
        private string model;
        private string vin;
        private int horsePower;
        private double fuelAvailable;
        private double fuelConsumptionPerRace;
        //----------------------------------------------------------------PROPS----------------------------------------------------------------
        protected Car(string make, string model, string vIN, int horsePower, double fuelAvailable, double fuelConsumptionPerRace)
        {
            Make = make;
            Model = model;
            VIN = vIN;
            HorsePower = horsePower;
            FuelAvailable = fuelAvailable;
            FuelConsumptionPerRace = fuelConsumptionPerRace;
        }

        //----------------------------------------------------------------PROPS----------------------------------------------------------------

        public string Make
        {
            get => make;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Car make cannot be null or empty.");
                }
                else
                {
                    make = value;
                }
            }
        }
        public string Model
        {
            get => model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Car model cannot be null or empty.");
                }
                else
                {
                    model = value;
                }
            }
        }
        public string VIN
        {
            get => vin;
            private set
            {
                if (value.Length != 17)
                {
                    throw new ArgumentException("Car VIN must be exactly 17 characters long.");
                }
                else
                {
                    vin = value;
                }
            }
        }
        public  int HorsePower
        {
            get { return horsePower; }
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Horse power cannot be below 0.");
                }
                else
                {
                    horsePower = value;
                }
            }
        }
        public double FuelAvailable
        {
            get { return fuelAvailable; }
            private set 
            {
                if (value < 0)
                {
                    value = 0; 
                }
                fuelAvailable = value;
            }
        }
        public double FuelConsumptionPerRace
        {
            get { return fuelConsumptionPerRace; }
            private set 
            { 
               if(value < 0)
                {
                    throw new ArgumentException("Fuel consumption cannot be below 0.");
                }
                else
                {
                    fuelConsumptionPerRace = value;
                }
            }
        }

        //----------------------------------------------------------------BEHAVIOR----------------------------------------------------------------
        
        public virtual void Drive()
        {
            if(FuelAvailable >= FuelConsumptionPerRace)
            FuelAvailable -= FuelConsumptionPerRace;
            if(FuelAvailable < 0)
            {
                fuelAvailable = 0;
            }
        }

    }
}
