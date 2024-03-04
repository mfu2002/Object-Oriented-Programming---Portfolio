using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventureCaseStudy
{
    public class Location : GameObject, IHaveInventory
    {

        private Inventory _inventory = new Inventory();

        public Inventory Inventory => _inventory;

        public Location(IEnumerable<string> ids, string name, string desc) : base(ids, name, desc)
        {

        }

        public override string FullDescription
        {
            get
            {
                string desc =  $"You are in the {Name}\n{base.FullDescription}\nIn this room you can see:\n{Inventory.ItemList}";
                // Implement Paths
                return desc;
            }
        }

        public GameObject? Locate(string id)
        {
            if (AreYou(id)) { return this; }
            return _inventory.Fetch(id);
        }
    }
}
