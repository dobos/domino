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
        private class DataItem
        {
            public string Key { get; set; }
            public ICheckBoxControl CheckBox { get; set; }
            public Control SelectionElement { get; set; }
        }

        protected const string ViewStateSelectedDataKeys = "SelectedDataKeys";
        protected const string DefaultSelectionCheckboxID = "selectionCheckbox";
        protected const string DefaultSelectionElementID = "selectionElement";

        private WebControl control;
        private StateBag viewState;
        private HashSet<string> selectedDataKeys;

        public ListSelectionMode SelectionMode
        {
            get { return (ListSelectionMode)(viewState["SelectionMode"] ?? ListSelectionMode.Multiple); }
            set { viewState["SelectionMode"] = value; }
        }

        public string SelectionCheckboxID
        {
            get { return (string)(viewState["SelectionCheckboxID"] ?? DefaultSelectionCheckboxID); }
            set { viewState["SelectionCheckboxID"] = value; }
        }

        public string SelectionElementID
        {
            get { return (string)(viewState["SelectionElementID"] ?? DefaultSelectionElementID); }
            set { viewState["SelectionElementID"] = value; }
        }

        public string CssClassSelected
        {
            get { return (string)(viewState["CssClassSelected"]); }
            set { viewState["CssClassSelected"] = value; }
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

        private Dictionary<string, DataItem> GetDataItems()
        {
            var items = new Dictionary<string, DataItem>();

            if (control is MultiSelectGridView)
            {
                var gv = (MultiSelectGridView)control;

                foreach (GridViewRow row in gv.Rows)
                {
                    var key = GetKey(gv.DataKeys[row.RowIndex]);
                    var cb = (CheckBox)row.FindControl(SelectionField.DefaultSelectionCheckBoxID);

                    var item = new DataItem()
                    {
                        Key = key,
                        CheckBox = cb,
                    };

                    items.Add(key, item);
                }
            }
            else if (control is MultiSelectListView)
            {
                var lv = (MultiSelectListView)control;

                foreach (ListViewItem li in lv.Items)
                {
                    var key = GetKey(lv.DataKeys[li.DisplayIndex]);
                    var cb = (ICheckBoxControl)li.FindControl(lv.SelectionCheckboxID);
                    var se = li.FindControl(SelectionElementID);

                    var item = new DataItem()
                    {
                        Key = key,
                        CheckBox = cb,
                        SelectionElement = se,
                    };

                    items.Add(key, item);
                }
            }
            else
            {
                throw new NotImplementedException();
            }

            return items;
        }

        public void SelectAll(bool select)
        {
            if (select)
            {
                var items = GetDataItems();

                foreach (var key in items.Keys)
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
            var items = GetDataItems();

            foreach (var key in items.Keys)
            {
                var item = items[key];

                if (item.CheckBox != null)
                {
                    if (item.CheckBox.Checked && !selectedDataKeys.Contains(key))
                    {
                        selectedDataKeys.Add(key);

                        if (SelectionMode == ListSelectionMode.Single)
                        {
                            break;
                        }
                    }

                    if (!item.CheckBox.Checked && selectedDataKeys.Contains(key))
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
                var items = GetDataItems();

                foreach (var key in items.Keys)
                {
                    var item = items[key];
                    var selected = selectedDataKeys.Contains(key);

                    if (item.CheckBox != null)
                    {
                        item.CheckBox.Checked = selected;
                    }

                    var se = item.SelectionElement as IAttributeAccessor;

                    if (se != null && selected)
                    {
                        var cls = se.GetAttribute("class");
                        if (cls == null)
                        {
                            se.SetAttribute("class", CssClassSelected);
                        }
                        else if (cls.IndexOf(CssClassSelected) < 0)
                        {
                            se.SetAttribute("class", cls + " " + CssClassSelected);
                        }
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