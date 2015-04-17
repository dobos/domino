using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private static readonly Regex ErrorRegex = new Regex(@"(^error:|^fatal:)", RegexOptions.Multiline | RegexOptions.IgnoreCase);

        private Git git;
        private Arguments args;
        private Process process;

        public TextReader Out
        {
            get
            {
                return process.StandardOutput;
            }
        }

        public static string Call(Git git, Arguments args)
        {
            using (var wrapper = Start(git, args)) 
            {
                var output = wrapper.process.StandardOutput.ReadToEnd();
                return output;
            }
        }

        public static GitWrapper Start(Git git, Arguments args)
        {
            var wrapper = new GitWrapper()
            {
                git = git,
                args = args
            };

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
            
            var error = process.StandardError.ReadToEnd().Trim();

            // TODO: timeout from config
            process.WaitForExit(30000);

            process.Close();

            if (!String.IsNullOrEmpty(error) && ErrorRegex.Match(error).Success)
            {
                error += "\r\nCommand: git " + args.ToString();
                throw new GitException(error);
            }
        }
    }
}
