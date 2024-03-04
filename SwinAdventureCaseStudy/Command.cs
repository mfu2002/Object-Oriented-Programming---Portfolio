using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventureCaseStudy
{
    public abstract class Command(IEnumerable<string> idents) : IdentifiableObject(idents)
    {
        public abstract string Execute(Player p, string[] text);
       
    }
}
