using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Student
{
    public partial class Assignment : EntityPage<Lib.Assignment>
    {
        public static string GetUrl(int assignmentID)
        {
            return String.Format("~/Student/Assignment.aspx?ID={0}", assignmentID);
        }

        protected Lib.AssignmentGrade assignmentGrade;

        protected override void CreateItem()
        {
            base.CreateItem();

            assignmentGrade = new Lib.AssignmentGrade(DatabaseContext);
            assignmentGrade.Load(Item.ID, DatabaseContext.User.ID);
        }

        protected override void UpdateForm()
        {
            base.UpdateForm();

            Description.Text = Item.Description;
            SemesterDescription.Text = Item.SemesterDescription;

            CourseDescription.Text = Item.CourseDescription;
            CourseDescription.NavigateUrl = Course.GetUrl(Item.CourseID);
            
            if (!String.IsNullOrWhiteSpace(Item.Url))
            {
                Url.NavigateUrl = Item.Url;
            }
            else
            {
                Url.Visible = false;
            }

            Comments.Text = Item.Comments;

            grade.Text = assignmentGrade.Grade > 0 ? assignmentGrade.Grade.ToString() : "-";
            gradeLabel.Text = Util.Enum.ToLocalized(typeof(Resources.Grades), Item.GradeType);

            NewSubmission.NavigateUrl = Submission.GetUrl(Item.ID);

            SubmissionList.CourseID = Item.CourseID;
            SubmissionList.AssignmentID = Item.ID;
        }
    }
}