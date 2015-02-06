using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Student
{
    public partial class Course : EntityPage<Lib.Course>
    {
        public static string GetUrl(int courseID)
        {
            return String.Format("~/Student/Course.aspx?ID={0}", courseID);
        }

        protected Lib.CourseGrade courseGrade;

        protected override void CreateItem()
        {
            base.CreateItem();

            // Load grade
            courseGrade = new Lib.CourseGrade(DatabaseContext);
            courseGrade.Load(Item.ID, DatabaseContext.User.ID);

            gradePanel.Visible = courseGrade.Grade > 0;
            grade.Text = courseGrade.Grade > 0 ? courseGrade.Grade.ToString() : "-";
            gradeLabel.Text = Util.Enum.ToLocalized(typeof(Resources.Grades), Item.GradeType);
        }

        protected override void UpdateForm()
        {
            base.UpdateForm();

            Description.Text = Item.Description;
            SemesterDescription.Text = Item.SemesterDescription;

            if (!String.IsNullOrWhiteSpace(Item.Url))
            {
                Url.NavigateUrl = Item.Url;
            }
            else
            {
                Url.Visible = false;
            }

            Comments.Text = Item.Comments;

            AssignmentList.CourseID = Item.ID;
        }
    }
}