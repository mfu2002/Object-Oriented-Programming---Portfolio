using SwinAdventureCaseStudy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventureUnitTests
{
    internal class BagTests
    {
        private Bag _bag;


        [SetUp]
        public void SetUp()
        {
            _bag = new Bag(["bag 1"], "Backpack", "A school backpack");
            Item[] _inventoryItems = [
                new Item(["Hammer"], "Hammer", "A kids hammer"),
                new Item(["Car", "Toy"], "Toy Car", "A kids toy car")
            ];
            foreach (Item item in _inventoryItems)
            {
                _bag.Inventory.Put(item); 
            }
        }

        [TestCase("hammer")]
        [TestCase("cAr")]
        [TestCase("toY")]
        public void TestLocatesItems(string id)
        {
            GameObject? item = _bag.Locate(id);
            Assert.That(item, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(item.AreYou(id));
                Assert.That(_bag.Inventory.HasItem(id));
            });
        }

        [TestCase("Bag 1")]
        public void TestLocatesItself(string id)
        {
            GameObject? item = _bag.Locate(id);
            Assert.That(item, Is.Not.Null);
            Assert.That(item, Is.EqualTo(_bag));
        }


        [TestCase("Bag 2")]
        [TestCase("books")]
        [TestCase("laPTop")]
        public void TestLocatesNothing(string id)
        {
            Assert.That(_bag.Locate(id), Is.Null);
        }

        [Test]
        public void TestFullDescription()
        {
            Assert.That(_bag.FullDescription, Is.EqualTo($"In the {_bag.Name} you can see:\n{_bag.Inventory.ItemList}"));
        }

        [Test]
        public void TestBagInBag()
        {
            Bag bag2 = new Bag(["bag 2"], "Purse", "A pink purse");
            bag2.Inventory.Put(new Item(["Lipstick"], "Lipstick", "a red lipstick"));

            _bag.Inventory.Put(bag2);

            Assert.Multiple(() =>
            {
                GameObject? locatedBag = _bag.Locate("bag 2");
                Assert.That(locatedBag, Is.Not.Null);
                Assert.That(locatedBag, Is.EqualTo(bag2));
                
                Assert.That(_bag.Locate("hammer"), Is.Not.Null);

                Assert.That(_bag.Locate("Lipstick"), Is.Null);
            });

        }

    }
}
