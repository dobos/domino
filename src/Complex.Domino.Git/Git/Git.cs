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
        private Author author;

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

        public Author Author
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
            this.author = new Author();
        }

        #endregion
        #region Git wrapper functions

        public void Init()
        {
            var args = new Arguments();

            args.Append("init");

            CallGit(args);
        }

        public void Add(string filename)
        {
            throw new NotImplementedException();
        }

        public void AddAll()
        {
            var args = new Arguments();

            args.Append("add");
            args.Append("--all");
            args.Append(".");

            CallGit(args);
        }

        public void Remove(string filename)
        {
            throw new NotImplementedException();
        }

        public void CommitAll(string message)
        {
            var args = new Arguments();

            args.Append("commit");
            args.Append("--all");
            args.Append("--message", message);
            args.Append("--author", author.ToString());

            CallGit(args);
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
        #region Process activation functions

        private void CallGit(Arguments args)
        {
            var pinfo = new ProcessStartInfo()
            {
                FileName = Path.Combine(binPath, "git.exe"),
                WorkingDirectory = repoPath,
                Arguments = args.ToString(),

                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,

                CreateNoWindow = true,
                UseShellExecute = false,
                LoadUserProfile = false,
            };

            var proc = new Process()
            {
                StartInfo = pinfo,
            };

            proc.Start();

            proc.WaitForExit();
        }

        #endregion
    }
}
