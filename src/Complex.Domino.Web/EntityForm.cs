using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web
{
    public abstract class EntityForm<T> : PageBase
        where T : Lib.Entity, new()
    {
        private T item;

        protected ITextControl Name;
        protected ITextControl Description;
        protected CheckBox Enabled;
        protected new CheckBox Visible;

        protected new int ID
        {
            get { return Util.UrlParser.ParseInt(Request.QueryString[Constants.RequestID], -1); }
        }

        protected T Item
        {
            get { return item; }
        }

        protected EntityForm()
        {
            this.Name = new TextBox();
            this.Enabled = new CheckBox();
            this.Visible = new CheckBox();
        }

        protected virtual void CreateItem()
        {
            item = new T()
            {
                ID = ID,
                Context = DatabaseContext
            };

            if (item.ID > 0)
            {
                item.Load();
            }
        }

        protected virtual void UpdateForm()
        {
            Name.Text = item.Name;
            Enabled.Checked = item.Enabled;
            Visible.Checked = item.Visible;
            // TODO: add comments
        }

        protected virtual void SaveForm()
        {
            item.Name = Name.Text;
            item.Enabled = Enabled.Checked;
            item.Visible = Visible.Checked;
            // TODO: add comments
        }

        protected virtual void DataBindForm()
        {
        }

        protected override void OnLoad(EventArgs e)
        {
            CreateItem();

            base.OnLoad(e);

            if (!IsPostBack)
            {
                UpdateForm();
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            DataBindForm();

            base.OnPreRender(e);
        }

        protected void Ok_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                SaveForm();
                item.Save();

                Response.Redirect(OriginalReferer);
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(OriginalReferer);
        }
    }
}