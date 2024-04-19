using _2_3SplashkitBasicShape;
using SplashKitSDK;

namespace SkmProject
{
    public class Program
    {
        public static void Main()
        {
            Window window = new Window("Shape Drawer", 800, 600);
            Shape myShape = new Shape();
            do
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();


                if (SplashKit.MouseClicked(MouseButton.LeftButton))
                {
                    myShape.X = SplashKit.MouseX();
                    myShape.Y = SplashKit.MouseY();
                }

                if (SplashKit.KeyTyped(KeyCode.SpaceKey) && myShape.IsAt(new Point2D() { X = SplashKit.MouseX(), Y = SplashKit.MouseY() }))
                {
                    myShape.Color = Color.Random();
                }
                myShape.Draw();

                SplashKit.RefreshScreen();
            } while (!window.CloseRequested);
        }
    }
}
