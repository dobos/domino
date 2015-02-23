using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Configuration;
using Complex.Domino.Lib;

namespace Complex.Domino.Plugins
{
    public class PluginManager : ContextObject
    {
        public PluginManager()
        {
        }

        public PluginManager(Context context)
            : base(context)
        {
        }

        public virtual IEnumerable<PluginBase> EnumeratePlugins()
        {
            // TODO: load plugins from config
            var plugins = ConfigurationManager.GetSection("complex.domino/plugins/registeredPlugins") as NameValueCollection;

            foreach (string plugin in plugins.Keys)
            {
                yield return GetPlugin(plugins[plugin]);
            }
        }

        public PluginBase GetPlugin(string pluginType)
        {
            PluginBase plugin;
            var type = Type.GetType(pluginType);

            if (Context == null)
            {
                plugin = (PluginBase)Activator.CreateInstance(type);

            }
            else
            {
                plugin = (PluginBase)Activator.CreateInstance(type, Context);
            }

            return plugin;
        }

        public PluginBase GetPlugin(Plugin instance)
        {
            var type = Type.GetType(instance.Name);
            var plugin = (PluginBase)Activator.CreateInstance(type, instance);

            return plugin;
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
