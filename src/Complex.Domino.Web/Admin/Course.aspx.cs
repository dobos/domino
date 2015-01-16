using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Admin
{
    public partial class Course : EntityForm<Lib.Course>
    {
        public static string GetUrl()
        {
            return "~/Admin/Course.aspx";
        }

        protected override void UpdateForm()
        {
            base.UpdateForm();

            RefreshSemesterList();

            Semester.SelectedValue = Item.Semester.Description;
            StartDate.Text = Item.StartDate.ToString();
            EndDate.Text = Item.EndDate.ToString();
            Url.Text = Item.Url;
            // TODO = Item.HtmlPage;
            GradeType.SelectedValue = Item.GradeType.ToString();
        }

        protected override void SaveForm()
        {
            base.SaveForm();

            Item.Semester.ID = int.Parse(Semester.SelectedValue);
            Item.StartDate = DateTime.Parse(StartDate.Text);
            Item.EndDate = DateTime.Parse(EndDate.Text);
            Item.Url = Url.Text;
            // Item.HtmlPage = // TODO
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