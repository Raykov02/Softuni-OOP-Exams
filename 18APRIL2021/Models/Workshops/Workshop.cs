using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes.Contracts;
using Easter.Models.Eggs.Contracts;
using Easter.Models.Workshops.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easter.Models.Workshops
{
    public class Workshop : IWorkshop
    {
        public Workshop()
        {

        }
        public void Color(IEgg egg, IBunny bunny)
        {
            if (bunny.Energy > 0 && bunny.Dyes.Any(x => x.IsFinished() == false))
            {
                IDye currentDye = bunny.Dyes.FirstOrDefault(x => x.IsFinished() == false);
                while (bunny.Energy > 0 && bunny.Dyes.Any(x => x.IsFinished() == false) && egg.IsDone() == false)
                {
                    bunny.Work();
                    currentDye.Use();
                    egg.GetColored();
                    if (currentDye.IsFinished())
                    {
                        currentDye = bunny.Dyes.FirstOrDefault(x => x.IsFinished() == false);
                        if(currentDye == null)
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
}
