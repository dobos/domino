using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web
{
    public class SubmissionPage : EntityPage<Lib.Submission>
    {
        protected Panel emptyPanel;
        protected Panel filesPanel;
        protected Label formLabel;
        protected Label cancelLabel;
        protected LinkButton ok;
        protected Label semesterDescription;
        protected Label courseDescription;
        protected Label assignmentDescription;
        protected Controls.FileBrowser fileBrowser;

        private Lib.Assignment assignment;
        private Lib.User student;
        private Lib.GitHelper gitHelper;

        protected int AssignmentID
        {
            get { return Util.Url.ParseInt(Request.QueryString[Constants.RequestAssignmentID]); }
        }

        protected Lib.Assignment Assignment
        {
            get { return assignment; }
        }

        protected Lib.User Student
        {
            get { return student; }
        }

        protected Lib.GitHelper GitHelper
        {
            get { return gitHelper; }
        }

        protected override void CreateItem()
        {
            base.CreateItem();

            assignment = new Lib.Assignment(DatabaseContext);
            assignment.Load(this.AssignmentID);

            Item.AssignmentID = assignment.ID;
            Item.CourseID = assignment.CourseID;

            // If the submission already exists we can take the student from
            // it.
            // TODO: need to extend logic in case of reply by teacher

            if (Item.IsExisting)
            {
                student = new Lib.User(DatabaseContext);
                student.Load(Item.StudentID);
            }
            else
            {
                // TODO: add reply-to logic here
                student = DatabaseContext.User;
            }

            // Initialize the git helper class with current session info

            gitHelper = new Lib.GitHelper()
            {
                SessionGuid = SessionGuid,
                Author = DatabaseContext.User,
                Student = student,
                Assignment = assignment,
                Submission = Item,
            };

            fileBrowser.PrefixPath = gitHelper.GetAssignmentPrefixPath();
            fileBrowser.BasePath = gitHelper.GetAssignmentPath();
        }

        protected override void UpdateForm()
        {
            base.UpdateForm();

            var semester = new Lib.Semester(DatabaseContext);
            semester.Load(assignment.SemesterID);

            var course = new Lib.Course(DatabaseContext);
            course.Load(assignment.CourseID);

            semesterDescription.Text = semester.Description;
            courseDescription.Text = course.Description;
            assignmentDescription.Text = assignment.Description;

            fileBrowser.AllowDelete = !Item.IsExisting;
            fileBrowser.AllowUpload = !Item.IsExisting;

            if (Item.IsExisting)
            {
                // Check out the current submission to view files
                // TODO: might need to be replace with smarter solution
                // that read file contents from git directly
                gitHelper.CheckOutSubmission();

                SwitchViewToFiles();
            }
            else
            {
                // Make sure the git repo is checked out, the assigment exists
                // and it is the tip of the branch
                gitHelper.EnsureAssignmentExists();

                // If this a first time visit to the page the user can choose whether to
                // keep existing files in the submission folder or delete them
                if (gitHelper.IsAssignmentEmpty())
                {
                    SwitchViewToFiles();
                }
            }
        }

        protected Git.Commit CommitSubmission(Lib.Submission submission)
        {
            var comments = submission.Comments;

            if (String.IsNullOrWhiteSpace(comments))
            {
                comments = "(no comments were given)";   // TODO
            }

            // Commit changes into git
            var commit = GitHelper.CommitSubmission(comments);

            return commit;
        }

        protected void NewSubmissionKeep_Click(object sender, EventArgs e)
        {
            SwitchViewToFiles();
        }

        protected void NewSubmissionEmpty_Click(object sender, EventArgs e)
        {
            gitHelper.EmptyAssignment();
            SwitchViewToFiles();
        }

        private void SwitchViewToFiles()
        {
            if (emptyPanel != null)
            {
                emptyPanel.Visible = false;
            }

            filesPanel.Visible = true;
        }

        
    }
}