using System.Net.Http.Headers;

namespace SwinAdventureCaseStudy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Location startLocation = CreateEntryPoint();
            Player player = CreatePlayer(startLocation);

            FillPlayerInventory(player);

            StartGame(player);
        }

        private static void StartGame(Player player)
        {
            string? userCommandInput;
            CommandProcessor commandProcessor = new CommandProcessor();

            do
            {
                Console.Write("Command -> ");
                userCommandInput = Console.ReadLine();
                if(!string.IsNullOrEmpty(userCommandInput))
                {
                    Console.WriteLine(commandProcessor.Execute(player, userCommandInput.Split()));
                }

            } while (userCommandInput != "quit");
        }

        private static Location CreateEntryPoint()
        {
            Location startLocation = new Location(["Hallway"], "Hallway", "This is a long well lit hallway.");

            Bag wallet = new Bag(["wallet"], "wallet", "wornout leather wallet");
            wallet.Inventory.Put(new Item(["photo"], "Photo", "Torn photo of a young woman"));
            wallet.Inventory.Put(new Item(["cash", "money"], "$10 note", "dirty $10 note"));
            startLocation.Inventory.Put(wallet);
            startLocation.Inventory.Put(new Item(["pickaxe", "tool"], "Pickaxe", "iron pickaxe"));
            startLocation.Inventory.Put(new Item(["helmet"], "Safety Helmet", "yellow safety helmet to be used in the mines."));

            Location loc1 = new Location(["Closet"], "closet", "A small dark closet, with an odd smell.");
            Location loc2 = new Location(["Garden"], "small garden", "There are many small shrubs and flowers growing from well tended garden bed.");
            startLocation.Paths.Add(new Path(["North-East", "ne"], "North-East", "You go through a door.", loc1));
            startLocation.Paths.Add(new Path(["South-West", "sw"], "South-West", "You travel through a small door, and then crawl a few meters before arriving from the north.", loc2));


            return startLocation;
        }

        private static Player CreatePlayer(Location startLocation)
        {
            string? name, desc;

            do
            {
                Console.Write("Please insert your name: ");
                name = Console.ReadLine();
            } while (string.IsNullOrEmpty(name));
            do
            {

                Console.WriteLine("Briefly describe yourself:");
                desc = Console.ReadLine();
            } while (string.IsNullOrEmpty(desc));


            return new Player(name, desc, startLocation);

        }

        private static void FillPlayerInventory(Player player)
        {
            Item[] items = [
                    new Item(["Pencil"], "Pencil", "An H2 pencil for completing university assignments"),
                    new Item(["Laptop", "PC", "Computer"], "MSI", "A laptop to take notes and watch lectures"),
                    new Item(["Card", "Student card"], "Student card", "A student card to access university's facilities.")
                ];
            Bag bag = new Bag(["Wallet"], "Wallet", "A leather wallet to hold cards and cash");

            bag.Inventory.Put(items[2]);
            player.Inventory.Put(bag);
            player.Inventory.Put(items[0]);
            player.Inventory.Put(items[1]);
        }
    }
}