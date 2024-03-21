using System;
using System.Numerics;
using SplashKitSDK;

namespace _5_3DrawingProgram
{
    public class Program
    {

        private enum ShapeKind
        {
            Rectangle,
            Circle,
            Line
        }

        public static void Main()
        {
            Window window = new Window("Shape Drawer", 800, 600);
            Drawing myDrawing = new Drawing();
            ShapeKind kindToAdd = ShapeKind.Circle;
            do
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();


                if (SplashKit.MouseClicked(MouseButton.LeftButton))
                {

                    Shape newShape;

                    switch (kindToAdd)
                    {
                        case ShapeKind.Line:
                            newShape = new MyLine();
                            break;
                        case ShapeKind.Circle:
                            newShape = new MyCircle();
                            break;
                        default:
                            newShape = new MyRectangle();
                            break;
                    }

                    newShape.X = SplashKit.MouseX();
                    newShape.Y = SplashKit.MouseY();
                    myDrawing.AddShape(newShape);
                }
                if (SplashKit.MouseClicked(MouseButton.RightButton))
                {
                    myDrawing.SelectShapesAt(new Point2D() { X = SplashKit.MouseX(), Y = SplashKit.MouseY() });
                }

                if (SplashKit.KeyTyped(KeyCode.SpaceKey))
                {
                    myDrawing.Background = Color.Random();
                }
                if (SplashKit.KeyTyped(KeyCode.RKey))
                {
                    kindToAdd = ShapeKind.Rectangle;
                }
                if (SplashKit.KeyTyped(KeyCode.CKey))
                {
                    kindToAdd = ShapeKind.Circle;
                }
                if (SplashKit.KeyTyped(KeyCode.LKey))
                {
                    kindToAdd = ShapeKind.Line;
                }
                if (SplashKit.KeyTyped(KeyCode.SKey))
                {
                    myDrawing.Save("drawings.txt");
                }
                if (SplashKit.KeyTyped(KeyCode.OKey))
                {
                    try
                    {

                    myDrawing.Load("drawings.txt");
                    }
                    catch (Exception e)
                    {
                        Console.Error.WriteLine($"Error loading file: {e.Message}");
                    }
                }
                if (SplashKit.KeyTyped(KeyCode.BackspaceKey) || SplashKit.KeyTyped(KeyCode.DeleteKey))
                {
                    foreach (Shape shape in myDrawing.SelectedShapes)
                    {
                        myDrawing.RemoveShape(shape);
                    }
                }
                myDrawing.Draw();

                SplashKit.RefreshScreen();
            } while (!window.CloseRequested);
        }
    }
}
