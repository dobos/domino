using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web
{
    public partial class Error : System.Web.UI.Page
    {
        public static string GetUrl()
        {
            return "~/Error.aspx";
        }

        protected Exception exception;
        protected string exceptionUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            exception = Server.GetLastError();
           
            if (exception == null)
            {
                exception = (Exception)Session[Constants.SessionException];
                exceptionUrl = (string)Session[Constants.SessionExceptionUrl];
            }

            Session[Constants.SessionException] = exception;

            if (!IsPostBack)
            {
                if (exception != null)
                {
                    ExceptionType.Text = exception.GetType().Name;
                    ExceptionMessage.Text = exception.Message;
                }
            }
        }

        protected void Ok_Click(object sender, EventArgs e)
        {
            SendEmail();

            Util.Url.RedirectTo("~");
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Util.Url.RedirectTo("~");
        }

        private void SendEmail()
        {
            var body = new StringBuilder(Resources.EmailTemplates.Error);

            var tokens = new Dictionary<string, string>()
                {
                     { "ExceptionUrl", exceptionUrl ?? "" },
                     { "DateTime", DateTime.Now.ToString() },
                     { "Comments", comments.Text },
                };

            if (exception != null)
            {
                tokens.Add("ExceptionType", exception.GetType().FullName);
                tokens.Add("ExceptionMessage", exception.Message);
                tokens.Add("StackTrace", exception.StackTrace);
            }

            Util.Email.ReplaceTokens(body, tokens);

            Util.Email.SendFromDomino(
                Lib.DominoConfiguration.Instance.EmailFromName,
                Lib.DominoConfiguration.Instance.EmailNoreplyAddress,
                Resources.EmailTemplates.ErrorSubject,
                body.ToString());
        }
    }
}