using CommonStructures.Geometry.Complex.Shapes;
using CommonStructures.Geometry.Primitives;
using System.Collections.Generic;
using System.Linq;

namespace CommonStructures.Geometry.Complex.Meshes
{
    public class TriangleMesh : Mesh
    {
        public List<Triangle> Triangles { get; set; }

        public TriangleMesh()
        {
            Triangles = new List<Triangle>();
        }

        public TriangleMesh(List<Triangle> triangles)
        {
            Triangles = triangles;
        }

        public List<Vertex> ExtractVertices()
        {
            List<Vertex> vertices = new List<Vertex>();

            foreach (var t in Triangles)
            {
                foreach (var v in t.GetVerticesEnumerator())
                {
                    vertices.Add(v);
                }
            }

            vertices = vertices.Distinct().ToList();

            return vertices;
        }
    }
}
