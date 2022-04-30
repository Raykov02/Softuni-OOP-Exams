using CarRacing.Models.Racers;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarRacing.Repositories
{
    public class RacerRepository : IRepository<IRacer>
    {
        private List<IRacer> models;
        public RacerRepository()
        {
            models = new List<IRacer>();
        }
        public IReadOnlyCollection<IRacer> Models => models.AsReadOnly();
        public void Add(IRacer model)
        {
            if(model == null)
            {
                throw new ArgumentException("Cannot add null in Racer Repository");
            }
            else
            {
                models.Add(model);
            }
        }

        public IRacer FindBy(string property)
        {
            return models.FirstOrDefault(x => x.Username == property);
        }

        public bool Remove(IRacer model)
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
