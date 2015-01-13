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
            // A submission is always associated with an assignment, load it

            assignment = new Lib.Assignment(DatabaseContext);
            assignment.Load(AssignmentID);

            // Initialize the git helper class with current session info

            git = new Lib.GitHelper()
            {
                SessionGuid = SessionGuid,
                User = DatabaseContext.User,
                Assignment = assignment,
            };

            if (!IsPostBack)
            {
                // If this a first time visit to the page the user can choose whether to
                // keep existing files in the submission folder or delete them

                // Make sure the git repo is checked out, the assigment exists
                // and it is the tip of the branch
                git.EnsureAssignmentExists();

                if (git.IsAssignmentEmpty())
                {
                    SwitchViewToUpload();
                }
            }

            fileBrowser.BasePath = git.GetAssignmentPath();
        }

        protected void NewSubmissionKeep_Click(object sender, EventArgs e)
        {
            SwitchViewToUpload();
        }

        protected void NewSubmissionEmpty_Click(object sender, EventArgs e)
        {
            git.EmptyAssignment();
            SwitchViewToUpload();
        }

        private void SwitchViewToUpload()
        {
            newSubmissionPanel.Visible = false;
            uploadSubmissionPanel.Visible = true;
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