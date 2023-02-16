using CommonStructures.Math.Geometry;
using CommonStructures.Misc;

namespace CommonStructures.Geometry.Primitives
{
    public class Vertex : IRenderableObject
    {
        public Point Point { get; set; }
        public Color Color { get; set; }

        public Vertex()
        {
            Point = new Point();
            Color = Color.GrayColor;
        }

        public Vertex(Point point = null, Color color = null)
        {
            Point = point is null ? new Point() : point;
            Color = color is null ? Color.GrayColor : color;
        }

        public float GetX()
        {
            return Point.Coordinates.X;
        }

        public float GetY()
        {
            return Point.Coordinates.Y;
        }

        public float GetZ()
        {
            return Point.Coordinates.Z;
        }
    }
}
