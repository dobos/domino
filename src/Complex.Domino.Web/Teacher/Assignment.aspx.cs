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
        public static string GetUrl()
        {
            return "~/Teacher/Assignment.aspx";
        }

        public static string GetUrl(int assignmentId)
        {
            return String.Format(
                "~/Teacher/Assignment.aspx?{0}={1}",
                Constants.RequestID, assignmentId);
        }

        protected override void UpdateForm()
        {
            base.UpdateForm();

            TitleLabel.Text = Item.IsExisting ? Resources.Labels.ModifyAssignment : Resources.Labels.NewAssignment;

            Course.Text = Item.CourseID.ToString();
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

            Item.StartDate = DateTime.Parse(StartDate.Text);
            Item.EndDate = DateTime.Parse(EndDate.Text);
            Item.EndDateSoft = DateTime.Parse(EndDateSoft.Text);
            Item.Url = Url.Text;
            Item.GradeType = (Lib.GradeType)Enum.Parse(typeof(Lib.GradeType), GradeType.SelectedValue);
            Item.GradeWeight = double.Parse(GradeWeight.Text);
        }
    }
}