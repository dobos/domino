using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using Complex.Domino.Lib;

namespace Complex.Domino.Plugins
{
    public class Execute : PluginBase
    {
        public static ExecuteConfiguration Configuration
        {
            get { return (ExecuteConfiguration)ConfigurationManager.GetSection("complex.domino/plugins/execute"); }
        }

        private string commandLine;
        private string console;

        public string CommandLine
        {
            get { return commandLine; }
            set { commandLine = value; }
        }

        public string Console
        {
            get { return console; }
        }

        public override string Description
        {
            get
            {
                return "Execute"; // TODO: use resource
            }
        }

        protected override Type ControlType
        {
            get { return typeof(ExecuteControl); }
        }

        protected override string ControlFileName
        {
            get { return ExecuteControl.GetUrl(); }
        }

        public Execute()
        {
            InitializeMembers();
        }

        public Execute(Context context)
            : base(context)
        {
            InitializeMembers();
        }

        public Execute(PluginInstance instance)
            : base(instance)
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.commandLine = String.Empty;
            this.console = null;
        }

        public override void LoadFromDataReader(SqlDataReader reader)
        {
            this.commandLine = reader.GetString("CommandLine");
        }

        protected override void OnLoad(int id)
        {
            var sql = @"
SELECT *
FROM [Plugin_Execute]
WHERE ID = @ID
";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;
                Context.TryExecuteCommandAsSingleObject(cmd, this);
            }
        }

        protected override void OnCreate()
        {
            var sql = @"
INSERT [Plugin_Execute]
    (ID, CommandLine)
VALUES
    (@ID, @CommandLine)
";

            using (var cmd = Context.CreateCommand(sql))
            {
                AppendCreateModifyParameters(cmd);
                Context.ExecuteCommandNonQuery(cmd);
            }
        }

        protected override void OnModify()
        {
            var sql = @"
UPDATE [Plugin_Execute]
SET CommandLine = @CommandLine
WHERE ID = @ID";

            using (var cmd = Context.CreateCommand(sql))
            {
                AppendCreateModifyParameters(cmd);
                Context.ExecuteCommandNonQuery(cmd);
            }
        }

        protected override void OnDelete(int id)
        {
            var sql = "DELETE [Plugin_Execute] WHERE ID = @ID";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;
                Context.ExecuteCommandNonQuery(cmd);
            }
        }

        private void AppendCreateModifyParameters(SqlCommand cmd)
        {
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Instance.ID;
            cmd.Parameters.Add("@CommandLine", SqlDbType.NVarChar).Value = commandLine;
        }

        public void Run(string workingDirectory)
        {
            // Save files into working directory
            var buffer = new byte[0x10000];     // 64k

            foreach (var file in Instance.Files.Values)
            {
                file.Context = Instance.Context;    // TODO: do this in factory

                using (var infile = file.ReadData())
                {
                    using (var outfile = new FileStream(Path.Combine(workingDirectory, file.Name), FileMode.Create, FileAccess.Write, FileShare.Read))
                    {
                        int res;

                        while ((res = infile.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            outfile.Write(buffer, 0, res);
                        }
                    }
                }
            }


            // Execute command
            var cm = commandLine.Trim();

            using (var w = new ProcessWrapper())
            {
                w.WorkingDirectory = workingDirectory;
                w.Command = cm;
                w.Path = Configuration.Path;

                w.Call();

                console = w.Console;
            }
        }

    }
}
