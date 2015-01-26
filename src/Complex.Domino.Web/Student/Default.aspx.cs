using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Student
{
    public partial class Default : PageBase
    {
        public static string GetUrl()
        {
            return "~/Student";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            NameLabel.Text = DatabaseContext.User.Description;

            CoursesLink.NavigateUrl = Courses.GetUrl();
            AssignmentsLink.NavigateUrl = Assignments.GetUrl();
        }
    }
}