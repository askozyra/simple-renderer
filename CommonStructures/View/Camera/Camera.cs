using CommonStructures.Math.Geometry;

namespace CommonStructures.View
{
    public class Camera
    {
        public Point Eye { get; private set; }
        public Point Target { get; private set; }
        public Vector Up { get; private set; }
        public Vector Right { get; private set; }

        public float _distanceToTarget;

        private float _yaw;
        private float _pitch;

        public float Yaw
        {
            get
            {
                return _yaw;
            }
            set
            {
                _yaw = value;
            }
        }

        public float Pitch
        {
            get
            {
                return _pitch;
            }
            set
            {
                _pitch = value > 89.0f ? 89.0f
                    : value < -89.0f ? -89.0f
                    : value;
            }
        }

        public Camera()
        {
            Eye = new Point();
            Target = new Point(0.0f, 0.0f, 0.0f);
            Up = new Vector(0.0f, 1.0f, 0.0f);
            Right = new Vector(1.0f, 0.0f, 0.0f);

            _distanceToTarget = 1;
        }

        public void OrbitScroll(float delta)
        {
            _distanceToTarget +=
                (delta > 0) ?
                (_distanceToTarget > 0.2f ? -0.2f : 0) :
                0.2f;

            ApplyOrbitRotation();
        }

        public void ApplyOrbitRotation()
        {
            SetPosition(
                new Coordinates(
                    (float)System.Math.Cos(Tools.Helpers.Converters.AngleConverter.DegreesToRadians(Yaw)) * (float)System.Math.Cos(Tools.Helpers.Converters.AngleConverter.DegreesToRadians(Pitch)),
                    (float)System.Math.Sin(Tools.Helpers.Converters.AngleConverter.DegreesToRadians(Pitch)),
                    (float)System.Math.Sin(Tools.Helpers.Converters.AngleConverter.DegreesToRadians(Yaw)) * (float)System.Math.Cos(Tools.Helpers.Converters.AngleConverter.DegreesToRadians(Pitch))
                )
            );

            ApplyDistanceToTargetCoef();
        }

        public void ApplyDistanceToTargetCoef()
        {
            SetPosition(Eye * _distanceToTarget);
        }

        public void Orbit(Vector offset)
        {
            Yaw += offset.X;
            Pitch += offset.Y;

            ApplyOrbitRotation();

            SetPosition(Eye + Target);
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