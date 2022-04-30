using EasterRaces.Models.Drivers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasterRaces.Repositories.Entities
{
    public class DriverRepository : Repository<IDriver>
    {
        public DriverRepository()
        {
        }

        public override IDriver GetByName(string name)
        {
            return Models.FirstOrDefault(x => x.Name == name);
        }
    }
}
