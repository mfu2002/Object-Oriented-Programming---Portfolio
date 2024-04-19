namespace SwinAdventureCaseStudy
{
    public class Player : GameObject, IHaveInventory
    {
        private Inventory _inventory = new Inventory();
        public Location Location { get; set; }

        public Player(string name, string desc, Location location) : base(["me", "inventory"], name, desc)
        {
            Location = location;
        }

        public GameObject? Locate(string id)
        {
            if (AreYou(id)) { return this; }
            GameObject? inventoryItem = _inventory.Fetch(id);
            if (inventoryItem != null) { return inventoryItem; }
            return Location.Locate(id);
        }
        public override string FullDescription => $"You are {Name} the {base.FullDescription}.\nYou are carrying\n{_inventory.ItemList}";

        public Inventory Inventory => _inventory;
    }
}
