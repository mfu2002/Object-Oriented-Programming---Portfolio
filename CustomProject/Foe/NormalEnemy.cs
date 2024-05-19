using SplashKitSDK;

namespace CustomProject.Foe
{
    public class NormalEnemy : Enemy
    {
        // Internal so cannot be created outside this namespace. Should be created using a factory.
        internal NormalEnemy() : base(50, 25, 10)
        {
        }

        public override void GetDrawInstructions(List<DrawInstructions> instructions)
        {
            instructions.Add(new DrawInstructions(() => SplashKit.FillCircle(Color.Red, Location.X, Location.Y, 10), 2));

            base.GetDrawInstructions(instructions);
        }

    }
}
