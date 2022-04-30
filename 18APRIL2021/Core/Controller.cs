using Easter.Core.Contracts;
using Easter.Models.Bunnies;
using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes;
using Easter.Models.Dyes.Contracts;
using Easter.Models.Eggs;
using Easter.Models.Eggs.Contracts;
using Easter.Models.Workshops;
using Easter.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easter.Core
{
    public class Controller : IController
    {
        private BunnyRepository bunnies;
        private EggRepository eggs;
        public Controller()
        {
            bunnies = new BunnyRepository();
            eggs = new EggRepository();
        }
        public string AddBunny(string bunnyType, string bunnyName)
        {
            IBunny bunny;
            if (bunnyType == "HappyBunny")
            {
                bunny = new HappyBunny(bunnyName);
            }
            else if (bunnyType == "SleepyBunny")
            {
                bunny = new SleepyBunny(bunnyName);
            }
            else
            {
                throw new InvalidOperationException("Invalid bunny type.");
            }
            bunnies.Add(bunny);
            return $"Successfully added {bunnyType} named {bunnyName}.";
        }

        public string AddDyeToBunny(string bunnyName, int power)
        {
            IBunny bunny = bunnies.FindByName(bunnyName);
            if(bunny == null)
            {
                throw new InvalidOperationException("The bunny you want to add a dye to doesn't exist!");
            }
            IDye dye = new Dye(power);
            bunny.AddDye(dye);
            return $"Successfully added dye with power {power} to bunny {bunnyName}!";
        }

        public string AddEgg(string eggName, int energyRequired)
        {
            IEgg egg = new Egg(eggName, energyRequired);
            eggs.Add(egg);
            return $"Successfully added egg: {eggName}!";
        }

        public string ColorEgg(string eggName)
        {
            IEgg eggToColor = eggs.FindByName(eggName);
            if(!bunnies.Models.Any(b => b.Energy >= 50))
            {
                throw new InvalidOperationException("There is no bunny ready to start coloring!");
            }
            Workshop workshop = new Workshop();
            foreach (var bunny in bunnies.Models.OrderByDescending(x => x.Energy))
            {
                workshop.Color(eggToColor, bunny);
                if (eggToColor.IsDone())
                {
                    break;
                }
            }
            List<IBunny> toRemove = bunnies.Models.Where(x => x.Energy == 0).ToList();
            foreach (var bunny in toRemove)
            {
                bunnies.Remove(bunny);
            }
            if (eggToColor.IsDone())
            {
                return $"Egg {eggName} is done.";
            }
            else
            {
                return $"Egg {eggName} is not done.";
            }
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{eggs.Models.Count(x => x.IsDone())} eggs are done!");
            sb.AppendLine("Bunnies info:");
            foreach (var bunny in bunnies.Models)
            {
                sb.AppendLine(bunny.ToString());
            }
            return sb.ToString().TrimEnd();
        }
        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}
