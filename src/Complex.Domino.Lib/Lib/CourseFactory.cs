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
        private int userID;

        public int SemesterID
        {
            get { return semesterID; }
            set { semesterID = value; }
        }

        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public CourseFactory(Context context)
            :base(context)
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.semesterID = -1;
            this.userID = -1;
        }

        public IEnumerable<Course> Find()
        {
            return Find(-1, -1);
        }

        public IEnumerable<Course> Find(int max, int from)
        {
            return base.Find<Course>(max, from);
        }

        protected override string GetTableQuery()
        {
            var needRoles = false;

            needRoles |= userID > 0;

            if (needRoles)
            {
                return @"
(SELECT c.*, r.UserID, r.UserRoleType
FROM Course c
INNER JOIN UserRole r
    ON c.ID = r.CourseID)";
            }
            else
            {
                return "Course";
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

            if (userID > 0)
            {
                AppendWhereCriterion(sb, "UserID = @UserID");
            }
        }
    }
}
