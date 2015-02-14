using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Complex.Domino.Web
{
    public class SubmissionPage : EntityPage<Lib.Submission>
    {
        protected Panel emptyPanel;
        protected Panel filesPanel;
        protected Panel messagePanel;
        protected Label formLabel;
        protected Label cancelLabel;
        protected LinkButton ok;
        protected Label semesterDescription;
        protected Label courseDescription;
        protected Label assignmentDescription;
        protected Files.FileBrowser fileBrowser;
        protected HtmlTableRow createdDateRow;
        protected Label createdDate;

        private Lib.Semester semester;
        private Lib.Course course;
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

            semester = new Lib.Semester(DatabaseContext);
            semester.Load(assignment.SemesterID);

            course = new Lib.Course(DatabaseContext);
            course.Load(assignment.CourseID);

            Item.AssignmentID = assignment.ID;
            Item.CourseID = assignment.CourseID;

            // If the submission already exists we can take the student from it.
            // TODO: need to extend logic in case of reply by teacher

            if (!Item.IsExisting)
            {
                // This is a new submission by a student
                student = DatabaseContext.User;
            }
            else
            {
                // This is an existing submission
                student = new Lib.User(DatabaseContext);
                student.Load(Item.StudentID);
            }

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

            semesterDescription.Text = semester.Description;
            courseDescription.Text = course.Description;
            assignmentDescription.Text = assignment.Description;

            if (!Item.IsExisting)
            {
                // This is a new submission by a student

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
            else
            {
                // This is an existing submission, the git repo
                // needs to be checked out at a given commit
                gitHelper.CheckOutSubmission();

                SwitchViewToFiles();
            }

            createdDate.Text = Util.DateTime.FormatFancy(Item.CreatedDate);
            createdDateRow.Visible = Item.IsExisting;
        }

        protected Git.Commit CommitSubmission(Lib.Submission submission)
        {
            var comments = submission.Comments;

            if (String.IsNullOrWhiteSpace(comments))
            {
                comments = "(no comments were given)";   // TODO
            }

            // Commit changes into git
            string branch = null;

            if (submission.TeacherID > 0)
            {
                // Teachers should commit into a new branch
                branch = String.Format("teacher_{0:yyMMddhhmmss}", DateTime.Now);
            }
            
            var commit = GitHelper.CommitSubmission(comments, branch);

            return commit;
        }

        private void SwitchViewToFiles()
        {
            if (emptyPanel != null)
            {
                emptyPanel.Visible = false;
            }

            filesPanel.Visible = true;
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

    }
}