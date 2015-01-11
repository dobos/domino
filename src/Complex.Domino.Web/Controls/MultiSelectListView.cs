using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

[assembly: WebResource("Complex.Domino.Web.Controls.MultiSelectListView.js", "text/javascript", PerformSubstitution = true)]

namespace Complex.Domino.Web.Controls
{
    /// <summary>
    /// Summary description for MultiselectListView
    /// </summary>
    public class MultiSelectListView : ListView, IScriptControl
    {
        private MultiSelectHelper helper;

        public string SelectionCheckboxID
        {
            get { return helper.SelectionCheckboxID; }
            set { helper.SelectionCheckboxID = value; }
        }

        public ListSelectionMode SelectionMode
        {
            get { return helper.SelectionMode; }
            set { helper.SelectionMode = value; }
        }

        public string SelectionElementID
        {
            get { return helper.SelectionElementID; }
            set { helper.SelectionElementID = value; }
        }

        [Themeable(true)]
        public string CssClassSelected
        {
            get { return helper.CssClassSelected; }
            set { helper.CssClassSelected = value; }
        }

        public HashSet<string> SelectedDataKeys
        {
            get { return helper.SelectedDataKeys; }
        }

        public MultiSelectListView()
        {
            this.helper = new MultiSelectHelper(this);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            helper.OnInit(this.ViewState);
        }
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            helper.OnLoad();
        }

        protected override void OnPreRender(EventArgs e)
        {
            helper.OnPreRender(DesignMode);

            base.OnPreRender(e);

            helper.ApplySelection();
        }

        public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            var descriptor = new ScriptControlDescriptor("Graywulf.MultiSelectListView", this.UniqueID);
            descriptor.AddProperty("SelectionMode", this.SelectionMode);
            descriptor.AddProperty("SelectionCheckboxID", this.SelectionCheckboxID);
            descriptor.AddProperty("SelectionElementID", this.SelectionElementID);
            descriptor.AddProperty("CssClassSelected", this.CssClassSelected);

            yield return descriptor;
        }

        public IEnumerable<ScriptReference> GetScriptReferences()
        {
            yield return new ScriptReference("Herschel.Ws.Controls.MultiSelectListView.js", "Herschel.Ws");
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (!this.DesignMode)
            {
                ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);
            }

            writer.AddAttribute("id", this.UniqueID);
            writer.RenderBeginTag("div");

            base.Render(writer);

            writer.RenderEndTag();
        }
    }
}