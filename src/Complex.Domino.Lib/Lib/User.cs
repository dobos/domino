using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace Complex.Domino.Lib
{
    public class User : Entity, IDatabaseTableObject
    {
        private string email;
        private string username;
        private string activationCode;
        private string passwordHash;

        private Dictionary<int, UserRole> roles;

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

        public string ActivationCode
        {
            get { return activationCode; }
            set { activationCode = value; }
        }

        public Dictionary<int, UserRole> Roles
        {
            get
            {
                EnsureRolesLoaded();
                return roles;
            }
        }

        public User()
        {
            InitializeMembers();
        }

        public User(Context context)
            :base(context)
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.email = null;
            this.username = null;
            this.activationCode = null;
            this.passwordHash = null;

            this.roles = null;
        }

        public override void LoadFromDataReader(SqlDataReader reader)
        {
            base.LoadFromDataReader(reader);

            this.email = reader.GetString("Email");
            this.username = reader.GetString("Username");
            this.activationCode = reader.GetString("ActivationCode");
            this.passwordHash = reader.GetString("PasswordHash");
        }

        public override void Load(int id)
        {
            var sql = @"
SELECT *
FROM [User]
WHERE ID = @ID";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;

                Context.ExecuteCommandSingleObject(cmd, this);
            }
        }

        public void Load(string username)
        {
            var sql = @"
SELECT *
FROM [User]
WHERE Username = @Username";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = username;

                Context.ExecuteCommandSingleObject(cmd, this);
            }
        }

        protected override void Create()
        {
            var sql = @"
INSERT [User]
    (Name, Visible, Enabled, Email, Username, ActivationCode, PasswordHash)
VALUES
    (@Name, @Visible, @Enabled, @Email, @Username, @ActivationCode, @PasswordHash)

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
UPDATE [User]
SET Name = @Name,
    Visible = @Visible,
    Enabled = @Enabled,
    Email = @Email,
    Username = @Username,
    ActivationCode = @ActivationCode,
    PasswordHash = @PasswordHash
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

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;
            cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = username;
            cmd.Parameters.Add("@ActivationCode", SqlDbType.NVarChar).Value = (object)activationCode ?? DBNull.Value;
            cmd.Parameters.Add("@PasswordHash", SqlDbType.VarChar).Value = passwordHash;
        }

        #region Password functions

        public void SetPassword(string password)
        {
            this.passwordHash = HashPassword(password);
        }

        public bool VerifyPassword(string password)
        {
            var hash = HashPassword(password);

            return StringComparer.InvariantCulture.Compare(this.passwordHash, hash) == 0;
        }

        internal static string HashPassword(string password)
        {
            HashAlgorithm hashalg = new SHA512Managed();
            var hash = hashalg.ComputeHash(Encoding.Unicode.GetBytes(password));

            return Convert.ToBase64String(hash);
        }

        public void SignIn(string usernameOrEmail, string password)
        {
            var hash = User.HashPassword(password);

            string sql = @"
SELECT *
FROM [User]
WHERE (Email = @UsernameOrEmail OR Username = @UsernameOrEmail) AND
	  PasswordHash = @PasswordHash";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@UsernameOrEmail", SqlDbType.NVarChar).Value = usernameOrEmail;
                cmd.Parameters.Add("@PasswordHash", SqlDbType.VarChar).Value = hash;

                try
                {
                    Context.ExecuteCommandSingleObject(cmd, this);
                }
                catch (NoResultsException ex)
                {
                    throw Error.InvalidUsernameOrPassword(ex);
                }
            }
        }

        #endregion
        #region Role functions

        public void LoadRoles()
        {
            roles = new Dictionary<int, UserRole>();

            var sql = @"
SELECT *
FROM UserRole
WHERE UserID = @UserID";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = ID;

                foreach (var role in Context.ExecuteCommandReader<UserRole>(cmd))
                {
                    roles.Add(role.CourseID, role);
                }
            }
        }

        private void EnsureRolesLoaded()
        {
            if (roles == null)
            {
                LoadRoles();
            }
        }

        public void AddRole(UserRole role)
        {
            if (role.UserID != this.ID)
            {
                throw Error.InvalidUserID();
            }

            EnsureRolesLoaded();

            // TODO: check duplicates

            var sql = @"
INSERT UserRole
    (UserID, CourseID, UserRoleType)
VALUES
    (@UserID, @CourseID, @UserRoleType)";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = ID;
                cmd.Parameters.Add("@CourseID", SqlDbType.Int).Value = role.CourseID;
                cmd.Parameters.Add("@UserRoleType", SqlDbType.Int).Value = (int)role.RoleType;

                Context.ExecuteCommandNonQuery(cmd);
            }

            roles.Add(role.CourseID, role);
        }

        public void DeleteRole(UserRole role)
        {
        }

        public bool IsInAdminRole()
        {
            if (Roles.ContainsKey(Constants.AdminRoleCourseID))
            {
                return Roles[Constants.AdminRoleCourseID].RoleType == UserRoleType.Admin;
            }
            else
            {
                return false;
            }
        }

        public bool IsInRole(int courseID, UserRoleType roleType)
        {
            if (Roles.ContainsKey(courseID))
            {
                return Roles[courseID].RoleType == roleType;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
