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
        public static string GetUrl()
        {
            return "~/Plugins/BuildControl.ascx";
        }

        protected override void UpdateForm()
        {
            commandLine.Text = Plugin.CommandLine;
            executeLink.NavigateUrl = BuildPage.GetUrl(this.Plugin.Instance.SubmissionID);

            adminView.Visible = Mode == PluginMode.Edit && (View == PluginView.Admin || View == PluginView.Teacher);
            executeView.Visible = Mode == PluginMode.View && (View == PluginView.Admin || View == PluginView.Teacher);
        }

        public override void SaveForm()
        {
            Plugin.CommandLine = commandLine.Text;
        }
    }
}