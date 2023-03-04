using CommonStructures.Geometry.Complex.Meshes;
using System.Collections.Generic;

namespace CommonStructures.WorldObjects.Scenes
{
    public class Layer
    {
        private List<Mesh> _meshes { get; set; }
        //private List<Viewport> _viewports { get; set; }
        //private List<Light> _lights { get; set; }

        public Layer()
        {
            _meshes = new List<Mesh>();
        }

        public void AddMesh(Mesh mesh)
        {
            _meshes.Add(mesh);
        }

        public void Clear()
        {
            _meshes.Clear();
        }

        public void RemoveMesh(Mesh mesh)
        {
            _meshes.Remove(mesh);

        }

        //public void AddRenderableObject(IRenderableObject rObj)
        //{

        //}
    }
}
