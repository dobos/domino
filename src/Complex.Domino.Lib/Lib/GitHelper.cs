﻿using System;
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
        private User author;
        private User student;
        private Assignment assignment;
        private Submission submission;

        public string SessionGuid
        {
            get { return sessionGuid; }
            set { sessionGuid = value; }
        }

        /// <summary>
        /// Gets or sets the user authoring the submission
        /// </summary>
        public User Author
        {
            get { return author; }
            set { author = value; }
        }

        /// <summary>
        /// Gets or sets the user who owns the submission
        /// </summary>
        public User Student
        {
            get { return student; }
            set { student = value; }
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
            this.author = null;
            this.student = null;
            this.assignment = null;
            this.submission = null;
        }

        private Git.Git CreateGit(string repoPath)
        {
            // Validate settings
            if (author != null && String.IsNullOrWhiteSpace(author.Name))
            {
                throw Error.InvalidUserName(author.Name);
            }

            if (author != null && String.IsNullOrWhiteSpace(author.Email))
            {
                throw Error.InvalidUserEmail();
            }

            var git = new Git.Git(repoPath);

            if (author != null)
            {
                git.Author = new Git.User()
                {
                    Name = author.Name.Trim(),
                    Email = String.Format(author.Email.Trim())
                };
            }

            return git;
        }

        public string GetRepoPath()
        {
            // Take path from the name of the owner

            return Path.Combine(
                DominoConfiguration.Instance.RepositoriesPath,
                student.Name.Trim());
        }

        public string GetScratchPath()
        {
            // Take path from the name of the owner

            return Path.Combine(
                DominoConfiguration.Instance.ScratchPath,
                sessionGuid,
                student.Name.Trim());
        }

        public string GetAssignmentPath()
        {
            return Path.Combine(
                GetScratchPath(),
                assignment.SemesterName.Trim(),
                assignment.CourseName.Trim(),
                assignment.Name.Trim());
        }

        public string GetAssignmentPrefixPath()
        {
            return Path.Combine(
                student.Name.Trim(),
                assignment.SemesterName.Trim(),
                assignment.CourseName.Trim(),
                assignment.Name.Trim());
        }

        public bool IsRepoInitialized()
        {
            var dir = GetRepoPath();
            return IsRepoInitialized(dir);
        }

        public bool IsScratchInitialized()
        {
            var dir = GetScratchPath();
            return IsRepoInitialized(dir);
        }

        public bool IsAssignmentInitialized()
        {
            var dir = GetAssignmentPath();
            return Directory.Exists(dir);
        }

        private bool IsRepoInitialized(string dir)
        {
            if (!Directory.Exists(dir))
            {
                return false;
            }

            // If directory exists it still has to contain a .git folder
            var temp = Path.Combine(dir, ".git");

            if (Directory.Exists(temp))
            {
                return true;
            }

            // Or a file named "HEAD" if it's a bare repo
            temp = Path.Combine(dir, "HEAD");

            if (System.IO.File.Exists(temp))
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
            git.Config("user.email", author.Email.Trim(), false);
            git.Config("user.name", author.Name.Trim(), false);
        }

        public void CheckoutScratchTip()
        {
            var scratchdir = GetScratchPath();
            var git = CreateGit(scratchdir);

            // Fetch all modifications from remote to scratch and
            // then make sure we're at the tip of the branch
            git.Fetch(true);

            // First try to check out the master branch. If it fails,
            // there are changes in the current repo that might need to be
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

                // Also, create scratch and check in a dummy page
                // so that we have a HEAD

                InitializeScratch();

                CreateInitialCommit();
            }
        }

        private void CreateInitialCommit()
        {
            var scratchdir = GetScratchPath();
            var git = CreateGit(scratchdir);

            // TODO: take text from resource

            System.IO.File.WriteAllText(
                Path.Combine(scratchdir, "README.md"),
                String.Format("Personal Domino repository of {0}.", author.Name.Trim()));

            git.Add("README.md", false);
            git.Commit("Initialized personal Domino repo", null, false);

            // Push initial commit, otherwise it won't work
            git.Push("origin", true);
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
            EnsureRepoExists();
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
        public Git.Commit CommitSubmission(string commitMessage, string branch)
        {
            var scratchdir = GetScratchPath();
            var git = CreateGit(scratchdir);

            var dir = GetAssignmentPath();

            // If it has to go into a separate branch, switch to it
            // This functionality is used when teachers send replies.
            if (branch != null)
            {
                git.CheckOut(branch, true);
            }

            // Add all/remove all files that have changed under assignment
            // directory in the working tree of the student
            git.Add(dir, true);

            // Save comments into a file

            var msgpath = Path.Combine(scratchdir, dir, "___commit_message");

            System.IO.File.WriteAllText(msgpath, commitMessage);

            // Commit changes
            git.Commit(null, msgpath, true);

            System.IO.File.Delete(msgpath);

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
            git.Pull();
            git.CheckOut(submission.Name);
            git.Reset(submission.Name, true);
        }

        public List<Git.Match> Search(string pattern)
        {
            var repodir = GetRepoPath();
            var git = CreateGit(repodir);

            // Execute search
            return git.Grep(pattern, submission.Name, null);
        }
    }
}
