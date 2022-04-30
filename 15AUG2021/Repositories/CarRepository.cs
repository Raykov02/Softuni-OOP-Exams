using CarRacing.Models.Cars;
using CarRacing.Models.Cars.Contracts;
using CarRacing.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarRacing.Repositories
{
    public class CarRepository : IRepository<ICar>
    {
        private List<ICar> models;
        public CarRepository()
        {
            models = new List<ICar>(); 
        }
        public IReadOnlyCollection<ICar> Models => models.AsReadOnly();

        public void Add(ICar model)
        {
            if(model == null)
            {
                throw new ArgumentException("Cannot add null in Car Repository");
            }
            else
            {
                models.Add(model);
            }
        }

        public ICar FindBy(string property)
        {
            return models.FirstOrDefault(x => x.VIN == property);
        }

        public bool Remove(ICar model)
        {
            if (models.Contains(model))
            {
                models.Remove(model);
                return true;
            }
            return false;
        }
    }
}
