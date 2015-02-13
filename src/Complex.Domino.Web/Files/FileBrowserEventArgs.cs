using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Complex.Domino.Web.Files
{
    public class FileBrowserEventArgs : EventArgs
    {
        public string FileName { get; set; }
    }
}