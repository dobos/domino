using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Domino.Plugins
{
    public abstract class PluginControlBase<T> : Web.UserControlBase, IPluginControl
        where T : PluginBase
    {
        private T plugin;

        public T Plugin
        {
            get { return plugin; }
            set { plugin = value; }
        }

        PluginBase IPluginControl.Plugin
        {
            get { return plugin; }
            set { plugin = (T)value; }
        }

        protected PluginControlBase()
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.plugin = null;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                UpdateForm();
            }
        }

        protected abstract void UpdateForm();

        protected abstract void SaveForm();
    }
}
