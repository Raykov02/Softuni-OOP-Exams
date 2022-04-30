using Gym.Core.Contracts;
using Gym.Models.Athletes;
using Gym.Models.Athletes.Contracts;
using Gym.Models.Equipment;
using Gym.Models.Equipment.Contracts;
using Gym.Models.Gyms;
using Gym.Models.Gyms.Contracts;
using Gym.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Core
{
    public class Controller : IController
    {
        private ICollection<IGym> gyms;
        private EquipmentRepository equipment;
        public Controller()
        {
            gyms = new List<IGym>();
            equipment = new EquipmentRepository();
        }
        public string AddAthlete(string gymName, string athleteType, string athleteName, string motivation, int numberOfMedals)
        {
            IAthlete athlete = null;
            if (athleteType == "Boxer")
            {
                athlete = new Boxer(athleteName, motivation, numberOfMedals);
            }
            else if (athleteType == "Weightlifter")
            {
                athlete = new Weightlifter(athleteName, motivation, numberOfMedals);
            }
            else
            {
                throw new InvalidOperationException("Invalid athlete type.");
            }
            IGym gymToGo = gyms.FirstOrDefault(x => x.Name == gymName);
            if((athleteType == "Boxer" && gymToGo.GetType().Name == "WeightliftingGym") ||
                (athleteType == "Weightlifter" && gymToGo.GetType().Name == "BoxingGym"))
            {
                return $"The gym is not appropriate.";
            }
            gymToGo.AddAthlete(athlete);
            return $"Successfully added {athleteType} to {gymName}.";
        }

        public string AddEquipment(string equipmentType)
        {
            if (equipmentType == "BoxingGloves")
            {
                equipment.Add(new BoxingGloves());
            }
            else if (equipmentType == "Kettlebell")
            {
                equipment.Add(new Kettlebell());
            }
            else
            {
                throw new InvalidOperationException("Invalid equipment type.");
            }
            return $"Successfully added {equipmentType}.";
        }

        public string AddGym(string gymType, string gymName)
        {
            if (gymType != "BoxingGym" && gymType != "WeightliftingGym")
            {
                throw new InvalidOperationException("Invalid gym type.");
            }
            else if (gymType == "BoxingGym")
            {
                gyms.Add(new BoxingGym(gymName));
            }
            else
            {
                gyms.Add(new WeightliftingGym(gymName));
            }
            return $"Successfully added {gymType}.";
        }

        public string EquipmentWeight(string gymName)
        {
            IGym gym = gyms.FirstOrDefault(x => x.Name == gymName);
            return $"The total weight of the equipment in the gym {gymName} is {gym.EquipmentWeight:f2} grams.";

        }

        public string InsertEquipment(string gymName, string equipmentType)
        {
            IEquipment toInsert = equipment.FindByType(equipmentType);
            if (toInsert != null)
            {
                equipment.Remove(toInsert);
                IGym toAddTo = gyms.FirstOrDefault(x => x.Name == gymName);
                toAddTo.Equipment.Add(toInsert);
                return $"Successfully added {equipmentType} to {gymName}.";
            }
            else
            {
                throw new InvalidOperationException($"There isn’t equipment of type {equipmentType}.");
            }
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var gym in gyms)
            {
                sb.AppendLine(gym.GymInfo());
            }
            return sb.ToString().TrimEnd();
        }

        public string TrainAthletes(string gymName)
        {
            IGym gym = gyms.FirstOrDefault(x => x.Name == gymName);
            foreach (var athlete in gym.Athletes)
            {
                athlete.Exercise();
            }
            return $"Exercise athletes: {gym.Athletes.Count}.";
        }
        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}
