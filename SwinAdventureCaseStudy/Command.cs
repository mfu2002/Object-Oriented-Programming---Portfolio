namespace SwinAdventureCaseStudy
{
    public abstract class Command(IEnumerable<string> idents) : IdentifiableObject(idents)
    {
        public abstract string Execute(Player p, string[] text);

    }
}
