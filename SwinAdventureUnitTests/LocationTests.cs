using SwinAdventureCaseStudy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventureUnitTests
{
    internal class LocationTests
    {
        private Location _location;

        [SetUp]
        public void SetUp()
        {
            _location = new Location(["Hallway"], "Hallway", "This is a long well lit hallway.");
            _location.Inventory.Put(new Item(["shovel"], "shovel", "A miner's shovel"));
            _location.Inventory.Put(new Item(["sword"], "bronze sword", "an ancient sword, wielded by mighty kings"));
        }

        [Test]
        public void TestFullDescription()
        {
            Assert.That(_location.FullDescription, Is.EqualTo($"You are in the Hallway\nThis is a long well lit hallway.\nThere are exists to the south.\nIn this room you can see:\n{_location.Inventory.ItemList}"));
        }

        [TestCase("Hallway")]
        public void TestLocateItself(string id)
        {
            Assert.That(_location.Locate(id), Is.EqualTo(_location));
        }

        [TestCase("shovel")]
        [TestCase("sword")]
        public void TestLocateItemInRoom(string id)
        {
            Assert.That(_location.Locate(id), Is.Not.Null);
        }

        [TestCase("gem")]
        [TestCase("ruby")]
        public void TestLocateItemNotInRoom(string id)
        {
            Assert.That(_location.Locate(id), Is.Null);
        }
    }
}
