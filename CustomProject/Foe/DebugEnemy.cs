using SplashKitSDK;

namespace CustomProject.Foe
{
    internal class DebugEnemy : Enemy
    {
        internal DebugEnemy() : base(10, 100, 10) { }

        public override void GetDrawInstructions(List<DrawInstructions> instructions)
        {
            instructions.Add(new DrawInstructions(() => SplashKit.FillCircle(Color.Pink, Location.X, Location.Y, 10), 2));

            base.GetDrawInstructions(instructions);
        }
    }
}
