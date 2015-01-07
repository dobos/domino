using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Domino.Lib
{
    public class CourseFactory : EntityFactory
    {
        private int semesterID;

        public int SemesterID
        {
            get { return semesterID; }
            set { semesterID = value; }
        }

        protected override string TableName
        {
            get { return "Course"; }
        }

        public CourseFactory(Context context)
            :base(context)
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.semesterID = -1;
        }

        public IEnumerable<Course> Find()
        {
            return Find(-1, -1);
        }

        public IEnumerable<Course> Find(int max, int from)
        {
            return base.Find<Course>(max, from);
        }

        protected override void AppendWhereCriteria(StringBuilder sb, System.Data.SqlClient.SqlCommand cmd)
        {
            base.AppendWhereCriteria(sb, cmd);

            if (semesterID > 0)
            {
                AppendWhereCriterion(sb, "SemesterID = @SemesterID");

                cmd.Parameters.Add("@SemesterID", System.Data.SqlDbType.Int).Value = semesterID;
            }
        }
    }
}
