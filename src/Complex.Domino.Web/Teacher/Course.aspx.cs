using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Teacher
{
    public partial class Course : EntityPage<Lib.Course>
    {
        public static string GetUrl(int courseId)
        {
            return String.Format(
                "~/Teacher/Course.aspx?{0}={1}",
                Constants.RequestID, courseId);
        }

        protected override void UpdateForm()
        {
            base.UpdateForm();

            TitleLabel.Text = Item.IsExisting ? Resources.Labels.ModifyCourse : Resources.Labels.NewCourse;

            RefreshSemesterList();

            Semester.SelectedValue = Item.SemesterID.ToString();
            StartDate.Text = Item.StartDate.ToString();
            EndDate.Text = Item.EndDate.ToString();
            Url.Text = Item.Url;
            GradeType.SelectedValue = Item.GradeType.ToString();
        }

        protected override void SaveForm()
        {
            base.SaveForm();

            Item.SemesterID = int.Parse(Semester.SelectedValue);
            Item.StartDate = DateTime.Parse(StartDate.Text);
            Item.EndDate = DateTime.Parse(EndDate.Text);
            Item.Url = Url.Text;
            Item.GradeType = (Lib.GradeType)Enum.Parse(typeof(Lib.GradeType), GradeType.SelectedValue);
        }

        private void RefreshSemesterList()
        {
            var f = new Lib.SemesterFactory(DatabaseContext);
            Semester.DataSource = f.Find().ToArray();
            Semester.DataBind();
        }
    }
}