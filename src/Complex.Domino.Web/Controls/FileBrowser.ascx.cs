using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Complex.Domino.Web.Controls
{
    public partial class FileBrowser : System.Web.UI.UserControl
    {
        private string basePath;

        public string BasePath
        {
            get { return basePath; }
            set { basePath = value; }
        }

        public string RelativePath
        {
            get { return (string)ViewState["RelativePath"] ?? ""; }
            set { ViewState["RelativePath"] = value; }
        }

        public bool ShowDirectories
        {
            get { return (bool)(ViewState["ShowDirectories"] ?? true); }
            set { ViewState["ShowDirectories"] = value; }
        }

        public bool ShowHidden
        {
            get { return (bool)(ViewState["ShowHidden"] ?? false); }
            set { ViewState["ShowHidden"] = value; }
        }

        public string AllowedExtensions
        {
            get { return (string)ViewState["AllowedExtensions"] ?? ""; }
            set { ViewState["AllowedExtensions"] = value; }
        }

        public string AllowedArchiveExtensions
        {
            get { return (string)ViewState["AllowedArchiveExtensions"] ?? ".zip|.tar.gz"; }
            set { ViewState["AllowedArchiveExtensions"] = value; }
        }

        public bool AllowSelection
        {
            get { return (bool)(ViewState["AllowSelection"] ?? true); }
            set { ViewState["AllowSelection"] = value; }
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

        public bool AllowEdit
        {
            get { return (bool)(ViewState["AllowEdit"] ?? true); }
            set { ViewState["AllowEdit"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            uploadPanel.Visible = AllowUpload;
        }

        protected void directoryList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var di = (DirectoryInfo)((ListViewDataItem)e.Item).DataItem;

                if (di != null)
                {
                    var name = (LinkButton)e.Item.FindControl("name");

                    var relpath = MakePathRelative(BasePath, di.FullName);

                    if (relpath == String.Empty)
                    {
                        name.Text = "~";
                    }
                    else
                    {
                        name.Text = di.Name;
                    }

                    name.CommandArgument = relpath;
                }
            }
        }

        protected void directoryList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var dir = (string)e.CommandArgument;
            this.RelativePath = dir;
        }

        protected void fileList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var fi = (FileInfo)((ListViewDataItem)e.Item).DataItem;

                if (fi != null)
                {
                    var dir = (fi.Attributes & FileAttributes.Directory) != 0;

                    var select = (CheckBox)e.Item.FindControl(fileList.SelectionCheckboxID);
                    var icon = (Image)e.Item.FindControl("icon");
                    var name = (LinkButton)e.Item.FindControl("name");
                    var size = (Label)e.Item.FindControl("size");
                    var delete = (LinkButton)e.Item.FindControl("delete");
                    var download = (LinkButton)e.Item.FindControl("download");
                    var view = (LinkButton)e.Item.FindControl("view");
                    var edit = (LinkButton)e.Item.FindControl("edit");

                    select.Visible = AllowSelection;

                    name.Text = fi.Name;
                    name.CommandArgument = fi.Name;

                    if ((fi.Attributes & FileAttributes.Directory) == 0)
                    {
                        size.Text = fi.Length.ToString();
                    }

                    delete.Visible = AllowDelete;
                    delete.CommandArgument = fi.Name;

                    download.Visible = AllowDownload;
                    download.CommandArgument = fi.Name;

                    view.Visible = !dir;
                    view.CommandArgument = fi.Name;

                    edit.Visible = !dir && AllowEdit;
                    edit.CommandArgument = fi.Name;
                }
            }
        }

        protected void fileList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            var filename = (string)e.Keys[0];

            var fi = new FileInfo(Path.Combine(BasePath, RelativePath, filename));

            // TODO: check if it's inside base

            if ((fi.Attributes & FileAttributes.Directory) != 0)
            {
                Complex.Domino.Util.IO.ForceDeleteDirectory(fi.FullName);
            }
            else
            {
                fi.Delete();
            }
        }

        protected void fileList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var filename = (string)e.CommandArgument;
            var path = Path.Combine(BasePath, RelativePath, filename);

            // If it's a directory, handle event and change current dir
            var fi = new FileInfo(path);

            if ((fi.Attributes & FileAttributes.Directory) != 0)
            {
                this.RelativePath = Path.Combine(this.RelativePath, filename);
            }
            else
            {
                // Propage event to control
                // TODO
            }
        }

        protected void Upload_Click(object sender, EventArgs e)
        {
            var filename = Path.GetFileName(UploadedFile.PostedFile.FileName);

            bool archive;
            string archiveExtension;

            VerifyUploadedFile(filename, out archive, out archiveExtension);

            // If not an archive, simply save file to directory
            if (!archive)
            {
                UploadedFile.PostedFile.SaveAs(Path.Combine(BasePath, RelativePath, filename));
            }
            else
            {
                // Extract files from archive
                throw new NotImplementedException();
            }
        }

        private DirectoryInfo[] GetDirectories()
        {
            var parts = RelativePath.Split(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);

            var dirs = new DirectoryInfo[parts.Length + 1];
            var dir = BasePath;

            dirs[0] = new DirectoryInfo(dir);

            for (int i = 0; i < parts.Length; i++)
            {
                dir = Path.Combine(dir, parts[i]);
                dirs[i + 1] = new DirectoryInfo(dir);
            }

            return dirs;
        }

        private FileInfo[] GetFiles()
        {
            var extensions = new HashSet<string>(
                AllowedExtensions.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries),
                StringComparer.InvariantCultureIgnoreCase);

            var dir = Path.Combine(BasePath, RelativePath);
            var entries = Directory.GetFileSystemEntries(dir);

            var files = new List<FileInfo>();

            foreach (var entry in entries)
            {
                var fi = new FileInfo(entry);

                // Filter out directories, if necessary
                if ((fi.Attributes & FileAttributes.Directory) != 0 && !ShowDirectories)
                {
                    continue;
                }

                // Filter out hidden files, if necessary
                if (((fi.Attributes & FileAttributes.Hidden) != 0 || fi.Name.StartsWith("."))
                    && !ShowHidden)
                {
                    continue;
                }

                // Filter out files with invalid extension
                if ((fi.Attributes & FileAttributes.Directory) == 0 &&
                    extensions.Count > 0 &&
                    !extensions.Contains(fi.Extension))
                {
                    continue;
                }

                files.Add(fi);
            }

            return files.ToArray();
        }

        protected override void OnLoad(EventArgs e)
        {
            DataBindAll();
                
            base.OnLoad(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            DataBindAll();
        }

        private void DataBindAll()
        {
            if (directoryList.Visible)
            {
                directoryList.DataSource = GetDirectories();
                directoryList.DataBind();
            }

            if (fileList.Visible)
            {
                fileList.DataSource = GetFiles();
                fileList.DataBind();
            }
        }

        private string MakePathRelative(string basePath, string path)
        {
            basePath = Path.GetFullPath(basePath);
            path = Path.GetFullPath(path);

            if (!path.StartsWith(basePath, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new ArgumentException("Path is outside base directory");  // TODO
            }

            if (path.Length < basePath.Length)
            {
                throw new ArgumentException("Cannot reference directory outside base path");    // TODO
            }

            var relpath = path.Substring(basePath.Length).TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

            return relpath;
        }

        private void VerifyUploadedFile(string filename, out bool archive, out string archiveExtension)
        {
            var extensions = new HashSet<string>(
                AllowedExtensions.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries),
                StringComparer.InvariantCultureIgnoreCase);

            var archives = new HashSet<string>(
                AllowedArchiveExtensions.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries),
                StringComparer.InvariantCultureIgnoreCase);

            // Certain extensions might contain dots so we need this loop
            archive = false;
            archiveExtension = null;
            var found = extensions.Count == 0;

            foreach (var ext in extensions)
            {
                if (filename.EndsWith(ext, StringComparison.InvariantCultureIgnoreCase))
                {
                    found = true;
                    break;
                }
            }

            foreach (var ext in archives)
            {
                if (filename.EndsWith(ext, StringComparison.InvariantCultureIgnoreCase))
                {
                    found = true;
                    archive = true;
                    archiveExtension = ext;
                    break;
                }
            }

            if (!found)
            {
                throw new Exception("Invalid file format.");        // TODO
            }
        }
    }
}