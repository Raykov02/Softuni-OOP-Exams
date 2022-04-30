using Formula1.Core.Contracts;
using Formula1.Models;
using Formula1.Models.Contracts;
using Formula1.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Formula1.Core
{
    public class Controller : IController
    {
        private PilotRepository pilotRepository;
        private RaceRepository raceRepository;
        private FormulaOneCarRepository carRepository;
        public Controller()
        {
            pilotRepository = new PilotRepository();
            raceRepository = new RaceRepository();
            carRepository = new FormulaOneCarRepository();
        }

        public string AddCarToPilot(string pilotName, string carModel)
        {
            IPilot pilot = pilotRepository.FindByName(pilotName);
            if(pilot == null || pilot.Car != null)
            {
                throw new InvalidOperationException($"Pilot {pilotName} does not exist or has a car.");
            }
            IFormulaOneCar car = carRepository.FindByName(carModel);
            if(car == null)
            {
                throw new NullReferenceException($"Car {carModel} does not exist.");
            }
            pilot.AddCar(car);
            carRepository.Remove(car);
            return $"Pilot {pilotName} will drive a {car.GetType().Name} {car.Model} car.";
        }

        public string AddPilotToRace(string raceName, string pilotFullName)
        {
            IRace race = raceRepository.FindByName(raceName);
            if(race == null)
            {
                throw new NullReferenceException($"Race {raceName} does not exist.");
            }
            IPilot pilot = pilotRepository.FindByName(pilotFullName);
            if(pilot == null || pilot.CanRace == false || race.Pilots.Any(x => x.FullName == pilotFullName))
            {
                throw new InvalidOperationException($"Can not add pilot {pilotFullName} to the race.");
            }
            race.AddPilot(pilot);
            return $"Pilot {pilot.FullName} is added to the {race.RaceName} race.";
        }

        public string CreateCar(string type, string model, int horsepower, double engineDisplacement)
        {
            IFormulaOneCar doesCarExist = carRepository.FindByName(model);
            if (doesCarExist != null)
            {
                throw new InvalidOperationException($"Formula one car {model} is already created.");
            }
            IFormulaOneCar realCar = null;
            if (type == "Ferrari")
            {
                realCar = new Ferrari(model, horsepower, engineDisplacement);
            }
            else if (type == "Williams")
            {
                realCar = new Williams(model, horsepower, engineDisplacement);
            }
            else
            {
                throw new InvalidOperationException($"Formula one car type {type} is not valid.");
            }
            carRepository.Add(realCar);
            return $"Car {type}, model {model} is created.";
        }

        public string CreatePilot(string fullName)
        {
            IPilot doesPilotExist = pilotRepository.FindByName(fullName);
            if (doesPilotExist != null)
            {
                throw new InvalidOperationException($"Pilot {fullName} is already created.");
            }
            IPilot realPilot = new Pilot(fullName);
            pilotRepository.Add(realPilot);
            return $"Pilot {fullName} is created.";
        }

        public string CreateRace(string raceName, int numberOfLaps)
        {
            IRace doesRaceExist = raceRepository.FindByName(raceName);
            if(doesRaceExist != null)
            {
                throw new InvalidOperationException($"Race {raceName} is already created.");
            }
            IRace realRace = new Race(raceName, numberOfLaps);
            raceRepository.Add(realRace);
            return $"Race {raceName} is created.";
        }

        public string PilotReport()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var pilot in pilotRepository.Models.OrderByDescending(x => x.NumberOfWins))
            {
                sb.AppendLine(pilot.ToString());
            }
            return sb.ToString().TrimEnd();
        }

        public string RaceReport()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var executedRace in raceRepository.Models.Where(x => x.TookPlace == true))
            {
                sb.AppendLine(executedRace.RaceInfo());
            }
            return sb.ToString().TrimEnd();
        }

        public string StartRace(string raceName)
        {
            IRace race = raceRepository.FindByName(raceName);
            if(race == null)
            {
                throw new NullReferenceException($"Race {raceName} does not exist.");
            }
            if(race.Pilots.Count < 3)
            {
                throw new InvalidOperationException($"Race {raceName} cannot start with less than three participants.");
            }
            if(race.TookPlace == true)
            {
                throw new InvalidOperationException($"Can not execute race {raceName}.");
            }
            List<IPilot> pilotsInRace = race.Pilots.OrderByDescending(x => x.Car.RaceScoreCalculator(race.NumberOfLaps)).Take(3).ToList();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Pilot {pilotsInRace[0].FullName} wins the {race.RaceName} race.");
            sb.AppendLine($"Pilot {pilotsInRace[1].FullName} is second in the {race.RaceName} race.");
            sb.AppendLine($"Pilot {pilotsInRace[2].FullName} is third in the {race.RaceName} race.");
            pilotsInRace[0].WinRace();
            race.TookPlace = true;
            return sb.ToString().TrimEnd();
        }

        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}
