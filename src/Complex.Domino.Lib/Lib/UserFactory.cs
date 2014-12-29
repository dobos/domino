using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Complex.Domino.Lib
{
    public class UserFactory : ContextObject
    {
        public UserFactory(Context context)
            :base(context)
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {

        }

        public User GetUser(string username)
        {
            string sql = @"
SELECT *
FROM [User]
WHERE Enabled = 1 AND Username = @Username";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = username;

                return Context.ExecuteCommandSingleObject<User>(cmd);
            }
        }

        public User SignInUser(string usernameOrEmail, string password)
        {
            var hash = User.HashPassword(password);

            string sql = @"
SELECT *
FROM [User]
WHERE Enabled = 1 AND
      (Email = @UsernameOrEmail OR Username = @UsernameOrEmail) AND
	  PasswordHash = @PasswordHash";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@UsernameOrEmail", SqlDbType.NVarChar).Value = usernameOrEmail;
                cmd.Parameters.Add("@PasswordHash", SqlDbType.VarChar).Value = hash;

                try
                {
                    return Context.ExecuteCommandSingleObject<User>(cmd);
                }
                catch (NoResultsException ex)
                {
                    throw Error.InvalidUsernameOrPassword(ex);
                }
            }
        }
    }
}
