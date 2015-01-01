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

        public IEnumerable<User> Find(int max, int from)
        {
            var sql = "SELECT * FROM [User]";

            using (var cmd = Context.CreateCommand(sql))
            {
                return Context.ExecuteCommandReader<User>(cmd);
            }
        }
    }
}
