using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventureCaseStudy
{
    public class Item : GameObject
    {
        public Item(IEnumerable<string> ids, string name, string desc) : base(ids, name, desc)
        {
        }
    }
}
