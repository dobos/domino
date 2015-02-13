using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Complex.Domino.Lib;

namespace Complex.Domino.Web.Files
{
    public partial class ImageViewer : FilePageBase
    {
        public static string GetUrl(string fileName)
        {
            return String.Format("~/Files/ImageViewer.aspx?file={0}", HttpUtility.UrlEncode(fileName.Replace('\\', '/')));
        }

        protected override void UpdateForm(FileType fileType, string filename)
        {
            if (fileType == null || fileType.Category != FileCategory.Image)
            {
                throw new InvalidOperationException();
            }

            ImageView.ImageUrl = Download.GetUrl(filename);
        }
    }
}