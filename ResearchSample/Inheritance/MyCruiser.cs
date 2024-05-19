namespace ResearchSample.Inheritance
{
    public class MyCruiser : Vehicle
    {
        public override string Make => "Toyota";

        public override string Model => "FJ Cruiser";

        public override int Year => 2015;

        public override int NoOfDoors => 4;

        public override int NoOfWheels => 4;

        public override int HorsePower => 268;
    }
}
