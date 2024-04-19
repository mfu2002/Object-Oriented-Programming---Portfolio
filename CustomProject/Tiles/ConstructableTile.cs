using SplashKitSDK;
using System.Numerics;

namespace CustomProject
{
    public class ConstructableTile(Vector2 loc) : Tile(Color.RGBColor(58, 239, 63), loc)
    {

        public DefenceTower? Tower { get; set; }

        public override bool Selectable => true;


        public override void GetDrawInstructions(List<DrawInstructions> drawInstructions)
        {
            base.GetDrawInstructions(drawInstructions);
            if (Tower != null && (Tower.Built || Selected))
            {
                Tower?.GetDrawInstructions(drawInstructions);
            }
            if (Tower != null && Selected)
            {

                string attributeString = $"A{Tower.AttackStrength} +{Tower.UpgradeStrengthIncrement}    " +
                                        $"S{Tower.AttackSpeed} +{Tower.UpgradeSpeedIncrement}    " +
                                        $"R{Tower.Range} +{Tower.UpgradeRangeIncrement}    " +
                                        $"L{Tower.LaserCapacity} +{Tower.UpgradeLaserCapacityIncrement}";

                drawInstructions.Add(new DrawInstructions(() => SplashKit.DrawText(attributeString, Color.Black, 10, SplashKit.ScreenHeight() - 10), 3));
                drawInstructions.Add(new DrawInstructions(() =>
                {
                    SplashKit.DrawCircle(Color.Black, Tower.Location.X, Tower.Location.Y, Tower.Range * Game.TILE_WIDTH);
                    SplashKit.DrawCircle(Color.Blue, Tower.Location.X, Tower.Location.Y, (Tower.Range + Tower.UpgradeRangeIncrement) * Game.TILE_WIDTH);
                }, 3));

            }
        }

        public override void Update(float deltaTime)
        {
            Tower?.Update(deltaTime);
        }
    }
}
