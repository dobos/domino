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
    public class Submission : Entity, IDatabaseTableObject
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
        private int studentID;
        private string studentName;
        private int teacherID;
        private string teacherName;
        private int replyToSubmissionID;
        private DateTime readDate;

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

        public int StudentID
        {
            get { return studentID; }
            set { studentID = value; }
        }

        public string StudentName
        {
            get { return studentName; }
            set { studentName = value; }
        }

        public int TeacherID
        {
            get { return teacherID; }
            set { teacherID = value; }
        }

        public int ReplyToSubmissionID
        {
            get { return replyToSubmissionID; }
            set { replyToSubmissionID = value; }
        }

        public DateTime ReadDate
        {
            get { return readDate; }
            set { readDate = value; }
        }

        public bool IsReply
        {
            get { return teacherID > 0; }
        }

        public bool IsRead
        {
            get { return readDate != DateTime.MinValue; }
        }

        public Submission()
        {
            InitializeMembers();
        }

        public Submission(Context context)
            : base(context)
        {
            InitializeMembers();
        }

        public Submission(Submission old)
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
            this.studentID = -1;
            this.studentName = null;
            this.teacherID = -1;
            this.teacherName = null;
            this.replyToSubmissionID = -1;
            this.readDate = DateTime.MinValue;
        }

        private void CopyMembers(Submission old)
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
            this.studentID = old.studentID;
            this.studentName = old.studentName;
            this.teacherID = old.teacherID;
            this.teacherName = null;
            this.replyToSubmissionID = -1;
            this.readDate = DateTime.MinValue;
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
            this.studentID = reader.GetInt32("StudentID");
            this.studentName = reader.GetString("StudentName");
            this.teacherID = reader.GetInt32("TeacherID");
            this.teacherName = reader.GetString("TeacherName");
            this.replyToSubmissionID = reader.GetInt32("ReplyToSubmissionID");
            this.readDate = reader.GetDateTime("ReadDate");

            base.LoadFromDataReader(reader);
        }

        protected override void OnLoad(int id)
        {
            var sql = @"
SELECT s.*, r.ID SemesterID, r.Name SemesterName, r.Description SemesterDescription,
       c.ID CourseID, c.Name CourseName, c.Description CourseDescription,
       a.Name AssignmentName, a.Description AssignmentDescription,
       student.Name StudentName, teacher.Name TeacherName
FROM [Submission] s
INNER JOIN [Assignment] a ON a.ID = s.AssignmentID
INNER JOIN [Course] c ON c.ID = a.CourseID
INNER JOIN [Semester] r ON r.ID = c.SemesterID
INNER JOIN [User] student ON student.ID = s.StudentID
LEFT OUTER JOIN [User] teacher ON teacher.ID = s.TeacherID
WHERE s.ID = @ID";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;

                Context.ExecuteCommandAsSingleObject(cmd, this);
            }
        }

        protected override void OnCreate(string columns, string values)
        {
            var sql = @"
INSERT [Submission]
    (AssignmentID, StudentID, TeacherID, ReplyToSubmissionID, ReadDate, {0})
VALUES
    (@AssignmentID, @StudentID, @TeacherID, @ReplyToSubmissionID, @ReadDate, {1})

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
UPDATE [Submission]
SET AssignmentID = @AssignmentID,
    StudentID = @StudentID,
    TeacherID = @TeacherID,
    ReplyToSubmissionID = @ReplyToSubmissionID, 
    ReadDate = @ReadDate,
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

            cmd.Parameters.Add("@AssignmentID", SqlDbType.Int).Value = assignmentID;
            cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = studentID;
            cmd.Parameters.Add("@TeacherID", SqlDbType.Int).Value = teacherID > 0 ? (object)teacherID : DBNull.Value;
            cmd.Parameters.Add("@ReplyToSubmissionID", SqlDbType.Int).Value = replyToSubmissionID > 0 ? (object)replyToSubmissionID : DBNull.Value;
            cmd.Parameters.Add("@ReadDate", SqlDbType.DateTime).Value = readDate != DateTime.MinValue ? (object)readDate : DBNull.Value;
        }

        protected override void OnDelete(int id)
        {
            var sql = "DELETE Submission WHERE ID = @ID";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;
                Context.ExecuteCommandNonQuery(cmd);
            }
        }

        public void MarkRead()
        {
            if (readDate == DateTime.MinValue)
            {

                var sql = @"
UPDATE Submission
SET ReadDate = GETDATE()
WHERE ID = @ID";

                using (var cmd = Context.CreateCommand(sql))
                {
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                    Context.ExecuteCommandNonQuery(cmd);
                }

                this.readDate = DateTime.Now;
            }
        }

        public void MarkUnread()
        {
            var sql = @"
UPDATE Submission
SET ReadDate = NULL
WHERE ID = @ID";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                Context.ExecuteCommandNonQuery(cmd);
            }

            this.readDate = DateTime.MinValue;
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
                    return new Access(true, true, false, false);
                }
            }

            return Access.None;
        }
    }
}
