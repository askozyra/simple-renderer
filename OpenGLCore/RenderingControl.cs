using CommonStructures.Geometry.Complex.Meshes;
using CommonStructures.Geometry.Complex.Shapes;
using CommonStructures.Geometry.Primitives;
using CommonStructures.Math.Geometry;
using CommonStructures.Misc;
using CommonStructures.Transforms;
using CommonStructures.View;
using CommonStructures.WorldObjects.Scenes;
using GLFW;
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
        private ViewportManager _viewportManager { get; set; }
        private SceneManager _sceneManager { get; set; }
        private LayerManager _layerManager { get; set; }

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
            _shaderManager.InitShaders();
            _shaderManager.SetShader(_shaderManager._basicShader);

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
            _viewportManager = new ViewportManager();
            _sceneManager = new SceneManager();
            _layerManager = new LayerManager();
        }

        // Temporary movement solution
        private void RenderingControl_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case System.Windows.Forms.Keys.W:
                    _viewportManager[0].CurrentCamera.Eye.Translate(y: 1);
                    break;
                case System.Windows.Forms.Keys.A:
                    _viewportManager[0].CurrentCamera.Eye.Translate(x: -1);
                    break;
                case System.Windows.Forms.Keys.S:
                    _viewportManager[0].CurrentCamera.Eye.Translate(y: -1);
                    break;
                case System.Windows.Forms.Keys.D:
                    _viewportManager[0].CurrentCamera.Eye.Translate(x: 1);
                    break;
                case System.Windows.Forms.Keys.Q:
                    _viewportManager[0].CurrentCamera.Eye.Translate(z: -1);
                    break;
                case System.Windows.Forms.Keys.E:
                    _viewportManager[0].CurrentCamera.Eye.Translate(z: 1);
                    break;
            }
        }

        public unsafe void StartLoop()
        {
            // Test data
            List<Triangle> triangles1 = new List<Triangle>()
            {
                new Triangle(new List<Vertex>()
                {
                    new Vertex(new Point(-0.5f, -0.5f, 0.0f), new Color(1, 0.7f, 0.7f)),
                    new Vertex(new Point(0.5f, -0.5f, 0.0f), new Color(0.7f, 1.0f, 0.7f)),
                    new Vertex(new Point(0.0f, 0.5f, 0.0f), new Color(0.7f, 0.7f, 1.0f))
                })
            };
            TriangleMesh triangle1 = new TriangleMesh(triangles1);

            _renderingManager.AddMesh(triangle1);

            _viewportManager.AddViewport(new Viewport());

            _openGLManager.LockFps();

            while (!_openGLManager.ShouldClose())
            {
                _openGLManager.InitFrame();

                var model = Transform.GetIdentityMatrix();

                var view = Transform.GetLookAtMatrix(
                    _viewportManager[0].CurrentCamera.Eye,
                    _viewportManager[0].CurrentCamera.Target,
                    _viewportManager[0].CurrentCamera.Up
                );

                var projection = Transform.GetPerspectiveMatrix
                (
                    90.0f, Width / (float)Height, 0.1f, 100.0f
                );

                var ProjectionLocation = OpenGL.glGetUniformLocation(_shaderManager.CurrentShader, "projection");
                var ViewLocation = OpenGL.glGetUniformLocation(_shaderManager.CurrentShader, "view");
                var ModelLocation = OpenGL.glGetUniformLocation(_shaderManager.CurrentShader, "model");
                OpenGL.glUniformMatrix4fv(ProjectionLocation, 1, false, projection.ToArray());
                OpenGL.glUniformMatrix4fv(ViewLocation, 1, false, view.ToArray());
                OpenGL.glUniformMatrix4fv(ModelLocation, 1, false, model.ToArray());

                _renderingManager.Draw();

                _openGLManager.FlushFrame();
            }

            _openGLManager.Terminate();
        }
    }

}