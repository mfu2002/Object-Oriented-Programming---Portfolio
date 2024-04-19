using SplashKitSDK;

using System.Numerics;


namespace CustomProject
{
    public class BlockedTile(Vector2 loc) : Tile(Color.RGBColor(255, 208, 133), loc)
    {
        public override bool Selectable => false;  // Possible future versions could have the users select blocked tiles and convert them to constructable. 
    }
}
