using System;

using WarCroft.Constants;
using WarCroft.Entities.Inventory;
using WarCroft.Entities.Items;

namespace WarCroft.Entities.Characters.Contracts
{
    public abstract class Character
    {
        private string name;

        protected Character(string name, double health, double armor, double abilityPoints,  IBag bag)
        {
            Name = name;
            BaseHealth = health;
            Health = BaseHealth;
            BaseArmor = armor;
            Armor = BaseArmor;
            AbilityPoints = abilityPoints;
            Bag = bag;
           
        }

        public bool IsAlive { get; set; } = true;
        public double BaseHealth { get; private set; }
        public double BaseArmor { get; private set; }
        public double AbilityPoints { get; private set; }
        public double Armor { get; private set; }
        public double Health { get; set; }
        public IBag Bag { get; private set; }
        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be null or whitespace!");
                }
                name = value;
            }
        }
        protected void EnsureAlive()
        {
            if (!this.IsAlive)
            {
                throw new InvalidOperationException(ExceptionMessages.AffectedCharacterDead);
            }
        }

        public void TakeDamage(double hitPoints)
        {
            EnsureAlive();
            if (Armor >= hitPoints)
            {
                Armor -= hitPoints;
            }
            else
            {
                hitPoints -= Armor;
                Armor = 0;
                Health -= hitPoints;
                if (Health <= 0)
                {
                    Health = 0;
                    IsAlive = false;
                }
            }

        }
        public void UseItem(Item item)
        {
            EnsureAlive();
            item.AffectCharacter(this);
        }
        public override string ToString()
        {
            string status = "Alive";
            if (!IsAlive)
            {
                status = "Dead";
            }
            return $"{name} - HP: {Health}/{BaseHealth}, AP: {Armor}/{BaseArmor}, Status: {status}";
        }
    }
}