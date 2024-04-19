using SwinAdventureCaseStudy;
using Path = SwinAdventureCaseStudy.Path;


namespace SwinAdventureUnitTests
{
    public class PathTests
    {
        private Path _path;
        [SetUp]
        public void SetUp()
        {
            Location destination = new Location(["room"], "small room", "A small dark room, with an odd smell");
            _path = new Path(["North-East", "NE"], "North-East", "You go through a door.", destination);
        }

        [Test]
        public void TestFullDescription()
        {
            Assert.That(_path.FullDescription, Is.EqualTo($"You go through a door.\nYou have arrived in a small room"));
        }
    }
}
