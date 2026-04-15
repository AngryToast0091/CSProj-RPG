using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;
using ClassLibrary.Inventory_System;

namespace ClassLibrary.Monster_Subclasses
{
    internal class Zombie : Monster
    {
        #region Properties
        //----------------------------------- Properties -----------------------------------
        public LootPool LootPool { get; private set; } = new LootPool();

        #endregion

        #region Functions
        //----------------------------------- Functions -----------------------------------
        private void InitializeLootPool()
        {
            LootPool.AddLootItem(new LootItem("Rotten Flesh", 50));
            LootPool.AddLootItem(new LootItem("Rags", 35));
            LootPool.AddLootItem(new LootItem("Zombie heart", 15));
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
        public Zombie(string name, int maxHP, int dmg) : base(name, maxHP, dmg) => InitializeLootPool();

        public Zombie(string name, int maxHP, int dmg, int level) : base(name, maxHP, dmg, level) => InitializeLootPool();

        #endregion
    }
}
