namespace CommonStructures.View
{
    public class Viewport
    {
        private int _width { get; set; }
        private int _height { get; set; }

        private float _fieldOfView { get; set; }

        private float _far { get; set; }
        private float _near { get; set; }

        private CameraManager _cameraManager { get; set; }

        public Camera CurrentCamera { get; private set; }

        public Viewport()
        {
            // Temporary camera solution
            CurrentCamera = new Camera();

            _cameraManager = new CameraManager();
        }
    }
}
