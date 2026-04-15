using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;
using ClassLibrary.Inventory_System;

namespace ClassLibrary.Monster_Subclasses
{
    internal class Orc : Monster
    {
        #region Properties
        //----------------------------------- Properties -----------------------------------
        public LootPool LootPool { get; private set; } = new LootPool();

        #endregion

        #region Functions
        //----------------------------------- Functions -----------------------------------
        private void InitializeLootPool()
        {
            LootPool.AddLootItem(new LootItem("Orc Tooth", 50));
            LootPool.AddLootItem(new LootItem("Orc Axe", 35));
            LootPool.AddLootItem(new LootItem("Orc Shield", 15));
        }

        public override LootItem DropLoot()
        {
            LootItem droppedItem = LootPool.GetRandomLoot();
            LootPool.ApplyBadLuckProtection(droppedItem);
            return droppedItem;
        }

        #endregion

        #region Constructors
        //----------------------------------- Constructors-----------------------------------
        public Orc(string name, int maxHP, int dmg) : base(name, maxHP, dmg) => InitializeLootPool();

        public Orc(string name, int maxHP, int dmg, int level) : base(name, maxHP, dmg, level) => InitializeLootPool();

        #endregion
    }
}
