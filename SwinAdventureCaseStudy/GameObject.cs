namespace SwinAdventureCaseStudy
{
    public abstract class GameObject : IdentifiableObject
    {
        private string _descripiton;
        private string _name;
        public string Name { get { return _name; } }

        protected GameObject(IEnumerable<string> ids, string name, string desc) : base(ids)
        {
            _name = name;
            _descripiton = desc;
        }
        public string ShortDescription
        {
            get
            {
                return $"a {Name} ({FirstId})";
            }
        }

        public virtual string FullDescription => _descripiton;

    }
}
