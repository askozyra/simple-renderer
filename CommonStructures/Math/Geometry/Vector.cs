﻿using System;

namespace CommonStructures.Math.Geometry
{
    public class Vector : Coordinates, ICloneable
    {
        public Vector()
        {
            X = 0.0f;
            Y = 0.0f;
            Z = 0.0f;
            W = 0.0f;
        }

        public Vector(Coordinates coords)
            : base(coords)
        { }

        public Vector(float x, float y, float z)
            : base(x, y, z, 0.0f)
        { }

        public override bool Equals(object obj)
        {
            return this == (Coordinates)obj;
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
