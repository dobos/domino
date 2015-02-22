using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Hosting;
using System.Reflection;

namespace Complex.Domino.Plugins
{
    public class PluginVirtualFile : VirtualFile
    {
        private string embeddedResourceName;

        public PluginVirtualFile(string virtualPath, string embeddedResourceName)
            : base(virtualPath)
        {
            this.embeddedResourceName = embeddedResourceName;
        }

        public override System.IO.Stream Open()
        {
            var a = Assembly.GetExecutingAssembly();
            return a.GetManifestResourceStream(embeddedResourceName);
        }

    }
}
