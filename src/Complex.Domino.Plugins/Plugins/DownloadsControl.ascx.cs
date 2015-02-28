using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Complex.Domino.Plugins
{
    public partial class DownloadsControl : PluginControlBase<Downloads>
    {
        protected override void CreateItem()
        {
            base.CreateItem();

            fileList.Plugin = Plugin;
        }

        protected override void UpdateForm()
        {
            fileList.AllowUpload = Mode == PluginMode.Edit;
            fileList.AllowDelete = Mode == PluginMode.Edit;
        }

        public override void SaveForm()
        {
        }
    }
}