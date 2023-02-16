using CommonStructures.Geometry.Complex.Meshes;
using CommonStructures.Geometry.Complex.Shapes;
using CommonStructures.Geometry.Primitives;
using CommonStructures.Math.Geometry;
using CommonStructures.Misc;
using OpenGLCore.Managers;
using OpenGLCore.Shaders;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OpenGLCore
{
    public partial class RenderingControl : UserControl
    {
        private ShaderManager _shaderManager { get; set; }
        private RenderingManager _renderingManager { get; set; }
        private OpenGLManager _openGLManager { get; set; }

        public RenderingControl()
        {
            InitManagers();

            InitializeComponent();
        }

        private void OpenGLRenderingControl_Destroyed(object sender, EventArgs e)
        {
            _openGLManager.SetWindowShouldClose(true);
        }

        private void OpenGLRenderingControl_Resize(object sender, EventArgs e)
        {
            _openGLManager.SetWindowSize(Width, Height);
        }

        private void OpenGLRenderingControl_Load(object sender, EventArgs e)
        {
            _openGLManager.Init(Handle);

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            this.Resize += OpenGLRenderingControl_Resize;
        }

        private void InitManagers()
        {
            _shaderManager = new ShaderManager();
            _renderingManager = new RenderingManager();
            _openGLManager = new OpenGLManager();
        }

        public unsafe void StartLoop()
        {
            // Test data
            List<Triangle> triangles1 = new List<Triangle>()
            {
                new Triangle(new List<Vertex>()
                {
                    new Vertex(new Point(1, 1f, 0.2f), new Color(1, 0, 0)),
                    new Vertex(new Point(0.5f, -0.5f, 0.5f), new Color(0.7f, 1.0f, 0.8f)),
                    new Vertex(new Point(-1f, -0.5f, 0.0f), new Color(0.8f, 0.7f, 1.0f))
                })
            };
            TriangleMesh triangle1 = new TriangleMesh(triangles1);

            _renderingManager.AddMesh(triangle1);

            _openGLManager.LockFps();

            while (!_openGLManager.ShouldClose())
            {
                _openGLManager.InitFrame();

                _shaderManager.SetShader(_shaderManager._basicShader);

                _renderingManager.Draw();

                _openGLManager.FlushFrame();
            }

            _openGLManager.Terminate();
        }

    }
}
