using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Complex.Domino.Lib;

namespace Complex.Domino.Web.Files
{
    public abstract class FilePageBase : PageBase
    {
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

        protected override void OnLoad(EventArgs e)
        {
            if (!IsPostBack)
            {
                var filename = this.FileName;
                var extension = Path.GetExtension(filename);
                var type = FileTypes.GetFileTypeByExtension(extension);

                UpdateForm(type, filename);
            }

            base.OnLoad(e);
        }

        protected abstract void UpdateForm(FileType fileType, string filename);
    }
}