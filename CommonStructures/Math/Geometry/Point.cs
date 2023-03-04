using System;

namespace CommonStructures.Math.Geometry
{
    public class Point : Coordinates, ICloneable
    {

        public Point()
        {
            X = 0.0f;
            Y = 0.0f;
            Z = 0.0f;
            W = 1.0f;
        }

        public Point(Coordinates coords)
        {
            X = coords.X;
            Y = coords.Y;
            Z = coords.Z;
            W = coords.W;
        }

        public Point(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
            W = 1.0f;
        }

        public Point Cross(Point v)
        {
            return Point.Cross(this, v);
        }

        public Point Normalize()
        {
            float distance = (float)System.Math.Sqrt(X * X + Y * Y + Z * Z);
            return new Point(X / distance, Y / distance, Z / distance);
        }

        public float Dot(Point v)
        {
            return Point.Dot(this, v);
        }

        public static Point Normalize(Point v)
        {
            float distance = (float)System.Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
            return new Point(v.X / distance, v.Y / distance, v.Z / distance);
        }

        public static Point Cross(Point v1, Point v2)
        {
            return new Point
            (
                v1.Y * v2.Z - v1.Z * v2.Y,
                v1.Z * v2.X - v1.X * v2.Z,
                v1.X * v2.Y - v1.Y * v2.X
            );
        }

        public static float Dot(Point v1, Point v2)
        {
            return (v1.X * v2.X) + (v1.Y * v2.Y) + (v1.Z * v2.Z);
        }

        public override bool Equals(object obj)
        {
            return this == (Point)obj;
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
