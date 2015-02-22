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
            get;
        }

        public string CommandLine { get; set; }

        public void RegisterVirtualPaths(PluginVirtualPathProvider vpp)
        {
        }
    }
}
