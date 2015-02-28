using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;

namespace Complex.Domino.Lib
{
    [Serializable]
    public class File : Entity, IDatabaseTableObject
    {
        private int semesterID;
        private int courseID;
        private int assignmentID;
        private int pluginInstanceID;
        private string mimeType;

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

        public int PluginInstanceID
        {
            get { return pluginInstanceID; }
            set { pluginInstanceID = value; }
        }

        public string MimeType
        {
            get { return mimeType; }
            set { mimeType = value; }
        }

        public File()
        {
            InitializeMembers();
        }

        public File(Context context)
            : base(context)
        {
            InitializeMembers();
        }
        
        private void InitializeMembers()
        {
            this.semesterID = -1;
            this.courseID = -1;
            this.assignmentID = -1;
            this.pluginInstanceID = -1;
            this.mimeType = null;
        }

        public override void LoadFromDataReader(SqlDataReader reader)
        {
            this.semesterID = reader.GetInt32("SemesterID");
            this.courseID = reader.GetInt32("CourseID");
            this.assignmentID = reader.GetInt32("AssignmentID");
            this.pluginInstanceID = reader.GetInt32("PluginInstanceID");
            this.mimeType = reader.GetString("MimeType");

            base.LoadFromDataReader(reader);
        }

        protected override void OnLoad(int id)
        {
            var sql = @"
SELECT f.ID, f.PluginInstanceID, p.SemesterID, p.CourseID, p.AssignmentID,
       f.Name, f.Description, f.Hidden, f.ReadOnly, f.CreatedDate, f.ModifiedDate, f.Comments,
       f.MimeType
FROM [File] f
INNER JOIN [PluginInstance] p ON p.ID = f.PluginInstanceID
WHERE f.ID = @ID";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;

                Context.ExecuteCommandSingleObject(cmd, this);
            }
        }

        protected override void OnCreate(string columns, string values)
        {
            var sql = @"
INSERT [File]
    (PluginInstanceID, MimeType, Blob, {0})
VALUES
    (@PluginInstanceID, @MimeType, NULL, {1})

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
UPDATE [File]
SET PluginInstanceID = @PluginInstanceID,
    MimeType = @MimeType,
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

            cmd.Parameters.Add("@PluginInstanceID", SqlDbType.Int).Value = pluginInstanceID > 0 ? (object)pluginInstanceID : DBNull.Value;
            cmd.Parameters.Add("@MimeType", SqlDbType.VarChar).Value = mimeType;
        }

        protected override void OnDelete(int id)
        {
            var sql = "DELETE [File] WHERE ID = @ID";

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
                    return new Access(true, true, true, true);
                }
                else if (Context.User.Roles[this.courseID].RoleType == UserRoleType.Student)
                {
                    // Student
                    return new Access(false, true, false, false);
                }
            }

            return Access.None;
        }

        public void WriteData(Stream infile)
        {
            var bytes = new SqlBytes(infile);

            var sql = @"
UPDATE [File] 
SET Blob = @Blob
WHERE ID = @ID";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = this.ID;
                cmd.Parameters.Add("@Blob", SqlDbType.VarBinary).Value = bytes;
                Context.ExecuteCommandNonQuery(cmd);
            }
        }

        public Stream ReadData()
        {
            var sql = @"
SELECT Blob 
FROM [File] 
WHERE ID = @ID";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = this.ID;

                using (var dr = cmd.ExecuteReader())
                {
                    dr.Read();

                    return dr.GetSqlBytes(0).Stream;
                }
            }
        }
    }
}
