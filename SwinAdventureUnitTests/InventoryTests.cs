using SwinAdventureCaseStudy;

namespace SwinAdventureUnitTests
{
    public class InventoryTests
    {

        private Inventory _inventory;
        private Item[] _inventoryItems;

        [SetUp]
        public void SetUp()
        {
            _inventory = new Inventory();
            _inventoryItems = [
                new Item(["Hammer"], "Hammer", "A kids hammer"),
                new Item(["Car", "Toy"], "Toy Car", "A kids toy car")
            ];
            foreach (Item item in _inventoryItems)
            {
                _inventory.Put(item);
            }
        }

        [TestCase("Hammer")]
        [TestCase("cAr")]
        [TestCase("toy")]
        public void TestItemIdentifiable(string id)
        {
            Assert.That(_inventory.HasItem(id));
        }


        [TestCase("Truck")]
        [TestCase("book")]
        [TestCase("HoUsE")]
        public void TestItemNotIdentifiable(string id)
        {
            Assert.That(_inventory.HasItem(id), Is.False);
        }

        [TestCase("Hammer")]
        [TestCase("cAr")]
        [TestCase("toy")]
        public void TestFetchItem(string id)
        {
            Item fetchedItem = _inventory.Fetch(id)!;
            Assert.Multiple(() =>
            {
                Assert.That(fetchedItem.AreYou(id));
                Assert.That(_inventory.HasItem(id));
            });
        }

        [TestCase("Hammer")]
        [TestCase("cAr")]
        [TestCase("toy")]
        public void TestTakeItem(string id)
        {
            Item fetchedItem = _inventory.Take(id)!;
            Assert.Multiple(() =>
            {
                Assert.That(fetchedItem.AreYou(id));
                Assert.That(_inventory.HasItem(id), Is.False);
            });
        }

        [Test]
        public void TestItemList()
        {
            string desc = _inventory.ItemList;
            string[] desc_lines = desc.Split("\r\n");
            Assert.Multiple(() =>
            {
                Assert.That(desc_lines.Length - 1, Is.EqualTo(_inventoryItems.Count()));
                for (int i = 0; i < _inventoryItems.Count(); i++)
                {
                    Assert.That(desc_lines[i], Is.EqualTo($"\t{_inventoryItems[i].ShortDescription}"));
                }
            });
        }


    }
}
