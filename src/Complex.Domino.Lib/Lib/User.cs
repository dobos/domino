using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;

namespace Complex.Domino.Lib
{
    [Serializable]
    public class User : Entity, IDatabaseTableObject
    {
        private static readonly char[] passwordChars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToArray();
        private static readonly Random rnd = new Random();

        private bool enabled;
        private string email;
        private string activationCode;
        private string passwordHash;

        private Dictionary<int, UserRole> roles;

        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
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
            : base(context)
        {
            InitializeMembers();
        }

        public User(User old)
            : base(old)
        {
            CopyMembers(old);
        }

        private void InitializeMembers()
        {
            this.enabled = true;
            this.email = null;
            this.activationCode = null;
            this.passwordHash = null;

            this.roles = null;
        }

        private void CopyMembers(User old)
        {
            this.enabled = old.enabled;
            this.email = old.email;
            this.activationCode = old.activationCode;
            this.passwordHash = old.passwordHash;

            // TODO
            this.roles = null;
        }

        public override void LoadFromDataReader(SqlDataReader reader)
        {
            this.enabled = reader.GetBoolean("Enabled");
            this.email = reader.GetString("Email");
            this.activationCode = reader.GetString("ActivationCode");
            this.passwordHash = reader.GetString("PasswordHash");

            base.LoadFromDataReader(reader);
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

        public void Load(string name)
        {
            var sql = @"
SELECT *
FROM [User]
WHERE Name = @Name";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;

                Context.ExecuteCommandSingleObject(cmd, this);
            }
        }

        protected override void Create(string columns, string values)
        {
            var sql = @"
INSERT [User]
    ({0}, Enabled, Email, ActivationCode, PasswordHash)
VALUES
    ({1}, @Enabled, @Email, @ActivationCode, @PasswordHash)

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
UPDATE [User]
SET {0},
    Enabled = @Enabled,
    Email = @Email,
    ActivationCode = @ActivationCode,
    PasswordHash = @PasswordHash
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

            cmd.Parameters.Add("@Enabled", SqlDbType.Bit).Value = enabled;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;
            cmd.Parameters.Add("@ActivationCode", SqlDbType.NVarChar).Value = (object)activationCode ?? DBNull.Value;
            cmd.Parameters.Add("@PasswordHash", SqlDbType.VarChar).Value = passwordHash;
        }

        public override void Delete(int id)
        {
            var sql = "DELETE [User] WHERE ID = @ID";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;
                Context.ExecuteCommandNonQuery(cmd);
            }
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

        public void SignIn(string nameOrEmail, string password)
        {
            var hash = User.HashPassword(password);

            string sql = @"
SELECT *
FROM [User]
WHERE (Email = @NameOrEmail OR Name = @NameOrEmail) AND
	  PasswordHash = @PasswordHash AND
      Enabled = 1";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@NameOrEmail", SqlDbType.NVarChar).Value = nameOrEmail;
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

        public void GenerateActivationCode()
        {
            lock (rnd)
            {
                var code = new char[50];

                for (int i = 0; i < code.Length; i++)
                {
                    code[i] = (char)((int)'a' + rnd.Next((int)('z' - 'a' + 1)));
                }

                this.activationCode = new String(code);
            }
        }

        public void GeneratePassword()
        {
            lock (rnd)
            {
                var code = new char[10];

                for (int i = 0; i < code.Length; i++)
                {
                    code[i] = passwordChars[rnd.Next(passwordChars.Length)];
                }

                this.activationCode = new String(code);
            }
        }

        #endregion
        #region Role functions

        public void LoadRoles()
        {
            roles = new Dictionary<int, UserRole>();

            var sql = @"
SELECT UserRole.*, Course.Name AS CourseName, Course.SemesterID, Semester.Name AS SemesterName
FROM UserRole
    INNER JOIN Course ON Course.ID = UserRole.CourseID
    INNER JOIN Semester ON Semester.ID = Course.SemesterID
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

            var uf = new UserFactory(Context);
            uf.AddRole(role);

            EnsureRolesLoaded();

            if (!roles.ContainsKey(role.CourseID))
            {
                roles.Add(role.CourseID, role);
            }
        }

        public void DeleteRole(UserRole role)
        {
            if (role.UserID != this.ID)
            {
                throw Error.InvalidUserID();
            }

            var uf = new UserFactory(Context);
            uf.DeleteRole(role);

            EnsureRolesLoaded();

            if (roles.ContainsKey(role.CourseID))
            {
                roles.Remove(role.CourseID);
            }
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

        public bool IsInRole(UserRoleType roleType)
        {
            foreach (var key in Roles.Keys)
            {
                if (Roles[key].RoleType == roleType)
                {
                    return true;
                }
            }

            return false;
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

        protected override Access GetAccess()
        {
            if (Context.User.IsInAdminRole())
            {
                return Access.All;
            }
            else if (Context.User.ID == this.ID)
            {
                // The user is themself
                return new Access(false, true, true, false);
            }
            else
            {
                // Check if teacher of student
                foreach (var courseId in this.Roles.Keys)
                {
                    if (Context.User.Roles.ContainsKey(courseId) && 
                        Context.User.Roles[courseId].RoleType == UserRoleType.Teacher)
                    {
                        return new Access(false, true, false, false);
                    }
                }
            }

            return Access.None;
        }
    }
}
