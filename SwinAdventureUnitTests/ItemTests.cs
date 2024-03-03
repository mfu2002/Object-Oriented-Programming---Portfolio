using SwinAdventureCaseStudy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventureUnitTests
{
    public class ItemTests
    {
        private Item _item;
        private const string fullDesc = "A 18\" MSI Laptop";
        [SetUp]
        public void SetUp()
        {
            _item = new Item(["Laptop", "PC", "Computer"], "Computer", fullDesc);
        }

        [TestCase("Laptop")]
        [TestCase("pc")]
        [TestCase("computer")]
        public void ItemIsIdentifiable(string id)
        {
            Assert.That(_item.AreYou(id));
        }

        [Test]
        public void TestShortDescription()
        {
            Assert.That(_item.ShortDescription, Is.EqualTo("a Computer (Laptop)"));
        }

        [Test]
        public void TestFullDescription()
        {
            Assert.That(_item.FullDescription, Is.EqualTo(fullDesc));
        }

    }
}
