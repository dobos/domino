using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Diagnostics;

namespace Complex.Domino.Git
{
    /// <summary>
    /// Implement a wrapper around the git command-line tool
    /// with minimal functionality to support Domino.
    /// </summary>
    public class Git
    {
        #region Static members

        public static GitConfiguration Configuration
        {
            get
            {
                return (GitConfiguration)ConfigurationManager.GetSection("git");
            }
        }

        #endregion
        #region Private member variables

        private string binPath;
        private string repoPath;
        private User author;

        #endregion
        #region Properties

        public string BinPath
        {
            get { return binPath; }
            set { binPath = value; }
        }

        public string RepoPath
        {
            get { return repoPath; }
            set { repoPath = value; }
        }

        public User Author
        {
            get { return author; }
            set { author = value; }
        }

        #endregion
        #region Constructors and initializers

        public Git()
        {
            InitializeMembers();
        }

        public Git(string repoPath)
            : this()
        {
            this.repoPath = repoPath;
        }

        private void InitializeMembers()
        {
            this.binPath = Configuration.BinPath;
            this.repoPath = ".";
            this.author = new User();
        }

        #endregion
        #region Git wrapper functions

        /// <summary>
        /// Initializes a git repository.
        /// </summary>
        /// <param name="bare"></param>
        public void Init(bool bare)
        {
            var args = new Arguments();

            args.Append("init");
            args.AppendIfTrue(bare, "--bare");

            GitWrapper.Call(this, args);
        }

        /// <summary>
        /// Clones a remote repository locally
        /// </summary>
        /// <param name="remotePath"></param>
        public void Clone(string remotePath)
        {
            var args = new Arguments();

            args.Append("clone");
            args.Append(remotePath);
            args.Append(repoPath);

            GitWrapper.Call(this, args);
        }

        /// <summary>
        /// Sets variables in git config files
        /// </summary>
        /// <param name="variable"></param>
        /// <param name="value"></param>
        /// <param name="global"></param>
        public void Config(string variable, string value, bool global)
        {
            var args = new Arguments();

            args.Append("config");
            args.AppendIfTrue(global, "--global");

            args.Append(variable);
            args.Append(value);

            GitWrapper.Call(this, args);
        }

        /// <summary>
        /// Adds files to the index.
        /// </summary>
        public void Add(string path, bool all)
        {
            var args = new Arguments();

            args.Append("add");
            args.AppendIfTrue(all, "--all");
            args.Append(path);

            GitWrapper.Call(this, args);
        }

        /// <summary>
        /// Commits changes
        /// </summary>
        /// <param name="message"></param>
        /// <param name="all"></param>
        public void Commit(string message, string messageFilePath, bool all)
        {
            var args = new Arguments();

            args.Append("commit");
            args.AppendIfTrue(all, "--all");

            if (message != null)
            {
                args.Append("--message", message);
            }
            else if (messageFilePath != null)
            {
                args.Append("--file", messageFilePath);
            }

            args.Append("--author", author.ToString());

            GitWrapper.Call(this, args);
        }

        /// <summary>
        /// Pushes changes to a remote repository
        /// </summary>
        /// <param name="remote"></param>
        /// <param name="all"></param>
        public void Push(string remote, bool all)
        {
            var args = new Arguments();

            args.Append("push");
            args.AppendIfTrue(all, "--all");
            args.AppendIfNotNull(remote);

            GitWrapper.Call(this, args);
        }

        public void Fetch(bool all)
        {
            var args = new Arguments();

            args.Append("fetch");
            args.AppendIfTrue(all, "--all");

            GitWrapper.Call(this, args);
        }

        public void Reset(string branch, bool hard)
        {
            var args = new Arguments();

            args.Append("reset");
            args.AppendIfTrue(hard, "--hard");
            args.AppendIfNotNull(branch);

            GitWrapper.Call(this, args);
        }

        public List<Commit> ReadLog()
        {
            return ReadLog(null, -1);
        }

        public List<Commit> ReadLog(string filename, int maxCount)
        {
            var args = new Arguments();
            var commits = new List<Commit>();

            args.Append("log");
            args.AppendIfTrue(maxCount > 0, "--max-count", maxCount.ToString());
            args.Append("--pretty", "raw");

            // TODO: single file log

            using (var w = GitWrapper.Start(this, args))
            {
                while (true)
                {
                    var commit = new Commit();

                    if (commit.Read(w.Out))
                    {
                        commits.Add(commit);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return commits;
        }

        public void Pull()
        {
            var args = new Arguments();

            args.Append("pull");

            GitWrapper.Call(this, args);
        }

        public void CheckOut(string branch)
        {
            CheckOut(branch, false);
        }

        public void CheckOut(string branch, bool newBranch)
        {
            var args = new Arguments();

            args.Append("checkout");

            if (newBranch)
            {
                args.Append("-b", branch);
            }
            else
            {
                args.Append(branch);
            }

            GitWrapper.Call(this, args);
        }

        #endregion
    }
}
