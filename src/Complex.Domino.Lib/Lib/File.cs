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
    public class File : Entity, IDatabaseTableObject
    {
        private int semesterID;
        private string semesterName;
        private string semesterDescription;
        private int courseID;
        private string courseName;
        private string courseDescription;
        private int assignmentID;
        private string assignmentName;
        private string assignmentDescription;

        private string mimeType;
        private string plugin;

        public int SemesterID
        {
            get { return semesterID; }
        }

        public string SemesterName
        {
            get { return semesterName; }
        }

        public string SemesterDescription
        {
            get { return semesterDescription; }
        }

        public int CourseID
        {
            get { return courseID; }
            set { courseID = value; }
        }

        public string CourseName
        {
            get { return courseName; }
        }

        public string CourseDescription
        {
            get { return courseDescription; }
        }

        public int AssignmentID
        {
            get { return assignmentID; }
            set { assignmentID = value; }
        }

        public string AssignmentName
        {
            get { return assignmentName; }
        }

        public string AssignmentDescription
        {
            get { return assignmentDescription; }
        }

        public string MimeType
        {
            get { return mimeType; }
            set { mimeType = value; }
        }

        public string Plugin
        {
            get { return plugin; }
            set { plugin = value; }
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

        public File(File old)
            : base(old)
        {
            CopyMembers(old);
        }

        private void InitializeMembers()
        {
            this.semesterID = -1;
            this.semesterName = null;
            this.semesterDescription = null;
            this.courseID = -1;
            this.courseName = null;
            this.courseDescription = null;
            this.assignmentID = -1;
            this.assignmentName = null;
            this.assignmentDescription = null;

            this.mimeType = null;
            this.plugin = null;
        }

        private void CopyMembers(File old)
        {
            this.semesterID = old.semesterID;
            this.semesterName = old.semesterName;
            this.semesterDescription = old.semesterDescription;
            this.courseID = old.courseID;
            this.courseName = old.courseName;
            this.courseDescription = old.courseDescription;
            this.assignmentID = old.assignmentID;
            this.assignmentName = old.assignmentName;
            this.assignmentDescription = old.assignmentDescription;

            this.mimeType = old.mimeType;
            this.plugin = old.plugin;
        }

        public override void LoadFromDataReader(SqlDataReader reader)
        {
            this.semesterID = reader.GetInt32("SemesterID");
            this.semesterName = reader.GetString("SemesterName");
            this.semesterDescription = reader.GetString("SemesterDescription");
            this.courseID = reader.GetInt32("CourseID");
            this.courseName = reader.GetString("CourseName");
            this.courseDescription = reader.GetString("CourseDescription");
            this.assignmentID = reader.GetInt32("AssignmentID");
            this.assignmentName = reader.GetString("AssignmentName");
            this.assignmentDescription = reader.GetString("AssignmentDescription");

            this.mimeType = reader.GetString("MimeType");
            this.plugin = reader.GetString("Plugin");

            base.LoadFromDataReader(reader);
        }

        protected override void OnLoad(int id)
        {
            var sql = @"
SELECT f.ID, f.SemesterID, f.CourseID, f.AssignmentID,
       f.Name, f.Description, f.Hidden, f.ReadOnly, f.CreatedDate, f.ModifiedDate, f.Comments,
       f.MimeType, f.Plugin,
       r.Name SemesterName, r.Description SemesterDescription,
       c.Name CourseName, c.Description CourseDescription,
       a.Name AssignmentName, a.Description AssignmentDescription,
FROM [File] f
LEFT OUTER JOIN [Assignment] a ON a.ID = f.AssignmentID
LEFT OUTER JOIN [Course] c ON c.ID = f.CourseID
LEFT OUTER JOIN [Semester] r ON r.ID = f.SemesterID
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
    (SemesterID, CourseID, AssignmentID, MimeType, Plugin, {0})
VALUES
    (@SemesterID, @CourseID, @AssignmentID, @MimeType, @Plugin, {1})

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
SET SemesterID = @SemesterID,
    CourseID = @CourseID,
    AssignmentID = @AssignmentID,
    MimeType = @MimeType,
    Plugin = @Plugin,
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
