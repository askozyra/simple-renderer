using OpenGLCore.GlfwWrapper;
using OpenGLCore.GlfwWrapper.Enums;
using OpenGLCore.GlfwWrapper.Structs;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace OpenGLCore
{
    public partial class RenderingControl : UserControl
    {
        private const string _user32Dll = "User32.dll";

        private Window Window { get; set; }
        private IntPtr hwNative { get; set; }

        public RenderingControl()
        {
            InitializeComponent();
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
            Glfw.SetWindowShouldClose(Window, true);
        }

        private void OpenGLRenderingControl_Resize(object sender, EventArgs e)
        {
            Glfw.SetWindowSize(Window, Width, Height);
        }

        private void OpenGLRenderingControl_Load(object sender, EventArgs e)
        {
            Glfw.Init();
            InitWindowHints();
            SetWindowParent();
            InitWindowStyle();
            ShowWindow(hwNative, 5);

            Glfw.MakeContextCurrent(Window);

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
            Window = Glfw.CreateWindow(Width, Height, "", Monitor.None, Window.None);
            hwNative = Native.GetWin32Window(Window);
            SetParent(hwNative, Handle);
        }

        private void InitWindowStyle()
        {
            long style = GetWindowLong(hwNative, -16);
            style &= ~0x80000000;
            style |= 1073741824;
            SetWindowLong(hwNative, -16, style);
        }

        public void StartLoop()
        {
            Glfw.SwapInterval(1);

            while (!Glfw.WindowShouldClose(Window))
            {
                int width, height;

                Glfw.GetFramebufferSize(Window, out width, out height);

                OpenGL.glViewport(0, 0, width, height);
                OpenGL.glClearColor(
                    1, 0, 1, 1);
                OpenGL.glClear(16384 | 256);

                Glfw.SwapBuffers(Window);

                Glfw.PollEvents();
            }

            Glfw.DestroyWindow(Window);
            Glfw.Terminate();
        }
    }
}
