using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

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
            :base(context)
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.email = null;
            this.username = null;
        }

        public int Count()
        {
            using (var cmd = Context.CreateCommand())
            {
                string sql = @"
SELECT COUNT(*) FROM [User]
{0}";

                var where = BuildWhereClause(cmd);

                cmd.CommandText = String.Format(sql, where);

                return Context.ExecuteCommandScalar(cmd);
            }
        }

        public IEnumerable<User> Find(int max, int from)
        {
            using (var cmd = Context.CreateCommand())
            {
                string sql = @"
WITH q AS
(
    SELECT [User].*, ROW_NUMBER() OVER({0}) AS rn
    FROM [User]
    {1}
)
SELECT * FROM q
WHERE rn BETWEEN @from AND @to
{0}
";

                var where = BuildWhereClause(cmd);
                var orderby = BuildOrderByClause();

                cmd.CommandText = String.Format(sql, orderby, where);

                cmd.Parameters.Add("@from", SqlDbType.Int).Value = from;
                cmd.Parameters.Add("@to", SqlDbType.Int).Value = from + max;

                return Context.ExecuteCommandReader<User>(cmd);
            }
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
    }
}
