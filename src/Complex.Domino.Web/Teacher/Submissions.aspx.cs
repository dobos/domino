using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Teacher
{
    public partial class Submissions : System.Web.UI.Page
    {
        public static string GetUrl(int courseId)
        {
            return GetUrl(courseId, -1);
        }

        public static string GetUrl(int courseId, int assignmentId)
        {
            var url = "~/Teacher/Submissions.aspx";
            url += String.Format("?{0}={1}", Constants.RequestCourseID, courseId);

            if (assignmentId > 0)
            {
                url += String.Format("&{0}={1}", Constants.RequestAssignmentID, assignmentId);
            }

            return url;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}