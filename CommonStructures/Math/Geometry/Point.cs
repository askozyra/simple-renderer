namespace CommonStructures.Math.Geometry
{
    public class Point
    {
        public Coordinates Coordinates { get; set; }

        public Point()
        {
            Coordinates = new Coordinates(w: 1.0f);
        }

        public Point(Coordinates coords)
        {
            Coordinates = new Coordinates(coords, w: 1.0f);
        }

        public Point(float x, float y, float z)
        {
            Coordinates = new Coordinates(x, y, z, w: 1.0f);
        }
    }
}
