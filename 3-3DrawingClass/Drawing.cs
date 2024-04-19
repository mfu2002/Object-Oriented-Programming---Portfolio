using SplashKitSDK;

namespace _3_3DrawingClass
{
    public class Drawing
    {
        private readonly List<Shape> _shapes;
        private Color _color;

        public Drawing() : this(Color.White)
        {

        }
        public Drawing(Color backgroundColor)
        {
            _shapes = new List<Shape>();
            _color = backgroundColor;
        }
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

        public void AddShape(Shape shape) { _shapes.Add(shape); }
        public void RemoveShape(Shape shape) { _shapes.Remove(shape); }

    }
}
