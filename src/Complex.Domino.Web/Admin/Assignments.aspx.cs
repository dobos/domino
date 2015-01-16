using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Admin
{
    public partial class Assignments : PageBase
    {
        public static string GetUrl()
        {
            return "~/Admin/Assignments.aspx";
        }

        private Lib.AssignmentFactory searchObject;

        protected void Page_Load(object sender, EventArgs e)
        {
            ToolbarCreate.NavigateUrl = Assignment.GetUrl();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (assignmentList.Visible)
            {
                assignmentList.DataBind();
            }
        }

        protected void assignmentDataSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            searchObject = new Lib.AssignmentFactory(DatabaseContext);

            // Set search criteria
            if (Request.QueryString[Constants.RequestSemesterID] != null)
            {
                searchObject.SemesterID = int.Parse(Request.QueryString[Constants.RequestSemesterID]);
            }

            if (Request.QueryString[Constants.RequestCourseID] != null)
            {
                searchObject.CourseID = int.Parse(Request.QueryString[Constants.RequestCourseID]);
            }

            e.ObjectInstance = searchObject;
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            foreach (int id in assignmentList.SelectedDataKeys.Select(id => int.Parse(id)))
            {
                var assignment = new Lib.Assignment(DatabaseContext);
                assignment.Delete(id);
            }
        }
    }
}