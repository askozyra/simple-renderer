using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CommonStructures.Geometry.Complex.Meshes;
using CommonStructures.Geometry.Complex.Shapes;
using CommonStructures.Geometry.Primitives;
using CommonStructures.Misc;
using GLFW;
using OpenGLCore.Shaders;

namespace OpenGLCore
{
    public partial class RenderingControl : UserControl
    {
        private const string _user32Dll = "User32.dll";

        private Window _window { get; set; }
        private IntPtr _hwNative { get; set; }

        private ShaderManager _shaderManager { get; set; }

        public RenderingControl()
        {
            InitializeComponent();

            InitManagers();
        }

        [DllImport(_user32Dll)]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport(_user32Dll)]
        private static extern int GetSystemMetrics(int which);

        [DllImport(_user32Dll)]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, long dwNewLong);

        [DllImport(_user32Dll)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport(_user32Dll, SetLastError = true)]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport(_user32Dll)]
        private static extern void ShowWindow(IntPtr hWnd, int nCmdShow);

        private void OpenGLRenderingControl_Destroyed(object sender, EventArgs e)
        {
            Glfw.SetWindowShouldClose(_window, true);
        }

        private void OpenGLRenderingControl_Resize(object sender, EventArgs e)
        {
            Glfw.SetWindowSize(_window, Width, Height);
        }

        private void OpenGLRenderingControl_Load(object sender, EventArgs e)
        {
            Glfw.Init();
            InitWindowHints();
            SetWindowParent();
            InitWindowStyle();
            ShowWindow(_hwNative, 5);

            Glfw.MakeContextCurrent(_window);

            OpenGL.Load(Glfw.GetProcAddress);
        }

        private void InitWindowHints()
        {
            Glfw.WindowHint(Hint.Decorated, false);
            Glfw.WindowHint(Hint.Visible, false);
            Glfw.WindowHint(Hint.DepthBits, true);
        }

        private void SetWindowParent()
        {
            _window = Glfw.CreateWindow(Width, Height, "", Monitor.None, Window.None);
            _hwNative = Native.GetWin32Window(_window);
            SetParent(_hwNative, Handle);
        }

        private void InitWindowStyle()
        {
            long style = GetWindowLong(_hwNative, -16);
            style &= ~0x80000000;
            style |= 1073741824;
            SetWindowLong(_hwNative, -16, style);
        }

        private void InitManagers()
        {
            _shaderManager = new ShaderManager();
        }

        public unsafe void StartLoop()
        {
            // Test data
            List<Triangle> triangles = new List<Triangle>()
            {
                new Triangle(new List<Vertex>()
                {
                    new Vertex(new CommonStructures.Math.Geometry.Point(0.5f, 0.5f, 0.2f), new Color(1.0f, 0.7f, 0.8f)),
                    new Vertex(new CommonStructures.Math.Geometry.Point(0.5f, -0.5f, 0.5f), new Color(0.7f, 1.0f, 0.8f)),
                    new Vertex(new CommonStructures.Math.Geometry.Point(-1f, -0.5f, 0.0f), new Color(0.8f, 0.7f, 1.0f))
                })
            };

            TriangleMesh triangle = new TriangleMesh(triangles);

            var VAO = OpenGL.glGenVertexArray();
            var VBO = OpenGL.glGenBuffer();

            OpenGL.glBindVertexArray(VAO);

            OpenGL.glBindBuffer(GlfwConstants.GL_ARRAY_BUFFER, VBO);

            List<Vertex> extractedVertices = triangle.ExtractVertices();
            float[] vertices = new float[extractedVertices.Count * 3];

            for (int i = 0; i < extractedVertices.Count; i++)
            {
                int startIndex = i * 3;
                Vertex extractedVertex = extractedVertices[i];

                vertices[startIndex] = extractedVertex.GetX();
                vertices[startIndex + 1] = extractedVertex.GetY();
                vertices[startIndex + 2] = extractedVertex.GetZ();
            }

            fixed (float* v = &vertices[0])
            {
                OpenGL.glBufferData(GlfwConstants.GL_ARRAY_BUFFER, sizeof(float) * vertices.Length, v, GlfwConstants.GL_STATIC_DRAW);
            }

            OpenGL.glVertexAttribPointer(0, 3, GlfwConstants.GL_FLOAT, false, 3 * sizeof(float), (void*)0);
            OpenGL.glEnableVertexAttribArray(0);

            Glfw.SwapInterval(1);

            while (!Glfw.WindowShouldClose(_window))
            {
                int width, height;

                Glfw.GetFramebufferSize(_window, out width, out height);

                OpenGL.glViewport(0, 0, width, height);
                OpenGL.glClearColor(
                    0.2f, 0.2f, 0.2f, 1);
                OpenGL.glClear(16384 | 256);

                OpenGL.glUseProgram(_shaderManager._basicShader);

                OpenGL.glColor3f(triangles[0][0].Color.R, triangles[0][0].Color.G, triangles[0][0].Color.B);
                OpenGL.glDrawArrays(GlfwConstants.GL_TRIANGLES, 0, 3);

                Glfw.SwapBuffers(_window);

                Glfw.PollEvents();
            }

            Glfw.DestroyWindow(_window);
            Glfw.Terminate();
        }

    }
}
