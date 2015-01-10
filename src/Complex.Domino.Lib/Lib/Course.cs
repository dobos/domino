using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Complex.Domino.Lib
{
    public class Course : Entity, IDatabaseTableObject
    {
        private int semesterID;
        private DateTime startDate;
        private DateTime endDate;
        private string url;
        private string htmlPage;
        private GradeType gradeType;

        public int SemesterID
        {
            get { return semesterID; }
            set { semesterID = value; }
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

        public Course()
        {
            InitializeMembers();
        }

        public Course(Context context)
            : base(context)
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.semesterID = -1;
            this.startDate = new DateTime(DateTime.Now.Year, 1, 1);
            this.endDate = new DateTime(DateTime.Now.Year, 12, 31);
            this.url = String.Empty;
            this.htmlPage = String.Empty;
            this.gradeType = Lib.GradeType.Unknown;
        }

        public override void LoadFromDataReader(SqlDataReader reader)
        {
            base.LoadFromDataReader(reader);

            this.semesterID = reader.GetInt32("SemesterID");
            this.startDate = reader.GetDateTime("StartDate");
            this.endDate = reader.GetDateTime("EndDate");
            this.url = reader.GetString("Url");
            this.htmlPage = reader.GetString("HtmlPage");
            this.gradeType = (Lib.GradeType)reader.GetInt32("GradeType");
        }

        public override void Load(int id)
        {
            var sql = @"
SELECT *
FROM [Course]
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
INSERT [Course]
    (SemesterID, Name, Visible, Enabled, StartDate, EndDate, Url, HtmlPage, GradeType)
VALUES
    (@SemesterID, @Name, @Visible, @Enabled, @StartDate, @EndDate, @Url, @HtmlPage, @GradeType)

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
UPDATE [Course]
SET SemesterID = @SemesterID,
    Name = @Name,
    Visible = @Visible,
    Enabled = @Enabled,
    StartDate = @StartDate,
    EndDate = @EndDate,
    Url = @Url,
    HtmlPage = @HtmlPage,
    GradeType = @GradeType
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

            cmd.Parameters.Add("@SemesterID", SqlDbType.Int).Value = semesterID;
            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = startDate;
            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = endDate;
            cmd.Parameters.Add("@Url", SqlDbType.NVarChar).Value = url;
            cmd.Parameters.Add("@HtmlPage", SqlDbType.NVarChar).Value = htmlPage;
            cmd.Parameters.Add("@GradeType", SqlDbType.Int).Value = gradeType;
        }

        public override void Delete(int id)
        {
            var sql = "DELETE Course WHERE ID = @ID";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;
                Context.ExecuteCommandNonQuery(cmd);
            }
        }
    }
}
