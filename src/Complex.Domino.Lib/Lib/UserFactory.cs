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

        public IEnumerable<User> Find(int max, int from)
        {
            return base.Find<User>(max, from);
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
    }
}
