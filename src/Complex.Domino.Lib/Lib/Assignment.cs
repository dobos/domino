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
        private int courseID;
        private DateTime startDate;
        private DateTime endDate;
        private DateTime endDateSoft;
        private string url;
        private string htmlPage;
        private GradeType gradeType;
        private double gradeWeigth;

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

        public string HtmlPage
        {
            get { return htmlPage; }
            set { htmlPage = value; }
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
            this.courseID = -1;
            this.startDate = new DateTime(DateTime.Now.Year, 1, 1);
            this.endDate = new DateTime(DateTime.Now.Year, 12, 31);
            this.endDateSoft = new DateTime(DateTime.Now.Year, 12, 31);
            this.url = String.Empty;
            this.htmlPage = String.Empty;
            this.gradeType = Lib.GradeType.Unknown;
            this.gradeWeigth = 1.0;
        }

        public override void LoadFromDataReader(SqlDataReader reader)
        {
            base.LoadFromDataReader(reader);

            this.semesterID = reader.GetInt32("SemesterID");
            this.courseID = reader.GetInt32("CourseID");
            this.startDate = reader.GetDateTime("StartDate");
            this.endDate = reader.GetDateTime("EndDate");
            this.endDateSoft = reader.GetDateTime("EndDateSoft");
            this.url = reader.GetString("Url");
            this.htmlPage = reader.GetString("HtmlPage");
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
    (CourseID, Name, Visible, Enabled,
     StartDate, EndDate, EndDateSoft, Url, HtmlPage, GradeType, GradeWeight)
VALUES
    (@CourseID, @Name, @Visible, @Enabled,
     @StartDate, @EndDate, @EndDateSoft, @Url, @HtmlPage, @GradeType, @GradeWeight)

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
    StartDate = @StartDate,
    EndDate = @EndDate,
    EndDateSoft = @EndDateSoft,
    Url = @Url,
    HtmlPage = @HtmlPage,
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
            cmd.Parameters.Add("@HtmlPage", SqlDbType.NVarChar).Value = htmlPage;
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
