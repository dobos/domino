using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web
{
    public partial class Default : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SignInLink.NavigateUrl = Auth.SignIn.GetUrl(Page.Request.Url);
            SignInLink.Visible = DatabaseContext.User == null;
        }
    }
}