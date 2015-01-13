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

        public GitHelper()
        {
        }

        private Git.Git CreateGit(string repoPath)
        {
            var git = new Git.Git(repoPath)
            {
                Author = new Git.User()
                {
                    Name = user.Username,
                    Email = String.Format(user.Email)
                }
            };

            return git;
        }

        public string GetRepoPath()
        {
            return Path.Combine(
                DominoConfiguration.Instance.RepositoriesPath,
                user.Username);
        }

        public string GetScratchPath()
        {
            return Path.Combine(
                DominoConfiguration.Instance.ScratchPath,
                sessionGuid,
                user.Username);
        }

        public string GetAssignmentPath()
        {
            return Path.Combine(
                GetScratchPath(),
                assignment.CourseID.ToString(),
                assignment.ID.ToString());
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
            git.Config("user.name", user.Username, false);
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

        public string CommitSubmission()
        {
            var scratchdir = GetScratchPath();
            
            var dir = GetAssignmentPath();

            var git = CreateGit(scratchdir);

            git.Add(".", true);
            git.Commit("just a commit of everything", true);       // TODO
            git.Push("origin", true);

            return git.GetHeadCommit().Hash;
        }
    }
}
