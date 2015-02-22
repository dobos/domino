using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace Complex.Domino.Plugins
{
    public class PluginManager
    {

        public static PluginManager Create()
        {
            return Create(null);
        }

        public static PluginManager Create(string typeName)
        {
            Type type;

            if (typeName != null)
            {
                type = Type.GetType(typeName);
            }
            else
            {
                type = typeof(PluginManager);
            }

            var pf = (PluginManager)Activator.CreateInstance(type);
            
            return pf;
        }

        public virtual IEnumerable<IPlugin> EnumeratePlugins()
        {
            yield return new Downloads();
            yield return new Build();
        }

        public IPlugin GetPlugin(string pluginType)
        {
            return (IPlugin)Activator.CreateInstance(Type.GetType(pluginType));
        }

        public virtual void RegisterPlugins()
        {
            var vpp = new PluginVirtualPathProvider();

            // Register plugins one by one
            foreach (var plugin in EnumeratePlugins())
            {
                plugin.RegisterVirtualPaths(vpp);
            }

            HostingEnvironment.RegisterVirtualPathProvider(vpp);
        }
    }
}
