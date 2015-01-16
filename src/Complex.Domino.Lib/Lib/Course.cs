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
        private Semester semester;
        private DateTime startDate;
        private DateTime endDate;
        private string url;
        private GradeType gradeType;

        public Semester Semester
        {
            get { return semester; }
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
            this.semester = new Semester(this.Context);
            this.startDate = new DateTime(DateTime.Now.Year, 1, 1);
            this.endDate = new DateTime(DateTime.Now.Year, 12, 31);
            this.url = String.Empty;
            this.gradeType = Lib.GradeType.Unknown;
        }

        public override void LoadFromDataReader(SqlDataReader reader)
        {
            base.LoadFromDataReader(reader);

            this.semester.ID = reader.GetInt32("SemesterID");
            this.startDate = reader.GetDateTime("StartDate");
            this.endDate = reader.GetDateTime("EndDate");
            this.url = reader.GetString("Url");
            this.gradeType = (Lib.GradeType)reader.GetInt32("GradeType");
        }

        public override void Load(int id)
        {
            var sql = @"
SELECT c.*
FROM [Course] c
WHERE ID = @ID";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;

                Context.ExecuteCommandSingleObject(cmd, this);
            }
        }

        protected override void Create(string columns, string values)
        {
            var sql = @"
INSERT [Course]
    (SemesterID, {0}, StartDate, EndDate, Url, GradeType)
VALUES
    (@SemesterID, {1}, @StartDate, @EndDate, @Url, @GradeType)

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
UPDATE [Course]
SET SemesterID = @SemesterID,
    {0},
    StartDate = @StartDate,
    EndDate = @EndDate,
    Url = @Url,
    GradeType = @GradeType
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

            cmd.Parameters.Add("@SemesterID", SqlDbType.Int).Value = semester.ID;
            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = startDate;
            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = endDate;
            cmd.Parameters.Add("@Url", SqlDbType.NVarChar).Value = url;
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
