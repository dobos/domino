using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;

namespace Complex.Domino.Web.Admin
{
    public partial class ImportUsers : PageBase
    {
        public static string GetUrl()
        {
            return "~/Admin/ImportUsers.aspx";
        }

        public static string GetUrl(int courseId)
        {
            return String.Format(
                "~/Admin/ImportUsers.aspx?{0}={1}",
                Constants.RequestCourseID, courseId);
        }

        protected int CourseID
        {
            get { return int.Parse(Request.QueryString["courseId"] ?? "-1"); }
        }

        protected List<Lib.User> Users
        {
            get { return (List<Lib.User>)ViewState["Users"]; }
            set { ViewState["Users"] = value; }
        }

        protected List<Lib.User> Duplicates
        {
            get { return (List<Lib.User>)ViewState["Duplicates"]; }
            set { ViewState["Duplicates"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RefreshCourseList();

                Course.SelectedValue = CourseID.ToString();
            }
        }

        protected void Ok_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (importPanel.Visible)
                {
                    var uf = new Lib.UserFactory(DatabaseContext);
                    var reader = new StreamReader(File.FileContent, Encoding.Default, true);
                    var txt = new Lib.TextFileReader(reader);

                    List<Lib.User> users, duplicates;
                    uf.Import(txt, out users);

                    foreach (var user in users)
                    {
                        
                    }
                    uf.FindUserDuplicates(users, out duplicates);

                    Users = users;
                    Duplicates = duplicates;

                    importPanel.Visible = false;
                    listPanel.Visible = true;
                    duplicatesPanel.Visible = true;
                }
                else
                {
                    SaveUsers();

                    if (int.Parse(Course.SelectedValue) > 0)
                    {
                        AddStudentRoles();
                    }

                    Util.Url.RedirectTo(OriginalReferer);
                }
            }
        }

        private void RefreshCourseList()
        {
            var f = new Lib.CourseFactory(DatabaseContext);
            Course.DataSource = f.Find().ToArray();
            Course.DataBind();
        }

        private void SaveUsers()
        {
            foreach (var user in Users)
            {
                // Generate password hash
                user.SetPassword(user.ActivationCode);
                user.ActivationCode = String.Empty;

                // Save user
                user.Context = DatabaseContext;
                user.Save();
            }
        }

        private void AddStudentRoles()
        {
            var courseId = int.Parse( Course.SelectedValue);

            if (courseId > 0)
            {
                var uf = new Lib.UserFactory(DatabaseContext);

                foreach (var user in Users.Concat(Duplicates))
                {
                    var role = new Lib.UserRole()
                    {
                        CourseID = courseId,
                        RoleType = Lib.UserRoleType.Student,
                        UserID = user.ID
                    };

                    if (!uf.IsRoleDuplicate(role))
                    {
                        uf.AddRole(role);
                    }
                }
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Util.Url.RedirectTo(OriginalReferer);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (userList.Visible)
            {
                userList.DataSource = Users;
                userList.DataBind();
            }

            if (duplicateList.Visible)
            {
                duplicateList.DataSource = Duplicates;
                duplicateList.DataBind();
            }
        }
    }
}