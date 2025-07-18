﻿using SwinAdventureCaseStudy;
using Path = SwinAdventureCaseStudy.Path;

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
            Location nextLoc = new Location(["room"], "small room", "a mouldy old room");
            _location.Paths.Add(new Path(["south", "s"], "south", "You go through a small door", nextLoc));
            Assert.That(_location.FullDescription, Is.EqualTo($"You are in the Hallway\nThis is a long well lit hallway.\nThere are exits to the south.\nIn this room you can see:\n{_location.Inventory.ItemList}"));
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
