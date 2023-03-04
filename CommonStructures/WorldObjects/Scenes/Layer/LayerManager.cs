using System.Collections.Generic;

namespace CommonStructures.WorldObjects.Scenes
{
    public class LayerManager
    {
        private List<Layer> _layers { get; set; }
        private Layer _currentLayer { get; set; }

        public LayerManager()
        {
            _layers = new List<Layer>();
        }
    }
}
