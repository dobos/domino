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
    public partial class SignIn : PageBase
    {
        public static string GetUrl(Uri returnUrl)
        {
            return String.Format("~/Auth/SignIn.aspx?ReturnUrl={0}", HttpUtility.UrlEncode(returnUrl.ToString()));
        }

        #region Event handlers

        protected override void OnLoad(EventArgs e)
        {
            BypassAuthentication();

            base.OnLoad(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            UpdateForm();
        }

        /// <summary>
        /// Fires when the user clicks on the Sign in button after providing a
        /// username and a password.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        protected void PasswordValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            // Attempt to log in with supplied username and password
            args.IsValid = AuthenticateByForm();
        }

        /// <summary>
        /// Fires when the user clicks on the Sign in button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Ok_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                RedirectAuthenticatedUser();
            }
        }

        #endregion
        #region Form update

        private void UpdateForm()
        {
            //ResetLink.NavigateUrl = Jhu.Graywulf.Web.Auth.RequestReset.GetUrl(ReturnUrl);
        }

        #endregion
        #region Authentication logic

        /// <summary>
        /// Authenticates a user coming in with a username and a password.
        /// </summary>
        private bool AuthenticateByForm()
        {
            // Try to authenticate the user.
            // It might happen that the user is awaiting activation.
            try
            {
                var u = new User(DatabaseContext);
                u.SignIn(Username.Text, Password.Text);

                if (!u.Enabled)
                {
                    throw Lib.Error.InvalidUsernameOrPassword();
                }

                u.LoadRoles();

                SetUser(u);

                return true;
            }
            catch (SecurityException)
            {
                ResetUser();

                return false;
            }
        }

        private void RedirectAuthenticatedUser()
        {
            FormsAuthentication.RedirectFromLoginPage(
                DatabaseContext.User.Username,
                Remember.Checked);
        }

        #endregion
    }
}