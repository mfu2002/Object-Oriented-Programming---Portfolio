namespace ResearchSample.Inheritance
{
    public class MyBike : Vehicle
    {
        public override string Make { get => "Honda"; }
        public override string Model { get => "CB125E"; }
        public override int Year { get => 2020; }
        public override int NoOfDoors { get => 0; }
        public override int NoOfWheels { get => 2; }
        public override int HorsePower { get => 10; }
    }
}
