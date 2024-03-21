namespace SwinAdventureCaseStudy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = CreatePlayer();

            FillPlayerInventory(player);

            StartGame(player);
        }

        private static void StartGame(Player player)
        {
            string? userCommandInput;
            LookCommand lookCommand = new LookCommand();

            do
            {
                Console.Write("Command -> ");
                userCommandInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(userCommandInput))
                {
                    Console.WriteLine(lookCommand.Execute(player, userCommandInput.Split()));
                }

            } while (userCommandInput != "quit");
        }


        private static Player CreatePlayer()
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


            return new Player(name, desc);

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