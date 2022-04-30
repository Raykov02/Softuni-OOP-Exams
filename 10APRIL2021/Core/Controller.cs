using AquaShop.Core.Contracts;
using AquaShop.Models.Aquariums;
using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Core
{
    public class Controller : IController
    {
        private DecorationRepository decorations;
        private ICollection<IAquarium> aquariums;
        public Controller()
        {
            decorations = new DecorationRepository();
            aquariums = new List<IAquarium>();
        }
        public string AddAquarium(string aquariumType, string aquariumName)
        {
            if (aquariumType == "FreshwaterAquarium")
            {
                aquariums.Add(new FreshwaterAquarium(aquariumName));
            }
            else if (aquariumType == "SaltwaterAquarium")
            {
                aquariums.Add(new SaltwaterAquarium(aquariumName));
            }
            else
            {
                throw new InvalidOperationException("Invalid aquarium type.");
            }
            return $"Successfully added {aquariumType}.";
        }

        public string AddDecoration(string decorationType)
        {
            if (decorationType == "Ornament")
            {
                decorations.Add(new Ornament());
            }
            else if (decorationType == "Plant")
            {
                decorations.Add(new Plant());
            }
            else
            {
                throw new InvalidOperationException("Invalid decoration type.");
            }
            return $"Successfully added {decorationType}.";
        }

        public string AddFish(string aquariumName, string fishType, string fishName, string fishSpecies, decimal price)
        {
            IFish fish = null;
            if (fishType == "FreshwaterFish")
            {
                fish = new FreshwaterFish(fishName, fishSpecies, price);
            }
            else if (fishType == "SaltwaterFish")
            {
                fish = new SaltwaterFish(fishName, fishSpecies, price);
            }
            else
            {
                throw new InvalidOperationException("Invalid fish type.");
            }
            IAquarium aquarium = aquariums.FirstOrDefault(x => x.Name == aquariumName);
            if((fishType == "FreshwaterFish" && aquarium.GetType().Name == "SaltwaterAquarium") 
                || 
                (fishType == "SaltwaterFish" && aquarium.GetType().Name == "FreshwaterAquarium"))
            {
                return "Water not suitable.";
            }
            else
            {
                aquarium.AddFish(fish);
                return $"Successfully added {fishType} to {aquariumName}.";
            }
        }

        public string CalculateValue(string aquariumName)
        {
            IAquarium aquarium = aquariums.FirstOrDefault(x => x.Name == aquariumName);
            decimal price = aquarium.Fish.Sum(x => x.Price) + aquarium.Decorations.Sum(x => x.Price);
            return $"The value of Aquarium {aquariumName} is {price:f2}.";
        }

        public string FeedFish(string aquariumName)
        {
            IAquarium aquarium = aquariums.FirstOrDefault(x => x.Name == aquariumName);
            aquarium.Feed();
            return $"Fish fed: {aquarium.Fish.Count}";
        }

        public string InsertDecoration(string aquariumName, string decorationType)
        {
            IDecoration decoration = decorations.FindByType(decorationType);
            if (decoration == null)
            {
                throw new InvalidOperationException($"There isn't a decoration of type {decorationType}.");
            }
            else
            {
                IAquarium toAddTo = aquariums.FirstOrDefault(x => x.Name == aquariumName);
                toAddTo.AddDecoration(decoration);
                decorations.Remove(decoration);
                return $"Successfully added {decorationType} to {aquariumName}.";
            }

        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var aquarium in aquariums)
            {
                sb.AppendLine(aquarium.GetInfo());
            }
            return sb.ToString().TrimEnd();
        }
    }
}
