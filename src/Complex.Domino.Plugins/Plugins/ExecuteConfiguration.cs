using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Complex.Domino.Plugins
{
    public class ExecuteConfiguration : ConfigurationSection
    {
        private static ConfigurationPropertyCollection properties;

        private static readonly ConfigurationProperty propPath = new ConfigurationProperty(
            "path", typeof(string), null, ConfigurationPropertyOptions.None);

        static ExecuteConfiguration()
        {
            properties = new ConfigurationPropertyCollection();

            properties.Add(propPath);
        }

        [ConfigurationProperty("path")]
        public string Path
        {
            get { return (string)base[propPath]; }
            set { base[propPath] = value; }
        }
    }
}
