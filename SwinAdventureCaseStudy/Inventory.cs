using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventureCaseStudy
{
    public class Inventory
    {
        private List<Item> _items = new List<Item>();
        public Inventory()
        {
        }
        public bool HasItem(string id)
        {
            foreach (Item item in _items)
            {
                if (item.AreYou(id)) { return true; }
            }
            return false;
        }

        public void Put(Item item)
        {
            _items.Add(item);
        }

        public Item? Take(string id)
        {
            foreach (Item item in _items)
            {
                if (item.AreYou(id))
                {
                    _items.Remove(item);
                    return item;
                }
            }
            return null;
        }
        public Item? Fetch(string id)
        {
            return _items.Find(item => item.AreYou(id));
        }

        public string ItemList { get
            {
                StringBuilder sb = new StringBuilder();
                foreach (Item item in _items)
                {
                    sb.AppendLine($"\t{item.ShortDescription}");
                }
                return sb.ToString();
            }
        }
    }
}
