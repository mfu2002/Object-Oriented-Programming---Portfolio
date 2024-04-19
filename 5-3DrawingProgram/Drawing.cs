using SplashKitSDK;

namespace _5_3DrawingProgram
{
    public class Drawing(Color backgroundColor)
    {
        private readonly List<Shape> _shapes = [];
        private Color _color = backgroundColor;

        public Drawing() : this(Color.White) { }

        public List<Shape> SelectedShapes => _shapes.FindAll(shape => shape.Selected);

        public int ShapeCount => _shapes.Count;

        public Color Background { get { return _color; } set { _color = value; } }

        public void Draw()
        {
            SplashKit.ClearScreen(Background);
            foreach (Shape shape in _shapes)
            {
                shape.Draw();
            }
        }

        public void SelectShapesAt(Point2D pt)
        {
            foreach (Shape shape in _shapes)
            {
                shape.Selected = shape.IsAt(pt);
            }

        }

        public void Load(string fileName)
        {
            StreamReader reader = new StreamReader(fileName);
            try
            {

                Background = reader.ReadColor();
                int count = reader.ReadInteger();
                _shapes.Clear();
                for (int i = 0; i < count; i++)
                {
                    string kind = reader.ReadLine();
                    Shape? s = null;
                    switch (kind)
                    {
                        case "Rectangle":
                            s = new MyRectangle();
                            break;
                        case "Circle":
                            s = new MyCircle();
                            break;
                        case "Line":
                            s = new MyLine();
                            break;
                        default:
                            throw new InvalidDataException($"Unknown shape kind: {kind}");
                    }
                    if (s == null)
                    {
                        continue;
                    }
                    s.LoadFrom(reader);

                    _shapes.Add(s);
                }

            }
            finally
            {
                reader.Close();
            }
        }


        public void Save(string fileName)
        {
            StreamWriter writer = new StreamWriter(fileName);
            try
            {

                writer.WriteColor(Background);
                writer.WriteLine(ShapeCount);
                foreach (Shape shape in _shapes)
                {
                    shape.SaveTo(writer);
                }
            }
            finally
            {

                writer.Close();
            }
        }


        public void AddShape(Shape shape) { _shapes.Add(shape); }
        public void RemoveShape(Shape shape) { _shapes.Remove(shape); }

    }
}
