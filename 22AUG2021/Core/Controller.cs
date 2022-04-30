using SpaceStation.Core.Contracts;
using SpaceStation.Models.Astronauts;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission;
using SpaceStation.Models.Mission.Contracts;
using SpaceStation.Models.Planets;
using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Repositories;
using SpaceStation.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStation.Core
{
    class Controller : IController
    {
        private PlanetRepository planetRepo = new PlanetRepository();
        private AstronautRepository astroRepo = new AstronautRepository();
        private int exploredPlanets = 0;
        public string AddAstronaut(string type, string astronautName)
        {
            IAstronaut astro = null;
            if (type == "Biologist")
            {
                astro = new Biologist(astronautName);
            }
            else if (type == "Geodesist")
            {
                astro = new Geodesist(astronautName);
            }
            else if (type == "Meteorologist")
            {
                astro = new Meteorologist(astronautName);
            }
            else
            {
                throw new InvalidOperationException("Astronaut type doesn't exists!");
            }
            astroRepo.Add(astro);
            return $"Successfully added {type}: {astronautName}!";
        }

        public string AddPlanet(string planetName, params string[] items)
        {
            IPlanet planet = new Planet(planetName);
            foreach (var item in items)
            {
                planet.Items.Add(item);
            }
            planetRepo.Add(planet);
            return $"Successfully added Planet: {planetName}!";
        }

        public string ExplorePlanet(string planetName)
        {
            IPlanet planetToExplore = planetRepo.FindByName(planetName);
            int count = astroRepo.Models.Count(x => x.Oxygen >= 60);
            if (count == 0)
            {
                throw new InvalidOperationException("You need at least one astronaut to explore the planet!");
            }
            else
            {
                IMission mission = new Mission();
                List<IAstronaut> capableAstronauts = astroRepo.Models.Where(x => x.Oxygen >= 60).ToList();
                mission.Explore(planetToExplore, capableAstronauts);
                int astronautsDead = 0;
                foreach (var astronaut in capableAstronauts.Where(x => x.Oxygen <= 0))
                {
                    astronautsDead++;
                }
                exploredPlanets++;
                return $"Planet: {planetName} was explored! Exploration finished with {astronautsDead} dead astronauts!";
            }
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{exploredPlanets} planets were explored!");
            sb.AppendLine("Astronauts info:");
            foreach (var astronaut in astroRepo.Models)
            {
                sb.AppendLine($"Name: {astronaut.Name}");
                sb.AppendLine($"Oxygen: {astronaut.Oxygen}");
                if(astronaut.Bag.Items.Count > 0)
                {
                    sb.AppendLine($"Bag items: {string.Join(", ",astronaut.Bag.Items)}");
                }
                else
                {
                    sb.AppendLine($"Bag items: none");
                }
            }
            return sb.ToString().TrimEnd();
        }

        public string RetireAstronaut(string astronautName)
        {
            if (!astroRepo.Models.Any(x => x.Name == astronautName))
            {
                throw new InvalidOperationException($"Astronaut {astronautName} doesn't exists!");
            }
            else
            {
                astroRepo.Remove(astroRepo.FindByName(astronautName));
                return $"Astronaut {astronautName} was retired!";
            }
        }
        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}
