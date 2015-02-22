using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Domino.Plugins
{
    public abstract class PluginBase
    {
        public abstract string Description { get; }

        public abstract Type ControlType { get; }

        public abstract string ControlFileName { get; }

        public abstract void RegisterVirtualPaths(PluginVirtualPathProvider vpp);
    }
}
