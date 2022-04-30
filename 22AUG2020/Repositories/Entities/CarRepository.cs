using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Cars.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasterRaces.Repositories.Entities
{
    public class CarRepository : Repository<ICar>
    {
        public CarRepository()
        {
        }

        public override ICar GetByName(string name)
        {
            return Models.FirstOrDefault(x => x.Model == name);
        }
    }
}
