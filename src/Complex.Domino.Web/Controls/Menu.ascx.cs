using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Controls
{
    public partial class Menu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var user = (Lib.User)Session[Constants.SessionUser];

            if (user != null)
            {
                // --- Admin menu

                AdminPanel.Visible = user.IsInAdminRole();

                AdminUsers.NavigateUrl = Admin.Users.GetUrl();
                AdminSemesters.NavigateUrl = Admin.Semesters.GetUrl();
                AdminCourses.NavigateUrl = Admin.Courses.GetUrl();
                AdminAssignments.NavigateUrl = Admin.Assignments.GetUrl();

                // --- Teacher menu

                TeacherPanel.Visible = user.IsInRole(Lib.UserRoleType.Teacher);

                TeacherCourses.NavigateUrl = Teacher.CourseList.GetUrl();

                // --- Student menu

                StudentPanel.Visible = user.IsInRole(Lib.UserRoleType.Student);

                StudentMain.NavigateUrl = Student.Default.GetUrl();
                StudentCourses.NavigateUrl = Student.Courses.GetUrl();
                StudentAssignments.NavigateUrl = Student.Assignments.GetUrl();

                // --- User menu

                UserPanel.Visible = true;

                UserAccount.NavigateUrl = Auth.User.GetUrl(Page.Request.Url);
                UserPassword.NavigateUrl = Auth.ChangePassword.GetUrl(Page.Request.Url);
                UserSignOut.NavigateUrl = Auth.SignOut.GetUrl();
            }
        }
    }
}