using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Admin
{
    public partial class CourseList : PageBase
    {
        public static string GetUrl()
        {
            return "~/Admin/CourseList.aspx";
        }

        private Lib.CourseFactory searchObject;

        protected void Page_Load(object sender, EventArgs e)
        {
            ToolbarCreate.NavigateUrl = Semester.GetUrl();
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

            // TODO: set search criteria here

            e.ObjectInstance = searchObject;
        }
    }
}