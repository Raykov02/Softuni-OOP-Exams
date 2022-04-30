using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Models.Aquariums
{
    public abstract class Aquarium : IAquarium
    {
        private string name;
        private ICollection<IDecoration> decorations;
        private ICollection<IFish> fish;

        protected Aquarium(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            decorations = new List<IDecoration>();
            fish = new List<IFish>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Aquarium name cannot be null or empty.");
                }
                name = value; 
            }
        }

        public int Capacity { get; private set; }

        public int Comfort => decorations.Sum(x => x.Comfort);

        public ICollection<IDecoration> Decorations => decorations;

        public ICollection<IFish> Fish => fish;

        public void AddDecoration(IDecoration decoration)
        {
            decorations.Add(decoration);
        }

        public void AddFish(IFish fish)
        {
           if(Fish.Count < this.Capacity)
            {
                this.fish.Add(fish);
            }
            else
            {
                throw new InvalidOperationException("Not enough capacity.");
            }
        }

        public void Feed()
        {
            foreach (var fish in this.Fish)
            {
                fish.Eat();
            }
        }

        public string GetInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Name} ({this.GetType().Name}):");
            if(Fish.Count > 0)
            {
                sb.AppendLine($"Fish: {string.Join(", ",Fish)}");
            }
            else
            {
                sb.AppendLine($"Fish: none");
            }
            sb.AppendLine($"Decorations: {Decorations.Count}");
            sb.AppendLine($"Comfort: {Comfort}");
            return sb.ToString().TrimEnd();
        }

        public bool RemoveFish(IFish fish)
        {
            return this.fish.Remove(fish);
        }
    }
}
