namespace CommonStructures.Math.Geometry
{
    public class Coordinates
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
    }
}
