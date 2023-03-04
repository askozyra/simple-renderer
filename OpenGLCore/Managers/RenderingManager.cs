using CommonStructures.Geometry.Complex.Meshes;
using CommonStructures.Geometry.Primitives;
using GLFW;
using System.Collections.Generic;

namespace OpenGLCore.Managers
{
    public class RenderingManager
    {
        private List<Mesh> _meshes { get; set; }

        public RenderingManager()
        {
            _meshes = new List<Mesh>();
        }

        public unsafe void Draw()
        {
            var VAO = OpenGL.glGenVertexArray();
            var VBO = OpenGL.glGenBuffer();

            OpenGL.glBindVertexArray(VAO);

            OpenGL.glBindBuffer(GlfwConstants.GL_ARRAY_BUFFER, VBO);

            List<float> pointsList = new List<float>();
            foreach (var mesh in _meshes)
            {
                List<Vertex> extractedVertices = ((TriangleMesh)mesh).ExtractVertices();

                for (int i = 0; i < extractedVertices.Count; i++)
                {
                    pointsList.Add(extractedVertices[i].X);
                    pointsList.Add(extractedVertices[i].Y);
                    pointsList.Add(extractedVertices[i].Z);

                    pointsList.Add(extractedVertices[i].Color.R);
                    pointsList.Add(extractedVertices[i].Color.G);
                    pointsList.Add(extractedVertices[i].Color.B);
                }
            }

            float[] pointsArray = pointsList.ToArray();

            fixed (float* v = &pointsArray[0])
            {
                OpenGL.glBufferData(GlfwConstants.GL_ARRAY_BUFFER, sizeof(float) * pointsArray.Length, v, GlfwConstants.GL_STATIC_DRAW);
            }

            OpenGL.glVertexAttribPointer(0, 3, GlfwConstants.GL_FLOAT, false, 6 * sizeof(float), (void*)0);
            OpenGL.glEnableVertexAttribArray(0);

            OpenGL.glVertexAttribPointer(1, 3, GlfwConstants.GL_FLOAT, false, 6 * sizeof(float), (void*)(3 * sizeof(float)));
            OpenGL.glEnableVertexAttribArray(1);

            int startIndex = 0;
            foreach (var mesh in _meshes)
            {
                TriangleMesh triangleMesh = (TriangleMesh)mesh;

                int countOfPoints = triangleMesh.ExtractVertices().Count;

                OpenGLWrapper.Color3f(triangleMesh.Triangles[0][0].Color);
                OpenGL.glDrawArrays(GlfwConstants.GL_TRIANGLES, startIndex, countOfPoints);

                startIndex += countOfPoints;
            }
        }

        public void AddMesh(Mesh mesh)
        {
            _meshes.Add(mesh);
        }
    }
}
