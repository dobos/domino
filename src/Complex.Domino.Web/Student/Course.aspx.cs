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

        protected override void UpdateForm()
        {
            base.UpdateForm();

            TitleLabel.Text = Item.Description;
            SemesterDescription.Text = Item.SemesterDescription;
            CourseDescription.Text = Item.Description;

            if (!String.IsNullOrWhiteSpace(Item.Url))
            {
                Url.Text = Item.Url;
                Url.NavigateUrl = Item.Url;
            }
            else
            {
                UrlRow.Visible = false;
            }

            if (!String.IsNullOrWhiteSpace(Item.Comments))
            {
                Comments.Text = Item.Comments;
            }
            else
            {
                CommentsPanel.Visible = false;
            }

            AssignmentList.CourseID = Item.ID;
        }
    }
}