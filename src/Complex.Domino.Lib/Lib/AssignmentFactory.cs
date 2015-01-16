using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Domino.Lib
{
    public class AssignmentFactory : EntityFactory
    {
        private int semesterID;
        private int courseID;
        private int userID;

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

        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public AssignmentFactory(Context context)
            :base(context)
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.semesterID = -1;
            this.courseID = -1;
            this.userID = -1;
        }

        public IEnumerable<Assignment> Find()
        {
            return Find(-1, -1);
        }

        public IEnumerable<Assignment> Find(int max, int from)
        {
            return base.Find<Assignment>(max, from);
        }

        protected override string GetTableQuery()
        {
            var from = @"
(SELECT a.*, c.Name CourseName, s.ID SemesterID, s.Name SemesterName
FROM Assignment a
INNER JOIN Course c ON a.CourseID = c.ID
INNER JOIN Semester s ON c.SemesterID = s.ID)
";

            var needRoles = false;

            needRoles |= userID > 0;

            if (needRoles)
            {
                return String.Format(
@"(SELECT a.*, r.UserID, r.UserRoleType
FROM {0} a
INNER JOIN UserRole r
    ON a.CourseID = r.CourseID)",
                    from);
            }
            else
            {
                return from;
            }
        }

        protected override void AppendWhereCriteria(StringBuilder sb, System.Data.SqlClient.SqlCommand cmd)
        {
            base.AppendWhereCriteria(sb, cmd);

            if (semesterID > 0)
            {
                AppendWhereCriterion(sb, "SemesterID = @SemesterID");
                cmd.Parameters.Add("@SemesterID", System.Data.SqlDbType.Int).Value = semesterID;
            }

            if (courseID > 0)
            {
                AppendWhereCriterion(sb, "CourseID = @CourseID");
                cmd.Parameters.Add("@CourseID", System.Data.SqlDbType.Int).Value = courseID;
            }

            if (userID > 0)
            {
                AppendWhereCriterion(sb, "UserID = @UserID");
                cmd.Parameters.Add("@UserID", System.Data.SqlDbType.Int).Value = userID;
            }
        }
    }
}
