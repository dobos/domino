using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Student
{
    public partial class Submissions : PageBase
    {
        public static string GetUrl()
        {
            return "~/Student/Submissions.aspx";
        }

        public static string GetUrl(int assignmentID)
        {
            return String.Format("~/Student/Submissions.aspx?assignmentID={0}", assignmentID);
        }

        private Lib.SubmissionFactory searchObject;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (submissionList.Visible)
            {
                submissionList.DataBind();
            }
        }

        protected void submissionDataSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            searchObject = new Lib.SubmissionFactory(DatabaseContext);

            // Set search criteria
            if (Request.QueryString[Constants.RequestSemesterID] != null)
            {
                searchObject.SemesterID = int.Parse(Request.QueryString[Constants.RequestSemesterID]);
            }

            if (Request.QueryString[Constants.RequestCourseID] != null)
            {
                searchObject.CourseID = int.Parse(Request.QueryString[Constants.RequestCourseID]);
            }

            if (Request.QueryString[Constants.RequestAssignmentID] != null)
            {
                searchObject.AssignmentID = int.Parse(Request.QueryString[Constants.RequestAssignmentID]);
            }

            searchObject.StudentID = DatabaseContext.User.ID;

            e.ObjectInstance = searchObject;
        }
    }
}