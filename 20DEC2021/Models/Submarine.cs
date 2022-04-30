using NavalVessels.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavalVessels.Models
{
    public class Submarine : Vessel, ISubmarine
    {
        public Submarine(string name, double mainWeaponCaliber, double speed) : base(name, mainWeaponCaliber, speed, 200)
        {
        }

        public bool SubmergeMode { get; private set; } = false;

        public override void RepairVessel()
        {
          if(ArmorThickness < 200)
            {
                ArmorThickness = 200;
            }
        }

        public void ToggleSubmergeMode()
        {
            SubmergeMode = !SubmergeMode;
            if (SubmergeMode)
            {
                MainWeaponCaliber += 40;
                Speed -= 4;
            }
            else
            {
                MainWeaponCaliber -= 40;
                Speed += 4;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(base.ToString());
            string onOff = SubmergeMode ? "ON" : "OFF";
            sb.AppendLine();
            sb.AppendLine($" *Submerge mode: {onOff}");
            return sb.ToString().TrimEnd();
        }
    }
}
