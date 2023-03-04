using CommonStructures.Math.Geometry;
using CommonStructures.Math.Matrices;
using Tools.Helpers.Converters;

using SMath = System.Math;

namespace CommonStructures.Transforms
{
    public static class Transform
    {
        public static Matrix GetOrthographic(float left, float right, float bottom, float top, float near, float far)
        {
            return new Matrix
            (
                new float[,]
                {
                    { 2 / (right - left), 0, 0, 0 },
                    { 0, 2 / (top - bottom), 0, 0 },
                    { 0, 0, -2 / (far - near), 0 },
                    { -((right + left) / (right - left)), -((top + bottom) / (top - bottom)), -((far + near) / (far - near)) , 1 },
                }
            );
        }

        public static Matrix GetPerspectiveMatrix(float fieldOfView, float aspectRatio, float near, float far)
        {
            var range = (float)SMath.Tan(AngleConverter.DegreesToRadians(fieldOfView / 2)) * near;
            var left = -range * aspectRatio;
            var right = range * aspectRatio;
            var bottom = range;
            var top = -range;

            return new Matrix
            (
                new float[,]
                {
                       { (2 * near) / (right - left), 0, 0, 0 },
                       { 0, -(2 * near) / (top - bottom), 0, 0 },
                       { 0, 0, -(far + near) / (far - near), -1 },
                       { 0, 0, -(2 * far * near) / (far - near), 1 },
                }
            );
        }

        public static Matrix GetIdentityMatrix()
        {
            return new Matrix
                (
                    new float[,]
                    {
                        { 1, 0, 0, 0 },
                        { 0, 1, 0, 0 },
                        { 0, 0, 1, 0 },
                        { 0, 0, 0, 1 }
                    }
                );
        }

        public static Matrix GetTranslationTransform(Vector offset)
        {
            return new Matrix(new float[,]
            {
                { 1, 0, 0, offset.X },
                { 0, 1, 0, offset.Y },
                { 0, 0, 1, offset.Z },
                { 0, 0, 0, 1 }
            });
        }

        public static Matrix GetRotationTransform(float angle, Vector direction)
        {
            return new Matrix(new float[,]
            {
                { (float)(SMath.Cos(angle) + SMath.Pow(direction.X, 2) * (1 - SMath.Cos(angle))),
                    (float)(direction.X * direction.Y * (1 - SMath.Cos(angle)) - direction.Z * SMath.Sin(angle)),
                    (float)(direction.X * direction.Z * (1 - SMath.Cos(angle)) + direction.Y * SMath.Sin(angle)), 0 },

                { (float)(direction.Y * direction.X * (1 - SMath.Cos(angle)) + direction.Z * SMath.Sin(angle)),
                    (float)(SMath.Cos(angle) + SMath.Pow(direction.Y, 2) * (1 - SMath.Cos(angle))),
                    (float)(direction.Y * direction.Z * (1 - SMath.Cos(angle)) - direction.X * SMath.Sin(angle)), 0 },

                { (float)(direction.Z * direction.X * (1 - SMath.Cos(angle)) - direction.Y * SMath.Sin(angle)),
                    (float)(direction.Z * direction.Y * (1 - SMath.Cos(angle)) + direction.X * SMath.Sin(angle)),
                    (float)(SMath.Cos(angle) + SMath.Pow(direction.Z, 2) * (1 - SMath.Cos(angle))), 0 },

                { 0, 0, 0, 1 }
            });
        }

        public static Matrix GetScalingTransform(Vector coeffs)
        {
            return new Matrix(new float[,]
            {
                { coeffs.X, 0, 0, 0 },
                { 0, coeffs.Y, 0, 0 },
                { 0, 0, coeffs.Z, 0 },
                { 0, 0, 0, 1 }
            });
        }

        public static Matrix Inverse(Matrix matrix)
        {
            float[,] m = Matrix.MatrixToArray(matrix);

            float det =
                m[0, 0] * m[1, 1] * m[2, 2] - m[1, 2] * m[2, 1] -
                m[0, 1] * m[1, 0] * m[2, 2] - m[1, 2] * m[2, 0] +
                m[0, 2] * m[1, 0] * m[2, 1] - m[1, 1] * m[2, 0];

            float[,] inverse = new float[,]
            {
                { (m[1, 1] * m[2, 2] - m[2, 1] * m[1, 2]) / det, -(m[1, 0] * m[2, 2] - m[2, 0] * m[1, 2]) / det, (m[1, 0] * m[2, 1] - m[2, 0] * m[1, 1]) / det, 0 },
                { -(m[0, 1] * m[2, 2] - m[2, 1] * m[0, 2]) / det, (m[0, 0] * m[2, 2] - m[2, 0] * m[0, 2]) / det, -(m[0, 0] * m[2, 1] - m[2, 0] * m[0, 1]) / det, 0 },
                { (m[0, 1] * m[1, 2] - m[1, 1] * m[0, 2]) / det, -(m[0, 0] * m[1, 2] - m[1, 0] * m[0, 2]) / det, (m[0, 0] * m[1, 1] - m[1, 0] * m[0, 1]) / det, 0 },
                { 0, 0, 0, 1 }
            };

            return new Matrix(inverse);
        }

        public static Matrix GetLookAtMatrix(Point cameraEye, Point cameraTarget, Vector cameraUp)
        {
            Coordinates zAxis = Coordinates.Normalize(cameraEye - cameraTarget);
            Coordinates xAxis = Coordinates.Normalize(Coordinates.Cross(cameraUp, zAxis));
            Coordinates yAxis = Coordinates.Cross(zAxis, xAxis);

            float[,] viewMatrix = new float[,]
            {
                    {            xAxis.X,                          yAxis.X,                             zAxis.X,                      0},
                    {            xAxis.Y,                          yAxis.Y,                             zAxis.Y,                      0},
                    {            xAxis.Z,                          yAxis.Z,                             zAxis.Z,                      0},
                    {    -Coordinates.Dot(xAxis, cameraEye), -Coordinates.Dot(yAxis, cameraEye), -Coordinates.Dot(zAxis, cameraEye),  1}
            };

            return new Matrix(viewMatrix);
        }
    }
}
