using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;

namespace Complex.Domino.Plugins
{
    public class PluginVirtualPathProvider : VirtualPathProvider
    {
        private static readonly StringComparer comparer = StringComparer.InvariantCultureIgnoreCase;

        private Dictionary<string, string> virtualFiles;

        public PluginVirtualPathProvider()
        {
        }

        private void InitializeMembers()
        {
            this.virtualFiles = new Dictionary<string, string>(comparer);
        }

        public override bool FileExists(string virtualPath)
        {
            var apprelpath = VirtualPathUtility.ToAppRelative(virtualPath);

            if (virtualFiles.ContainsKey(apprelpath))
            {
                return true;
            }

            return base.FileExists(virtualPath);
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            var apprelpath = VirtualPathUtility.ToAppRelative(virtualPath);

            if (virtualFiles.ContainsKey(apprelpath))
            {
                return new PluginVirtualFile(virtualPath, virtualFiles[apprelpath]);
            }

            return base.GetFile(virtualPath);
        }

        public void RegisterVirtualPath(string appRelativePath, string embeddedResourceName)
        {
            virtualFiles.Add(appRelativePath, embeddedResourceName);
        }
    }
}
