using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Controls
{
    public partial class UserStatus : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            authenticatedPanel.Visible = Page.User.Identity.IsAuthenticated;
            anonymousPanel.Visible = !Page.User.Identity.IsAuthenticated;

            if (Page.User.Identity.IsAuthenticated)
            {
                Username.Text = Page.User.Identity.Name;
                Username.NavigateUrl = Auth.User.GetUrl(Page.Request.Url);
                SignOut.NavigateUrl = Auth.SignOut.GetUrl(Page.Request.Url);
            }
            else
            {
                SignIn.NavigateUrl = Auth.SignIn.GetUrl(Page.Request.Url);
            }
        }
    }
}