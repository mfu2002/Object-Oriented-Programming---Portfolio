using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventureCaseStudy
{
    public class Player : GameObject, IHaveInventory
    {
        private Inventory _inventory = new Inventory();

        public Player(string name, string desc) : base(["me", "inventory"], name, desc)
        {
        }

        public GameObject? Locate(string id)
        {
            if (AreYou(id)) { return this; }
            return _inventory.Fetch(id);
        }
        public override string FullDescription => $"You are {Name} {ShortDescription}.\nYou are carrying\n{_inventory.ItemList}";

        public Inventory Inventory => _inventory;
    }
}
