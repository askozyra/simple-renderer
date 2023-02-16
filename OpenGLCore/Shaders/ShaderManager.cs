using GLFW;
using System.IO;
using Tools.Helpers.PathHelper;

namespace OpenGLCore.Shaders
{
    public class ShaderManager
    {
        public uint _basicShader { get; set; }

        private void InitShader(uint shader, string pathToShader)
        {
            var shaderVertex = CreateShader(GlfwConstants.GL_VERTEX_SHADER,
                LoadShader(PathHelper.BuildAbsolutePath(pathToShader + ".vert")));
            var shaderFragment = CreateShader(GlfwConstants.GL_FRAGMENT_SHADER,
                LoadShader(PathHelper.BuildAbsolutePath(pathToShader + ".frag")));

            shader = OpenGL.glCreateProgram();

            OpenGL.glAttachShader(shader, shaderVertex);
            OpenGL.glAttachShader(shader, shaderFragment);

            OpenGL.glLinkProgram(shader);

            OpenGL.glDeleteShader(shaderVertex);
            OpenGL.glDeleteShader(shaderFragment);
        }

        private string LoadShader(string path)
        {
            return File.ReadAllText(path);
        }

        private uint CreateShader(int type, string source)
        {
            var shader = OpenGL.glCreateShader(type);
            OpenGL.glShaderSource(shader, source);
            OpenGL.glCompileShader(shader);
            return shader;
        }


        public void InitShaders()
        {
            var relativePath = Path.Combine(ProjectDirectory.OpenGLCore, @"Shaders\BasicShader\shader");
            var basicShaderPath = PathHelper.BuildAbsolutePath(relativePath);

            InitShader(_basicShader, basicShaderPath);
        }

        public void SetShader(uint shader)
        {
            OpenGL.glUseProgram(shader);
        }
    }
}
