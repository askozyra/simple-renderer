using System.Collections.Generic;

namespace CommonStructures.WorldObjects.Scenes
{
    public class SceneManager
    {
        private List<Scene> _scenes { get; set; }
        private Scene _currentScene { get; set; }

        public SceneManager()
        {
            _scenes = new List<Scene>();
        }
    }
}
