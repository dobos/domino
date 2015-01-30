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
    public class UserFactory : EntityFactory
    {
        private string email;
        private string username;

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

        public UserFactory(Context context)
            : base(context)
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.email = null;
            this.username = null;
        }

        public IEnumerable<User> Find(int max, int from, string orderBy)
        {
            return base.Find<User>(max, from, orderBy);
        }

        protected override string GetTableQuery()
        {
            return "[User]";
        }

        protected override void AppendWhereCriteria(StringBuilder sb, SqlCommand cmd)
        {
            base.AppendWhereCriteria(sb, cmd);

            if (email != null)
            {
                AppendWhereCriterion(sb, "Email LIKE @Email");

                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = '%' + email + '%';
            }

            if (username != null)
            {
                AppendWhereCriterion(sb, "Username LIKE @Username");

                cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = '%' + username + '%';
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

        public bool IsDuplicate(string name)
        {
            string sql = @"
SELECT ISNULL(COUNT(*), 0)
FROM [User]
WHERE Name = @Name";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
                return Context.ExecuteCommandScalar(cmd) > 0;
            }
        }

        public void Import(IEnumerable<string[]> lines, out List<User> users, out List<User> duplicates)
        {
            users = new List<User>();
            duplicates = new List<User>();

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

                    if (!IsDuplicate(user.Name))
                    {
                        users.Add(user);
                    }
                    else
                    {
                        duplicates.Add(user);
                    }
                }
            }
        }
    }
}
