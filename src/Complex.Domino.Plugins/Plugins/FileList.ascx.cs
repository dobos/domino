using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web
{
    public partial class FileList : UserControlBase
    {
        public int PluginID
        {
            get { return (int)(ViewState["PluginID"] ?? -1); }
            set { ViewState["PluginID"] = value; }
        }
    }
}