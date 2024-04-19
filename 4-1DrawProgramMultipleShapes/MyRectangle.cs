using SplashKitSDK;

namespace _4_1DrawProgramMultipleShapes
{
    internal class MyRectangle : Shape
    {
        private int _width;
        private int _height;


        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }


        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public MyRectangle(Color color, float x, float y, int width, int height) : base(color)
        {
            X = x;
            Y = y;
            _width = width;
            _height = height;

        }
        public MyRectangle() : this(Color.Green, 0f, 0f, 100, 100)
        {

        }

        public override void DrawOutline()
        {
            SplashKit.FillRectangle(Color.Black, X - 2, Y - 2, _width + 4, _height + 4);
        }

        public override void Draw()
        {
            if (Selected) { DrawOutline(); }

            SplashKit.FillRectangle(Color, X, Y, _width, _height);
        }
        public override bool IsAt(Point2D point)
        {
            {

                if (point.X < X) return false;
                if (point.X > _width + X) return false;
                if (point.Y < Y) return false;
                if (point.Y > _height + Y) return false;
                return true;
            }
        }
    }
}
