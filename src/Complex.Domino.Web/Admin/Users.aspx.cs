﻿using System;
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

        private Lib.UserFactory searchObject;

        protected void Page_Load(object sender, EventArgs e)
        {
            ToolbarCreate.NavigateUrl = Web.Admin.User.GetUrl();
            ToolbarImport.NavigateUrl = Web.Admin.ImportUsers.GetUrl();
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
            searchObject = new Lib.UserFactory(DatabaseContext);

            // TODO: set search criteria here

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