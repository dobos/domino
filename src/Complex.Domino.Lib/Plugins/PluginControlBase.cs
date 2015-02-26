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
        private PluginView view;
        private PluginMode mode;

        public T Plugin
        {
            get { return plugin; }
            set { plugin = value; }
        }

        public PluginView View
        {
            get { return (PluginView)ViewState["View"]; }
            set { ViewState["View"] = value; }
        }

        public PluginMode Mode
        {
            get { return (PluginMode)ViewState["Mode"]; }
            set { ViewState["Mode"] = value; }
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

        public abstract void SaveForm();
    }
}
