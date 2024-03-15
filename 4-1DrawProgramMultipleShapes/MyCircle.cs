using SplashKitSDK;

namespace _4_1DrawProgramMultipleShapes
{
    public class MyCircle : Shape
    {
		private int _radius;

		public int Radius
		{
			get { return _radius; }
			set { _radius = value; }
		}
        public MyCircle(Color color, int radius) : base(color) 
        {
            Radius = radius;
        }
        public MyCircle() : this(Color.Blue, 50)
        {
            
        }
        public override void Draw()
        {
            if (Selected) { DrawOutline(); }

            SplashKit.FillCircle(Color, X, Y, Radius);
        }

        public override void DrawOutline()
        {
            SplashKit.DrawCircle(Color.Black, X, Y, Radius + 2);
        }

        public override bool IsAt(Point2D point)
        {
            return Math.Pow(point.X - X,2) + Math.Pow(point.Y - Y,2) <= Math.Pow(Radius,2);
        }

    }
}
