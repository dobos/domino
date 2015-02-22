using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace Complex.Domino.Plugins
{
    public class PluginFactory
    {

        public static PluginFactory Create(string typeName)
        {
            Type type;

            if (typeName == null)
            {
                type = Type.GetType(typeName);
            }
            else
            {
                type = typeof(PluginFactory);
            }

            var pf = (PluginFactory)Activator.CreateInstance(type);
            
            return pf;
        }

        protected virtual IEnumerable<IPlugin> CreatePlugins()
        {
            yield return new Build();
        }

        public virtual void RegisterPlugins()
        {
            var vpp = new PluginVirtualPathProvider();

            // Register plugins one by one
            foreach (var plugin in CreatePlugins())
            {
                plugin.RegisterVirtualPaths(vpp);
            }

            HostingEnvironment.RegisterVirtualPathProvider(new PluginVirtualPathProvider());
        }
    }
}
