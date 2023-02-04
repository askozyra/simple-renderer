namespace CommonStructures.Math.Geometry
{
    public class Vector
    {
        public Coordinates Coordinates { get; set; }

        public Vector()
        {
            Coordinates = new Coordinates(w: 0.0f);
        }

        public Vector(Coordinates coords)
        {
            Coordinates = new Coordinates(coords, w: 0.0f);
        }

        public Vector(float x, float y, float z)
        {
            Coordinates = new Coordinates(x, y, z, w: 0.0f);
        }
    }
}
