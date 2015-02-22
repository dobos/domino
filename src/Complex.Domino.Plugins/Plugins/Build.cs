using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complex.Domino.Lib;

namespace Complex.Domino.Plugins
{
    public class Build : PluginBase
    {
        public static BuildConfiguration Configuration
        {
            get { return null; }
        }

        public override string Description
        {
            get { return "Build"; }
        }

        public override Type ControlType
        {
            get { return typeof(BuildControl); }
        }

        public override string ControlFileName
        {
            get { return "~/Plugins/BuildControl.ascx"; }
        }

        public string CommandLine { get; set; }

        public override void RegisterVirtualPaths(PluginVirtualPathProvider vpp)
        {
            vpp.RegisterVirtualPath(ControlFileName, typeof(Complex.Domino.Plugins.BuildControl).FullName + ".ascx");
        }
    }
}
