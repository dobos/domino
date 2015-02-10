using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Security;
using Complex.Domino.Lib;

namespace Complex.Domino.Web.Auth
{
    public partial class ResetPassword : PageBase
    {
        public static string GetUrl()
        {
            return String.Format("~/Auth/ResetPassword.aspx");
        }

        private Lib.User item;

        protected void EmailValidator_ServerValidate(object sender, ServerValidateEventArgs args)
        {
            // Try to authenticate the user.
            // It might happen that the user is awaiting activation.
            try
            {
                var uf = new UserFactory(DatabaseContext);
                item = uf.LoadByEmail(Email.Text);

                args.IsValid = item.ReadOnly;
            }
            catch (SecurityException)
            {
                args.IsValid = false;
            }
        }

        protected void Ok_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                item.GenerateActivationCode();
                item.Save();

                var body = new StringBuilder(Resources.EmailTemplates.ResetPassword);


                var tokens = new Dictionary<string, string>()
                {
                     { "Name", item.Description },
                     { "Url", VirtualPathUtility.ToAbsolute(Auth.ChangePassword.GetResetUrl(item.ActivationCode)) }
                };

                Util.Email.ReplaceTokens(body, tokens);

                Util.Email.Send(
                    DominoConfiguration.Instance.EmailFromName,
                    DominoConfiguration.Instance.EmailNoreplyAddress,
                    item.Description,
                    item.Email,
                    Resources.EmailTemplates.ResetPasswordSubject,
                    body.ToString());

                resetPanel.Visible = false;
                messagePanel.Visible = true;
            }
        }
    }
}