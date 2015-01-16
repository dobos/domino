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
        private Assignment assignment;
        private User student;
        private User teacher;
        private SubmissionDirection direction;
        private DateTime date;
        private string gitCommitHash;

        public Assignment Assignment
        {
            get { return assignment; }
        }

        public User Student
        {
            get { return student; }
        }

        public User Teacher
        {
            get { return teacher; }
        }

        public SubmissionDirection Direction
        {
            get
            {
                EnsureLoaded();
                return direction;
            }
            set { direction = value; }
        }

        public DateTime Date
        {
            get
            {
                EnsureLoaded();
                return date;
            }
            set { date = value; }
        }

        public string GitCommitHash
        {
            get
            {
                EnsureLoaded();
                return gitCommitHash;
            }
            set { gitCommitHash = value; }
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
            this.assignment = new Assignment(this.Context);
            this.student = new User();
            this.teacher = new User();
            this.direction = SubmissionDirection.Unknown;
            this.date = new DateTime(2015, 1, 1);
            this.gitCommitHash = null;
        }

        public override void LoadFromDataReader(SqlDataReader reader)
        {
            base.LoadFromDataReader(reader);

            this.assignment.ID = reader.GetInt32("AssignmentID");
            this.student.ID = reader.GetInt32("StudentID");
            this.teacher.ID = reader.GetInt32("TeacherID");
            this.direction = (SubmissionDirection)reader.GetInt32("Direction");
            this.date = reader.GetDateTime("Date");
            this.gitCommitHash = reader.GetString("GitCommitHash");
        }

        public override void Load(int id)
        {
            var sql = @"
SELECT s.*
FROM [Submission] s
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
    (AssignmentID, StudentID, TeacherID, Direction, {0}, Date, GitCommitHash)
VALUES
    (@AssignmentID, @StudentID, @TeacherID, @Direction, {1}, @Date, @GitCommitHash)

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
    Date = @Date,
    GitCommitHash = @GitCommitHash
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

            cmd.Parameters.Add("@AssignmentID", SqlDbType.Int).Value = assignment.ID;
            cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = student.ID;
            cmd.Parameters.Add("@TeacherID", SqlDbType.Int).Value = teacher.ID > 0 ? (object)teacher.ID : DBNull.Value;
            cmd.Parameters.Add("@Direction", SqlDbType.Int).Value = (int)direction;
            cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = Date;
            cmd.Parameters.Add("@GitCommitHash", SqlDbType.NVarChar).Value = gitCommitHash;
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
