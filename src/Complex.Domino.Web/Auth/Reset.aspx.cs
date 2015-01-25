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
    public partial class Reset : PageBase
    {
        public static string GetUrl()
        {
            return String.Format("~/Auth/Reset.aspx");
        }

        protected void Ok_Click(object sender, EventArgs e)
        {
        }
    }
}