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
        public static string GetUrl(Uri returnUrl)
        {
            return String.Format("~/Auth/User.aspx?ReturnUrl={0}", HttpUtility.UrlEncode(returnUrl.ToString()));
        }

        protected void Ok_Click(object sender, EventHandler e)
        {
        }

        protected void Cancel_Click(object sender, EventHandler e)
        {
        }
    }
}