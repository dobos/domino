using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Complex.Domino.Web;

namespace Complex.Domino.Plugins
{
    public partial class FileList : UserControlBase
    {
        public static string GetUrl()
        {
            return "~/Plugins/FileList.ascx";
        }

        private PluginBase plugin;

        public PluginBase Plugin
        {
            get { return plugin; }
            set { plugin = value; }
        }

        protected void Upload_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                var file = new Lib.File(DatabaseContext)
                {
                    SemesterID = Plugin.Instance.SemesterID,
                    CourseID = Plugin.Instance.CourseID,
                    AssignmentID = Plugin.Instance.AssignmentID,
                    PluginInstanceID = Plugin.Instance.ID,
                    Name = Path.GetFileName(UploadedFile.PostedFile.FileName),
                    MimeType = UploadedFile.PostedFile.ContentType,
                };

                file.Save();

                // TODO: save data
                file.WriteData(UploadedFile.PostedFile.InputStream);
            }
        }
    }
}