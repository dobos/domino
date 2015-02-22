using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Complex.Domino.Util
{
    public static class Path
    {
        public static string MakeRelative(string basePath, string path)
        {
            basePath = System.IO.Path.GetFullPath(basePath);
            path = System.IO.Path.GetFullPath(path);

            if (!path.StartsWith(basePath, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new ArgumentException("Path is outside base directory");  // TODO
            }

            if (path.Length < basePath.Length)
            {
                throw new ArgumentException("Cannot reference directory outside base path");    // TODO
            }

            var relpath = path.Substring(basePath.Length).TrimStart(System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar);

            return relpath;
        }
    }
}