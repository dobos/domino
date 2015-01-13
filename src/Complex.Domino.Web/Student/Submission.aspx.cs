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

            if (Item.IsExisting)
            {
                assignment.Load(Item.AssignmentID);
            }
            else
            {
                assignment.Load(AssignmentID);
            }

            // Initialize the git helper class with current session info

            git = new Lib.GitHelper()
            {
                SessionGuid = SessionGuid,
                User = DatabaseContext.User,
                Assignment = assignment,
                Submission = Item,
            };

            fileBrowser.BasePath = git.GetAssignmentPath();            
        }

        protected void NewSubmissionKeep_Click(object sender, EventArgs e)
        {
            SwitchViewToFiles();
        }

        protected void NewSubmissionEmpty_Click(object sender, EventArgs e)
        {
            git.EmptyAssignment();
            SwitchViewToFiles();
        }

        private void SwitchViewToFiles()
        {
            emptyPanel.Visible = false;
            filesPanel.Visible = true;
        }

        protected override void UpdateForm()
        {
            base.UpdateForm();

            fileBrowser.AllowDelete = !Item.IsExisting;
            fileBrowser.AllowEdit = !Item.IsExisting;
            fileBrowser.AllowUpload = !Item.IsExisting;

            if (Item.IsExisting)
            {
                // Check out the current submission to view files
                // TODO: might need to be replace with smarter solution
                // that read file contents from git directly
                git.CheckOutSubmission();

                SwitchViewToFiles();
            }
            else
            {
                // Make sure the git repo is checked out, the assigment exists
                // and it is the tip of the branch
                git.EnsureAssignmentExists();

                // If this a first time visit to the page the user can choose whether to
                // keep existing files in the submission folder or delete them
                if (git.IsAssignmentEmpty())
                {
                    SwitchViewToFiles();
                }
            }
        }

        protected override void SaveForm()
        {
            base.SaveForm();

            // Commit changes into git
            var commit = git.CommitSubmission("comments");  // TODO: use comments

            Item.StudentID = DatabaseContext.User.ID;
            Item.AssignmentID = assignment.ID;
            Item.Direction = Lib.SubmissionDirection.StudentToTeacher;
            Item.Date = commit.Date;
            Item.GitCommitHash = commit.Hash;
        }
    }
}