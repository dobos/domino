using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Controls
{
    class MultiSelectHelper
    {
        protected const string ViewStateSelectedDataKeys = "SelectedDataKeys";

        private WebControl control;
        private StateBag viewState;
        private HashSet<string> selectedDataKeys;

        public ListSelectionMode SelectionMode
        {
            get { return (ListSelectionMode)(viewState["SelectionMode"] ?? ListSelectionMode.Multiple); }
            set { viewState["SelectionMode"] = value; }
        }

        public HashSet<string> SelectedDataKeys
        {
            get { return selectedDataKeys; }
        }

        public MultiSelectHelper(WebControl control)
        {
            InitializeMembers();

            this.control = control;
        }

        private void InitializeMembers()
        {
            this.selectedDataKeys = new HashSet<string>();
        }

        public void OnInit(StateBag viewState)
        {
            this.viewState = viewState;
        }

        public void OnLoad()
        {
            if (!control.Page.IsPostBack)
            {
                selectedDataKeys = new HashSet<string>();
            }
            else if (control.Visible)
            {
                switch (SelectionMode)
                {
                    case ListSelectionMode.Single:
                        selectedDataKeys = new HashSet<string>();
                        break;
                    case ListSelectionMode.Multiple:
                        selectedDataKeys = (HashSet<string>)(viewState[ViewStateSelectedDataKeys] ?? new HashSet<string>());
                        break;
                }

                SaveSelection();
            }

            viewState[ViewStateSelectedDataKeys] = selectedDataKeys;
        }

        public string GetKey(DataKey key)
        {
            string res = "";

            for (int i = 0; i < key.Values.Count; i++)
            {
                if (i > 0)
                {
                    res += "|";
                }

                res += key.Values[i].ToString();
            }

            return res;
        }

        private Dictionary<string, ICheckBoxControl> GetCheckboxes()
        {
            var checkboxes = new Dictionary<string, ICheckBoxControl>();

            if (control is MultiSelectGridView)
            {
                var gv = (MultiSelectGridView)control;

                foreach (GridViewRow row in gv.Rows)
                {
                    var key = GetKey(gv.DataKeys[row.RowIndex]);
                    var cb = (CheckBox)row.FindControl(SelectionField.DefaultSelectionCheckBoxID);

                    checkboxes.Add(key, cb);
                }
            }
            else if (control is MultiSelectListView)
            {
                var lv = (MultiSelectListView)control;

                foreach (ListViewItem item in lv.Items)
                {
                    var key = GetKey(lv.DataKeys[item.DisplayIndex]);
                    var cb = (ICheckBoxControl)item.FindControl(lv.SelectionCheckboxID);

                    checkboxes.Add(key, cb);
                }
            }
            else
            {
                throw new NotImplementedException();
            }

            return checkboxes;
        }

        public void SelectAll(bool select)
        {
            if (select)
            {
                var checkboxes = GetCheckboxes();

                foreach (var key in checkboxes.Keys)
                {
                    if (!selectedDataKeys.Contains(key))
                    {
                        selectedDataKeys.Add(key);
                    }
                }
            }
            else
            {
                selectedDataKeys.Clear();
            }
        }

        public void SaveSelection()
        {
            var checkboxes = GetCheckboxes();

            foreach (var key in checkboxes.Keys)
            {
                var cb = checkboxes[key];

                if (cb != null)
                {
                    if (cb.Checked && !selectedDataKeys.Contains(key))
                    {
                        selectedDataKeys.Add(key);

                        if (SelectionMode == ListSelectionMode.Single)
                        {
                            break;
                        }
                    }

                    if (!cb.Checked && selectedDataKeys.Contains(key))
                    {
                        selectedDataKeys.Remove(key);
                    }
                }
            }
        }

        public void ApplySelection()
        {
            if (selectedDataKeys != null)
            {
                var checkboxes = GetCheckboxes();

                foreach (var key in checkboxes.Keys)
                {
                    var cb = checkboxes[key];
                    var selected = selectedDataKeys.Contains(key);

                    if (cb != null)
                    {
                        cb.Checked = selected;
                    }
                }

            }
        }

        public void OnPreRender(bool designMode)
        {
            if (!designMode)
            {
                var scriptManager = ScriptManager.GetCurrent(control.Page);
                if (scriptManager != null)
                {
                    if (control is MultiSelectGridView)
                    {
                        scriptManager.RegisterScriptControl((MultiSelectGridView)control);
                    }
                    else if (control is MultiSelectListView)
                    {
                        scriptManager.RegisterScriptControl((MultiSelectListView)control);
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }

                    scriptManager.Scripts.Add(new ScriptReference() { Name = "jquery" });
                }
                else
                {
                    throw new InvalidOperationException("You must have a ScriptManager on the Page.");
                }
            }

            if (((IDataBoundListControl)control).DataKeyNames == null)
            {
                throw new InvalidOperationException("DataKeyNames must be set");
            }
        }

        public void Render(bool designMode)
        {
            if (!designMode)
            {
                ScriptManager.GetCurrent(control.Page).RegisterScriptDescriptors((IScriptControl)control);
            }
        }
    }
}