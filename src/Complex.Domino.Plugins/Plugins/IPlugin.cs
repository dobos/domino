using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Domino.Plugins
{
    public interface IPlugin
    {
        string Description { get; }

        Type ControlType { get; }
        
        string ControlFileName { get; }

        void RegisterVirtualPaths(PluginVirtualPathProvider vpp);
    }
}
