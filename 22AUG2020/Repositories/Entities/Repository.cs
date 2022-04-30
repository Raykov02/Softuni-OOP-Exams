using EasterRaces.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasterRaces.Repositories.Entities
{
    public abstract class Repository<T> : IRepository<T>
    {
        public List<T> Models { get; set; }
        protected Repository()
        {
            Models = new List<T>();
        }
        public void Add(T model)
        {
            Models.Add(model);
        }

        public IReadOnlyCollection<T> GetAll()
        {
            return Models.AsReadOnly();
        }

        public abstract T GetByName(string name);
     

        public bool Remove(T model)
        {
            return Models.Remove(model);
        }
    }
}
