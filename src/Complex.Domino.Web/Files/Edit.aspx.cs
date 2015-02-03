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
    public partial class Edit : PageBase
    {
        public static string GetUrl(string fileName)
        {
            return String.Format("~/Files/Edit.aspx?file={0}", HttpUtility.UrlEncode(fileName.Replace('\\', '/')));
        }

        protected string FileName
        {
            get { return Request.QueryString[Constants.RequestFile].Replace('/', '\\'); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // TODO: this is just a quick solution, fix this later

            var guid = (string)Session[Constants.SessionGuid];
            var filename = this.FileName;
            var extension = Path.GetExtension(filename);
            var type = FileTypes.GetFileTypeByExtension(extension);

            var path = Path.Combine(
                DominoConfiguration.Instance.ScratchPath,
                guid,
                filename);

            if (type != null)
            {
                switch (type.Category)
                {
                    case FileCategory.Image:
                        ImageView.ImageUrl = Download.GetUrl(filename);
                        ImagePanel.Visible = true;
                        break;
                    case FileCategory.Code:
                        CodePanel.Visible = true;
                        CodeView.Text = File.ReadAllText(path);
                        CodeView.Mode = type.MimeType;
                        break;
                    default:
                        DownloadLink.NavigateUrl = Download.GetUrl(filename);
                        DownloadPanel.Visible = true;
                        break;
                }
            }
        }

    }
}