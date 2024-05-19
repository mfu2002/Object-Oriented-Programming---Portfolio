using ResearchSample.Composite;

namespace ResearchSample.Inheritance
{
    abstract public class Vehicle
    {
        public abstract string Make { get; }
        public abstract string Model { get; }
        public abstract int Year { get; }
        public abstract int NoOfDoors { get; }
        public abstract int NoOfWheels { get; }
        public abstract int HorsePower { get; }

        public override string ToString()
        {
            return $"Make: {Make}, Model: #{Model}, Year: {Year}, HorsePower: {HorsePower} Number of Doors: {NoOfDoors}, Number of Wheel: {NoOfWheels}";
        }

    }
}
