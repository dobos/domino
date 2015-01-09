using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Teacher
{
    public partial class CourseList : PageBase
    {
        public static string GetUrl()
        {
            return "~/Teacher/CourseList.aspx";
        }

        private Lib.CourseFactory searchObject;


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
    }
}