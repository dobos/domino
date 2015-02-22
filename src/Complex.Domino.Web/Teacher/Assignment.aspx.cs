using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Teacher
{
    public partial class Assignment : EntityPage<Lib.Assignment>
    {
        public static string GetUrl(int courseId, int assignmentId)
        {
            var par = "";

            if (courseId > 0)
            {
                par += String.Format(
                    "&{0}={1}", Constants.RequestCourseID, courseId);
            }

            if (assignmentId > 0)
            {
                par += String.Format(
                    "&{0}={1}", Constants.RequestID, assignmentId);
            }

            if (par.Length > 0)
            {
                par = "?" + par.Substring(1);
            }

            return "~/Teacher/Assignment.aspx" + par;
        }

        protected Lib.Course course;

        public int CourseID
        {
            get { return int.Parse(Request["CourseID"] ?? "-1"); }
        }

        protected override void CreateItem()
        {
            base.CreateItem();

            course = new Lib.Course(DatabaseContext);
            course.Load(CourseID);
        }

        protected override void UpdateForm()
        {
            base.UpdateForm();

            TitleLabel.Text = Item.IsExisting ? Resources.Labels.ModifyAssignment : Resources.Labels.NewAssignment;

            CourseDescription.Text = course.Description;
            StartDate.Text = Item.StartDate.ToString();
            EndDate.Text = Item.EndDate.ToString();
            EndDateSoft.Text = Item.EndDateSoft.ToString();
            Url.Text = Item.Url;
            GradeType.SelectedValue = Item.GradeType.ToString();
            GradeWeight.Text = Item.GradeWeight.ToString();

            Plugins.Visible = Item.IsExisting;
            Plugins.SemesterID = Item.SemesterID;
            Plugins.CourseID = Item.CourseID;
            Plugins.AssignmentID = Item.ID;
        }

        protected override void SaveForm()
        {
            base.SaveForm();

            Item.CourseID = course.ID;
            Item.StartDate = DateTime.Parse(StartDate.Text);
            Item.EndDate = DateTime.Parse(EndDate.Text);
            Item.EndDateSoft = DateTime.Parse(EndDateSoft.Text);
            Item.Url = Url.Text;
            Item.GradeType = (Lib.GradeType)Enum.Parse(typeof(Lib.GradeType), GradeType.SelectedValue);
            Item.GradeWeight = double.Parse(GradeWeight.Text);
        }

        protected override void OnOkClick()
        {
            if (Item.IsExisting)
            {
                base.OnOkClick();
            }
            else
            {
                SaveForm();
                Item.Save();
                UpdateForm();
            }
        }
    }
}