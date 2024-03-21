using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject
{
    public class BlockedTile : Tile
    {
        public BlockedTile(Vector2 loc) : base(Color.RGBColor(255,208,133), loc)
        {
        }

        public override bool Selectable => false;  // Possible future versions could have the users select blocked tiles and convert them to constructable. 
    }
}
