using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Domino.Lib
{
    public class PluginInstanceFactory : EntityFactory<PluginInstance>
    {
        private int semesterID;
        private int courseID;
        private int assignmentID;

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

        public int AssignmentID
        {
            get { return assignmentID; }
            set { assignmentID = value; }
        }

        public PluginInstanceFactory(Context context)
            : base(context)
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.semesterID = -1;
            this.courseID = -1;
            this.assignmentID = -1;
        }

        protected override string GetTableQuery()
        {
            return "PluginInstance";
        }

        protected override void AppendWhereCriteria(StringBuilder sb, System.Data.SqlClient.SqlCommand cmd)
        {
            AppendWhereCriterion(sb, "SemesterID = @SemesterID");
            cmd.Parameters.Add("@SemesterID", System.Data.SqlDbType.Int).Value = semesterID > 0 ? (object)semesterID : DBNull.Value;

            AppendWhereCriterion(sb, "CourseID = @CourseID");
            cmd.Parameters.Add("@CourseID", System.Data.SqlDbType.Int).Value = courseID > 0 ? (object)courseID : DBNull.Value;

            AppendWhereCriterion(sb, "AssignmentID = @AssignmentID");
            cmd.Parameters.Add("@AssignmentID", System.Data.SqlDbType.Int).Value = assignmentID > 0 ? (object)assignmentID : DBNull.Value;
        }
    }
}
