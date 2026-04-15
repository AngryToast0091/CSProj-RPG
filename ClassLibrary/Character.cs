using ClassLibrary.Inventory_System;

namespace ClassLibrary
{
    public abstract class Character
    {
        #region Properties
        //----------------------------------- Properties -----------------------------------
        public string Name { get; protected set; }
        public int MaxHP { get; protected set; }
        public int CurrentHP { get; protected set; }
        public int Damage { get; protected set; }
        public int Level { get; protected set; } = 1;

        #endregion

        #region Functions
        //----------------------------------- Functions -----------------------------------

        //----------------- Combat system functions -----------------

        public void Attack(Character opponent)
        {
            Console.WriteLine($"{Name} attacks {opponent.Name} for {Damage} damage!");
            opponent.TakeDamage(Damage);
        }

        public void TakeDamage(int damage)
        {
            CurrentHP -= damage;
            Console.WriteLine($"{Name} takes {damage} damage and now has {CurrentHP} HP.");
        }

        public bool IsAlive()
        {
            return CurrentHP > 0;
        }

        #endregion

        #region Constructors
        //----------------------------------- Constructors -----------------------------------
        public Character(string name, int maxHP, int dmg, int level = 1)
        {
            Name = name;
            MaxHP = maxHP;
            CurrentHP = maxHP;
            Damage = dmg;
            Level = level;
        }
        #endregion
    }

    public abstract class Monster : Character
    {
        public abstract LootItem DropLoot();

        protected Monster(string name, int maxHP, int dmg, int level = 1) : base(name, maxHP, dmg, level)
        {
        }
    }
}
