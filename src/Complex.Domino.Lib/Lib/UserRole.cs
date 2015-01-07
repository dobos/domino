using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Complex.Domino.Lib
{
    public class UserRole : IDatabaseTableObject
    {
        private int userId;
        private int courseId;
        private UserRoleType roleType;

        public int UserID
        {
            get { return userId; }
            set { userId = value; }
        }

        public int CourseID
        {
            get { return courseId; }
            set { courseId = value; }
        }

        public UserRoleType RoleType
        {
            get { return roleType; }
            set { roleType = value; }
        }

        public void LoadFromDataReader(SqlDataReader reader)
        {
            userId = reader.GetInt32("UserID");
            courseId = reader.GetInt32("CourseID");
            roleType = (UserRoleType)reader.GetInt32("UserRoleType");
        }
    }
}
