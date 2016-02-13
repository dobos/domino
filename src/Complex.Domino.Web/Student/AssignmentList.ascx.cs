using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Student
{
    public partial class AssignmentList : UserControlBase
    {
        private Lib.AssignmentFactory searchObject;

        public int CourseID
        {
            get { return (int)(ViewState["CourseID"] ?? -1); }
            set { ViewState["CourseID"] = value; }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (assignmentList.Visible)
            {
                assignmentList.DataBind();
            }
        }

        protected void AssignmentDataSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            searchObject = new Lib.AssignmentFactory(DatabaseContext);

            searchObject.CourseID = CourseID;
            searchObject.UserID = DatabaseContext.User.ID;

            if (CourseID != -1)
            {
                searchObject.SemesterFilter = Lib.DateTimeFilter.All;
            }
            else
            {
                searchObject.SemesterFilter = Lib.DateTimeFilter.Active;
            }

            e.ObjectInstance = searchObject;
        }

        protected void AssignmentList_OnItemCreated(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var assignment = (Lib.Assignment)((ListViewDataItem)e.Item).DataItem;

                if (assignment != null)
                {
                    var semester = e.Item.FindControl("semester") as Label;
                    var course = e.Item.FindControl("course") as Label;
                    var endDateSoft = e.Item.FindControl("endDateSoft") as Label;
                    var grade = e.Item.FindControl("grade") as Label;
                    var gradeLabel = e.Item.FindControl("gradeLabel") as Label;

                    semester.Text = assignment.SemesterDescription;
                    course.Text = assignment.CourseDescription;
                    endDateSoft.Text = Util.DateTime.FormatFancy(assignment.EndDateSoft);

                    // TODO: use datetime control and formatting
                    if (assignment.EndDateSoft < DateTime.Now)
                    {
                        endDateSoft.CssClass = "expired";
                    }

                    // Load grade
                    var assignmentGrade = new Lib.AssignmentGrade(DatabaseContext);
                    assignmentGrade.Load(assignment.ID, DatabaseContext.User.ID);

                    grade.Text = assignmentGrade.Grade > 0 ? assignmentGrade.Grade.ToString() : "-";
                    gradeLabel.Text = Util.Enum.ToLocalized(typeof(Resources.Grades), assignment.GradeType);
                }
            }
        }
    }
}