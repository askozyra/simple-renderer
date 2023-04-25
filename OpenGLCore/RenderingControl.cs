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
        private MouseCallback _mouseMoveCallback { get; set; }
        private FocusCallback _focusCallback { get; set; }
        private MouseButtonCallback _mouseButtonCallback { get; set; }
        private MouseCallback _mouseScrollCallback { get; set; }

        private System.Drawing.Point _mousePos { get; set; }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

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
            BindCallbacks();
        }

        private void WindowsFocusCallback(bool focusing)
        {
            if (focusing)
                this.Focus();
        }

        private void BindCallbacks()
        {
            _focusCallback = (_, focusing) => WindowsFocusCallback(focusing);
            _mouseButtonCallback = (_, button, action, mods) => GlfwMouseButtonPressed(button, action, mods);
            _mouseScrollCallback = (_, xoffset, yoffset) => GlfwMouseScrolled(xoffset, yoffset);
            _mouseMoveCallback = (_, xoffset, yoffset) => GlfwMouseMove(xoffset, yoffset);

            Glfw.SetWindowFocusCallback(_openGLManager.Window, _focusCallback);
            Glfw.SetMouseButtonCallback(_openGLManager.Window, _mouseButtonCallback);
            Glfw.SetScrollCallback(_openGLManager.Window, _mouseScrollCallback);
            Glfw.SetCursorPositionCallback(_openGLManager.Window, _mouseMoveCallback);
        }

        private void GlfwMouseScrolled(double xoffset, double yoffset)
        {
            var pos = PointToClient(MousePosition);
            MouseEventArgs mouseEventArgs = new MouseEventArgs(MouseButtons.None, 0, pos.X, pos.Y, (int)yoffset);
            RenderingControl_Scroll(this, mouseEventArgs);
        }

        private void GlfwMouseButtonPressed(MouseButton button, InputState action, ModifierKeys mods)
        {
            MouseButtons mouseButtons = MouseButtons.None;
            switch ((uint)button)
            {
                case 0:
                    mouseButtons = MouseButtons.Left;
                    break;
                case 1:
                    mouseButtons = MouseButtons.Right;
                    break;
                case 2:
                    mouseButtons = MouseButtons.Middle;
                    break;
            }

            System.Drawing.Point pos = PointToClient(MousePosition);

            MouseEventArgs mouseEventArgs = new MouseEventArgs(mouseButtons, 1, pos.X, pos.Y, 0);
            if ((byte)action == 1)
            {
                RenderingControl_MouseDown(this, mouseEventArgs);
            }
            else
            {
                RenderingControl_MouseUp(this, mouseEventArgs);
            }
        }

        private void HandleCameraMovement()
        {
            System.Drawing.Point pos = PointToClient(MousePosition);

            float xoffset = pos.X - _mousePos.X;
            float yoffset = pos.Y - _mousePos.Y;

            xoffset *= 0.3f;
            yoffset *= 0.3f;

            if (Glfw.GetMouseButton(_openGLManager.Window, MouseButton.Right) == InputState.Press)
            {
                int x = Screen.PrimaryScreen.Bounds.Width;
                int y = Screen.PrimaryScreen.Bounds.Height;
                if (MousePosition.X < 1)
                    SetCursorPos(x, MousePosition.Y);
                else
                if (MousePosition.X > x - 2)
                    SetCursorPos(0, MousePosition.Y);

                if (MousePosition.Y < 1)
                    SetCursorPos(MousePosition.X, y);
                else
                if (MousePosition.Y > y - 2)
                    SetCursorPos(MousePosition.X, 0);

                _viewportManager[0].CurrentCamera.Orbit(new Vector(xoffset, yoffset));
            }

            _mousePos = pos;
        }

        private void GlfwMouseMove(double xoffset, double yoffset)
        {
            System.Drawing.Point pos = PointToClient(MousePosition);
            MouseEventArgs mouseMoveEventArgs = new MouseEventArgs(MouseButtons.None, 1, pos.X, pos.Y, 0);
            RenderingControl_MouseMove(this, mouseMoveEventArgs);
        }

        private void RenderingControl_MouseMove(object sender, MouseEventArgs e)
        {
            HandleCameraMovement();
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

                int ProjectionLocation = OpenGL.glGetUniformLocation(_shaderManager.CurrentShader, "projection");
                int ViewLocation = OpenGL.glGetUniformLocation(_shaderManager.CurrentShader, "view");
                int ModelLocation = OpenGL.glGetUniformLocation(_shaderManager.CurrentShader, "model");
                OpenGL.glUniformMatrix4fv(ProjectionLocation, 1, false, projection.ToArray());
                OpenGL.glUniformMatrix4fv(ViewLocation, 1, false, view.ToArray());
                OpenGL.glUniformMatrix4fv(ModelLocation, 1, false, model.ToArray());

                _renderingManager.Draw();

                _openGLManager.FlushFrame();
            }

            _openGLManager.Terminate();
        }

        private void RenderingControl_Scroll(object sender, MouseEventArgs e)
        {
            _viewportManager[0].CurrentCamera.OrbitScroll(e.Delta);
        }

        private void RenderingControl_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        { }

        private void RenderingControl_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        { }

        private void RenderingControl_MouseUp(object sender, MouseEventArgs e)
        { }

        private void RenderingControl_MouseDown(object sender, MouseEventArgs e)
        { }
    }
}