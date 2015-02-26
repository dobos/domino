using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Plugins
{
    public partial class BuildControl : PluginControlBase<Build>
    {

        protected override void UpdateForm()
        {
            commandLine.Text = Plugin.CommandLine;
        }

        public override void SaveForm()
        {
            Plugin.CommandLine = commandLine.Text;
        }
    }
}