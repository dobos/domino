using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web
{
    public abstract class EntityPage<T> : PageBase
        where T : Lib.Entity, new()
    {
        private T item;
        protected Controls.EntityForm entityForm;

        protected new int ID
        {
            get { return Util.Url.ParseInt(Request.QueryString[Constants.RequestID], -1); }
        }

        protected T Item
        {
            get { return item; }
        }

        protected EntityPage()
        {
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
            if (entityForm != null)
            {
                entityForm.UpdateForm(item);
            }
        }

        protected virtual void SaveForm()
        {
            if (entityForm != null)
            {
                entityForm.SaveForm(item);
            }
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
                OnOkClick();
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            OnCancelClick();
        }

        protected virtual void OnOkClick()
        {
            SaveForm();
            item.Save();

            OnRedirect();
        }

        protected virtual void OnCancelClick()
        {
            OnRedirect();
        }

        protected virtual void OnRedirect()
        {
            if (!String.IsNullOrWhiteSpace(ReturnUrl))
            {
                Util.Url.RedirectTo(ReturnUrl);
            }
            else
            {
                Util.Url.RedirectTo(OriginalReferer);
            }
        }
    }
}