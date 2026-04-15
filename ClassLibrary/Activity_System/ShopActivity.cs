using ClassLibrary;
using ClassLibrary.Inventory_System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Activity_System
{
    public class ShopActivity : IActivity
    {
        public string Name => "Visit Shop";
        private readonly List<LootItem> _inventory;

        public ShopActivity(List<LootItem> inventory)
        {
            _inventory = inventory;
        }

        public ShopActivity()
        {
            // Example stock (you can tweak as needed)
            _inventory =
            [
            new LootItem("Health Potion", defaultWeight: 10, amount: 1),
            new LootItem("Mana Potion", defaultWeight: 12, amount: 1),
            new LootItem("Iron Sword", defaultWeight: 25, amount: 1),
            new LootItem("Leather Armor", defaultWeight: 30, amount: 1)
            ];
        }

        public bool CanExecute(Character character) => character is Hero;

        public void Execute(Character character)
        {
            if (character is not Hero hero)
            {
                Console.WriteLine("Only heroes can visit shops.");
                return;
            }

            Console.WriteLine($"Welcome to the shop, {hero.Name}!");
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n1. Buy item\n2. Sell item\n0. Leave");
                Console.Write("Choose an option: ");
                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        BuyItem(hero);
                        break;
                    case "2":
                        SellItem(hero);
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        private void BuyItem(Hero hero)
        {
            Console.WriteLine("\nItems for sale:");
            for (int i = 0; i < _inventory.Count; i++)
            {
                var item = _inventory[i];
                int price = item.DefaultWeight * 2;
                Console.WriteLine($"{i + 1}. {item.Name} - {price} gold");
            }

            Console.Write("Select item number or 0 to cancel: ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= _inventory.Count)
            {
                var item = _inventory[choice - 1];
                int price = item.DefaultWeight * 2;
                if (hero.Money >= price)
                {
                    hero.Money -= price;
                    hero.Loot(new LootItem(item.Name, item.DefaultWeight, 1));
                    Console.WriteLine($"You bought {item.Name} for {price} gold!");
                }
                else Console.WriteLine("Not enough gold!");
            }
        }


        private void SellItem(Hero hero)
        {
            if (hero.Inventory.Items.Count == 0)
            {
                Console.WriteLine("You have nothing to sell.");
                return;
            }

            Console.WriteLine("\nYour inventory:");
            for (int i = 0; i < hero.Inventory.Items.Count; i++)
            {
                var item = hero.Inventory.Items[i];
                int sellPriceEach = 1000 / item.DefaultWeight; // formula to calculate sell price
                Console.WriteLine($"{i + 1}. {item.Name} x{item.Amount} - Sell price: {sellPriceEach} gold each");
            }

            Console.Write("Select item to sell or 0 to cancel: ");
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice <= 0 || choice > hero.Inventory.Items.Count)
                return;

            var selectedItem = hero.Inventory.Items[choice - 1];

            Console.Write($"Sell how many (max {selectedItem.Amount}): ");
            if (!int.TryParse(Console.ReadLine(), out int amountToSell) || amountToSell <= 0 || amountToSell > selectedItem.Amount)
            {
                Console.WriteLine("Invalid amount.");
                return;
            }

            // Transaction logic moved here instead of Hero
            var (success, amountRemoved) = hero.Inventory.RemoveItem(selectedItem, amountToSell);

            if (success)
            {
                int moneyEarned = amountRemoved * (1000 / selectedItem.DefaultWeight);
                hero.Money += moneyEarned;

                Console.WriteLine($"{hero.Name} sold {amountRemoved} {selectedItem.Name}(s) for {moneyEarned} gold. Total Gold: {hero.Money}");
            }
            else
            {
                Console.WriteLine($"{selectedItem.Name} could not be sold.");
            }
        }


    }

}
