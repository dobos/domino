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

        public void Init()
        {
            var args = new Arguments();

            args.Append("init");

            GitWrapper.Call(this, args);
        }

        public void AddAll()
        {
            var args = new Arguments();

            args.Append("add");
            args.Append("--all");
            args.Append(".");

            GitWrapper.Call(this, args);
        }

        public void CommitAll(string message)
        {
            var args = new Arguments();

            args.Append("commit");
            args.Append("--all");
            args.Append("--message", message);
            args.Append("--author", author.ToString());

            GitWrapper.Call(this, args);
        }

        public List<Commit> GetLog()
        {
            return GetLog(null);
        }

        public List<Commit> GetLog(string filename)
        {
            var args = new Arguments();
            var commits = new List<Commit>();

            args.Append("log");
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

        public Commit GetCurrentCommit()
        {
            throw new NotImplementedException();
        }

        public void CheckOut(Commit commit)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
