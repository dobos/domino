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
    public class UserFactory : EntityFactory<User>
    {
        private string email;
        private string username;
        private int teacherId;
        private int courseId;
        private UserRoleType role;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public int TeacherID
        {
            get { return teacherId; }
            set { teacherId = value; }
        }

        public int CourseID
        {
            get { return courseId; }
            set { courseId = value; }
        }

        public UserRoleType Role
        {
            get { return role; }
            set { role = value; }
        }

        public UserFactory(Context context)
            : base(context)
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.email = null;
            this.username = null;
            this.teacherId = -1;
            this.courseId = -1;
            this.role = UserRoleType.Unknown;
        }

        protected override string GetTableQuery()
        {
            if (teacherId > 0)
            {
                return @"
(SELECT u.*, r.CourseID, r.UserRoleType, rt.UserID TeacherID
FROM [User] u
INNER JOIN [UserRole] r ON r.UserID = u.ID
INNER JOIN [UserRole] rt ON rt.UserRoleType = 2 AND rt.CourseID = r.CourseID)";
            }
            else if (courseId > 0 || role != UserRoleType.Unknown)
            {
                return @"
(SELECT u.*, r.CourseID, r.UserRoleType
FROM [User] u
INNER JOIN [UserRole] r ON r.UserID = u.ID)
";
            }
            else
            {
                return "[User]";
            }
        }

        protected override void AppendWhereCriteria(StringBuilder sb, SqlCommand cmd)
        {
            base.AppendWhereCriteria(sb, cmd);

            if (email != null)
            {
                AppendWhereCriterion(sb, "Email LIKE @Email");

                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;
            }

            if (username != null)
            {
                AppendWhereCriterion(sb, "Name LIKE @Username");

                cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = username;
            }

            if (teacherId > 0)
            {
                AppendWhereCriterion(sb, "TeacherID = @TeacherID");

                cmd.Parameters.Add("@TeacherID", SqlDbType.Int).Value = teacherId;
            }

            if (courseId > 0)
            {
                AppendWhereCriterion(sb, "CourseID = @CourseID");

                cmd.Parameters.Add("@CourseID", SqlDbType.Int).Value = courseId;
            }

            if (role != UserRoleType.Unknown)
            {
                AppendWhereCriterion(sb, "@UserRoleType = -1 OR UserRoleType = @UserRoleType");

                cmd.Parameters.Add("@UserRoleType", SqlDbType.Int).Value = (int)role;
            }
        }

        /// <summary>
        /// Loads user by e-mail address.
        /// </summary>
        /// <remarks>
        /// Function is to be used by the password reset page.
        /// </remarks>
        /// <param name="email"></param>
        public User LoadByEmail(string email)
        {
            string sql = @"
SELECT *
FROM [User]
WHERE Email = @Email";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;

                try
                {
                    var user = new User(Context);
                    Context.ExecuteCommandSingleObject(cmd, user);

                    return user;
                }
                catch (NoResultsException)
                {
                    throw Error.InvalidUserEmail();
                }
            }
        }

        public User LoadByActivationCode(string activationCode)
        {
            string sql = @"
SELECT *
FROM [User]
WHERE ActivationCode = @ActivationCode";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@ActivationCode", SqlDbType.NVarChar).Value = activationCode;

                try
                {
                    var user = new User(Context);
                    Context.ExecuteCommandSingleObject(cmd, user);

                    return user;
                }
                catch (NoResultsException)
                {
                    throw Error.InvalidActivationCode();
                }
            }
        }

        /// <summary>
        /// Tests if the user name is a duplicate
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsUserDuplicate(string name, out User user)
        {
            string sql = @"
SELECT *
FROM [User] u
WHERE Name = @Name";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;

                try
                {
                    user = new User(Context);
                    Context.ExecuteCommandSingleObject(cmd, user);
                    return true;
                }
                catch (NoResultsException)
                {
                    user = null;
                    return false;
                }
            }
        }

        /// <summary>
        /// Tests if the user is already in the role
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="role"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsRoleDuplicate(UserRole role)
        {
            string sql = @"
SELECT ISNULL(COUNT(*), 0)
FROM [UserRole] r
WHERE r.UserID = @UserID AND r.CourseID = @CourseID AND r.UserRoleType = @UserRoleType";

            using (var cmd = Context.CreateCommand(sql))
            {
                AppendRoleParameters(cmd, role);
                return Context.ExecuteCommandScalar(cmd) > 0;
            }
        }

        public void AddRole(UserRole role)
        {
            var sql = @"
INSERT UserRole
    (UserID, CourseID, UserRoleType)
VALUES
    (@UserID, @CourseID, @UserRoleType)";

            using (var cmd = Context.CreateCommand(sql))
            {
                AppendRoleParameters(cmd, role);
                Context.ExecuteCommandNonQuery(cmd);
            }
        }

        public void DeleteRole(UserRole role)
        {
            var sql = @"
DELETE UserRole
WHERE UserID = @UserID AND CourseID = @CourseID AND UserRoleType = @UserRoleType";

            using (var cmd = Context.CreateCommand(sql))
            {
                AppendRoleParameters(cmd, role);
                Context.ExecuteCommandNonQuery(cmd);
            }
        }

        private void AppendRoleParameters(SqlCommand cmd, UserRole role)
        {
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = role.UserID;
            cmd.Parameters.Add("@CourseID", SqlDbType.Int).Value = role.CourseID;
            cmd.Parameters.Add("@UserRoleType", SqlDbType.Int).Value = (int)role.RoleType;
        }

        public void Import(IEnumerable<string[]> lines, out List<User> users)
        {
            users = new List<User>();

            foreach (var line in lines)
            {
                var user = new User(Context);

                if (line.Length >= 2)
                {
                    user.Name = line[1];            // user name
                    user.Description = line[0];     // name

                    if (line.Length >= 3)
                    {
                        user.Email = line[2];
                    }
                    else
                    {
                        user.Email = String.Empty;
                    }

                    if (line.Length >= 4)
                    {
                        user.ActivationCode = line[3];
                    }
                    else
                    {
                        user.GeneratePassword();
                    }

                    users.Add(user);
                }
            }
        }

        public void FindUserDuplicates(List<User> users, out List<User> duplicates)
        {
            duplicates = new List<User>();

            int i = 0;
            while (i < users.Count)
            {
                User user;

                if (IsUserDuplicate(users[i].Name, out user))
                {
                    users.RemoveAt(i);
                    duplicates.Add(user);
                }
                else
                {
                    i++;
                }
            }
        }

        public void FindRoleDuplicates(int courseId, UserRoleType roleType, List<User> users, out List<User> duplicates)
        {
            duplicates = new List<User>();

            int i = 0;
            while (i < users.Count)
            {
                var user = users[i];
                var role = new UserRole()
                {
                    CourseID = courseId,
                    RoleType = roleType,
                    UserID = user.ID,
                };

                if (IsRoleDuplicate(role))
                {
                    users.RemoveAt(i);
                    duplicates.Add(user);
                }
                else
                {
                    i++;
                }
            }
        }
    }
}
