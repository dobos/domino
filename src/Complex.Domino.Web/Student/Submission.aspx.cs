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
        protected Lib.GitHelper git;
        protected Lib.Assignment assignment;

        protected int AssignmentID
        {
            get { return Util.UrlParser.ParseInt(Request.QueryString[Constants.RequestAssignmentID]); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            assignment = new Lib.Assignment(DatabaseContext);
            assignment.Load(AssignmentID);

            git = new Lib.GitHelper()
            {
                SessionGuid = SessionGuid,
                User = DatabaseContext.User,
                Assignment = assignment,
            };

            if (!IsPostBack)
            {
                git.EnsureAssignmentExists();
            }

            fileBrowser.BasePath = git.GetAssignmentPath();
        }

        protected override void SaveForm()
        {
            base.SaveForm();

            // Commit changes into git
            Item.StudentID = DatabaseContext.User.ID;
            Item.AssignmentID = assignment.ID;
            Item.Date = DateTime.UtcNow;
            Item.Direction = Lib.SubmissionDirection.StudentToTeacher;
            Item.GitCommitHash = git.CommitSubmission();
        }
    }
}