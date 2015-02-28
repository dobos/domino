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

        public bool AllowUpload
        {
            get { return (bool)(ViewState["AllowUpload"] ?? true); }
            set { ViewState["AllowUpload"] = value; }
        }

        public bool AllowDownload
        {
            get { return (bool)(ViewState["AllowDownload"] ?? true); }
            set { ViewState["AllowDownload"] = value; }
        }

        public bool AllowDelete
        {
            get { return (bool)(ViewState["AllowDelete"] ?? true); }
            set { ViewState["AllowDelete"] = value; }
        }

        public string ValidationGroup
        {
            get { return (string)ViewState["ValidationGroup"]; }
            set { ViewState["ValidationGroup"] = value; }
        }

        private void RefreshFileList()
        {
            Plugin.Instance.LoadFiles();
            fileList.DataSource = Plugin.Instance.Files.Values;
            fileList.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefreshFileList();

            UploadedFileRequiredValidator.ValidationGroup += ValidationGroup;
            upload.ValidationGroup += ValidationGroup;

            uploadPanel.Visible = AllowUpload;
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

                RefreshFileList();
            }
        }

        protected void FileList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var file = (Lib.File)e.Item.DataItem;

                if (file != null)
                {
                    var name = (Label)e.Item.FindControl("name");
                    var view = (HyperLink)e.Item.FindControl("view");
                    var delete = (LinkButton)e.Item.FindControl("delete");

                    name.Text = file.Name;
                    delete.CommandArgument = file.ID.ToString();

                    view.Visible = AllowDownload;
                    delete.Visible = AllowDelete;
                }
            }
        }

        protected void FileList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            var file = new Lib.File(DatabaseContext);
            file.Load((int)e.Keys[0]);

            file.Delete();

            RefreshFileList();
        }
    }
}