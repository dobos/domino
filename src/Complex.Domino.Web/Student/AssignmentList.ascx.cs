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

        protected void assignmentDataSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            searchObject = new Lib.AssignmentFactory(DatabaseContext);

            searchObject.CourseID = CourseID;
            searchObject.UserID = DatabaseContext.User.ID;

            e.ObjectInstance = searchObject;
        }

        protected void assignmentList_OnItemCreated(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var di = (Lib.Assignment)((ListViewDataItem)e.Item).DataItem;

                if (di != null)
                {
                    var semester = e.Item.FindControl("semester") as Label;
                    var course = e.Item.FindControl("course") as Label;
                    var endDateSoft = e.Item.FindControl("endDateSoft") as Label;

                    semester.Text = di.SemesterDescription;
                    course.Text = di.CourseDescription;
                    endDateSoft.Text = Util.DateTime.FormatFancy(di.EndDateSoft);

                    // TODO: use datetime control and formatting
                    if (di.EndDateSoft < DateTime.Now)
                    {
                        endDateSoft.CssClass = "expired";
                    }
                }
            }
        }
    }
}