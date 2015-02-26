using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Complex.Domino.Plugins;

namespace Complex.Domino.Lib
{
    [Serializable]
    public class PluginInstance : Entity, IDatabaseTableObject
    {
        private int semesterID;
        private int courseID;
        private int assignmentID;

        private string pluginType;

        public int SemesterID
        {
            get { return semesterID; }
            set { semesterID = value; }
        }

        public int CourseID
        {
            get { return courseID; }
            set { courseID = value; }
        }

        public int AssignmentID
        {
            get { return assignmentID; }
            set { assignmentID = value; }
        }

        public PluginInstance()
        {
            InitializeMembers();
        }

        public PluginInstance(Context context)
            : base(context)
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.semesterID = -1;
            this.courseID = -1;
            this.assignmentID = -1;

            this.Name = GetType().FullName;
        }

        public override void LoadFromDataReader(SqlDataReader reader)
        {
            this.semesterID = reader.GetInt32("SemesterID");
            this.courseID = reader.GetInt32("CourseID");
            this.assignmentID = reader.GetInt32("AssignmentID");

            base.LoadFromDataReader(reader);
        }

        protected override void OnLoad(int id)
        {
            var sql = @"
SELECT p.*
FROM [PluginInstance] p
WHERE p.ID = @ID";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;

                Context.ExecuteCommandSingleObject(cmd, this);
            }
        }

        protected override void OnCreate(string columns, string values)
        {
            var sql = @"
INSERT [PluginInstance]
    (SemesterID, CourseID, AssignmentID, {0})
VALUES
    (@SemesterID, @CourseID, @AssignmentID, {1})

SELECT @@IDENTITY
";

            sql = String.Format(sql, columns, values);

            using (var cmd = Context.CreateCommand(sql))
            {
                AppendCreateModifyCommandParameters(cmd);
                ID = Context.ExecuteCommandScalar(cmd);
            }
        }

        protected override void OnModify(string columns)
        {
            var sql = @"
UPDATE [PluginInstance]
SET SemesterID = @SemesterID,
    CourseID = @CourseID,
    AssignmentID = @AssignmentID,
    {0}
WHERE ID = @ID";

            sql = String.Format(sql, columns);

            using (var cmd = Context.CreateCommand(sql))
            {
                AppendCreateModifyCommandParameters(cmd);
                Context.ExecuteCommandNonQuery(cmd);
            }
        }

        protected override void AppendCreateModifyCommandParameters(SqlCommand cmd)
        {
            base.AppendCreateModifyCommandParameters(cmd);

            cmd.Parameters.Add("@SemesterID", SqlDbType.Int).Value = semesterID > 0 ? (object)semesterID : DBNull.Value;
            cmd.Parameters.Add("@CourseID", SqlDbType.Int).Value = courseID > 0 ? (object)courseID : DBNull.Value;
            cmd.Parameters.Add("@AssignmentID", SqlDbType.Int).Value = assignmentID > 0 ? (object)assignmentID : DBNull.Value;
        }

        protected override void OnDelete(int id)
        {
            var sql = "DELETE PluginInstance WHERE ID = @ID";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;
                Context.ExecuteCommandNonQuery(cmd);
            }
        }

        protected override Access GetAccess()
        {
            if (Context.User.IsInAdminRole())
            {
                return Access.All;
            }
            else if (Context.User.Roles.ContainsKey(this.courseID))
            {
                if (Context.User.Roles[this.courseID].RoleType == UserRoleType.Teacher)
                {
                    // Teacher
                    return new Access(true, true, true, false);
                }
                else if (Context.User.Roles[this.courseID].RoleType == UserRoleType.Student)
                {
                    // Student
                    return new Access(false, true, false, false);
                }
            }

            return Access.None;
        }

        public PluginBase GetPlugin()
        {
            var type = Type.GetType(Name);
            var plugin = (PluginBase)Activator.CreateInstance(type, this);
            
            plugin.Load();

            return plugin;
        }
    }
}
