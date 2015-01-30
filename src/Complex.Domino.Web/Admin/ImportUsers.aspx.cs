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
                    uf.Import(txt, out users, out duplicates);

                    Users = users;
                    Duplicates = duplicates;

                    importPanel.Visible = false;
                    listPanel.Visible = true;
                }
                else
                {
                    // Save users
                    foreach (var user in Users)
                    {
                        // Generate password hash
                        user.SetPassword(user.ActivationCode);
                        user.ActivationCode = String.Empty;

                        // Save user
                        user.Context = DatabaseContext;
                        user.Save();
                    }

                    Util.Url.RedirectTo(OriginalReferer);
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