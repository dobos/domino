using System;
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

            ok.Visible = Mode == PluginMode.View && Plugin.Instance.SubmissionID > 0;
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

            Plugin.Run(dir);
            output.Text = Plugin.Console;
            console.Visible = true;
        }
    }
}