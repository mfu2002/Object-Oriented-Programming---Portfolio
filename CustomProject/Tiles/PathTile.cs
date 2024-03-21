using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject
{
    internal class PathTile : Tile
    {
        public PathTile(Vector2 loc) : base(Color.RGBColor(234, 145, 3), loc) { }

        public override bool Selectable => false; // Possible future versions could have the users select path tiles and launch attacks. 
    }
}
