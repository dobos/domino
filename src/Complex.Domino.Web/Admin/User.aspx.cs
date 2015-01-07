using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Admin
{
    public partial class User : EntityForm<Lib.User>
    {
        public static string GetUrl()
        {
            return "~/Admin/User.aspx";
        }

        protected override void UpdateForm()
        {
            base.UpdateForm();

            Email.Text = Item.Email;
            Username.Text = Item.Username;

            if (Item.IsExisting)
            {
                RolesPanel.Visible = true;
                RefreshCourseList();
            }
        }

        protected override void SaveForm()
        {
            base.SaveForm();

            Item.Email = Email.Text;
            Item.Username = Username.Text;

            if (!Item.IsExisting || !String.IsNullOrWhiteSpace(Password.Text))
            {
                Item.SetPassword(Password.Text);
            }
        }

        protected override void DataBindForm()
        {
            base.DataBindForm();

            if (userRoleList.Visible)
            {
                userRoleList.DataSource = Item.Roles.Values;
                userRoleList.DataBind();
            }
        }

        private void RefreshCourseList()
        {
            var f = new Lib.CourseFactory(DatabaseContext);
            Course.DataSource = f.Find().ToArray();
            Course.DataBind();
        }

        protected void AddRole_Click(object sender, EventArgs e)
        {
            var role = new Lib.UserRole()
            {
                UserID = Item.ID,
                CourseID = int.Parse(Course.SelectedValue),
                RoleType = (Lib.UserRoleType)Enum.Parse(typeof(Lib.UserRoleType), RoleType.SelectedValue),
            };

            Item.AddRole(role);
        }
    }
}