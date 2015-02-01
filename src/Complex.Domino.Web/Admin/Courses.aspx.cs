using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Admin
{
    public partial class Courses : PageBase
    {
        public static string GetUrl()
        {
            return "~/Admin/Courses.aspx";
        }

        private Lib.CourseFactory searchObject;

        protected void Page_Load(object sender, EventArgs e)
        {
            ToolbarCreate.NavigateUrl = Course.GetUrl();
            Delete.OnClientClick = String.Format("return confirm('{0}');", Resources.Labels.ConfirmDeleteEntities);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (courseList.Visible)
            {
                courseList.DataBind();
            }
        }

        protected void courseDataSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            searchObject = new Lib.CourseFactory(DatabaseContext);

            // Set search criteria
            if (Request.QueryString[Constants.RequestSemesterID] != null)
            {
                searchObject.SemesterID = int.Parse(Request.QueryString[Constants.RequestSemesterID]);
            }

            e.ObjectInstance = searchObject;
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            foreach (int id in courseList.SelectedDataKeys.Select(id => int.Parse(id)))
            {
                var course = new Lib.Course(DatabaseContext);
                course.Delete(id);
            }
        }
    }
}