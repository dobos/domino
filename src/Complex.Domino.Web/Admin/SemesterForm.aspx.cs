using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Admin
{
    public partial class SemesterForm : EntityForm<Lib.Semester>
    {
        public static string GetUrl()
        {
            return "~/Admin/SemesterForm.aspx";
        }

        protected override void UpdateForm()
        {
            base.UpdateForm();

            StartDate.Text = Item.StartDate.ToString();
            EndDate.Text = Item.EndDate.ToString();
        }

        protected override void SaveForm()
        {
            base.SaveForm();

            Item.StartDate = DateTime.Parse(StartDate.Text);
            Item.EndDate = DateTime.Parse(EndDate.Text);
        }
    }
}