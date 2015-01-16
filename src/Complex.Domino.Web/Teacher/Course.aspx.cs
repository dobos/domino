using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Teacher
{
    public partial class Course : EntityForm<Lib.Course>
    {
        public static string GetUrl()
        {
            return "~/Teacher/Course.aspx";
        }

        protected override void UpdateForm()
        {
            base.UpdateForm();

            Semester.Text = Item.Semester.Description;
            StartDate.Text = Item.StartDate.ToString();
            EndDate.Text = Item.EndDate.ToString();
            Url.Text = Item.Url;
            // TODO = Item.HtmlPage;
            GradeType.SelectedValue = Item.GradeType.ToString();
        }

        protected override void SaveForm()
        {
            base.SaveForm();

            Item.Url = Url.Text;
            // Item.HtmlPage = // TODO
            Item.GradeType = (Lib.GradeType)Enum.Parse(typeof(Lib.GradeType), GradeType.SelectedValue);
        }
    }
}