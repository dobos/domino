using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Complex.Domino.Lib;

namespace Complex.Domino.Plugins
{
    public partial class BuildControl : PluginControlBase<Build>
    {
        public static string GetUrl()
        {
            return "~/Plugins/BuildControl.ascx";
        }

        private Assignment assignment;
        private Submission submission;
        private User student;

        protected override void CreateItem()
        {
            base.CreateItem();

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

            executeRow.Visible = Mode == PluginMode.View && Plugin.Instance.SubmissionID > 0;
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

            Plugin.Run(gitHelper.GetAssignmentPath(), commandLine.Text);
            output.Text = Plugin.Console;
            console.Visible = true;
        }
    }
}