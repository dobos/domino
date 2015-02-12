using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Security;
using System.Text;
using Complex.Domino.Lib;

namespace Complex.Domino.Web.Auth
{
    public partial class User : PageBase
    {
        public static string GetUrl(Uri returnUrl)
        {
            return GetUrl(returnUrl.ToString());
        }

        public static string GetUrl(string returnUrl)
        {
            return String.Format("~/Auth/User.aspx?ReturnUrl={0}", HttpUtility.UrlEncode(returnUrl));
        }

        private Lib.User item;

        protected void Page_Load(object sender, EventArgs e)
        {
            CreateItem();

            if (!IsPostBack)
            {
                UpdateForm();
            }
        }

        protected void Ok_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                var emailchanged = StringComparer.InvariantCultureIgnoreCase.Compare(item.Email, Email.Text) != 0;

                SaveForm();
                item.Save();

                // Send confirmation email

                if (emailchanged)
                {
                    var body = new StringBuilder(Resources.EmailTemplates.UpdateAccount);

                    var tokens = new Dictionary<string, string>()
                    {
                         { "Name", item.Description },
                    };

                    Util.Email.ReplaceTokens(body, tokens);

                    Util.Email.SendFromDomino(
                        item,
                        Resources.EmailTemplates.UpdateAccountSubject,
                        body.ToString());
                }

                // Make sure cached identity is refreshed
                SetUser(item);

                formPanel.Visible = false;
                messagePanel.Visible = true;
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Util.Url.RedirectTo(ReturnUrl);
        }

        private void CreateItem()
        {
            item = new Lib.User(DatabaseContext);
            item.Load(DatabaseContext.User.ID);
        }

        private void UpdateForm()
        {
            Name.Text = item.Name;
            Description.Text = item.Description;
            Email.Text = item.Email;
        }

        private void SaveForm()
        {
            item.Email = Email.Text;
        }
    }
}