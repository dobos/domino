using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Complex.Domino.Git
{
    public class GitConfiguration : ConfigurationSection
    {
        private static ConfigurationPropertyCollection properties;

        private static readonly ConfigurationProperty propBinPath = new ConfigurationProperty(
            "binPath", typeof(string), String.Empty, ConfigurationPropertyOptions.None);

        static GitConfiguration()
        {
            properties = new ConfigurationPropertyCollection();

            properties.Add(propBinPath);
        }

        [ConfigurationProperty("binPath")]
        public string BinPath
        {
            get { return (string)base[propBinPath]; }
            set { base[propBinPath] = value; }
        }
    }
}
