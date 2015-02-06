using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Student
{
    public partial class CourseList : UserControlBase
    {
        private Lib.CourseFactory searchObject;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (courseList.Visible)
            {
                courseList.DataBind();
            }
        }

        protected void courseDataSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            searchObject = new Lib.CourseFactory(DatabaseContext);

            // Limit to user
            searchObject.UserID = DatabaseContext.User.ID;
            searchObject.RoleType = Lib.UserRoleType.Student;

            // Set search criteria
            if (Request.QueryString[Constants.RequestSemesterID] != null)
            {
                searchObject.SemesterID = int.Parse(Request.QueryString[Constants.RequestSemesterID]);
            }

            e.ObjectInstance = searchObject;
        }

        protected void CourseList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var course = (Lib.Course)((ListViewDataItem)e.Item).DataItem;

                if (course != null)
                {
                    var grade = e.Item.FindControl("grade") as Label;
                    var gradeLabel = e.Item.FindControl("gradeLabel") as Label;

                    // Load grade
                    var courseGrade = new Lib.CourseGrade(DatabaseContext);
                    courseGrade.Load(course.ID, DatabaseContext.User.ID);

                    grade.Text = courseGrade.Grade > 0 ? courseGrade.Grade.ToString() : "-";
                    gradeLabel.Text = Util.Enum.ToLocalized(typeof(Resources.Grades), course.GradeType);
                }
            }
        }
    }
}