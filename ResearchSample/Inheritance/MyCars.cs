
namespace ResearchSample.Inheritance
{
    public class MyCars : Vehicle
    {
        public override string Make => "Ford";
        public override string Model => "Falcon XT";

        public override int Year => 2009;

        public override int NoOfDoors => 4;

        public override int NoOfWheels => 4;

        public override int HorsePower => 262;

    }
}
