using SwinAdventureCaseStudy;
using Path = SwinAdventureCaseStudy.Path;

namespace SwinAdventureUnitTests
{
    public class MoveCommandTests
    {
        private MoveCommand _command;
        private Player _player;

        [SetUp]
        public void SetUp()
        {
            _command = new MoveCommand();

            Location startLocation = new Location(["Hallway"], "Hallway", "This is a long well lit hallway.");
            _player = new Player("Faisal", "amazing programmer", startLocation);
            Location loc1 = new Location(["Closet"], "closet", "A small dark closet, with an odd smell.");
            Location loc2 = new Location(["Garden"], "small garden", "There are many small shrubs and flowers growing from well tended garden bed.");
            startLocation.Paths.Add(new Path(["North-East", "ne"], "North-East", "You go through a door.", loc1));
            startLocation.Paths.Add(new Path(["South-West", "sw"], "South-West", "You travel through a small door, and then crawl a few meters before arriving from the north.", loc2));

        }

        [TestCase("North-East")]
        [TestCase("North-east")]
        [TestCase("ne")]
        [TestCase("south-west")]
        [TestCase("sw")]
        public void TestMoveToPath(string direction)
        {
            Path nextPath = _player.Location.LocatePath(direction)!;
            string movePlayer = _command.Execute(_player, ["move", direction]);
            Assert.Multiple(() =>
            {
                Assert.That(movePlayer, Is.EqualTo($"You head {nextPath.Name}\n{nextPath.FullDescription}"));
                Assert.That(_player.Location, Is.EqualTo(nextPath.Destination));
            });
        }

        [TestCase("North")]
        [TestCase("East")]
        [TestCase("South")]
        [TestCase("Up")]
        public void TestMoveToNoPath(string direction)
        {
            Location prevLocation = _player.Location;
            string movePlayer = _command.Execute(_player, ["move", direction]);
            Assert.Multiple(() =>
            {
                Assert.That(movePlayer, Is.EqualTo("No path that way"));
                Assert.That(_player.Location, Is.EqualTo(prevLocation));
            });
        }

        [TestCase("Move")]
        [TestCase("Move North then east")]
        [TestCase("")]
        public void TestInvalidCommandLength(string commandStr)
        {
            Location prevLocation = _player.Location;
            string movePlayer = _command.Execute(_player, commandStr.Split());
            Assert.Multiple(() =>
            {
                Assert.That(movePlayer, Is.EqualTo("I don't know how to move like that"));
                Assert.That(_player.Location, Is.EqualTo(prevLocation));
            });
        }

        [TestCase("go")]
        [TestCase("head")]
        [TestCase("leave")]
        [TestCase("Move")]
        public void TestValidCommandStart(string commandStr)
        {
            string direction = "ne";
            Path nextPath = _player.Location.LocatePath(direction)!;
            string movePlayer = _command.Execute(_player, [commandStr, direction]);
            Assert.Multiple(() =>
            {
                Assert.That(movePlayer, Is.EqualTo($"You head {nextPath.Name}\n{nextPath.FullDescription}"));
                Assert.That(_player.Location, Is.EqualTo(nextPath.Destination));
            });
        }

        [TestCase("look")]
        [TestCase("hear")]
        [TestCase("taste")]
        [TestCase("smell")]
        public void TestInvalidCommandStart(string commandStr)
        {
            string direction = "ne";
            Location prevLocation = _player.Location;
            string movePlayer = _command.Execute(_player, [commandStr, direction]);
            Assert.Multiple(() =>
            {
                Assert.That(movePlayer, Is.EqualTo("Error in move input"));
                Assert.That(_player.Location, Is.EqualTo(prevLocation));
            });

        }

    }
}
