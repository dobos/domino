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

        public override Type ControlType
        {
            get { return typeof(DownloadsControl); }
        }

        public override string ControlFileName
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
    }
}
