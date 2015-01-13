using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Complex.Domino.Util
{
    public static class IO
    {
        public static void ForceDeleteDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                ForceEmptyDirectory(path);

                var directory = new DirectoryInfo(path) { Attributes = FileAttributes.Normal };
                directory.Delete(true);
            }
        }

        public static void ForceEmptyDirectory(string path)
        {
            var directory = new DirectoryInfo(path) { Attributes = FileAttributes.Normal };

            foreach (var info in directory.GetFileSystemInfos("*", SearchOption.AllDirectories))
            {
                info.Attributes = FileAttributes.Normal;
                info.Delete();
            }
        }
    }
}
