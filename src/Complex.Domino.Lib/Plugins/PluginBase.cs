using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Complex.Domino.Lib;

namespace Complex.Domino.Plugins
{
    public abstract class PluginBase : ContextObject, IDatabaseTableObject
    {
        private PluginInstance instance;

        public PluginInstance Instance
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

        protected abstract Type ControlType { get; }

        protected abstract string ControlFileName { get; }

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

        protected PluginBase(PluginInstance instance)
            :base(instance.Context)
        {
            this.instance = instance;
        }

        private void InitializeMembers()
        {
            instance = new PluginInstance(Context)
            {
                Name = TypeName,
                Description = Description
            };
        }

        public abstract void LoadFromDataReader(SqlDataReader reader);

        public virtual void RegisterVirtualPaths(PluginVirtualPathProvider vpp)
        {
            var cname = ControlType.FullName;
            var aname = ControlType.Assembly.GetName().Name;

            vpp.RegisterVirtualPath(ControlFileName, cname + ".ascx, " + aname);
        }

        public IPluginControl LoadControl(System.Web.UI.UserControl parent)
        {
            var control = (IPluginControl)parent.LoadControl(ControlFileName);

            control.Plugin = this;

            return control;
        }

        public void Load()
        {
            Load(instance.ID);
        }

        public void Load(int id)
        {
            OnLoad(id);
        }

        public void Save()
        {
            var isExisting = instance.IsExisting;

            instance.Save();

            if (!isExisting)
            {
                OnCreate();
            }
            else
            {
                OnModify();
            }
        }

        public void Delete()
        {
            Delete(instance.ID);
        }

        public void Delete(int id)
        {
            OnDelete(id);
        }

        protected abstract void OnLoad(int id);

        protected abstract void OnCreate();

        protected abstract void OnModify();

        protected abstract void OnDelete(int id);
    }
}
