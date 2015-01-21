using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Complex.Domino.Web.Files
{
    public partial class View : PageBase
    {
        public static string GetUrl(string fileName)
        {
            return String.Format("~/Files/View.aspx?file={0}", HttpUtility.UrlEncode(fileName.Replace('\\', '/')));
        }

        protected string FileName
        {
            get { return Request.QueryString[Constants.RequestFile].Replace('/', '\\'); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // TODO: this is just a quick solution, fix this later

            var filename = this.FileName;
            var extension = Path.GetExtension(filename);
            var type = FileTypes.GetFileTypeByExtension(extension);

            switch (type.Category)
            {
                case FileCategory.Image:
                    ImageView.ImageUrl = Download.GetUrl(filename);
                    ImagePanel.Visible = true;
                    break;
                case FileCategory.Code:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
        }

    }
}