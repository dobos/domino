using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Teacher
{
    public partial class Students : PageBase
    {
        public static string GetUrl()
        {
            return "~/Teacher/Students.aspx";
        }

        public static string GetUrl(int courseId)
        {
            return String.Format(
                "~/Teacher/Students.aspx?{0}={1}",
                Constants.RequestCourseID, courseId);
        }

        private Lib.UserFactory searchObject;

        protected int CourseID
        {
            get { return int.Parse(Request.QueryString["courseId"] ?? "-1"); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (userList.Visible)
            {
                userList.DataBind();
            }
        }

        protected void userDataSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            searchObject = new Lib.UserFactory(DatabaseContext)
            {
                TeacherID = DatabaseContext.User.ID,
                CourseID = this.CourseID,
                Role = Lib.UserRoleType.Student,
            };

            e.ObjectInstance = searchObject;
        }
    }
}