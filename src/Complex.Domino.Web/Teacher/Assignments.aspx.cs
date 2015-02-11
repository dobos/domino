using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Teacher
{
    public partial class Assignments : PageBase
    {
        public static string GetUrl()
        {
            return "~/Teacher/Assignments.aspx";
        }

        public static string GetUrl(int courseId)
        {
            return String.Format(
                "~/Teacher/Assignments.aspx?{0}={1}",
                Constants.RequestCourseID, courseId);
        }

        public int CourseID
        {
            get { return int.Parse(Request[Constants.RequestCourseID] ?? "-1"); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            NewAssignment.NavigateUrl = Assignment.GetUrl(CourseID, -1);
            NewAssignment.Visible = CourseID > 0;
            
            AssignmentList.CourseID = CourseID;
        }

    }
}