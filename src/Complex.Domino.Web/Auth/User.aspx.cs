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
            return String.Format("~/Auth/User.aspx?ReturnUrl={0}", HttpUtility.UrlEncode(returnUrl.ToString()));
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

                Response.Redirect(ReturnUrl);
            }
        }

        protected void OkPassword_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                item.SetPassword(PasswordNew.Text);
                item.Save();

                Response.Redirect(ReturnUrl);
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(ReturnUrl);
        }

        protected void PasswordValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = item.VerifyPassword(args.Value);
        }

        protected void PasswordComplexityValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool upper = false;
            bool lower = false;
            bool digits = false;

            for (int i = 0; i < args.Value.Length; i++)
            {
                var c = args.Value[i];

                upper |= Char.IsUpper(c);
                lower |= Char.IsLower(c);
                digits |= Char.IsDigit(c);
            }

            args.IsValid = args.Value.Length > 3 && upper && lower && digits;
        }

        protected void PasswordConfurmValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = PasswordNew.Text == PasswordConfirm.Text;
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