using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Domino.Plugins
{
    public class Downloads : PluginBase
    {
        public override string Description
        {
            get { return "Downloads"; }
        }

        public override Type ControlType
        {
            get { return typeof(DownloadsControl); }
        }

        public override string ControlFileName
        {
            get { return "~/Plugins/DownloadsControl.ascx"; }
        }

        public override void RegisterVirtualPaths(PluginVirtualPathProvider vpp)
        {
            vpp.RegisterVirtualPath(ControlFileName, typeof(Complex.Domino.Plugins.DownloadsControl).FullName + ".ascx");
        }
    }
}
