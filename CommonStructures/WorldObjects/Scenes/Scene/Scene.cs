using System.Collections.Generic;

namespace CommonStructures.WorldObjects.Scenes
{
    public class Scene
    {
        private List<Layer> _layers { get; set; }

        public Scene()
        {
            _layers = new List<Layer>();
        }

        public Layer this[int index]
        {
            get
            {
                return _layers[index];
            }
            set
            {
                _layers[index] = value;
            }
        }

    }
}
