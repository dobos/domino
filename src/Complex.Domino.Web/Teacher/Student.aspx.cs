using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Teacher
{
    public partial class Student : EntityPage<Lib.User>
    {
        public static string GetUrl(int studentId)
        {
            return String.Format(
                "~/Teacher/Student.aspx?{0}={1}",
                Constants.RequestID, studentId);
        }

        public static string GetUrl(int courseId, int studentId)
        {
            return String.Format(
                "~/Teacher/Student.aspx?{0}={1}&{2}={3}",
                Constants.RequestCourseID, courseId,
                Constants.RequestID, studentId);
        }

        protected int CourseID
        {
            get { return int.Parse(Request.QueryString["courseId"] ?? "-1"); }
        }

        protected override void UpdateForm()
        {
            base.UpdateForm();

            Email.Text = Item.Email;
            Email.NavigateUrl = "mailto:" + Item.Email;

            submissionList.CourseID = CourseID;
            submissionList.UserID = ID;
        }
    }
}