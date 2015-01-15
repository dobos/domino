using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Student
{
    public partial class Default : System.Web.UI.Page
    {
        public static string GetUrl()
        {
            return "~/Student";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CoursesLink.NavigateUrl = Courses.GetUrl();
            AssignmentsLink.NavigateUrl = Assignments.GetUrl();
            SubmissionsLink.NavigateUrl = Submissions.GetUrl();
        }
    }
}