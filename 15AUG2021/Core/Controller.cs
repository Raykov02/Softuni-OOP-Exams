using CarRacing.Core.Contracts;
using CarRacing.Models.Cars;
using CarRacing.Models.Cars.Contracts;
using CarRacing.Models.Maps;
using CarRacing.Models.Maps.Contracts;
using CarRacing.Models.Racers;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarRacing.Core
{
    public class Controller : IController
    {
        private CarRepository cars = new CarRepository();
        private RacerRepository racers = new RacerRepository();
        IMap map = new Map();
        public string AddCar(string type, string make, string model, string VIN, int horsePower)
        {
            if (type == "TunedCar")
            {
                cars.Add(new TunedCar(make, model, VIN, horsePower));
                return $"Successfully added car {make} {model} ({VIN}).";
            }
            else if (type == "SuperCar")
            {
                cars.Add(new SuperCar(make, model, VIN, horsePower));
                return $"Successfully added car {make} {model} ({VIN}).";
            }
            else
            {
                throw new ArgumentException("Invalid car type!");
            }
        }

        public string AddRacer(string type, string username, string carVIN)
        {
            ICar carForRacer = cars.Models.FirstOrDefault(x => x.VIN == carVIN);
            if (carForRacer == null)
            {
                throw new ArgumentException("Car cannot be found!");
            }
            if (type == "ProfessionalRacer")
            {
                racers.Add(new ProfessionalRacer(username, carForRacer));
            }
            else if (type == "StreetRacer")
            {
                racers.Add(new StreetRacer(username, carForRacer));
            }
            else
            {
                throw new ArgumentException("Invalid racer type!");
            }
            return $"Successfully added racer {username}.";
        }

        public string BeginRace(string racerOneUsername, string racerTwoUsername)
        {
            IRacer racer1 = racers.Models.FirstOrDefault(x => x.Username == racerOneUsername);
            if(racer1 == null)
            {
                throw new ArgumentException($"Racer {racerOneUsername} cannot be found!");
            }
            IRacer racer2 = racers.Models.FirstOrDefault(x => x.Username == racerTwoUsername);
            if (racer2 == null)
            {
                throw new ArgumentException($"Racer {racerTwoUsername} cannot be found!");
            }
            return map.StartRace(racer1, racer2);
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var racer in racers.Models.OrderByDescending(x => x.DrivingExperience).ThenBy(x => x.Username))
            {
                sb.AppendLine(racer.ToString());
            }
            return sb.ToString().TrimEnd();
        }

        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}
