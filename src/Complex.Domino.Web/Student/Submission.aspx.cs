using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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

            // If this is a reply by a teacher, mark it as read
            if (Item.TeacherID > 0)
            {
                Item.MarkRead();
            }
        }

        protected override void UpdateForm()
        {
            base.UpdateForm();

            formLabel.Text = Item.IsExisting ?
                Resources.Labels.Submission :
                Resources.Labels.NewSubmission;

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

            emptyPanel.Visible = false;
            filesPanel.Visible = false;
            messagePanel.Visible = true;
        }
    }
}