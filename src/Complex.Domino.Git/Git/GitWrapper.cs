using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace Complex.Domino.Git
{
    /// <summary>
    /// Wraps the git process into a disposable class that
    /// flushes buffers and waits for process to exit on dispose.
    /// </summary>
    class GitWrapper : IDisposable
    {
        private Process process;

        public TextReader Out
        {
            get
            {
                return process.StandardOutput;
            }
        }

        public static void Call(Git git, Arguments args)
        {
            using (var w = Start(git, args)) { }
        }

        public static GitWrapper Start(Git git, Arguments args)
        {
            var wrapper = new GitWrapper();

            var pinfo = new ProcessStartInfo()
            {
                FileName = Path.Combine(git.BinPath, "git.exe"),
                WorkingDirectory = git.RepoPath,
                Arguments = args.ToString(),

                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,

                CreateNoWindow = true,
                UseShellExecute = false,
                LoadUserProfile = true,
            };

            wrapper.process = new Process()
            {
                StartInfo = pinfo,
            };

            wrapper.process.Start();

            return wrapper;
        }

        public void Dispose()
        {
            // Flush buffers
            process.StandardOutput.ReadToEnd();
            
            var error = process.StandardError.ReadToEnd();

            process.WaitForExit(30000);
            process.WaitForExit();

            process.Close();

            if (!String.IsNullOrEmpty(error) && error.Contains("error"))
            {
                throw new GitException(error);
            }
        }
    }
}
