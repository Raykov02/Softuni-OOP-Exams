using NavalVessels.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavalVessels.Models
{
    public class Battleship : Vessel, IBattleship
    {
        public Battleship(string name, double mainWeaponCaliber, double speed) : base(name, mainWeaponCaliber, speed, 300)
        {
        }

        public bool SonarMode { get; private set; } = false;

        public override void RepairVessel()
        {
          if(ArmorThickness < 300)
            {
                ArmorThickness = 300;
            }
        }

        public void ToggleSonarMode()
        {
            SonarMode = !SonarMode;
            if (SonarMode)
            {
                MainWeaponCaliber += 40;
                Speed -= 5;
            }
            else
            {
                MainWeaponCaliber -= 40;
                Speed += 5;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(base.ToString());
            string onOff = SonarMode ? "ON" : "OFF";
            sb.AppendLine();
            sb.AppendLine($" *Sonar mode: {onOff}");
            return sb.ToString().TrimEnd();
        }
    }
}
