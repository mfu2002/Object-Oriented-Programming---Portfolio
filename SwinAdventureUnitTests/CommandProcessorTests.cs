using SwinAdventureCaseStudy;

namespace SwinAdventureUnitTests
{
    public class CommandProcessorTests
    {
        private CommandProcessor _commandProcessor;
        private Player _player;

        [SetUp]
        public void SetUp()
        {
            _commandProcessor = new CommandProcessor();
            Location startLocation = new Location(["Hallway"], "Hallway", "This is a long well lit hallway.");
            _player = new Player("Faisal", "amazing programmer", startLocation);
        }


        [TestCase("Move")]
        [TestCase("go")]
        [TestCase("head")]
        [TestCase("Leave")]
        [TestCase("LEAVE")]
        [TestCase("MOVE")]
        public void TestFindMoveCommand(string command)
        {
            Command? processor = _commandProcessor.FindCommand(command);
            Assert.That(processor, Is.InstanceOf(typeof(MoveCommand)));

        }

        [TestCase("look")]
        [TestCase("LOOK")]
        public void TestFindLookCommand(string command)
        {
            Command? processor = _commandProcessor.FindCommand(command);
            Assert.That(processor, Is.InstanceOf(typeof(LookCommand)));
        }

        [TestCase("Test")]
        [TestCase("Hello")]
        [TestCase("Sit")]
        [TestCase("Levitate")]
        public void TestFindInvalidCommand(string command)
        {
            Command? processor = _commandProcessor.FindCommand(command);
            Assert.That(processor, Is.Null);
        }

        [TestCase("Test")]
        [TestCase("Hello")]
        [TestCase("Sit")]
        [TestCase("Levitate")]
        public void TestExecuteInvalidCommand(string command)
        {
            string[] text = command.Split();
            string result = _commandProcessor.Execute(_player, text);
            Assert.That(result, Is.EqualTo($"I don't understand ${text[0]}"));
        }

    }
}
