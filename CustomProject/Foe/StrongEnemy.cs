using SplashKitSDK;

namespace CustomProject.Foe
{
    internal class StrongEnemy : Enemy
    {
        // Internal so cannot be created outside this namespace. Should be created using a factory.
        internal StrongEnemy() : base(150, 20, 20)
        {
        }

        public override void GetDrawInstructions(List<DrawInstructions> instructions)
        {
            int width = 20;
            int height = 20;
            instructions.Add(new DrawInstructions(() => SplashKit.FillRectangle(Color.Green, Location.X - width / 2, Location.Y - height / 2, width, height), 2));

            base.GetDrawInstructions(instructions);
        }
    }
}
