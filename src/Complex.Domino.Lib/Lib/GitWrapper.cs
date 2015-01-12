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
            dir = Path.Combine(dir, ".git");

            if (!Directory.Exists(dir))
            {
                return false;
            }

            return true;
        }

        public void InitializeRepo()
        {
            var dir = GetRepoPath();

            Directory.CreateDirectory(dir);

            var git = new Git.Git(dir);

            git.Init();
        }

        public void InitializeScratch()
        {
            var repodir = GetRepoPath();
            var scratchdir = GetScratchPath();

            Directory.CreateDirectory(scratchdir);

            var git = new Git.Git(scratchdir);

            git.Clone(repodir);
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
    }
}
