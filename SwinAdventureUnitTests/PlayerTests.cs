using SwinAdventureCaseStudy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SwinAdventureUnitTests
{

    public class PlayerTests
    {

        private Player _player;
        private Item[] _inventoryItems;

        [SetUp]
        public void SetUp()
        {
            Location startLocation = new Location(["Hallway"], "Hallway", "This is a long well lit hallway.");

            _player = new Player("Faisal", "amazing programmer", startLocation);
            _inventoryItems = [
                new Item(["Hammer"], "Hammer", "A kids hammer"),
                new Item(["Car", "Toy"], "Toy Car", "A kids toy car")
            ];
            foreach (Item item in _inventoryItems)
            {
                _player.Inventory.Put(item);
            }
        }

        [TestCase("Me")]
        [TestCase("inventory")]
        public void TestPlayerIdentifiable(string id)
        {
            Assert.That(_player.AreYou(id));
        }

        [TestCase("Hammer")]
        [TestCase("car")]
        [TestCase("toY")]
        public void TestLocatesInventoryItems(string id)
        {
            Assert.That(_player.Locate(id), Is.Not.Null);
        }

        [TestCase("Me")]
        [TestCase("inventory")]
        public void TestLocatesItself(string id)
        {
            Assert.That(_player.Locate(id), Is.Not.Null);
        }

        [TestCase("Gem")]
        [TestCase("money")]
        [TestCase("Kidney")]
        public void TestLocatesNothing(string id)
        {
            Assert.That(_player.Locate(id), Is.Null);
        }

        [Test]
        public void TestFullDescription()
        {
            Assert.That(_player.FullDescription, Is.EqualTo($"You are {_player.Name} the amazing programmer.\nYou are carrying\n{_player.Inventory.ItemList}"));
        }
    }
}
