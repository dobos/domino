using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Complex.Domino.Lib
{
    public class GitWrapper
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

        public GitWrapper()
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

            if (!IsAssignmentInitialized())
            {
                InitializeAssignment();
            }
        }

        public string CommitSubmission()
        {
            var scratchdir = GetScratchPath();
            
            var dir = GetAssignmentPath();

            var git = CreateGit(scratchdir);

            git.AddAll();
            git.CommitAll("just a commit of everything");
            git.PushAll("origin");

            return git.GetHeadCommit().Hash;
        }
    }
}
