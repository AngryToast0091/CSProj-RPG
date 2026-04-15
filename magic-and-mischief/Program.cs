using ClassLibrary;
using ClassLibrary.Activity_System;
using ClassLibrary.Map_System;
using System;

Console.WriteLine("starting");

Location startLoc = new Location("Starting town", "A basic town", 10, 10);
startLoc.AddActivity(new ShopActivity());
Location endLoc = new Location("Second town", "A different town", 35, 15);
startLoc.ConnectTo(endLoc);

Hero hero = new Hero("Mira Hinata", startLoc, 120, 20);


bool again = true;
string action;

Console.WriteLine("leave -> end the game");
Console.WriteLine("fight -> generate a random enemy and fight them");
Console.WriteLine("inventory -> check what is inside your inventory");
Console.WriteLine("shop -> open shop and buy/sell items (use any commands above to continue your journey)");

while (again)
{
    Console.WriteLine("----------------------------------------------------------");
    Console.WriteLine("What now?");
    action = Console.ReadLine();

	switch (action)
	{
		case "fight":
			Monster monster = MonsterFactory.CreateMonster();
			hero.Fight(monster);
			break;
		case "inventory":
			hero.Inventory.DisplayInventory();
			break;
		case "shop":
			if (hero.CurrentLocation.HasActivity<ShopActivity>())
			{
                hero.CurrentLocation.GetActivity<ShopActivity>()
                                    .Execute(hero);
            }
			break;

        case "leave":
			again = false;
			break;

		default:
            Console.WriteLine("incorrect command");
            break;
	}

}

Console.WriteLine("ended");