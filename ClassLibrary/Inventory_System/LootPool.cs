using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Inventory_System
{
    internal class LootPool
    {
        #region Properties
        //----------------------------------- Properties -----------------------------------
        private List<LootItem> lootItems = new List<LootItem>();
        private Random random = new Random();
        private int weightIncreaseAmount = 1; // bad luck protection

        #endregion

        #region Functions
        //----------------------------------- Functions -----------------------------------
        public void AddLootItem(LootItem item)
        {
            lootItems.Add(item);
        }

        public LootItem GetRandomLoot()
        {
            // Calculate total weight of all items
            int totalWeight = lootItems.Sum(item => item.CurrentWeight);

            int roll = random.Next(0, totalWeight);
            // Iterate through loot items and select the one based on the random roll
            int cumulativeWeight = 0;
            foreach (LootItem item in lootItems)
            {
                cumulativeWeight += item.CurrentWeight;

                if (roll < cumulativeWeight)
                {
                    item.ResetWeight(); // Reset weight of the dropped item
                    return item;        // Return dropped item
                }
            }
            return new LootItem("Goop");
        }

        public void ApplyBadLuckProtection(LootItem droppedItem)
        {
            foreach (LootItem item in lootItems)
            {
                // Increase the weight of all items except the one that was dropped
                if (item != droppedItem)
                {
                    item.IncreaseWeight(weightIncreaseAmount);
                }
            }
        }

        #endregion

        #region Constructors
        //----------------------------------- Constructors -----------------------------------

        #endregion
    }
}
