using System.IO;

namespace Tools.Helpers
{
    public static class PathHelper
    {
        private static readonly string _solutionFolder;

        static PathHelper()
        {
            _solutionFolder = Directory.GetCurrentDirectory();
        }

        public static string BuildAbsolutePath(string relativePath)
        {
            return _solutionFolder + relativePath;
        }
    }
}
