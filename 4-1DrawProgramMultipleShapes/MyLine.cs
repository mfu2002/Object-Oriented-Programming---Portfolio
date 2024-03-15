using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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


        public MyLine() : this(Color.Red, 0, 0, SplashKit.ScreenWidth()/2,SplashKit.ScreenHeight() /2) 
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
            float slope = (Y - EndY) / (X - EndX);
            float c = Y - slope * X;  // Rearrange Y = mx +C

            return Math.Abs(point.Y - (int)(slope * point.X + c)) <= 2;

        }
    }
}
