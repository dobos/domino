using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Admin
{
    public partial class User : EntityPage<Lib.User>
    {
        public static string GetUrl()
        {
            return "~/Admin/User.aspx";
        }

        public static string GetUrl(int id, string returnUrl)
        {
            var url = "~/Admin/User.aspx?ID={0}";

            if (returnUrl != null)
            {
                url += String.Format("&{0}={1}", Constants.ReturnUrl, HttpUtility.UrlEncode(returnUrl));
            }

            return url;
        }

        protected override void UpdateForm()
        {
            base.UpdateForm();

            TitleLabel.Text = Item.IsExisting ? Resources.Labels.ModifyUser : Resources.Labels.NewUser;

            Email.Text = Item.Email;

            if (Item.IsExisting)
            {
                rolesPanel.Visible = true;
                RefreshCourseList();
                DeleteRole.OnClientClick = String.Format("return confirm('{0}');", Resources.Labels.ConfirmDeleteRole);
            }
        }

        protected override void SaveForm()
        {
            base.SaveForm();

            Item.Email = Email.Text;

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

        protected override void OnOkClick()
        {
            if (Item.IsExisting)
            {
                base.OnOkClick();
            }
            else
            {
                SaveForm();
                Item.Save();

                Util.Url.RedirectTo(GetUrl(Item.ID, OriginalReferer));
            }
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
            Item.LoadRoles();

            userRoleList.DataBind();
        }

        protected void DeleteRole_Click(object sender, EventArgs e)
        {
            foreach (string id in userRoleList.SelectedDataKeys)
            {
                var parts = id.Split('|');

                var role = new Lib.UserRole()
                {
                    UserID = Item.ID,
                    CourseID = int.Parse(parts[0]),
                    RoleType = (Lib.UserRoleType)Enum.Parse(typeof(Lib.UserRoleType), parts[1]),
                };

                Item.DeleteRole(role);
            }

            Item.LoadRoles();
            userRoleList.DataBind();
        }
    }
}