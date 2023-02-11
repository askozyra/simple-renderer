using System.IO;

namespace Tools.Helpers.PathHelper
{
    public static class PathHelper
    {
        private static readonly string _solutionFolder;

        static PathHelper()
        {
            _solutionFolder = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), ProjectDirectory.Solution));
        }

        public static string BuildAbsolutePath(string relativePath)
        {
            return Path.GetFullPath(Path.Combine(_solutionFolder, relativePath));
        }
    }
}
