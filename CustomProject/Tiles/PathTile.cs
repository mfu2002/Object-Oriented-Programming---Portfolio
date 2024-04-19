using SplashKitSDK;
using System.Numerics;

namespace CustomProject
{
    internal class PathTile(Vector2 loc) : Tile(Color.RGBColor(234, 145, 3), loc)
    {
        public override bool Selectable => false; // Possible future versions could have the users select path tiles and launch attacks. 
    }
}
