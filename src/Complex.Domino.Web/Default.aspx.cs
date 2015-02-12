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
            if (User.IsInRole(Lib.UserRoleType.Admin.ToString()))
            {
                Response.Redirect(Admin.Default.GetUrl());
            }
            else if (User.IsInRole(Lib.UserRoleType.Teacher.ToString()))
            {
                Response.Redirect(Teacher.Default.GetUrl());
            }
            else if (User.IsInRole(Lib.UserRoleType.Student.ToString()))
            {
                Response.Redirect(Student.Default.GetUrl());
            }

            SignInLink.NavigateUrl = Auth.SignIn.GetUrl(Page.Request.Url);
            SignInLink.Visible = DatabaseContext.User == null;
        }
    }
}