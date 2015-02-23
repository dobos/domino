using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complex.Domino.Lib;

namespace Complex.Domino.Plugins
{
    public abstract class PluginBase : ContextObject
    {
        private Plugin instance;

        public Plugin Instance
        {
            get { return instance; }
        }

        public string TypeName
        {
            get
            {
                var type = GetType();
                var name = String.Format("{0}, {1}", type.FullName, type.Assembly.GetName().Name);

                return name;
            }
        }

        public abstract Type ControlType { get; }

        public abstract string ControlFileName { get; }

        public abstract string Description { get; }

        protected PluginBase()
        {
            InitializeMembers();
        }

        protected PluginBase(Context context)
            : base(context)
        {
            InitializeMembers();
        }

        protected PluginBase(Plugin instance)
            :base(instance.Context)
        {
            this.instance = instance;
        }

        private void InitializeMembers()
        {
            instance = new Plugin(Context)
            {
                Name = TypeName,
                Description = Description
            };
        }

        public virtual void RegisterVirtualPaths(PluginVirtualPathProvider vpp)
        {
            var cname = ControlType.FullName;
            var aname = ControlType.Assembly.GetName().Name;

            vpp.RegisterVirtualPath(ControlFileName, cname + ".ascx, " + aname);
        }

        public void Load()
        {
        }

        public void Load(int id)
        {
        }

        public void Save()
        {
            var isExisting = instance.IsExisting;

            instance.Save();

            if (isExisting)
            {
            }
            else
            {
            }
        }
    }
}
