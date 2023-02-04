using CommonStructures.Geometry.Primitives;
using System.Collections.Generic;

namespace CommonStructures.Geometry.Complex.Shapes
{
    public abstract class Shape
    {
        protected List<Vertex> Vertices { get; set; }

        public Shape()
        {
            Vertices = new List<Vertex>();
        }

        public Vertex this[int i]
        {
            get => Vertices[i];
            set => Vertices[i] = value;
        }
    }
}
