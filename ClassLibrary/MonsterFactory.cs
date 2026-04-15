using ClassLibrary;
using ClassLibrary.Monster_Subclasses;

namespace ClassLibrary
{
    public static class MonsterFactory
    {
        private static readonly Random Random = new Random();

        public static Monster CreateMonster() //create lvl 1 random monster
        {
            // Generate random moster type and its health and damage
            int monsterType = Random.Next(4);
            int health = Random.Next(80, 121);
            int damage = Random.Next(10, 16);

            switch (monsterType)
            {
                case 0:
                    return new Zombie("Zombie", health, damage);
                case 1:
                    return new Skeleton("Skeleton", health, damage);
                case 2:
                    return new Goblin("Goblin", health, damage);
                case 3:
                    return new Orc("Orc", health, damage);
                default:
                    throw new Exception("Unknown monster type");
            }
        }
        //Create a configuration JSON file solution or database solution later (switch sucks)
    }
}
