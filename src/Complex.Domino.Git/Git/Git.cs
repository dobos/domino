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
        }

        #endregion
        #region Git wrapper functions

        public void Init()
        {
            CallGit("init");
        }

        #endregion
        #region Process activation functions

        private void CallGit(string arguments)
        {
            var pinfo = new ProcessStartInfo()
            {
                FileName = Path.Combine(binPath, "git.exe"),
                WorkingDirectory = repoPath,
                Arguments = arguments,

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
