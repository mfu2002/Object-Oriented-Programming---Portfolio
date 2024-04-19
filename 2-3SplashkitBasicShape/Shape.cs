using SplashKitSDK;

namespace _2_3SplashkitBasicShape
{
    internal class Shape
    {

        private Color _color;
        private float _x;
        private float _y;
        private int _width;
        private int _height;

        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }


        public float X
        {
            get { return _x; }
            set { _x = value; }
        }


        public float Y
        {
            get { return _y; }
            set { _y = value; }
        }


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

        public Shape()
        {
            _color = Color.Green;
            _x = 0f;
            _y = 0f;
            _width = 100;
            _height = 100;
        }

        public void Draw()
        {
            SplashKit.FillRectangle(_color, _x, _y, _width, _height);
        }
        public bool IsAt(Point2D point)
        {

            if (point.X < _x) return false;
            if (point.X > _width + _x) return false;
            if (point.Y < _y) return false;
            if (point.Y > _height + _y) return false;
            return true;
        }
    }
}
