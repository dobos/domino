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
    public partial class TextEditor : FilePageBase
    {
        public static string GetUrl(string fileName, bool edit)
        {
            var url = "~/Files/TextEditor.aspx";

            var par = "";

            if (edit)
            {
                par += "&edit=true";
            }

            if (fileName != null)
            {
                par += String.Format("&file={0}", HttpUtility.UrlEncode(fileName.Replace('\\', '/')));
            }

            if (par.Length > 0)
            {
                url += "?" + par.Substring(1);
            }

            return url;
        }

        protected bool Edit
        {
            get { return Request.QueryString[Constants.RequestEdit] == "true"; }
        }

        protected override void UpdateForm(FileType fileType, string filename)
        {
            if (fileType == null || fileType.Category != FileCategory.Code)
            {
                throw new InvalidOperationException();
            }

            CodeView.Text = File.ReadAllText(FullPath);
            CodeView.Mode = fileType.MimeType;

            Save.Visible = Edit;
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            File.WriteAllText(FullPath, CodeView.Text);
        }

    }
}