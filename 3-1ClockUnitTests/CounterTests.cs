using _2_2CounterTask;

namespace _3_1ClockUnitTests
{
    internal class CounterTests
    {

        private Counter _counter;

        [SetUp]
        public void SetUp()
        {
            _counter = new Counter("Seconds");
        }
        [Test]
        public void TestInitialCount()
        {
            Assert.That(_counter.Ticks, Is.EqualTo(0));
        }
        [TestCase(1)]
        [TestCase(10)]
        [TestCase(50)]
        public void TestTick(int numberOfTicks)
        {
            for (int i = 0; i < numberOfTicks; i++)
            {
                _counter.Increment();
            }
            Assert.That(_counter.Ticks, Is.EqualTo(numberOfTicks));
        }

        [TestCase(1)]
        [TestCase(10)]
        [TestCase(50)]
        public void TestReset(int numberOfTicks)
        {
            for (int i = 0; i < numberOfTicks; i++)
            {
                _counter.Increment();
            }
            _counter.Reset();
            Assert.That(_counter.Ticks, Is.EqualTo(0));
        }
    }
}
