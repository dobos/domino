using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Complex.Domino.Plugins
{
    public class BuildConfiguration : ConfigurationSection
    {
        private static ConfigurationPropertyCollection properties;

        private static readonly ConfigurationProperty propPath = new ConfigurationProperty(
            "path", typeof(string), null, ConfigurationPropertyOptions.None);

        private static readonly ConfigurationProperty propCompilerC = new ConfigurationProperty(
            "compilerC", typeof(string), String.Empty, ConfigurationPropertyOptions.None);

        private static readonly ConfigurationProperty propCompilerCpp = new ConfigurationProperty(
            "compilerCpp", typeof(string), String.Empty, ConfigurationPropertyOptions.None);

        private static readonly ConfigurationProperty propCompilerJava = new ConfigurationProperty(
            "compilerJava", typeof(string), String.Empty, ConfigurationPropertyOptions.None);

        static BuildConfiguration()
        {
            properties = new ConfigurationPropertyCollection();

            properties.Add(propPath);
            properties.Add(propCompilerC);
            properties.Add(propCompilerCpp);
            properties.Add(propCompilerJava);
        }

        [ConfigurationProperty("path")]
        public string Path
        {
            get { return (string)base[propPath]; }
            set { base[propPath] = value; }
        }

        [ConfigurationProperty("compilerC")]
        public string CompilerC
        {
            get { return (string)base[propCompilerC]; }
            set { base[propCompilerC] = value; }
        }

        [ConfigurationProperty("compilerCpp")]
        public string CompilerCpp
        {
            get { return (string)base[propCompilerCpp]; }
            set { base[propCompilerCpp] = value; }
        }

        [ConfigurationProperty("compilerJava")]
        public string CompilerJava
        {
            get { return (string)base[propCompilerJava]; }
            set { base[propCompilerJava] = value; }
        }
    }
}
