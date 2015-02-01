using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Domino.Lib
{
    public class CourseFactory : EntityFactory<Course>
    {
        private int semesterID;
        private int userID;
        private UserRoleType roleType;

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

        public UserRoleType RoleType
        {
            get { return roleType; }
            set { roleType = value; }
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
            this.roleType = UserRoleType.Unknown;
        }

        protected override string GetTableQuery()
        {
            var needRoles = false;

            needRoles |= userID > 0;

            if (needRoles)
            {
                return @"
(SELECT c.*, s.Name SemesterName, s.Description SemesterDescription, r.UserID, r.UserRoleType
FROM Course c
INNER JOIN Semester s ON s.ID = c.SemesterID
INNER JOIN UserRole r ON c.ID = r.CourseID)";
            }
            else
            {
                return @"
(SELECT c.*, s.Name SemesterName, s.Description SemesterDescription
FROM Course c
INNER JOIN Semester s ON s.ID = c.SemesterID)
";
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
                cmd.Parameters.Add("@UserID", System.Data.SqlDbType.Int).Value = userID;

                if (roleType != UserRoleType.Unknown)
                {
                    AppendWhereCriterion(sb, "UserRoleType = @UserRoleType");
                    cmd.Parameters.Add("@UserRoleType", System.Data.SqlDbType.Int).Value = (int)roleType;
                }
            }
        }
    }
}
