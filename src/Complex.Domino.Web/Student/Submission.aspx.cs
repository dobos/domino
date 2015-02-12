using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;

namespace Complex.Domino.Web.Student
{
    public partial class Submission : SubmissionPage
    {
        public static string GetUrl(int assignmentID)
        {
            return GetUrl(assignmentID, -1);
        }

        public static string GetUrl(int assignmentID, int submissionID)
        {
            var url = "~/Student/Submission.aspx";
            var query = "";

            if (assignmentID > 0)
            {
                query += "&assignmentID=" + assignmentID.ToString();
            }

            if (submissionID > 0)
            {
                query += "&id=" + submissionID.ToString();
            }

            if (query.Length > 0)
            {
                url += "?" + query.Substring(1);
            }

            return url;
        }

        protected override void CreateItem()
        {
            base.CreateItem();

            // If this is a reply by a teacher, mark it as read automatically
            if (Item.IsReply)
            {
                Item.MarkRead();
            }
        }

        protected override void UpdateForm()
        {
            base.UpdateForm();

            if (Item.IsReply)
            {
                formLabel.Text = Resources.Labels.Reply;
            }
            else if (Item.IsExisting)
            {
                formLabel.Text = Resources.Labels.Submission;
            }
            else
            {
                formLabel.Text = Resources.Labels.NewSubmission;
            }

            cancelLabel.Text = Item.IsExisting ?
                Resources.Labels.Ok :
                Resources.Labels.Cancel;

            ok.Visible = !Item.IsExisting;
        }

        protected override void SaveForm()
        {
            base.SaveForm();

            var commit = CommitSubmission(Item);

            Item.StudentID = DatabaseContext.User.ID;
            Item.Name = commit.Hash;
        }

        protected override void OnOkClick()
        {
            SaveForm();
            Item.Save();

            SendEmail();

            emptyPanel.Visible = false;
            filesPanel.Visible = false;
            messagePanel.Visible = true;
        }

        private void SendEmail()
        {
            // Get file list
            var filelist = new StringBuilder();
            var path = GitHelper.GetAssignmentPath();
            var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

            for (int i = 0; i < files.Length; i++)
            {
                filelist.AppendLine(Util.Path.MakeRelative(path, files[i]));
            }

            var body = new StringBuilder(Resources.EmailTemplates.Submission);

            var tokens = new Dictionary<string, string>()
                {
                     { "Name", DatabaseContext.User.Description },
                     { "DateTime", DateTime.Now.ToString() },
                     { "Assignment", Assignment.Description },
                     { "Files", filelist.ToString() },
                };

            Util.Email.ReplaceTokens(body, tokens);

            Util.Email.SendFromDomino(
                DatabaseContext.User,
                Resources.EmailTemplates.SubmissionSubject,
                body.ToString());
        }
    }
}