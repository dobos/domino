﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Complex.Domino.Lib;

namespace Complex.Domino.Plugins
{
    public partial class ExecuteControl : PluginControlBase<Execute>
    {
        public static string GetUrl()
        {
            return "~/Plugins/ExecuteControl.ascx";
        }

        private Assignment assignment;
        private Submission submission;
        private User student;

        protected override void CreateItem()
        {
            base.CreateItem();

            fileList.Plugin = Plugin;

            if (Mode == PluginMode.View && Plugin.Instance.SubmissionID > 0)
            {
                assignment = new Assignment(DatabaseContext);
                assignment.Load(Plugin.Instance.AssignmentID);

                submission = new Submission(DatabaseContext);
                submission.Load(Plugin.Instance.SubmissionID);

                student = new User(DatabaseContext);
                student.Load(submission.StudentID);
            }
        }

        protected override void UpdateForm()
        {
            commandLine.Text = Plugin.CommandLine;

            executeRow.Visible = Mode == PluginMode.View && Plugin.Instance.SubmissionID > 0; ;
            fileList.AllowUpload = Mode == PluginMode.Edit;
            fileList.AllowDelete = Mode == PluginMode.Edit;
        }

        public override void SaveForm()
        {
            Plugin.CommandLine = commandLine.Text;
        }

        protected void Ok_Click(object sender, EventArgs e)
        {
            Execute();
        }

        private void Execute()
        {
            var gitHelper = new Lib.GitHelper()
            {
                SessionGuid = SessionGuid,
                Author = DatabaseContext.User,
                Student = student,
                Assignment = assignment,
                Submission = submission,
            };

            var dir = gitHelper.GetAssignmentPath();

            // TODO: save files to the directory

            Plugin.Run(dir, commandLine.Text);
            output.Text = Plugin.Console;
            outputPanel.Visible = true;

            DisplayOutputFile(dir, "stdout.log", stdout, stdoutPanel);
            DisplayOutputFile(dir, "stderr.log", stderr, stderrPanel);
            DisplayOutputFile(dir, "debug.log", debug, debugPanel);
        }

        private void DisplayOutputFile(string dir, string filename, Literal text, Panel panel)
        {
            filename = System.IO.Path.Combine(dir, filename);
            if (System.IO.File.Exists(filename))
            {
                text.Text = System.IO.File.ReadAllText(filename);
                panel.Visible = true;
            }
            else
            {
                panel.Visible = false;
            }
        }
    }
}