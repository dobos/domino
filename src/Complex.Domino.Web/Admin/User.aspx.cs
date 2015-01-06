using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Admin
{
    public partial class User : EntityForm<Lib.User>
    {
        public static string GetUrl()
        {
            return "~/Admin/User.aspx";
        }

        protected override void UpdateForm()
        {
            base.UpdateForm();

            Email.Text = Item.Email;
            Username.Text = Item.Username;
        }

        protected override void SaveForm()
        {
            base.SaveForm();

            Item.Email = Email.Text;
            Item.Username = Username.Text;

            if (!Item.IsExisting || !String.IsNullOrWhiteSpace(Password.Text))
            {
                Item.SetPassword(Password.Text);
            }
        }
    }
}