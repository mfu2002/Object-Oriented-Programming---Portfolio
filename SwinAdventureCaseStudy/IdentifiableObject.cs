namespace SwinAdventureCaseStudy
{
    public class IdentifiableObject
    {
        List<string> _identifiers = new List<string>();

        public string FirstId
        {
            get
            {
                if (_identifiers.Count == 0)
                    return "";
                {
                    return _identifiers.First();
                }
            }
        }

        public IdentifiableObject(IEnumerable<string> idents)
        {

            _identifiers.AddRange(idents);
        }

        public bool AreYou(string id)
        {
            foreach (string _id in _identifiers)
            {
                if (_id.ToLower() == id.ToLower()) return true;
            }
            return false;
        }

        public void AddIdentifier(string id)
        {
            _identifiers.Add(id);
        }

    }
}
