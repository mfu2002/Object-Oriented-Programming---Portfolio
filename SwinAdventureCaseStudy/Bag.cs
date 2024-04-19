namespace SwinAdventureCaseStudy
{
    public class Bag(IEnumerable<string> ids, string name, string desc) : Item(ids, name, desc), IHaveInventory
    {

        private Inventory _inventory = new Inventory();

        public Inventory Inventory => _inventory;

        public GameObject? Locate(string id)
        {
            if (AreYou(id)) { return this; }
            return _inventory.Fetch(id);
        }

        public override string FullDescription => $"In the {Name} you can see:\n{_inventory.ItemList}";

    }
}
