using EasterRaces.Core.Contracts;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Cars.Entities;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Models.Drivers.Entities;
using EasterRaces.Models.Races.Contracts;
using EasterRaces.Models.Races.Entities;
using EasterRaces.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasterRaces.Core.Entities
{
    class ChampionshipController : IChampionshipController
    {
        private DriverRepository driverRepository;
        private CarRepository carRepository;
        private RaceRepository raceRepository;
        public ChampionshipController()
        {
            carRepository = new CarRepository();
            raceRepository = new RaceRepository();
            driverRepository = new DriverRepository();
        }
        public string AddCarToDriver(string driverName, string carModel)
        {
            IDriver driver = driverRepository.GetByName(driverName);
            if(driver == null)
            {
                throw new InvalidOperationException($"Driver {driverName} could not be found.");
            }
            ICar car = carRepository.GetByName(carModel);
            if(car == null)
            {
                throw new InvalidOperationException($"Car {carModel} could not be found.");
            }
            driver.AddCar(car);
            return $"Driver {driverName} received car {carModel}.";
        }

        public string AddDriverToRace(string raceName, string driverName)
        {
            IRace race = raceRepository.GetByName(raceName);
            if(race == null)
            {
                throw new InvalidOperationException($"Race {raceName} could not be found.");
            }
            IDriver driver = driverRepository.GetByName(driverName);
            if(driver == null)
            {
                throw new InvalidOperationException($"Driver {driverName} could not be found.");
            }
            race.AddDriver(driver);
            return $"Driver {driverName} added in {raceName} race.";
        }

        public string CreateCar(string type, string model, int horsePower)
        {
            ICar car = null;
            if (carRepository.Models.Any(x => x.Model == model))
            {
                throw new ArgumentException($"Car {model} is already created.");
            }
            if(type == "Muscle")
            {
                car = new MuscleCar(model, horsePower);
            }
            else if(type == "Sports")
            {
                car = new SportsCar(model, horsePower);
            }
            carRepository.Add(car);
            return $"{car.GetType().Name} {model} is created.";
        }

        public string CreateDriver(string driverName)
        {
            IDriver driver = new Driver(driverName);
            if(driverRepository.Models.Any(x => x.Name == driverName))
            {
                throw new ArgumentException($"Driver {driverName} is already created.");
            }
            driverRepository.Add(driver);
            return $"Driver {driverName} is created.";
        }

        public string CreateRace(string name, int laps)
        {
            IRace race;
            if(raceRepository.Models.Any(x => x.Name == name))
            {
                throw new InvalidOperationException($"Race {name} is already create.");
            }
            race = new Race(name, laps);
            raceRepository.Add(race);
            return $"Race {name} is created.";
        }

        public string StartRace(string raceName)
        {
            IRace race = raceRepository.GetByName(raceName);
            if(race == null)
            {
                throw new InvalidOperationException($"Race {raceName} could not be found.");
            }
            List<IDriver> best3 = race.Drivers.OrderByDescending(x => x.Car.CalculateRacePoints(race.Laps)).Take(3).ToList();
            if(best3.Count < 3)
            {
                throw new InvalidOperationException($"Race {raceName} cannot start with less than 3 participants.");
            }
            best3[0].WinRace();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Driver {best3[0].Name} wins {race.Name} race.");
            sb.AppendLine($"Driver {best3[1].Name} is second in {race.Name} race.");
            sb.AppendLine($"Driver {best3[2].Name} is third in {race.Name} race.");
            return sb.ToString().TrimEnd();
        }
        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}
