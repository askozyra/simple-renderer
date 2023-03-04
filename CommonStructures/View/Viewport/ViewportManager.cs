using System.Collections.Generic;

namespace CommonStructures.View
{
    public class ViewportManager
    {
        private List<Viewport> _viewports { get; set; }

        public ViewportManager()
        {
            _viewports = new List<Viewport>();
        }

        public void AddViewport(Viewport viewport)
        {
            _viewports.Add(viewport);
        }

        public Viewport this[int i]
        {
            get
            {
                return _viewports[i];
            }
            set
            {
                _viewports[i] = value;
            }
        }

    }
}
