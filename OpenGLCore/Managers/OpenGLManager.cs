using GLFW;
using System;
using System.Runtime.InteropServices;

namespace OpenGLCore.Managers
{
    class OpenGLManager
    {
        private const string _user32Dll = "User32.dll";

        private Window _window { get; set; }
        private IntPtr _hwNative { get; set; }
        private IntPtr _handle { get; set; }

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

        private void InitWindowHints()
        {
            Glfw.WindowHint(Hint.Decorated, false);
            Glfw.WindowHint(Hint.Visible, false);
            Glfw.WindowHint(Hint.DepthBits, true);
        }

        private void EnableOpenGLFeatures()
        {
            OpenGL.glEnable(GlfwConstants.GL_DEPTH);
        }

        private void SetWindowParent()
        {
            _window = Glfw.CreateWindow(800, 600, "", Monitor.None, Window.None);
            _hwNative = Native.GetWin32Window(_window);
            SetParent(_hwNative, _handle);
        }

        private void InitWindowStyle()
        {
            long style = GetWindowLong(_hwNative, -16);
            style &= ~0x80000000;
            style |= 1073741824;
            SetWindowLong(_hwNative, -16, style);
        }

        public void SetWindowShouldClose(bool value)
        {
            Glfw.SetWindowShouldClose(_window, value);
        }

        public void SetWindowSize(int width, int height)
        {
            Glfw.SetWindowSize(_window, width, height);
        }

        public void Init(IntPtr handle)
        {
            _handle = handle;

            Glfw.Init();
            InitWindowHints();
            SetWindowParent();
            InitWindowStyle();
            ShowWindow(_hwNative, 5);

            Glfw.MakeContextCurrent(_window);

            OpenGL.Load(Glfw.GetProcAddress);

            EnableOpenGLFeatures();
        }

        public void LockFps()
        {
            Glfw.SwapInterval(1);
        }

        public bool ShouldClose()
        {
            return Glfw.WindowShouldClose(_window);
        }

        public void InitFrame()
        {
            int width, height;
            Glfw.GetFramebufferSize(_window, out width, out height);

            OpenGL.glViewport(0, 0, width, height);
            OpenGLWrapper.ClearColor(OptionsManager.BackgroundColor);
            OpenGL.glClear(16384 | 256);
        }

        public void FlushFrame()
        {
            Glfw.SwapBuffers(_window);
            Glfw.PollEvents();
        }

        public void Terminate()
        {
            Glfw.DestroyWindow(_window);
            Glfw.Terminate();
        }
    }
}
