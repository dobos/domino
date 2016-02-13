using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Teacher
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
    }
}