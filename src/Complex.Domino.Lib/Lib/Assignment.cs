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
    public class Assignment : Entity, IDatabaseTableObject
    {
        private Course course;
        private DateTime startDate;
        private DateTime endDate;
        private DateTime endDateSoft;
        private string url;
        private GradeType gradeType;
        private double gradeWeigth;

        public Course Course
        {
            get { return course; }
        }

        public DateTime StartDate
        {
            get
            {
                EnsureLoaded();
                return startDate;
            }
            set { startDate = value; }
        }

        public DateTime EndDate
        {
            get
            {
                EnsureLoaded();
                return endDate;
            }
            set { endDate = value; }
        }

        public DateTime EndDateSoft
        {
            get
            {
                EnsureLoaded();
                return endDateSoft;
            }
            set { endDateSoft = value; }
        }

        public string Url
        {
            get
            {
                EnsureLoaded();
                return url;
            }
            set { url = value; }
        }

        public GradeType GradeType
        {
            get
            {
                EnsureLoaded();
                return gradeType;
            }
            set { gradeType = value; }
        }

        public double GradeWeight
        {
            get
            {
                EnsureLoaded();
                return gradeWeigth;
            }
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
            this.course = new Course(this.Context);
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

            this.course.ID = reader.GetInt32("CourseID");
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
SELECT a.*
FROM [Assignment] a
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
UPDATE [Assigment]
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

            cmd.Parameters.Add("@CourseID", SqlDbType.Int).Value = course.ID;
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
