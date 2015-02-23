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
            Assembly a;
            string rname;
            var idx = embeddedResourceName.IndexOf(',');

            if (idx < 0)
            {
                rname = embeddedResourceName;
                a = Assembly.GetExecutingAssembly();
            }
            else
            {
                var aname = new AssemblyName(embeddedResourceName.Substring(idx + 1));
                rname = embeddedResourceName.Substring(0, idx);
                a = Assembly.Load(aname);
            }

            var s = a.GetManifestResourceStream(rname);

            return s;
        }

    }
}
