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
        public static string GetUrl()
        {
            return "~/Auth/SignOut.aspx";
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
        }

        protected void Ok_Click(object sender, EventArgs e)
        {
            Util.Url.RedirectTo("~");
        }
    }
}