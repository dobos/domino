using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Complex.Domino.Lib
{
    [Serializable]
    public class Assignment : Entity, IDatabaseTableObject
    {
        private int semesterID;
        private string semesterName;
        private string semesterDescription;
        private int courseID;
        private string courseName;
        private string courseDescription;
        private DateTime startDate;
        private DateTime endDate;
        private DateTime endDateSoft;
        private string url;
        private GradeType gradeType;
        private double gradeWeigth;

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

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        public DateTime EndDateSoft
        {
            get { return endDateSoft; }
            set { endDateSoft = value; }
        }

        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        public GradeType GradeType
        {
            get { return gradeType; }
            set { gradeType = value; }
        }

        public double GradeWeight
        {
            get { return gradeWeigth; }
            set { gradeWeigth = value; }
        }

        public Assignment()
        {
            InitializeMembers();
        }

        public Assignment(Context context)
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
            this.startDate = new DateTime(DateTime.Now.Year, 1, 1);
            this.endDate = new DateTime(DateTime.Now.Year, 12, 31);
            this.endDateSoft = new DateTime(DateTime.Now.Year, 12, 31);
            this.url = String.Empty;
            this.gradeType = Lib.GradeType.Unknown;
            this.gradeWeigth = 1.0;
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
            this.startDate = reader.GetDateTime("StartDate");
            this.endDate = reader.GetDateTime("EndDate");
            this.endDateSoft = reader.GetDateTime("EndDateSoft");
            this.url = reader.GetString("Url");
            this.gradeType = (Lib.GradeType)reader.GetInt32("GradeType");
            this.gradeWeigth = reader.GetDouble("GradeWeight");
        }

        public override void Load(int id)
        {
            var sql = @"
SELECT a.*, c.Name CourseName, c.Description CourseDescription,
       r.ID SemesterID, r.Name SemesterName, r.Description SemesterDescription
FROM [Assignment] a
INNER JOIN [Course] c ON c.ID = a.CourseID
INNER JOIN [Semester] r ON r.ID = c.SemesterID
WHERE a.ID = @ID";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;

                Context.ExecuteCommandSingleObject(cmd, this);
            }
        }

        protected override void Create(string columns, string values)
        {

            var sql = @"
INSERT [Assignment]
    (CourseID, {0}, StartDate, EndDate, EndDateSoft, Url, GradeType, GradeWeight)
VALUES
    (@CourseID, {1}, @StartDate, @EndDate, @EndDateSoft, @Url, @GradeType, @GradeWeight)

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
UPDATE [Assignment]
SET CourseID = @CourseID,
    {0},
    StartDate = @StartDate,
    EndDate = @EndDate,
    EndDateSoft = @EndDateSoft,
    Url = @Url,
    GradeType = @GradeType,
    GradeWeight = @GradeWeight
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

            cmd.Parameters.Add("@CourseID", SqlDbType.Int).Value = courseID;
            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = startDate;
            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = endDate;
            cmd.Parameters.Add("@EndDateSoft", SqlDbType.DateTime).Value = endDateSoft;
            cmd.Parameters.Add("@Url", SqlDbType.NVarChar).Value = url;
            cmd.Parameters.Add("@GradeType", SqlDbType.Int).Value = gradeType;
            cmd.Parameters.Add("@GradeWeight", SqlDbType.Float).Value = gradeWeigth;
        }

        public override void Delete(int id)
        {
            var sql = "DELETE Assignment WHERE ID = @ID";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;
                Context.ExecuteCommandNonQuery(cmd);
            }
        }
    }
}
