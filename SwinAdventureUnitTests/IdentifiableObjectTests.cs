using SwinAdventureCaseStudy;

namespace SwinAdventureUnitTests
{
    public class IdentifiableObjectTests
    {

        [TestCase("fred")]
        [TestCase("bob")]
        public void TestAreYou(string testId)
        {
            IdentifiableObject identifiableObject = new IdentifiableObject(["fred", "bob"]);

            Assert.IsTrue(identifiableObject.AreYou(testId));
        }

        [TestCase("id1")]
        [TestCase("")]
        [TestCase("id3")]
        public void TestNotAreYou(string testId)
        {
            IdentifiableObject identifiableObject = new IdentifiableObject(["Fred", "Bob"]);

            Assert.IsFalse(identifiableObject.AreYou(testId));
        }

        [TestCase("Fred")]
        [TestCase("fred")]
        [TestCase("freD")]
        [TestCase("Bob")]
        [TestCase("bob")]
        public void TestCaseSensitiveAreYou(string testId)
        {
            IdentifiableObject identifiableObject = new IdentifiableObject(["Fred", "Bob"]);

            Assert.IsTrue(identifiableObject.AreYou(testId));
        }

        [Test]
        public void TestFirstId()
        {
            IdentifiableObject identifiableObject = new IdentifiableObject(["Fred", "Bob"]);
            Assert.That(identifiableObject.FirstId, Is.EqualTo("Fred"));
        }
        [Test]
        public void TestFirstIdWithNoIds()
        {
            IdentifiableObject identifiableObject = new IdentifiableObject([]);
            Assert.That(identifiableObject.FirstId, Is.EqualTo(""));
        }

        [TestCase("James")]
        [TestCase("wiLL")]
        public void TestAddId(string id)
        {
            IdentifiableObject identifiableObject = new IdentifiableObject(["Fred", "Bob"]);
            identifiableObject.AddIdentifier(id);

            Assert.IsTrue(identifiableObject.AreYou(id));
        }
    }
}