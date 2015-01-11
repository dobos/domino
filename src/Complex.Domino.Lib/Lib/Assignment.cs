using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Complex.Domino.Lib
{
    public class Assignment : Entity, IDatabaseTableObject
    {
        private int semesterID;
        private string semesterName;
        private int courseID;
        private string courseName;
        private DateTime startDate;
        private DateTime endDate;
        private DateTime endDateSoft;
        private string url;
        private GradeType gradeType;
        private double gradeWeigth;

        public int SemesterID
        {
            get { return semesterID; }
            set { semesterID = value; }
        }

        public string SemesterName
        {
            get { return semesterName; }
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
            this.courseID = -1;
            this.courseName = null;
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
            this.courseID = reader.GetInt32("CourseID");
            this.courseName = reader.GetString("CourseName");
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
SELECT *
FROM [Assignment]
WHERE ID = @ID";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;

                Context.ExecuteCommandSingleObject(cmd, this);
            }
        }

        protected override void Create()
        {
            var sql = @"
INSERT [Assignment]
    (CourseID, Name, Visible, Enabled, Comments,
     StartDate, EndDate, EndDateSoft, Url, GradeType, GradeWeight)
VALUES
    (@CourseID, @Name, @Visible, @Enabled, @Comments,
     @StartDate, @EndDate, @EndDateSoft, @Url, @GradeType, @GradeWeight)

SELECT @@IDENTITY
";

            using (var cmd = Context.CreateCommand(sql))
            {
                AppendCreateModifyCommandParameters(cmd);
                ID = Context.ExecuteCommandScalar(cmd);
            }
        }

        protected override void Modify()
        {
            var sql = @"
UPDATE [Assigment]
SET CourseID = @CourseID,
    Name = @Name,
    Visible = @Visible,
    Enabled = @Enabled,
    Comments = @Comments,
    StartDate = @StartDate,
    EndDate = @EndDate,
    EndDateSoft = @EndDateSoft,
    Url = @Url,
    GradeType = @GradeType,
    GradeWeight = @GradeWeight
WHERE ID = @ID";

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
