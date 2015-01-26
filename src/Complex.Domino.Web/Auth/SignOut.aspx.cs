using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace Complex.Domino.Web.Auth
{
    public partial class SignOut : PageBase
    {
        public static string GetUrl(Uri returnUrl)
        {
            return String.Format("~/Auth/SignOut.aspx?ReturnUrl={0}", HttpUtility.UrlEncode(returnUrl.ToString()));
        }

        protected override void OnLoad(EventArgs e)
        {
            BypassAuthentication();

            base.OnLoad(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            Util.Url.RedirectTo(ReturnUrl);
        }
    }
}