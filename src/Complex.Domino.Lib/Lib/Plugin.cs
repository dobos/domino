using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Complex.Domino.Lib
{
    [Serializable]
    public class Plugin : Entity, IDatabaseTableObject
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

        public string PluginType
        {
            get { return pluginType; }
            set { pluginType = value; }
        }

        public Plugin()
        {
            InitializeMembers();
        }

        public Plugin(Context context)
            : base(context)
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.semesterID = -1;
            this.courseID = -1;
            this.assignmentID = -1;

            this.pluginType = null;
        }

        public override void LoadFromDataReader(SqlDataReader reader)
        {
            this.semesterID = reader.GetInt32("SemesterID");
            this.courseID = reader.GetInt32("CourseID");
            this.assignmentID = reader.GetInt32("AssignmentID");
            this.pluginType = reader.GetString("PluginType");

            base.LoadFromDataReader(reader);
        }

        protected override void OnLoad(int id)
        {
            var sql = @"
SELECT p.*
FROM [Plugin] p
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
INSERT [Plugin]
    (SemesterID, CourseID, AssignmentID, PluginType, {0})
VALUES
    (@SemesterID, @CourseID, @AssignmentID, @PluginType, {1})

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
UPDATE [Plugin]
SET SemesterID = @SemesterID,
    CourseID = @CourseID,
    AssignmentID = @AssignmentID,
    PluginType = @PluginType,
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
            cmd.Parameters.Add("@PluginType", SqlDbType.VarChar).Value = pluginType;
        }

        protected override void OnDelete(int id)
        {
            var sql = "DELETE Plugin WHERE ID = @ID";

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
    }
}
