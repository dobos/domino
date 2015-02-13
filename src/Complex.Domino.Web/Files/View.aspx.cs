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

        protected string FullPath
        {
            get
            {
                var guid = (string)Session[Constants.SessionGuid];
                var path = Path.Combine(DominoConfiguration.Instance.ScratchPath, guid, FileName);

                return path;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                // TODO: this is just a quick solution, fix this later
                // take file types from assignment

                var filename = this.FileName;
                var extension = Path.GetExtension(filename);
                var type = FileTypes.GetFileTypeByExtension(extension);

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
                            CodeView.Text = File.ReadAllText(FullPath);
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

        protected void Save_Click(object sender, EventArgs e)
        {
            File.WriteAllText(FullPath, CodeView.Text);
        }

    }
}