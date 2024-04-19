using _2_2CounterTask;

namespace _3_1_Clock
{
    public class Clock
    {
        private readonly Counter _seconds = new Counter("Seconds");
        private readonly Counter _minutes = new Counter("Minutes");
        private readonly Counter _hours = new Counter("Hours");

        public Clock()
        {
        }

        public void Tick()
        {
            _seconds.Increment();

            if (_seconds.Ticks == 60)
            {
                _minutes.Increment();
                _seconds.Reset();
            }

            if (_minutes.Ticks == 60)
            {
                _hours.Increment();
                _minutes.Reset();
            }

            if (_hours.Ticks == 24)
            {
                _hours.Reset();
            }
        }
        public void Reset()
        {
            _seconds.Reset();
            _minutes.Reset();
            _hours.Reset();
        }

        public string Time => $"{_hours.Ticks:00}:{_minutes.Ticks:00}:{_seconds.Ticks:00}";
    }
}
