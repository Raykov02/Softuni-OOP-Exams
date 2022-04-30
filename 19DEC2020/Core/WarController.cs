using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarCroft.Entities.Characters;
using WarCroft.Entities.Characters.Contracts;
using WarCroft.Entities.Items;

namespace WarCroft.Core
{
    public class WarController
    {
        private List<Character> party;
        private List<Item> itemsPool;
        public WarController()
        {
            party = new List<Character>();
            itemsPool = new List<Item>();
        }

        public string JoinParty(string[] args)
        {
            string characterType = args[0];
            string name = args[1];
            if (characterType != "Warrior" && characterType != "Priest")
            {
                throw new ArgumentException($"Invalid character type \"{characterType}\"!");
            }
            else if (characterType == "Warrior")
            {
                party.Add(new Warrior(name));
            }
            else
            {
                party.Add(new Priest(name));
            }
            return $"{name} joined the party!";
        }

        public string AddItemToPool(string[] args)
        {
            string itemName = args[0];
            if (itemName == "HealthPotion")
            {
                itemsPool.Add(new HealthPotion());
            }
            else if (itemName == "FirePotion")
            {
                itemsPool.Add(new FirePotion());
            }
            else
            {
                throw new ArgumentException($"Invalid item \"{ itemName }\"!");
            }
            return $"{itemName} added to pool.";
        }

        public string PickUpItem(string[] args)
        {
            string characterName = args[0];
            Character character = party.FirstOrDefault(x => x.Name == characterName);
            if(character == null)
            {
                throw new ArgumentException($"Character {characterName} not found!");
            }
            if(itemsPool.Count == 0)
            {
                throw new InvalidOperationException("No items left in pool!");
            }
            Item itemToGet = itemsPool[itemsPool.Count-1];
            character.Bag.AddItem(itemToGet);
            itemsPool.RemoveAt(itemsPool.Count-1);
            return $"{characterName} picked up {itemToGet.GetType().Name}!";
        }

        public string UseItem(string[] args)
        {
            string characterName = args[0];
            string itemName = args[1];
            Character character = party.FirstOrDefault(x => x.Name == characterName);
            if(character == null)
            {
                throw new ArgumentException($"Character {characterName} not found!");
            }
            Item item = character.Bag.GetItem(itemName);
            character.UseItem(item);
            return $"{character.Name} used {itemName}.";

        }

        public string GetStats()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var player in party.OrderByDescending(x => x.IsAlive).ThenByDescending(x => x.Health))
            {
                sb.AppendLine(player.ToString());
            }
            return sb.ToString().TrimEnd();
        }

        public string Attack(string[] args)
        {
            string attackerName = args[0];
            string receiverName = args[1];
            Character atacker = party.FirstOrDefault(x => x.Name == attackerName);
            if(atacker == null)
            {
                throw new ArgumentException($"Character {attackerName} not found!");
            }
            Character reciver = party.FirstOrDefault(x => x.Name == receiverName);
            if (reciver == null)
            {
                throw new ArgumentException($"Character {receiverName} not found!");
            }
            if (atacker is IAttacker)
            {
                Warrior warrior = atacker as Warrior;
                warrior.Attack(reciver);
                if(reciver.Health > 0)
                {
                    return $"{attackerName} attacks {receiverName} for {atacker.AbilityPoints} hit points! {receiverName} has " +
                    $"{reciver.Health}/{reciver.BaseHealth} HP and {reciver.Armor}/{reciver.BaseArmor} AP left!";
                }
                else
                {
                    return $"{attackerName} attacks {receiverName} for {atacker.AbilityPoints} hit points! {receiverName} has " +
                    $"{reciver.Health}/{reciver.BaseHealth} HP and {reciver.Armor}/{reciver.BaseArmor} AP left!" + Environment.NewLine + $"{reciver.Name} is dead!";
                }
               
            }
            else
            {
                throw new ArgumentException($"{atacker.Name} cannot attack!");
            }
        }

        public string Heal(string[] args)
        {
            string healerName = args[0];
            string healingReciverName = args[1];
            Character healer = party.FirstOrDefault(x => x.Name == healerName);
            if (healer == null)
            {
                throw new ArgumentException($"Character {healerName} not found!");
            }
            Character reciver = party.FirstOrDefault(x => x.Name == healingReciverName);
            if (reciver == null)
            {
                throw new ArgumentException($"Character {healingReciverName} not found!");
            }
            if (healer is IHealer)
            {
                Priest priest = healer as Priest;
                priest.Heal(reciver);
                return $"{healer.Name} heals {reciver.Name} for {healer.AbilityPoints}! {reciver.Name} has {reciver.Health} health now!";
            }
            else
            {
                throw new ArgumentException($"{healer.Name} cannot heal!");
            }
        }
    }
}
