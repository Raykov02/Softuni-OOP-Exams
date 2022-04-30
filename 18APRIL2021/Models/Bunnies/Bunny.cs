using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easter.Models.Bunnies
{
    public abstract class Bunny : IBunny
    {
        private string name;
        private List<IDye> dyes;

        protected Bunny(string name, int energy)
        {
            Name = name;
            Energy = energy;
            dyes = new List<IDye>();
        }


        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Bunny name cannot be null or empty.");
                }
                name = value;
            }
        }

        public int Energy { get; protected set; }

        public ICollection<IDye> Dyes => dyes;

        public void AddDye(IDye dye)
        {
            dyes.Add(dye);
        }

        public abstract void Work();
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Name: {Name}");
            sb.AppendLine($"Energy: {Energy}");
            sb.AppendLine($"Dyes: {Dyes.Count(x => x.IsFinished() == false)} not finished");
            return sb.ToString().TrimEnd();
        }
    }
}
