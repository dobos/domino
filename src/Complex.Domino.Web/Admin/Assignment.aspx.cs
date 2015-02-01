using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Admin
{
    public partial class Assignment : EntityPage<Lib.Assignment>
    {
        public static string GetUrl()
        {
            return "~/Admin/Assignment.aspx";
        }

        protected override void UpdateForm()
        {
            base.UpdateForm();

            TitleLabel.Text = Item.IsExisting ? Resources.Labels.ModifyAssignment : Resources.Labels.NewAssignment;

            RefreshCourseList();

            Course.SelectedValue = Item.CourseID.ToString();
            StartDate.Text = Item.StartDate.ToString();
            EndDate.Text = Item.EndDate.ToString();
            EndDateSoft.Text = Item.EndDateSoft.ToString();
            Url.Text = Item.Url;
            GradeType.SelectedValue = Item.GradeType.ToString();
            GradeWeight.Text = Item.GradeWeight.ToString();
        }

        protected override void SaveForm()
        {
            base.SaveForm();

            Item.CourseID = int.Parse(Course.SelectedValue);
            Item.StartDate = DateTime.Parse(StartDate.Text);
            Item.EndDate = DateTime.Parse(EndDate.Text);
            Item.EndDateSoft = DateTime.Parse(EndDateSoft.Text);
            Item.Url = Url.Text;
            Item.GradeType = (Lib.GradeType)Enum.Parse(typeof(Lib.GradeType), GradeType.SelectedValue);
            Item.GradeWeight = double.Parse(GradeWeight.Text);
        }

        private void RefreshCourseList()
        {
            // TODO: filter for user

            var f = new Lib.CourseFactory(DatabaseContext);
            Course.DataSource = f.Find().ToArray();
            Course.DataBind();
        }
    }
}