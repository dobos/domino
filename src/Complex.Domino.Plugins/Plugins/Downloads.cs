using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complex.Domino.Lib;

namespace Complex.Domino.Plugins
{
    public class Downloads : PluginBase
    {
        public override string Description
        {
            get
            {
                return "Downloads"; // TODO: use resource
            }
        }

        protected override Type ControlType
        {
            get { return typeof(DownloadsControl); }
        }

        protected override string ControlFileName
        {
            get { return "~/Plugins/DownloadsControl.ascx"; }
        }

        public Downloads()
        {
            InitializeMembers();
        }

        public Downloads(Context context)
            : base(context)
        {
            InitializeMembers();
        }

        public Downloads(PluginInstance instance)
            : base(instance)
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
        }

        public override void LoadFromDataReader(System.Data.SqlClient.SqlDataReader reader)
        {
        }

        public override void RegisterVirtualPaths(PluginVirtualPathProvider vpp)
        {
            base.RegisterVirtualPaths(vpp);

            vpp.RegisterVirtualPath(FileList.GetUrl(), GetResourceName(typeof(FileList), ".ascx"));
        }

        protected override void OnLoad(int id)
        {
        }

        protected override void OnCreate()
        {
        }

        protected override void OnModify()
        {
        }

        protected override void OnDelete(int id)
        {
        }
    }
}
