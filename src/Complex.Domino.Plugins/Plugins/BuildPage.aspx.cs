using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Complex.Domino.Lib;
using Complex.Domino.Web;

namespace Complex.Domino.Plugins
{
    public partial class BuildPage : EntityPage<Submission>
    {
        public static string GetUrl()
        {
            return "~/Plugins/BuildPage.aspx";
        }

        public static string GetUrl(int submissionID)
        {
            return String.Format("~/Plugins/BuildPage.aspx?ID={0}", submissionID);
        }

        private Build plugin;

        protected Build Plugin
        {
            get { return plugin; }
        }

        protected override void CreateItem()
        {
            base.CreateItem();

            var pf = new PluginInstanceFactory(DatabaseContext)
            {
                Name = new Build().TypeName,
                SemesterID = Item.SemesterID,
                CourseID = Item.CourseID,
                AssignmentID = Item.AssignmentID,
            };

            var pi = pf.Find().FirstOrDefault();

            // TODO: move this to factory
            pi.Context = DatabaseContext;

            plugin = (Build)pi.GetPlugin();
        }

        protected override void UpdateForm()
        {
            base.UpdateForm();

            CommandLine.Text = Plugin.CommandLine;
        }

        protected void Ok_Click(object sender, EventArgs e)
        {
            
        }
    }
}