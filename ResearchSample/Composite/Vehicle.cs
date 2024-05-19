namespace ResearchSample.Composite
{
    public class Vehicle(string make, string model, int year, Engine engine, Door[] doors, Wheel[] wheels)
    {
        public string Make { get; } = make;
        public string Model { get; } = model;
        public int Year { get; } = year;

        public Door[] Doors { get; } = doors;

        public Wheel[] Wheels { get; } = wheels;

        public Engine Engine { get; } = engine;


        public override string ToString()
        {
            return $"Make: {Make}, Model: #{Model}, Year: {Year}, HorsePower: {Engine.HorsePower} Number of Doors: {Doors.Length}, Number of Wheel: {Wheels.Length}";
        }

    }
}
