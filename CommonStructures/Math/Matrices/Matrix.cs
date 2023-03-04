using System.Linq;

namespace CommonStructures.Math.Matrices
{
    public class Matrix
    {
        private float[,] _values { get; set; }

        public static Matrix ArrayToMatrix(float[,] array)
        {
            return new Matrix(array);
        }

        public static float[,] MatrixToArray(Matrix matrix)
        {
            return matrix._values;
        }

        public float[] ToArray()
        {
            return _values.Cast<float>().ToArray();
        }

        public Matrix(float[,] values)
        {
            this._values = values;
        }

        public float this[uint r, uint c]
        {
            get
            {
                return _values[r, c];
            }
            set
            {
                _values[r, c] = value;
            }
        }
    }
}
