using System.Collections.Generic;

namespace CommonStructures.View
{
    public class CameraManager
    {
        private List<Camera> _cameras { get; set; }

        public CameraManager()
        {
            _cameras = new List<Camera>();
        }

        public Camera this[int i]
        {
            get
            {
                return _cameras[i];
            }
            set
            {
                _cameras[i] = value;
            }
        }
    }
}
