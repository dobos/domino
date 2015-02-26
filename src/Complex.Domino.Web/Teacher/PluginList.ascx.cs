using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Complex.Domino.Lib;
using Complex.Domino.Plugins;

namespace Complex.Domino.Web.Teacher
{
    public partial class PluginList : UserControlBase
    {
        public int SemesterID
        {
            get { return (int)(ViewState["SemesterID"] ?? -1); }
            set { ViewState["SemesterID"] = value; }
        }

        public int CourseID
        {
            get { return (int)(ViewState["CourseID"] ?? -1); }
            set { ViewState["CourseID"] = value; }
        }

        public int AssignmentID
        {
            get { return (int)(ViewState["AssignmentID"] ?? -1); }
            set { ViewState["AssignmentID"] = value; }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!IsPostBack)
            {
                RefreshPluginList();
            }

            base.OnLoad(e);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            
        }

        protected void PluginType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(PluginType.SelectedValue))
            {
                // TODO: test if this type of plugin already exists

                var pm = new PluginManager(DatabaseContext);

                var pp = pm.GetPlugin(PluginType.SelectedValue);

                pp.Instance.SemesterID = SemesterID;
                pp.Instance.CourseID = CourseID;
                pp.Instance.AssignmentID = AssignmentID;

                pp.Save();

                CreatePluginControls();
            }
        }

        protected void Plugins_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem && e.Item.DataItem != null)
            {
                var ph = e.Item.FindControl("pluginControlPlaceholder");

                var pi = (PluginInstance)e.Item.DataItem;
                pi.Context = DatabaseContext;       // TODO: this could be set by the factory...

                var pp = pi.GetPlugin();
                var control = pp.LoadControl(this);
                control.ID = "pluginControl";
                ph.Controls.Add(control);
            }
        }

        private void RefreshPluginList()
        {
            var pm = new PluginManager(DatabaseContext);

            PluginType.Items.Clear();
            PluginType.Items.Add(new ListItem(Resources.Labels.SelectPlugin, ""));

            foreach (var plugin in pm.EnumeratePlugins())
            {
                PluginType.Items.Add(new ListItem(plugin.Description, plugin.TypeName));
            }
        }

        public void CreatePluginControls()
        {
            var pf = new PluginInstanceFactory(DatabaseContext)
            {
                SemesterID = SemesterID,
                CourseID = CourseID,
                AssignmentID = AssignmentID,
            };

            plugins.DataSource = pf.Find();
            plugins.DataBind();
        }

        public void Save()
        {
            foreach (var item in plugins.Items)
            {
                if (item.ItemType == ListViewItemType.DataItem)
                {
                    var ph = item.FindControl("pluginControlPlaceholder");
                    var control = (IPluginControl)ph.FindControl("pluginControl");

                    control.SaveForm();
                    control.Plugin.Save();
                }
            }
        }

    }
}