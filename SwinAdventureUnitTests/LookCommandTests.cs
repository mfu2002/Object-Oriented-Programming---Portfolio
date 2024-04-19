using SwinAdventureCaseStudy;

namespace SwinAdventureUnitTests
{
    public class LookCommandTests
    {
        private LookCommand _command;
        private Player _player;

        [SetUp]
        public void SetUp()
        {
            _command = new LookCommand();
            Location startLocation = new Location(["Hallway"], "Hallway", "This is a long well lit hallway.");

            Bag wallet = new Bag(["wallet"], "wallet", "wornout leather wallet");
            wallet.Inventory.Put(new Item(["photo"], "Photo", "Torn photo of a young woman"));
            wallet.Inventory.Put(new Item(["cash", "money"], "$10 note", "dirty $10 note"));
            startLocation.Inventory.Put(wallet);
            startLocation.Inventory.Put(new Item(["pickaxe", "tool"], "Pickaxe", "iron pickaxe"));
            startLocation.Inventory.Put(new Item(["helmet"], "Safety Helmet", "yellow safety helmet to be used in the mines."));
            _player = new Player("Faisal", "the amazing programmer", startLocation);
            _player.Inventory.Put(new Item(["gem", "ruby"], "ruby", "A big, red ruby"));
            _player.Inventory.Put(new Item(["diamond"], "diamond", "A big, shiny diamond"));

            Bag bag = new Bag(["bag"], "sack", "A miner's sack");
            bag.Inventory.Put(new Item(["book"], "Mining Economics Explained", "A book on mining"));
            _player.Inventory.Put(bag);

        }
        [Test]
        public void TestLook()
        {
            Assert.That(_command.Execute(_player, ["look"]), Is.EqualTo(_player.Location.FullDescription));
        }

        [Test]
        public void TestLookAtMe()
        {
            string[] command = "Look at me".Split();
            Assert.That(_command.Execute(_player, command), Is.EqualTo(_player.FullDescription));
        }

        [TestCase("gem")]
        [TestCase("ruBy")]
        [TestCase("diamond")]
        public void TestLookAtItem(string item)
        {
            string[] command = $"Look at {item}".Split();
            Assert.That(_command.Execute(_player, command), Is.EqualTo(_player.Inventory.Fetch(item)!.FullDescription));
        }

        [TestCase("Saphire")]
        [TestCase("gloves")]
        [TestCase("torch")]
        public void TestLookAtUnknown(string item)
        {
            string[] command = $"Look at {item}".Split();
            Assert.That(_command.Execute(_player, command), Is.EqualTo($"I can't find the {item}"));
        }

        [TestCase("gem")]
        [TestCase("ruBy")]
        [TestCase("diamond")]
        public void TestLookAtItemInMe(string item)
        {
            string[] command = $"Look at {item} in me".Split();
            Assert.That(_command.Execute(_player, command), Is.EqualTo(_player.Inventory.Fetch(item)!.FullDescription));
        }

        [TestCase("book")]
        public void TestLookAtItemInBag(string item)
        {
            string[] command = $"Look at {item} in bag".Split();
            Bag? bag = _player.Inventory.Fetch("bag") as Bag;
            Assert.That(_command.Execute(_player, command), Is.EqualTo(bag!.Inventory.Fetch(item)!.FullDescription));
        }

        [Test]
        public void TestLookAtItemInNoBag()
        {
            string[] command = "Look at book in bag1".Split();
            Assert.That(_command.Execute(_player, command), Is.EqualTo("I can't find the bag1"));
        }

        [TestCase("gem")]
        [TestCase("diamond")]
        [TestCase("ruby")]
        public void TestLookAtNoItemInBag(string item)
        {
            string[] command = $"Look at {item} in bag".Split();
            Assert.That(_command.Execute(_player, command), Is.EqualTo($"I can't find the {item} in the sack")); // TODO: check with changed requirements. 
        }

        [TestCase("pickaxe")]
        [TestCase("tooL")]
        [TestCase("heLmet")]
        public void TestLookAtItemInRoom(string item)
        {
            string[] command = $"Look at {item}".Split();
            Assert.That(_command.Execute(_player, command), Is.EqualTo(_player.Location.Inventory.Fetch(item)!.FullDescription));
        }

        [TestCase("photo")]
        [TestCase("cash")]
        public void TestLookAtItemInBagInRoom(string item)
        {
            string[] command = $"Look at {item} in wallet".Split();
            Bag? bag = _player.Location.Inventory.Fetch("wallet") as Bag;
            Assert.That(_command.Execute(_player, command), Is.EqualTo(bag!.Inventory.Fetch(item)!.FullDescription));
        }

        [TestCase("card")]
        [TestCase("note")]
        [TestCase("keys")]
        public void TestLookAtNoItemInBagInRoom(string item)
        {
            string[] command = $"Look at {item} in wallet".Split();
            Assert.That(_command.Execute(_player, command), Is.EqualTo($"I can't find the {item} in the wallet")); // TODO: check with changed requirements.
        }

        [TestCase("Look around")]
        [TestCase("Look here and there")]
        [TestCase("Look here, there, everywhere, but nowhere")]
        [TestCase("")]
        public void TestInvalidInputLength(string command)
        {
            Assert.That(_command.Execute(_player, command.Split()), Is.EqualTo("I don't know how to look like that"));
        }

        [TestCase("wave at me")]
        [TestCase("throw at gem in room")]
        [TestCase("kick at ball in bag")]
        [TestCase("flick at mosquite in purse")]
        public void TestInvalidFirstWord(string command)
        {
            Assert.That(_command.Execute(_player, command.Split()), Is.EqualTo("Error in look input"));
        }
        [TestCase("look at gem under room")]
        [TestCase("look at ball beside bag")]
        [TestCase("look at mosquite on purse")]
        public void TestInvalidFourthWord(string command)
        {
            Assert.That(_command.Execute(_player, command.Split()), Is.EqualTo("What do you want to look in?"));
        }
    }
}
