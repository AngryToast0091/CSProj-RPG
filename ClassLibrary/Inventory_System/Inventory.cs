using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Inventory_System
{
    public class Inventory
    {
        #region Properties 
        //----------------------------------- Properties -----------------------------------
        private List<LootItem> items;
        public List<LootItem> Items { get { return items; } }

        #endregion

        #region Functions
        //----------------------------------- Functions -----------------------------------
        public void AddItem(LootItem newItem)
        {
            // Check if the item is already in the inventory
            var existingItem = items.Find(item => item.Name == newItem.Name);

            if (existingItem != null)
            {
                // If the item exists, increase its amount
                existingItem.Amount += newItem.Amount;
                Console.WriteLine($"Stacked {newItem.Amount} of {newItem.Name}. You now have {existingItem.Amount}.");
            }
            else
            {
                // Otherwise, add it as a new item
                items.Add(newItem);
                Console.WriteLine($"{newItem.Name} added to the inventory.");
            }
        }

        public (bool, int) RemoveItem(LootItem item, int amountToRemove)
        {
            var existingItem = items.Find(i => i.Name == item.Name);

            if (existingItem != null)
            {
                if (existingItem.Amount >= amountToRemove)
                {
                    existingItem.Amount -= amountToRemove;
                    Console.WriteLine($"Removed {amountToRemove} of {existingItem.Name}. Remaining: {existingItem.Amount}.");

                    if (existingItem.Amount == 0)
                    {
                        items.Remove(existingItem);
                    }

                    return (true, amountToRemove);
                }
                else
                {
                    Console.WriteLine($"Could not remove {amountToRemove} of {existingItem.Name}. You are trying to remove more than you have!");

                    return (false, 0);
                }
            }
            else
            {
                Console.WriteLine($"{item.Name} not found in inventory.");
                return (false, 0);
            }
        }

        public void DisplayInventory()
        {
            Console.WriteLine("Inventory contains:");
            foreach (var item in items)
            {
                Console.WriteLine($"- {item.Name} x{item.Amount}");
            }
        }

        #endregion

        #region Constructors
        //----------------------------------- Constructors -----------------------------------
        public Inventory() => items = new List<LootItem>();

        #endregion
    }
}
