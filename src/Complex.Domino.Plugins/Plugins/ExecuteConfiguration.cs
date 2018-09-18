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

        private static readonly ConfigurationProperty propBash = new ConfigurationProperty(
            "bash", typeof(string), String.Empty, ConfigurationPropertyOptions.None);

        static ExecuteConfiguration()
        {
            properties = new ConfigurationPropertyCollection();

            properties.Add(propPath);
            properties.Add(propBash);
        }

        [ConfigurationProperty("path")]
        public string Path
        {
            get { return (string)base[propPath]; }
            set { base[propPath] = value; }
        }

        [ConfigurationProperty("bash")]
        public string Bash
        {
            get { return (string)base[propBash]; }
            set { base[propBash] = value; }
        }
    }
}
