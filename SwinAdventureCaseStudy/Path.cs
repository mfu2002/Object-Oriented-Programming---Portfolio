namespace SwinAdventureCaseStudy
{
    public class Path : GameObject
    {
        public Location Destination { get; }
        public Path(IEnumerable<string> idents, string name, string desc, Location destination) : base(idents, name, desc)
        {
            Destination = destination;
        }

        public override string FullDescription => $"{base.FullDescription}\nYou have arrived in a {Destination.Name}";

        public override string ToString()
        {
            return ShortDescription;
        }
    }
}
