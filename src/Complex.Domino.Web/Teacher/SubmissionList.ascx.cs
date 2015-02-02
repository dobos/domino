using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Teacher
{
    public partial class SubmissionList : UserControlBase
    {
        private Lib.SubmissionFactory searchObject;

        public int AssignmentID
        {
            get { return (int)(ViewState["AssignmentID"] ?? -1); }
            set { ViewState["AssignmentID"] = value; }
        }

        public int UserID
        {
            get { return (int)(ViewState["UserID"] ?? -1); }
            set { ViewState["UserID"] = value; }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (submissionList.Visible)
            {
                submissionList.DataBind();
            }
        }

        protected void submissionDataSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            searchObject = new Lib.SubmissionFactory(DatabaseContext);

            searchObject.AssignmentID = AssignmentID;
            searchObject.StudentID = UserID;

            e.ObjectInstance = searchObject;
        }
    }
}