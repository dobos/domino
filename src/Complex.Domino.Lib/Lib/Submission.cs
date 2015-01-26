using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Complex.Domino.Lib
{
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
        private SubmissionDirection direction;
        private DateTime date;

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

        public SubmissionDirection Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
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
            this.direction = SubmissionDirection.Unknown;
            this.date = new DateTime(2015, 1, 1);
        }

        public override void LoadFromDataReader(SqlDataReader reader)
        {
            base.LoadFromDataReader(reader);

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
            this.direction = (SubmissionDirection)reader.GetInt32("Direction");
            this.date = reader.GetDateTime("Date");
        }

        public override void Load(int id)
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

                Context.ExecuteCommandSingleObject(cmd, this);
            }
        }

        protected override void Create(string columns, string values)
        {
            var sql = @"
INSERT [Submission]
    (AssignmentID, StudentID, TeacherID, Direction, {0}, Date)
VALUES
    (@AssignmentID, @StudentID, @TeacherID, @Direction, {1}, @Date)

SELECT @@IDENTITY
";

            sql = String.Format(sql, columns, values);

            using (var cmd = Context.CreateCommand(sql))
            {
                AppendCreateModifyCommandParameters(cmd);
                ID = Context.ExecuteCommandScalar(cmd);
            }
        }

        protected override void Modify(string columns)
        {
            var sql = @"
UPDATE [Submission]
SET AssignmentID = @AssignmentID,
    StudentID = @StudentID,
    TeacherID = @TeacherID,
    Direction = @Direction,
    {0},
    Date = @Date
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
            cmd.Parameters.Add("@Direction", SqlDbType.Int).Value = (int)direction;
            cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = Date;
        }

        public override void Delete(int id)
        {
            var sql = "DELETE Submission WHERE ID = @ID";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;
                Context.ExecuteCommandNonQuery(cmd);
            }
        }
    }
}
