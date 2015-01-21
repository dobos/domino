using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Complex.Domino.Web.Files
{
    public class FileType
    {
        public string Extension { get; set; }
        public string MimeType { get; set; }
        public FileCategory Category { get; set; }
    }
}