using System;

namespace CommonStructures.Math.Geometry
{
    public class Coordinates : ICloneable
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float W { get; set; }

        public Coordinates() { }

        public Coordinates(Coordinates coords)
        {
            X = coords.X;
            Y = coords.Y;
            Z = coords.Z;
            W = coords.W;
        }

        public Coordinates(Coordinates coords, float w)
        {
            X = coords.X;
            Y = coords.Y;
            Z = coords.Z;
            W = w;
        }

        public Coordinates(float x = 0.0f, float y = 0.0f, float z = 0.0f, float w = 0.0f)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public void Patch(float? x = null, float? y = null, float? z = null, float? w = null)
        {
            this.X = x ?? this.X;
            this.Y = y ?? this.Y;
            this.Z = z ?? this.Z;
            this.W = w ?? this.W;
        }

        public void Translate(float? x = null, float? y = null, float? z = null, float? w = null)
        {
            this.X += x ?? 0;
            this.Y += y ?? 0;
            this.Z += z ?? 0;
            this.W += w ?? 0;
        }

        public static Coordinates operator +(Coordinates c, float n)
        {
            return new Coordinates(
                c.X + n,
                c.Y + n,
                c.Z + n);
        }

        public static Coordinates operator -(Coordinates c, float n)
        {
            return new Coordinates(
                c.X - n,
                c.Y - n,
                c.Z - n);
        }

        public static Coordinates operator -(float n, Coordinates c)
        {
            return new Coordinates(
                n - c.X,
                n - c.Y,
                n - c.Z);
        }

        public static Coordinates operator *(Coordinates c, float n)
        {
            return new Coordinates(
                c.X * n,
                c.Y * n,
                c.Z * n);
        }

        public static Coordinates operator /(float n, Coordinates c)
        {
            return new Coordinates(
                n / c.X,
                n / c.Y,
                n / c.Z);
        }

        public static Coordinates operator /(Coordinates c, float n)
        {
            return new Coordinates(
                c.X / n,
                c.Y / n,
                c.Z / n);
        }

        public static Coordinates operator +(Coordinates c1, Coordinates c2)
        {
            return new Coordinates(
                c1.X + c2.X,
                c1.Y + c2.Y,
                c1.Z + c2.Z);
        }

        public static Coordinates operator -(Coordinates c1, Coordinates c2)
        {
            return new Coordinates(
                c1.X - c2.X,
                c1.Y - c2.Y,
                c1.Z - c2.Z);
        }

        public static Coordinates operator *(Coordinates c1, Coordinates c2)
        {
            return new Coordinates(
                c1.X * c2.X,
                c1.Y * c2.Y,
                c1.Z * c2.Z);
        }

        public static Coordinates operator /(Coordinates c1, Coordinates c2)
        {
            return new Coordinates(
                c1.X / c2.X,
                c1.Y / c2.Y,
                c1.Z / c2.Z);
        }

        public Coordinates Cross(Coordinates c)
        {
            return Coordinates.Cross(this, c);
        }

        public Coordinates Normalize()
        {
            float distance = (float)System.Math.Sqrt(X * X + Y * Y + Z * Z);
            return new Coordinates(X / distance, Y / distance, Z / distance);
        }

        public float Dot(Coordinates c)
        {
            return Coordinates.Dot(this, c);
        }

        public static Coordinates Normalize(Coordinates c)
        {
            float distance = (float)System.Math.Sqrt(c.X * c.X + c.Y * c.Y + c.Z * c.Z);
            return new Coordinates(c.X / distance, c.Y / distance, c.Z / distance);
        }

        public static Coordinates Cross(Coordinates c1, Coordinates c2)
        {
            return new Coordinates
            (
                c1.Y * c2.Z - c1.Z * c2.Y,
                c1.Z * c2.X - c1.X * c2.Z,
                c1.X * c2.Y - c1.Y * c2.X
            );
        }

        public static float Dot(Coordinates c1, Coordinates c2)
        {
            return (c1.X * c2.X) + (c1.Y * c2.Y) + (c1.Z * c2.Z);
        }

        public float Distance(Coordinates c)
        {
            Coordinates diff = this - c;

            return (int)System.Math.Sqrt(
                System.Math.Pow(diff.X, 2) +
                System.Math.Pow(diff.Y, 2) +
                System.Math.Pow(diff.Z, 2));
        }

        public static float Distance(Coordinates c1, Coordinates c2)
        {
            Coordinates diff = c1 - c2;

            return (int)System.Math.Sqrt(
                System.Math.Pow(diff.X, 2) +
                System.Math.Pow(diff.Y, 2) +
                System.Math.Pow(diff.Z, 2));

        }

        public Vector ToVector()
        {
            return new Vector(this);
        }
        public Point ToPoint()
        {
            return new Point(this);
        }

        public override bool Equals(object obj)
        {
            return
                this.X == ((Coordinates)obj).X &&
                this.Y == ((Coordinates)obj).Y &&
                this.Z == ((Coordinates)obj).Z;
        }

        public object Clone()
        {
            return new Coordinates(this);
        }
    }
}
