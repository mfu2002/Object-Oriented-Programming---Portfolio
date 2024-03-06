using _3_1_Clock;

namespace _3_1ClockUnitTests
{
    public class ClockTests
    {
        private Clock _clock;
        [SetUp]
        public void Setup()
        {
            _clock = new Clock();
        }

        [Test]
        public void TestInitialTime()
        {
            Assert.That(_clock.Time, Is.EqualTo("00:00:00"));
        }
        [Test]
        public void TestNextSecond()
        {
            _clock.Tick();
            Assert.That(_clock.Time, Is.EqualTo("00:00:01"));
        }

        [Test]
        public void TestNextMinute()
        {
            for (int i = 0; i < 60; i++)
            {
            _clock.Tick();
            }
            Assert.That(_clock.Time, Is.EqualTo("00:01:00"));
        }
        [Test]
        public void TestNextHour()
        {
            for (int i = 0; i < 60 * 60; i++)
            {
                _clock.Tick();
            }
            Assert.That(_clock.Time, Is.EqualTo("01:00:00"));
        }
        [Test]
        public void TestNextDay()
        {
            for (int i = 0; i < 60 * 60 * 24; i++)
            {
                _clock.Tick();
            }
            Assert.That(_clock.Time, Is.EqualTo("00:00:00"));
        }
    }
}