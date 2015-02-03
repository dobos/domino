using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Teacher
{
    public partial class Submission : SubmissionPage
    {
        public static string GetUrl(int assignmentID)
        {
            return GetUrl(assignmentID, -1);
        }

        public static string GetUrl(int assignmentID, int submissionID)
        {
            var url = "~/Teacher/Submission.aspx";
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

        protected override void UpdateForm()
        {
            base.UpdateForm();

            createdDate.Text = Util.DateTime.FormatFancy(Item.CreatedDate);
            studentName.Text = Student.Name;
            studentName.NavigateUrl = Teacher.Student.GetUrl(Student.ID);
            studentDescription.Text = Student.Description;
            studentDescription.NavigateUrl = Teacher.Student.GetUrl(Student.ID);
            comments.Text = Item.Comments;
            gradeLabel.Text = Util.Enum.ToLocalized(typeof(Resources.Grades), Assignment.GradeType);
            //grade.Text =  TODO: read actual grade from database
        }

        protected void SendReply_CheckedChanged(object sender, EventArgs e)
        {
            CommentsRow3.Visible = sendReply.Checked;
            CommentsRow4.Visible = sendReply.Checked;
        }
    }
}