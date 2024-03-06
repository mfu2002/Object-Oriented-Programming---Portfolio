namespace _3_1_Clock
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Clock clock = new Clock();
            for (int i = 0; i < 60; i++) { clock.Tick(); }
            Console.WriteLine(clock.Time);
        }
    }
}
