using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Complex.Domino.Web.Controls;

[assembly: WebResource("Complex.Domino.Web.Controls.MultiSelectGridView.js", "text/javascript", PerformSubstitution = true)]

namespace Complex.Domino.Web.Controls
{
    public class MultiSelectGridView : GridView, IScriptControl
    {
        private MultiSelectHelper helper;
        private int selectionFieldIndex = -1;
        private CheckBox selectAllCheckbox;

        public ListSelectionMode SelectionMode
        {
            get { return helper.SelectionMode; }
            set { helper.SelectionMode = value; }
        }

        public HashSet<string> SelectedDataKeys
        {
            get { return helper.SelectedDataKeys; }
        }

        public MultiSelectGridView()
        {
            this.helper = new MultiSelectHelper(this);
        }

        private string GetKey(DataKey key)
        {
            return helper.GetKey(key);
        }

        protected override System.Collections.ICollection CreateColumns(PagedDataSource dataSource, bool useDataSource)
        {
            var columns = base.CreateColumns(dataSource, useDataSource);

            // Look for duplicate selection fields
            var found = false;

            int i = 0;
            foreach (DataControlField col in columns)
            {
                if (col is SelectionField)
                {
                    if (found)
                    {
                        throw new InvalidOperationException("MultiSelectGridView can contain one selection column only.");
                    }
                    selectionFieldIndex = i;
                    found = true;
                }

                i++;
            }

            return columns;
        }

        protected override void OnRowCreated(GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header && selectionFieldIndex != -1 && SelectionMode == ListSelectionMode.Multiple)
            {
                selectAllCheckbox = new CheckBox()
                {
                    ID = "SelectAllCheckBox",
                    AutoPostBack = true,
                    CausesValidation = false
                };

                selectAllCheckbox.CheckedChanged += SelectAllCheckBox_CheckedChanged;

                e.Row.Cells[selectionFieldIndex].Controls.Add(selectAllCheckbox);
            }

            base.OnRowCreated(e);
        }

        void SelectAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            helper.SelectAll(selectAllCheckbox.Checked);
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
            if (!this.DesignMode)
            {
                if (selectAllCheckbox != null)
                {
                    selectAllCheckbox.Checked = SelectedDataKeys.Count > 0;
                }
            }

            helper.OnPreRender(DesignMode);

            base.OnPreRender(e);

            helper.ApplySelection();
        }

        public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            if (Rows.Count > 0)
            {
                var descriptor = new ScriptControlDescriptor("Graywulf.MultiSelectGridView", this.ClientID);
                descriptor.AddProperty("SelectionMode", this.SelectionMode);
                descriptor.AddProperty("SelectionCheckboxID", SelectionField.DefaultSelectionCheckBoxID);
                descriptor.AddProperty("CssClass", this.CssClass);
                descriptor.AddProperty("SelectedRowCssClass", this.SelectedRowStyle.CssClass);

                yield return descriptor;
            }
            else
            {
                yield break;
            }
        }

        public IEnumerable<ScriptReference> GetScriptReferences()
        {
            if (Rows.Count > 0)
            {
                yield return new ScriptReference(typeof(MultiSelectGridView).FullName + ".js", typeof(MultiSelectGridView).Assembly.GetName().Name);
            }
            else
            {
                yield break;
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            helper.Render(DesignMode);
            
            base.Render(writer);
        }
    }
}