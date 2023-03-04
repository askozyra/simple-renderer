using CommonStructures.Math.Geometry;
using CommonStructures.Misc;

namespace CommonStructures.Geometry.Primitives
{
    public class Vertex : IRenderableObject
    {
        public Point Point { get; set; }
        public Color Color { get; set; }

        public float X
        {
            get
            {
                return Point.X;
            }
            set
            {
                Point.X = value;
            }
        }
        public float Y
        {
            get
            {
                return Point.Y;
            }
            set
            {
                Point.Y = value;
            }
        }
        public float Z
        {
            get
            {
                return Point.Z;
            }
            set
            {
                Point.Z = value;
            }
        }

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
    }
}
