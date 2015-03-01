using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Complex.Domino.Lib
{
    public class AssignmentGrade : ContextObject, IDatabaseTableObject
    {
        private int assignmentId;
        private int studentId;
        private int grade;
        private string comments;

        public int AssignmentId
        {
            get { return assignmentId; }
            set { assignmentId = value; }
        }

        public int StudentId
        {
            get { return studentId; }
            set { studentId = value; }
        }

        public int Grade
        {
            get { return grade; }
            set { grade = value; }
        }

        public string Comments
        {
            get { return comments; }
            set { comments = value; }
        }

        public AssignmentGrade()
        {
            InitializeMembers();
        }

        public AssignmentGrade(Context context)
            : base(context)
        {
            InitializeMembers();
        }

        public AssignmentGrade(AssignmentGrade old)
            : base(old.Context)
        {
            CopyMembers(old);
        }

        private void InitializeMembers()
        {
            this.assignmentId = -1;
            this.studentId = -1;
            this.grade = 0;
            this.comments = String.Empty;
        }

        private void CopyMembers(AssignmentGrade old)
        {
            this.assignmentId = old.assignmentId;
            this.studentId = old.studentId;
            this.grade = old.grade;
            this.comments = old.comments;
        }

        public virtual void LoadFromDataReader(SqlDataReader reader)
        {
            this.assignmentId = reader.GetInt32("AssignmentID");
            this.studentId = reader.GetInt32("StudentID");
            this.grade = reader.GetInt32("Grade");
            this.comments = reader.GetString("Comments");
        }

        public void Load()
        {
            Load(assignmentId, studentId);
        }

        public void Load(int assignmentId, int studentId)
        {
            var sql = @"
SELECT a.*
FROM [AssignmentGrade] a
WHERE AssignmentID = @AssignmentID AND StudentID = @StudentID
";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@AssignmentID", SqlDbType.Int).Value = assignmentId;
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = studentId;

                try
                {
                    Context.ExecuteCommandAsSingleObject(cmd, this);
                }
                catch (NoResultsException)
                {
                    InitializeMembers();

                    this.assignmentId = assignmentId;
                    this.studentId = studentId;
                }
            }
        }

        public void Save()
        {
            var sql = @"
UPDATE AssignmentGrade
SET    Grade = @Grade,
       Comments = @Comments
WHERE AssignmentID = @AssignmentID AND StudentID = @StudentID

IF (@@ROWCOUNT = 0)
BEGIN

    INSERT AssignmentGrade
        (AssignmentID, StudentID, Grade, Comments)
    VALUES
        (@AssignmentID, @StudentID, @Grade, @Comments)

END
";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@AssignmentID", SqlDbType.Int).Value = assignmentId;
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = studentId;
                cmd.Parameters.Add("@Grade", SqlDbType.Int).Value = grade;
                cmd.Parameters.Add("@Comments", SqlDbType.NVarChar).Value = comments;

                Context.ExecuteCommandNonQuery(cmd);
            }
        }
    }
}
