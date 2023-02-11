using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonStructures.WorldObjects.Scenes
{
    public class Scene
    {
        private List<Layer> _layers { get; set; }

        public Scene()
        {

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
