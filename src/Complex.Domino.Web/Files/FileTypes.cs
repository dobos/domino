using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Complex.Domino.Web.Files
{
    public class FileTypes
    {
        private static readonly FileType[] types = new FileType[]
        {
            new FileType() { Extension=".gif", MimeType="image/gif", Category = FileCategory.Image },
            new FileType() { Extension=".bmp", MimeType="image/bmp", Category = FileCategory.Image },
            new FileType() { Extension=".jpg", MimeType="image/jpeg", Category = FileCategory.Image },
            new FileType() { Extension=".jpeg", MimeType="image/jpeg", Category = FileCategory.Image },
            new FileType() { Extension=".png", MimeType="image/png", Category = FileCategory.Image },

            new FileType() { Extension=".txt", MimeType="text/plain", Category = FileCategory.Code },

            new FileType() { Extension=".c", MimeType="text/x-c", Category = FileCategory.Code },
            new FileType() { Extension=".cc", MimeType="text/x-c", Category = FileCategory.Code },
            new FileType() { Extension=".cpp", MimeType="text/x-c", Category = FileCategory.Code },
            new FileType() { Extension=".cxx", MimeType="text/x-c", Category = FileCategory.Code },
            new FileType() { Extension=".h", MimeType="text/x-c", Category = FileCategory.Code },
            new FileType() { Extension=".hh", MimeType="text/x-c", Category = FileCategory.Code },
            new FileType() { Extension=".hpp", MimeType="text/x-c", Category = FileCategory.Code },
            new FileType() { Extension=".hxx", MimeType="text/x-c", Category = FileCategory.Code },

            new FileType() { Extension=".f", MimeType="text/x-fortran", Category = FileCategory.Code },
            new FileType() { Extension=".for", MimeType="text/x-fortran", Category = FileCategory.Code },
            new FileType() { Extension=".f77", MimeType="text/x-fortran", Category = FileCategory.Code },
            new FileType() { Extension=".f90", MimeType="text/x-fortran", Category = FileCategory.Code },

            new FileType() { Extension=".java", MimeType="text/x-java-source", Category = FileCategory.Code },

            new FileType() { Extension=".pdf", MimeType="application/x-pdf", Category = FileCategory.Document },
        };

        private static readonly Dictionary<string, FileType> fileTypesByExtension;

        static FileTypes()
        {
            fileTypesByExtension = new Dictionary<string, FileType>(StringComparer.InvariantCultureIgnoreCase);

            for (int i = 0; i < types.Length; i++)
            {
                fileTypesByExtension.Add(types[i].Extension, types[i]);
            }
        }

        public static FileType GetFileTypeByExtension(string extension)
        {
            if (fileTypesByExtension.ContainsKey(extension))
            {
                return fileTypesByExtension[extension];
            }
            else
            {
                return null;
            }
        }
    }
}