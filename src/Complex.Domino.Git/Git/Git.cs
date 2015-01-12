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

        public void Init(bool bare)
        {
            var args = new Arguments();

            args.Append("init");

            if (bare)
            {
                args.Append("--bare");
            }

            GitWrapper.Call(this, args);
        }

        public void Config(string variable, string value, bool global)
        {
            var args = new Arguments();

            args.Append("config");

            if (global)
            {
                args.Append("--global");
            }

            args.Append(variable);
            args.Append(value);

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

        public List<Commit> ReadLog()
        {
            return ReadLog(null);
        }

        public List<Commit> ReadLog(string filename)
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

        public void Clone(string remotePath)
        {
            var args = new Arguments();

            args.Append("clone");
            args.Append(remotePath);
            args.Append(repoPath);

            GitWrapper.Call(this, args);
        }

        public void PushAll(string remote)
        {
            var args = new Arguments();

            args.Append("push");
            args.Append("--all");
            args.Append(remote);

            GitWrapper.Call(this, args);
        }

        public Commit GetHeadCommit()
        {
            var args = new Arguments();

            args.Append("rev-parse");
            args.Append("HEAD");

            var hash = GitWrapper.Call(this, args);

            return new Commit()
            {
                Author = author,
                Date = DateTime.UtcNow,
                Hash = hash
            };
        }

        #endregion
    }
}
