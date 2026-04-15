using ClassLibrary.Inventory_System;
using ClassLibrary.Map_System;
using System.Text;

namespace ClassLibrary
{
    public class Hero : Character
    {
        #region Properties
        //----------------------------------- Properties -----------------------------------

        public int XP { get; private set; } = 0;
        private readonly int baseXPReq = 100; // used for level up XP requirement calculation

        public Inventory Inventory { get; private set; } = new Inventory();
        public int Money { get; set; } = 0;
        public Location CurrentLocation { get; private set; }

        #endregion

        #region Functions
        //----------------------------------- Functions -----------------------------------

        //----------------- Combat functions -----------------
        
        public bool Fight(Monster monster)  // Simulate a simple fight
        {
            bool result = false;
            while (IsAlive() && monster.IsAlive())
            {
                Attack(monster);
                if (monster.IsAlive())
                {
                    monster.Attack(this);
                }
            }

            // Check if hero survived and if yes, loot the monster for XP and item
            if (IsAlive())
            {
                LootEnemy(monster);
                result = true;
            }
            FullHeal();
            return result;
        }

        public void FullHeal() => CurrentHP = MaxHP;

        public void LootEnemy(Monster defeatedMonster)
        {
            GainXP(defeatedMonster);
            Loot(defeatedMonster.DropLoot());
        }

        //----------------- Leveling functions -----------------

        public int XPRequiredForNextLevel() // Calculation of XP that is required for a level up
        {
            return baseXPReq * Level * Level;
        }

        public void GainXP(int xp) // Character gains XP and levels up as many times as possible
        {
            XP += xp;
            TryLevelUp();
        }
        public void GainXP(Character defeatedEnemy)
        {
            XP += defeatedEnemy.Level * 50;
            TryLevelUp();
        }

        private void TryLevelUp() //XP subtraction, lvl increase + lvl up message
        {
            while (XP >= XPRequiredForNextLevel())
            {
                XP -= XPRequiredForNextLevel();
                Level++;
                Console.WriteLine($"{Name} has leveled up to level {Level}!"); 
            }
        }

        //----------------- Inventory functions -----------------
        public void Loot(LootItem item)
        {
            Inventory.AddItem(item);
        }

        //----------------- Navigation functions -----------------
        public void MoveToLocation(Location location)
        {
            if (CurrentLocation.ConnectedLocations.Contains(location))
            {
                CurrentLocation = location;
                Console.WriteLine("succesfully moved to: " + location.Name);
            }
        }

        #endregion

        #region Constructors
        //----------------------------------- Constructors -----------------------------------

        public Hero(string name, Location spawnpoint, int maxHP, int dmg, int level = 1) : base(name, maxHP, dmg, level)
        {
            CurrentLocation = spawnpoint;
        }



        #endregion
    }

    public class DHero : Hero
    {
        private ulong userID;

        public ulong GetID()
        {
            return userID;
        }

        // Function to generate CSV content for saving the hero
        public string GenerateHeroCsv()
        {
            StringBuilder csvBuilder = new StringBuilder();

            csvBuilder.AppendLine("Name,MaxHP,Damage,Level,UserID");
            csvBuilder.AppendLine($"{Name},{MaxHP},{Damage},{Level},{userID}");

            // Add inventory header and items (name, amount, weight)
            csvBuilder.AppendLine("InventoryItem,Amount,Weight");
            foreach (var item in Inventory.Items)
            {
                csvBuilder.AppendLine(item.ToString());
            }

            return csvBuilder.ToString();
        }

        public DHero(string name, Location spawnpoint, int hp, int dmg, ulong userID, int level = 1) : base(name, spawnpoint, hp, dmg, level) => this.userID = userID;
    }


}