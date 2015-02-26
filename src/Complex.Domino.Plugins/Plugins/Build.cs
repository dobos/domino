using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Complex.Domino.Lib;

namespace Complex.Domino.Plugins
{
    public class Build : PluginBase
    {
        public static BuildConfiguration Configuration
        {
            get { return null; }
        }

        private string commandLine;

        public string CommandLine
        {
            get { return commandLine; }
            set { commandLine = value; }
        }

        public override string Description
        {
            get {
                return "Build"; // TODO: use resource
            }
        }

        protected override Type ControlType
        {
            get { return typeof(BuildControl); }
        }

        protected override string ControlFileName
        {
            get { return "~/Plugins/BuildControl.ascx"; }
        }

        public Build()
        {
            InitializeMembers();
        }

        public Build(Context context)
            : base(context)
        {
            InitializeMembers();
        }

        public Build(PluginInstance instance)
            : base(instance)
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.commandLine = String.Empty;
        }

        public override void LoadFromDataReader(SqlDataReader reader)
        {
            this.commandLine = reader.GetString("CommandLine");
        }

        protected override void OnLoad(int id)
        {
            var sql = @"
SELECT *
FROM [Plugin_Build]
WHERE ID = @ID
";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;
                Context.TryExecuteCommandSingleObject(cmd, this);
            }
        }

        protected override void OnCreate()
        {
            var sql = @"
INSERT [Plugin_Build]
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
UPDATE [Plugin_Build]
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
            var sql = "DELETE [Plugin_Build] WHERE ID = @ID";

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
    }
}
