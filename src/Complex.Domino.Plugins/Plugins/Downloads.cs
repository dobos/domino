using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Domino.Plugins
{
    public class Downloads : IPlugin
    {
        public string Description
        {
            get { return "Downloads"; }
        }

        public Type ControlType
        {
            get { return typeof(DownloadsControl); }
        }

        public string ControlFileName
        {
            get { return "~/Plugins/DownloadsControl.ascx"; }
        }

        public void RegisterVirtualPaths(PluginVirtualPathProvider vpp)
        {
            vpp.RegisterVirtualPath(ControlFileName, typeof(Complex.Domino.Plugins.DownloadsControl).FullName + ".ascx");
        }
    }
}
