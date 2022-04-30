using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarCroft.Entities.Items;

namespace WarCroft.Entities.Inventory
{
    public abstract class Bag : IBag
    {
        private List<Item> items;

        protected Bag(int capacity)
        {
            Capacity = capacity;
            items = new List<Item>();
        }

        public int Capacity { get; set; } = 100;

        public int Load => items.Sum(i => i.Weight);

        public IReadOnlyCollection<Item> Items => items.AsReadOnly();

        public void AddItem(Item item)
        {
            if(Load + item.Weight > Capacity)
            {
                throw new InvalidOperationException("Bag is full!");
            }
            else
            {
                items.Add(item);
            }
        }

        public Item GetItem(string name)
        {
            if(Items.Count == 0)
            {
                throw new InvalidOperationException("Bag is empty!");
            }
            Item item = items.FirstOrDefault(x => x.GetType().Name == name);
            if(item == null)
            {
                throw new ArgumentException($"No item with name {name} in bag!");
            }
            else
            {
                items.Remove(item);
                return item;
            }
        }
    }
}
