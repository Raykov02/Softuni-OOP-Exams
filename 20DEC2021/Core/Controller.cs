using NavalVessels.Core.Contracts;
using NavalVessels.Models;
using NavalVessels.Models.Contracts;
using NavalVessels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NavalVessels.Core
{
    public class Controller : IController
    {
        private VesselRepository vessels;
        private List<ICaptain> captains;
        public Controller()
        {
            vessels = new VesselRepository();
            captains = new List<ICaptain>();
        }
        public string AssignCaptain(string selectedCaptainName, string selectedVesselName)
        {
            ICaptain toAssing = captains.FirstOrDefault(x => x.FullName == selectedCaptainName);
            if (toAssing == null)
            {
                return $"Captain {selectedCaptainName} could not be found.";
            }
            IVessel vessel = vessels.FindByName(selectedVesselName);
            if (vessel == null)
            {
                return $"Vessel {selectedVesselName} could not be found.";
            }
            if (vessel.Captain != null)
            {
                return $"Vessel {selectedVesselName} is already occupied.";
            }
            vessel.Captain = toAssing;
            toAssing.AddVessel(vessel);
            return $"Captain {selectedCaptainName} command vessel {selectedVesselName}.";
        }

        public string AttackVessels(string attackingVesselName, string defendingVesselName)
        {
            IVessel attacking = vessels.FindByName(attackingVesselName);
            if(attacking == null)
            {
                return $"Vessel {attackingVesselName} could not be found.";
            }
            IVessel defending = vessels.FindByName(defendingVesselName);
            if(defending == null)
            {
                return $"Vessel {defendingVesselName} could not be found.";
            }
            if(attacking.ArmorThickness == 0)
            {
                return $"Unarmored vessel {attackingVesselName} cannot attack or be attacked.";
            }
            if (defending.ArmorThickness == 0)
            {
                return $"Unarmored vessel {defendingVesselName} cannot attack or be attacked.";
            }
            attacking.Attack(defending); // vuzmojno da gurmi promeni kapitan xpto
            return $"Vessel {defendingVesselName} was attacked by vessel {attackingVesselName} - current armor thickness: {defending.ArmorThickness}.";
        }

        public string CaptainReport(string captainFullName)
        {
            ICaptain captain = captains.FirstOrDefault(x => x.FullName == captainFullName);
            if (captain != null && captain.Vessels.Count > 0)
            {
                return captain.Report();
            }
            else
            {
                return "";
            }
        }

        public string HireCaptain(string fullName)
        {
            if (captains.Any(x => x.FullName == fullName))
            {
                return $"Captain {fullName} is already hired.";
            }
            else
            {
                ICaptain toHire = new Captain(fullName);
                captains.Add(toHire);
                return $"Captain {fullName} is hired.";
            }
        }

        public string ProduceVessel(string name, string vesselType, double mainWeaponCaliber, double speed)
        {
            IVessel toProduce;
            if (vessels.FindByName(name) != null)
            {
                return $"{vesselType} vessel {name} is already manufactured."; //could be wrong
            }
            if (vesselType == "Submarine")
            {
                toProduce = new Submarine(name, mainWeaponCaliber, speed);
            }
            else if (vesselType == "Battleship")
            {
                toProduce = new Battleship(name, mainWeaponCaliber, speed);
            }
            else
            {
                return "Invalid vessel type.";
            }
            vessels.Add(toProduce);
            return $"{vesselType} {name} is manufactured with the main weapon caliber of {mainWeaponCaliber} inches and a maximum speed of {speed} knots.";
        }

        public string ServiceVessel(string vesselName)
        {
            IVessel vessel = vessels.FindByName(vesselName);
            if (vessel == null)
            {
                return $"Vessel {vesselName} could not be found.";
            }
            if (vessel is Battleship && vessel.ArmorThickness < 300)
            {
                vessel.RepairVessel();
                return $"Vessel {vesselName} was repaired.";
            }
            else if (vessel is Submarine && vessel.ArmorThickness < 200)
            {
                vessel.RepairVessel();
                return $"Vessel {vesselName} was repaired.";
            }
            else
            {
                return "";
            }
        }

        public string ToggleSpecialMode(string vesselName)
        {
            IVessel vessel = vessels.FindByName(vesselName);
            if (vessel == null)
            {
                return $"Vessel {vesselName} could not be found.";
            }
            if (vessel is Battleship)
            {
                IBattleship battleship = vessel as Battleship;
                battleship.ToggleSonarMode();
                return $"Battleship {vesselName} toggled sonar mode.";
            }
            else
            {
                ISubmarine submarine = vessel as Submarine;
                submarine.ToggleSubmergeMode();
                return $"Submarine {vesselName} toggled submerge mode.";
            }
        }

        public string VesselReport(string vesselName)
        {
            IVessel vessel = vessels.FindByName(vesselName);
            if (vessel != null)
            {
                return vessel.ToString();
            }
            else
            {
                return "";
            }
        }
    }
}
