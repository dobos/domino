using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Complex.Domino.Lib
{
    public class GitHelper
    {
        private string sessionGuid;
        private User user;
        private Assignment assignment;
        private Submission submission;

        public string SessionGuid
        {
            get { return sessionGuid; }
            set { sessionGuid = value; }
        }

        public User User
        {
            get { return user; }
            set { user = value; }
        }

        public Assignment Assignment
        {
            get { return assignment; }
            set { assignment = value; }
        }

        public Submission Submission
        {
            get { return submission; }
            set { submission = value; }
        }

        public GitHelper()
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.sessionGuid = null;
            this.user = null;
            this.assignment = null;
            this.submission = null;
        }

        private Git.Git CreateGit(string repoPath)
        {
            var git = new Git.Git(repoPath)
            {
                Author = new Git.User()
                {
                    Name = user.Name,
                    Email = String.Format(user.Email)
                }
            };

            return git;
        }

        public string GetRepoPath()
        {
            return Path.Combine(
                DominoConfiguration.Instance.RepositoriesPath,
                user.Name);
        }

        public string GetScratchPath()
        {
            return Path.Combine(
                DominoConfiguration.Instance.ScratchPath,
                sessionGuid,
                user.Name);
        }

        public string GetAssignmentPath()
        {
            return Path.Combine(GetScratchPath(), GetAssignmentPrefixPath());
        }

        public string GetAssignmentPrefixPath()
        {
            return Path.Combine(
                assignment.SemesterName,
                assignment.CourseName,
                assignment.Name);
        }

        public bool IsRepoInitialized()
        {
            var dir = GetRepoPath();
            return IsGitDirectoryInitialized(dir);
        }

        public bool IsScratchInitialized()
        {
            var dir = GetScratchPath();
            return IsGitDirectoryInitialized(dir);
        }

        public bool IsAssignmentInitialized()
        {
            var dir = GetAssignmentPath();
            return Directory.Exists(dir);
        }

        private bool IsGitDirectoryInitialized(string dir)
        {
            if (!Directory.Exists(dir))
            {
                return false;
            }

            // If directory exists it still has to contain a .git folder
            var temp = Path.Combine(dir, ".git");

            if (Directory.Exists(dir))
            {
                return true;
            }

            // Or a file named "HEAD" if it's a bare repo
            temp = Path.Combine(dir, "HEAD");

            if (File.Exists(temp))
            {
                return true;
            }

            return false;
        }

        public void InitializeRepo()
        {
            var dir = GetRepoPath();

            Directory.CreateDirectory(dir);

            var git = CreateGit(dir);

            git.Init(true);
        }

        public void InitializeScratch()
        {
            var repodir = GetRepoPath();
            var scratchdir = GetScratchPath();

            Directory.CreateDirectory(scratchdir);

            var git = CreateGit(scratchdir);
            git.Clone(repodir);

            // Set variables required for commit
            // These values will be overwritten at commit
            git.Config("user.email", user.Email, false);
            git.Config("user.name", user.Name, false);
        }

        public void CheckoutScratchTip()
        {
            var scratchdir = GetScratchPath();
            var git = CreateGit(scratchdir);

            // Fetch all modifications from remote to scratch and
            // then make sure we're at the tip of the branch
            git.Fetch(true);

            // First try to check out the master branch. If it fails,
            // there are changes in the current repo that might need to
            // merged but couldn't be done automatically. In this case
            // we simply reset and throw away changes.
            try
            {
                git.CheckOut("master");
            }
            catch (Git.GitException)
            {
                git.Reset("origin/master", true);
            }
        }

        public void InitializeAssignment()
        {
            var dir = GetAssignmentPath();
            Directory.CreateDirectory(dir);
        }

        public void EnsureRepoExists()
        {
            // Test if user's git repo exists. If not, create it now
            if (!IsRepoInitialized())
            {
                InitializeRepo();
            }
        }

        public void EnsureScratchExists()
        {
            EnsureRepoExists();

            if (!IsScratchInitialized())
            {
                InitializeScratch();
            }
        }

        public void EnsureAssignmentExists()
        {
            EnsureScratchExists();
            CheckoutScratchTip();

            if (!IsAssignmentInitialized())
            {
                InitializeAssignment();
            }
        }

        public bool IsAssignmentEmpty()
        {
            var dir = GetAssignmentPath();
            return Directory.GetFileSystemEntries(dir).Length == 0;
        }

        public void EmptyAssignment()
        {
            var dir = GetAssignmentPath();
            Util.IO.ForceEmptyDirectory(dir);
        }

        /// <summary>
        /// Commits a submission and pushed it to origin
        /// </summary>
        /// <returns></returns>
        public Git.Commit CommitSubmission(string commitMessage)
        {
            var scratchdir = GetScratchPath();
            var git = CreateGit(scratchdir);

            var dir = GetAssignmentPath();

            // Add all/remove all files that have changed under assignment
            // directory in the working tree of the student
            git.Add(dir, true);

            // Commit changes
            git.Commit(commitMessage, true);

            // Try to simply push the commit to origin

            // TODO: It might have happend though, that the origin is now
            // ahead. This is a rare case whan another submission has just
            // happened. In this case, we might just force-push changes but
            // that's dangerous... better throw an exception.

            git.Push("origin", true);

            return git.ReadLog(null, 1)[0];
        }

        public void CheckOutSubmission()
        {
            EnsureScratchExists();

            var scratchdir = GetScratchPath();
            var git = CreateGit(scratchdir);

            // Check-out branch but also reset it, in case it was checked out
            // already but has changed.

            git.CheckOut(submission.Name);
            git.Reset(submission.Name, true);
        }
    }
}
