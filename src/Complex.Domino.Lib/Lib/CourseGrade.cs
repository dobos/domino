using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Complex.Domino.Lib
{
    public class CourseGrade : ContextObject, IDatabaseTableObject
    {
        private int courseId;
        private int studentId;
        private int grade;
        private string comments;

        public int CourseId
        {
            get { return courseId; }
            set { courseId = value; }
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

        public CourseGrade()
        {
            InitializeMembers();
        }

        public CourseGrade(Context context)
            : base(context)
        {
            InitializeMembers();
        }

        public CourseGrade(CourseGrade old)
            : base(old.Context)
        {
            CopyMembers(old);
        }

        private void InitializeMembers()
        {
            this.courseId = -1;
            this.studentId = -1;
            this.grade = 0;
            this.comments = String.Empty;
        }

        private void CopyMembers(CourseGrade old)
        {
            this.courseId = old.courseId;
            this.studentId = old.studentId;
            this.grade = old.grade;
            this.comments = old.comments;
        }

        public virtual void LoadFromDataReader(SqlDataReader reader)
        {
            this.courseId = reader.GetInt32("CourseID");
            this.studentId = reader.GetInt32("StudentID");
            this.grade = reader.GetInt32("Grade");
            this.comments = reader.GetString("Comments");
        }

        public void Load()
        {
            Load(courseId, studentId);
        }

        public void Load(int courseId, int studentId)
        {
            var sql = @"
SELECT a.*
FROM [CourseGrade] a
WHERE CourseID = @CourseID AND StudentID = @StudentID
";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@CourseID", SqlDbType.Int).Value = courseId;
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = studentId;

                try
                {
                    Context.ExecuteCommandAsSingleObject(cmd, this);
                }
                catch (NoResultsException)
                {
                    InitializeMembers();

                    this.courseId = courseId;
                    this.studentId = studentId;
                }
            }
        }

        public void Save()
        {
            var sql = @"
UPDATE CourseGrade
SET    Grade = @Grade,
       Comments = @Comments
WHERE CourseID = @CourseID AND StudentID = @StudentID

IF (@@ROWCOUNT = 0)
BEGIN

    INSERT CourseGrade
        (CourseID, StudentID, Grade, Comments)
    VALUES
        (@CourseID, @StudentID, @Grade, @Comments)

END
";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@CourseID", SqlDbType.Int).Value = courseId;
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = studentId;
                cmd.Parameters.Add("@Grade", SqlDbType.Int).Value = grade;
                cmd.Parameters.Add("@Comments", SqlDbType.NVarChar).Value = comments;

                Context.ExecuteCommandNonQuery(cmd);
            }
        }
    }
}
