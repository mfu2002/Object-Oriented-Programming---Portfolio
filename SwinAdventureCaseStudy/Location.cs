using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventureCaseStudy
{
    public class Location(IEnumerable<string> ids, string name, string desc) : GameObject(ids, name, desc), IHaveInventory
    {

        private Inventory _inventory = new Inventory();
        public List<Path> Paths { get;} = new List<Path>();

        public Inventory Inventory => _inventory;

        public override string FullDescription
        {
            get
            {
                string directionsList = string.Join(", ", Paths.Take(Paths.Count() - 1));
                if (Paths.Count > 1) { directionsList += $" and {Paths.Last()}"; }

                string desc =  $"You are in the {Name}\n{base.FullDescription}\nThere are exits to the {DirectionsList}.\nIn this room you can see:\n{Inventory.ItemList}";
                // Implement Paths
                return desc;
            }
        }

        public IEnumerable<string> Directions => Paths.Select(path => path.Name);
        public string DirectionsList { get
            {
                int numberOfDirections = Directions.Count();
                if (numberOfDirections == 0) { return ""; }
                else if(numberOfDirections == 1) { return Directions.First(); }
                return $"{string.Join(", ", Directions.Take(numberOfDirections - 1))} and {Directions.Last()}";
            }
        }

        public Path? LocatePath(string direction)
        {
            return Paths.Find(item => item.AreYou(direction));
        }

        public GameObject? Locate(string id)
        {
            if (AreYou(id)) { return this; }
            return _inventory.Fetch(id);
        }

        public override string ToString()
        {
            return ShortDescription;
        }
    }
}
