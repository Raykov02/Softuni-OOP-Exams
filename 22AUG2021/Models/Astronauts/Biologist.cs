using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceStation.Models.Astronauts
{
    public class Biologist : Astronaut
    {
        public Biologist(string name) : base(name, 70)
        {
        }
        public override bool CanBreath => Oxygen >= 5;
        public override void Breath()
        {
            if (CanBreath)
            {
                Oxygen -= 5;
            }
        }
    }
}
