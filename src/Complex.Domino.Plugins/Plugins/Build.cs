using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complex.Domino.Lib;

namespace Complex.Domino.Plugins
{
    public class Build : IAssignmentPlugin
    {
        public static BuildConfiguration Configuration
        {
            get { return null; }
        }

        public string CommandLine { get; set; }

        public string Description
        {
            get { return "Build"; }
        }

        public Type ControlType
        {
            get { return typeof(BuildControl); }
        }

        public string ControlFileName
        {
            get { return "~/Plugins/BuildControl.ascx"; }
        }

        public void RegisterVirtualPaths(PluginVirtualPathProvider vpp)
        {
            vpp.RegisterVirtualPath(ControlFileName, typeof(Complex.Domino.Plugins.BuildControl).FullName + ".ascx");
        }
    }
}
