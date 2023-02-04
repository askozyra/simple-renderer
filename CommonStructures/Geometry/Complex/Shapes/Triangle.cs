using CommonStructures.Geometry.Primitives;
using System.Collections.Generic;

namespace CommonStructures.Geometry.Complex.Shapes
{
    public class Triangle : Shape
    {
        public Triangle() { }

        public Triangle(List<Vertex> vertices)
        {
            Vertices = vertices;
        }

        public Triangle(Vertex v1, Vertex v2, Vertex v3)
        {
            Vertices = new List<Vertex>()
            {
                v1, v2, v3
            };
        }

        public IEnumerable<Vertex> GetVerticesEnumerator()
        {
            foreach (var vertex in Vertices)
                yield return vertex;
        }
    }
}
