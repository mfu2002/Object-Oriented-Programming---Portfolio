using SplashKitSDK;

namespace _4_1DrawProgramMultipleShapes
{
    internal class MyLine : Shape
    {

        private float _endX;

        public float EndX
        {
            get { return _endX; }
            set { _endX = value; }
        }

        private float _endY;

        public float EndY
        {
            get { return _endY; }
            set { _endY = value; }
        }


        public MyLine() : this(Color.Red, 0, 0, SplashKit.ScreenWidth() / 2, SplashKit.ScreenHeight() / 2)
        {

        }

        public MyLine(Color color, float startX, int startY, int endX, int endY) : base(color)
        {
            X = startX;
            Y = startY;
            EndX = endX;
            EndY = endY;


        }



        public override void Draw()
        {
            if (Selected) { DrawOutline(); }

            SplashKit.DrawLine(Color, X, Y, EndX, EndY);
        }

        public override void DrawOutline()
        {
            SplashKit.DrawCircle(Color.Black, X, Y, 10);
            SplashKit.DrawCircle(Color.Black, EndX, EndY, 10);
        }

        public override bool IsAt(Point2D point)
        {
            Point2D startPoint = new Point2D();
            startPoint.X = X;
            startPoint.Y = Y;

            Point2D endPoint = new Point2D();
            endPoint.X = EndX;
            endPoint.Y = EndY;

            Line line = new Line();
            line.StartPoint = startPoint;
            line.EndPoint = endPoint;

            return SplashKit.PointOnLine(point, line);

        }
    }
}
