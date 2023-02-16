using CommonStructures.Misc;

namespace GLFW
{
    public static class OpenGLWrapper
    {
        public static void ClearColor(Color color)
        {
            OpenGL.glClearColor(color.R, color.G, color.B, color.A);
        }

        public static void Color3f(Color color)
        {
            OpenGL.glColor3f(color.R, color.G, color.B);
        }
    }
}
