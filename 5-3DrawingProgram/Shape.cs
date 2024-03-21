using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_3DrawingProgram
{
    public abstract class Shape
    {

        private Color _color;
        private float _x;
        private float _y;


        private bool _selected = false;

        public bool Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }

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


        public Shape(Color color)
        {
            _color = color;
            _x = 0f;
            _y = 0f;
        }

        public Shape() : this(Color.Yellow)
        {

        }

        public abstract void Draw();
       
        public abstract bool IsAt(Point2D point);


        public abstract void DrawOutline();

        public virtual void SaveTo(StreamWriter writer)
        {
            writer.WriteColor(Color);
            writer.WriteLine(X);
            writer.WriteLine(Y);
        }

        public virtual void LoadFrom(StreamReader reader)
        {
            Color = reader.ReadColor();
            X = reader.ReadInteger();
            Y = reader.ReadInteger();
        }

    }
}
