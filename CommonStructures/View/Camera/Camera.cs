using CommonStructures.Math.Geometry;

namespace CommonStructures.View
{
    public class Camera
    {
        public Point Eye { get; private set; }
        public Point Target { get; private set; }
        public Vector Up { get; private set; }
        public Vector Right { get; private set; }

        public Camera()
        {
            Eye = new Point();
            Target = new Point(0.0f, 0.0f, 0.0f);
            Up = new Vector(0.0f, 1.0f, 0.0f);
            Right = new Vector(1.0f, 0.0f, 0.0f);
        }

        public void SetPosition(Coordinates coords)
        {
            Eye = coords.ToPoint();
        }

        public void SetTarget(Coordinates coords)
        {
            Target = coords.ToPoint();
        }
    }
}
