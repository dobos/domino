using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Student
{
    public partial class Submission : EntityForm<Lib.Submission>
    {
        protected Lib.Assignment assignment;

        protected int AssignmentID
        {
            get { return Util.UrlParser.ParseInt(Request.QueryString[Constants.RequestAssignmentID]); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            assignment = new Lib.Assignment(DatabaseContext);
            assignment.Load(AssignmentID);

            var wrapper = new Lib.GitWrapper()
            {
                SessionGuid = SessionGuid,
                User = DatabaseContext.User,
                Assignment = assignment,
            };

            wrapper.EnsureAssignmentExists();

            fileBrowser.BasePath = wrapper.GetAssignmentPath();
        }
    }
}