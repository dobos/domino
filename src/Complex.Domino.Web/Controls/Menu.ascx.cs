using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Controls
{
    public partial class Menu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Semesters.NavigateUrl = Admin.SemesterList.GetUrl();
            Courses.NavigateUrl = Admin.CourseList.GetUrl();
            Users.NavigateUrl = Admin.UserList.GetUrl();
        }
    }
}