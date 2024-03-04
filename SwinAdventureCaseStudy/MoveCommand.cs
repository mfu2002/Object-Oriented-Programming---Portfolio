using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventureCaseStudy
{
    public class MoveCommand : Command
    {
        public MoveCommand() : base(["move", "go", "head", "leave"])
        {
        }

        public override string Execute(Player p, string[] text)
        {
            if (text.Length != 2) { return "I don't know how to move like that"; }
            if (!AreYou(text[0])) { return "Error in move input"; }

            Path? path = p.Location.LocatePath(text[1]);
            if (path == null) { return "No path that way"; }
            p.Location = path.Destination;
            return $"You head {path.Name}\n{path.FullDescription}";
        }
    }
}
