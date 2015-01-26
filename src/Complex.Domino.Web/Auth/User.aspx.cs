using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Security;
using Complex.Domino.Lib;

namespace Complex.Domino.Web.Auth
{
    public partial class User : PageBase
    {
        private Lib.User item;

        public static string GetUrl(Uri returnUrl)
        {
            return GetUrl(returnUrl.ToString());
        }

        public static string GetUrl(string returnUrl)
        {
            return String.Format("~/Auth/User.aspx?ReturnUrl={0}", HttpUtility.UrlEncode(returnUrl));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CreateItem();

            if (!IsPostBack)
            {
                UpdateForm();
            }
        }

        protected void Ok_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                SaveForm();
                item.Save();

                Util.Url.RedirectTo(ReturnUrl);
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Util.Url.RedirectTo(ReturnUrl);
        }
        
        private void CreateItem()
        {
            item = new Lib.User(DatabaseContext);
            item.Load(DatabaseContext.User.ID);
        }

        private void UpdateForm()
        {
            Name.Text = item.Name;
            Description.Text = item.Description;
            Email.Text = item.Email;
        }

        private void SaveForm()
        {
            item.Email = Email.Text;
        }
    }
}