using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Net.Mail;

namespace Complex.Domino.Web.Util
{
    public static class Email
    {
        public static void ReplaceTokens(StringBuilder body, Dictionary<string, string> tokens)
        {
            ReplaceBaseUrl(body);

            foreach (var key in tokens.Keys)
            {
                body.Replace("[$" + key + "]", tokens[key]);
            }
        }

        private static void ReplaceBaseUrl(StringBuilder body)
        {
            body.Replace("[$BaseUrl]", Url.GetAppUrl());
        }

        public static void SendFromDomino(Lib.User user, string subject, string body)
        {
            SendFromDomino(user.Description, user.Email, subject, body);
        }

        public static void SendFromDomino(string toName, string toEmail, string subject, string body)
        {
            Send(
                Lib.DominoConfiguration.Instance.EmailFromName,
                    Lib.DominoConfiguration.Instance.EmailNoreplyAddress,
                    toName, toEmail, subject, body);
        }

        public static void Send(string fromName, string fromEmail, string toName, string toEmail, string subject, string body)
        {
            var smtp = new SmtpClient();

            var msg = new MailMessage();
            msg.From = new MailAddress(fromEmail, fromName);
            msg.To.Add(new MailAddress(toEmail, toName));
            msg.Subject = subject;
            msg.SubjectEncoding = Encoding.UTF8;
            msg.Body = body;
            msg.BodyEncoding = Encoding.UTF8;
            msg.BodyTransferEncoding = System.Net.Mime.TransferEncoding.QuotedPrintable;

            smtp.Send(msg);
        }
    }
}