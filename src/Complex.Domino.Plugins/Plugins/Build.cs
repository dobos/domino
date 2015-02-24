using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complex.Domino.Lib;

namespace Complex.Domino.Plugins
{
    public class Build : PluginBase
    {
        public static BuildConfiguration Configuration
        {
            get { return null; }
        }

        private string commandLine;

        public string CommandLine
        {
            get { return commandLine; }
            set { commandLine = value; }
        }

        public override string Description
        {
            get {
                return "Build"; // TODO: use resource
            }
        }

        public override Type ControlType
        {
            get { return typeof(BuildControl); }
        }

        public override string ControlFileName
        {
            get { return "~/Plugins/BuildControl.ascx"; }
        }

        public Build()
        {
            InitializeMembers();
        }

        public Build(Context context)
            : base(context)
        {
            InitializeMembers();
        }

        public Build(PluginInstance instance)
            : base(instance)
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.commandLine = null;
        }
    }
}
