using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Admin
{
    public partial class Users : PageBase
    {
        public static string GetUrl()
        {
            return "~/Admin/Users.aspx";
        }

        public static string GetUrl(int courseId)
        {
            return String.Format(
                "~/Admin/Users.aspx?{0}={1}",
                Constants.RequestCourseID, courseId);
        }

        private Lib.UserFactory searchObject;

        protected int CourseID
        {
            get { return int.Parse(Request.QueryString["courseId"] ?? "-1"); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ToolbarCreate.NavigateUrl = Web.Admin.User.GetUrl();
            ToolbarImport.NavigateUrl = Web.Admin.ImportUsers.GetUrl(CourseID);
            Delete.OnClientClick = String.Format("return confirm('{0}');", Resources.Labels.ConfirmDeleteEntities);
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
                CourseID = this.CourseID,
                Role = Lib.UserRoleType.Student,
            };

            e.ObjectInstance = searchObject;
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            foreach (int id in userList.SelectedDataKeys.Select(id => int.Parse(id)))
            {
                var user = new Lib.User(DatabaseContext);
                user.Delete(id);
            }
        }
    }
}