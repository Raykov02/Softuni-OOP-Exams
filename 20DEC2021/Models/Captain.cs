using NavalVessels.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavalVessels.Models
{
    public class Captain : ICaptain
    {
        private string fullName;
        private List<IVessel> vessels;

        public Captain(string fullName)
        {
            FullName = fullName;
            vessels = new List<IVessel>();
        }

        public string FullName
        {
            get => fullName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Captain full name cannot be null or empty string.");
                }
                fullName = value;
            }
        }

        public int CombatExperience { get; private set; } = 0;

        public ICollection<IVessel> Vessels => vessels;

        public void AddVessel(IVessel vessel)
        {
            if(vessel == null)
            {
                throw new NullReferenceException("Null vessel cannot be added to the captain.");
            }
            vessels.Add(vessel);
        }

        public void IncreaseCombatExperience()
        {
            CombatExperience += 10;
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{FullName} has {CombatExperience} combat experience and commands {Vessels.Count} vessels.");
            foreach (var vessel in Vessels)
            {
                sb.AppendLine(vessel.ToString());
            }
            return sb.ToString().TrimEnd();
        }
    }
}
