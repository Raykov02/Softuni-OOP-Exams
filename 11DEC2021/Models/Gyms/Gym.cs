using Gym.Models.Athletes.Contracts;
using Gym.Models.Equipment.Contracts;
using Gym.Models.Gyms.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Models.Gyms
{
    public abstract class Gym : IGym
    {
        private string name;
        private List<IEquipment> equipment;
        private List<IAthlete> athletes;

        protected Gym(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            equipment = new List<IEquipment>();
            athletes = new List<IAthlete>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Gym name cannot be null or empty.");
                }
                name = value;
            }
        }

        public int Capacity { get; private set; }

        public double EquipmentWeight => Equipment.Sum(x => x.Weight);

        public ICollection<IEquipment> Equipment => equipment;

        public ICollection<IAthlete> Athletes => athletes;

        public void AddAthlete(IAthlete athlete)
        {
            if(Athletes.Count < Capacity)
            {
                Athletes.Add(athlete);
            }
            else
            {
                throw new InvalidOperationException("Not enough space in the gym.");
            }
        }

        public void AddEquipment(IEquipment equipment)
        {
            Equipment.Add(equipment);
        }

        public void Exercise()
        {
            foreach (var athlete in Athletes)
            {
                athlete.Exercise();
            }
        }

        public string GymInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{this.Name} is a {this.GetType().Name}:");
            if(Athletes.Count > 0)
            {
                sb.AppendLine($"Athletes: {string.Join(", ", Athletes)}");
            }
            else
            {
                sb.AppendLine("Athletes: No athletes");
            }
            sb.AppendLine($"Equipment total count: {Equipment.Count}");
            sb.AppendLine($"Equipment total weight: {EquipmentWeight:f2} grams");
            return sb.ToString().TrimEnd();
        }

        public bool RemoveAthlete(IAthlete athlete)
        {
            return Athletes.Remove(athlete);
        }
    }
}
