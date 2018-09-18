using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Web.UI;

namespace Complex.Domino.Plugins
{
    public class ProcessWrapper : IDisposable
    {
        private string workingDirectory;
        private string command;
        private string path;

        private AutoResetEvent evnt;
        private Process process;

        private StringBuilder console;
        private StringWriter consoleOutput;
        private HtmlTextWriter consoleHtmlOutput;

        public string WorkingDirectory
        {
            get { return workingDirectory; }
            set { workingDirectory = value; }
        }

        public string Command
        {
            get { return command; }
            set { command = value; }
        }

        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        public string Console
        {
            get { return console.ToString(); }
        }

        public ProcessWrapper()
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.workingDirectory = null;
            this.command = null;
            this.path = null;
        }

        public void Dispose()
        {
            process.Close();
            process = null;

            evnt.Dispose();
            evnt = null;

            consoleHtmlOutput.Dispose();
            consoleHtmlOutput = null;

            consoleOutput.Dispose();
            consoleOutput = null;
        }

        public void Call()
        {
            Start();
            WaitForExit();
        }

        public void Start()
        {
            string filename, arguments;
            GetCommandLine(command, out filename, out arguments);

            evnt = new AutoResetEvent(false);

            process = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    WorkingDirectory = workingDirectory,
                    FileName = System.IO.Path.Combine(workingDirectory, filename),
                    Arguments = arguments,

                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,

                    LoadUserProfile = false,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            AppendPath(process, path);

            // Set up IO redirection
            console = new StringBuilder();
            consoleOutput = new StringWriter(console);
            consoleHtmlOutput = new HtmlTextWriter(consoleOutput, "");

            process.EnableRaisingEvents = true;
            process.Exited += new EventHandler(Process_Exited);

            /*
            process.OutputDataReceived += new DataReceivedEventHandler(Process_OutputDataReceived);
            process.ErrorDataReceived += new DataReceivedEventHandler(Process_ErrorDataReceived);
            */

            /*
            var t1 = new Task(() =>
            {
                string line;

                while ((line = process.StandardOutput.ReadLine()) != null)
                {
                    WriteOutput(line, "stdout");
                }
            });*/

            //t1.Start();

            process.Start();

            /*process.BeginOutputReadLine();
            process.BeginErrorReadLine();*/

            
        }

        public void WaitForExit()
        {
            evnt.WaitOne();

            string line;

            while ((line = process.StandardOutput.ReadLine()) != null)
            {
                WriteOutput(line, "stdout");
            }

            
        }

        void Process_Exited(object sender, EventArgs e)
        {
            process.WaitForExit(-1);
            process.WaitForExit();
            evnt.Set();
        }

        void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                WriteOutput(e.Data, "stdout");
            }
        }

        void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                WriteOutput(e.Data, "stderr");
            }
        }

        void WriteOutput(string line, string style)
        {
            lock (consoleHtmlOutput)
            {
                consoleHtmlOutput.AddAttribute("class", style);
                consoleHtmlOutput.RenderBeginTag(HtmlTextWriterTag.Pre);
                consoleHtmlOutput.Write(line);
                consoleHtmlOutput.RenderEndTag();
                consoleHtmlOutput.WriteLine();
            }
        }

        private static void GetCommandLine(string command, out string filename, out string arguments)
        {
            var idx = command.IndexOf(' ');

            if (idx < 0)
            {
                filename = command;
                arguments = String.Empty;
            }
            else
            {
                filename = command.Substring(0, idx);
                arguments = command.Substring(idx + 1);
            }
        }

        private static void AppendPath(Process process, string path)
        {
            // Append to path
            if (!String.IsNullOrWhiteSpace(path))
            {
                path = path.Trim();

                if (!path.EndsWith(";"))
                {
                    path += ";";
                }

                path += process.StartInfo.EnvironmentVariables["PATH"];

                process.StartInfo.EnvironmentVariables["PATH"] = path;
            }
        }
    }
}
