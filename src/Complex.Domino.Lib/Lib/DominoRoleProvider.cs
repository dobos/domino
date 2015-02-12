using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Complex.Domino.Lib
{
    public class DominoRoleProvider : RoleProvider
    {

        private string applicationName;
        private HashSet<string> roles;

        public override string ApplicationName
        {
            get { return applicationName; }
            set { applicationName = value; }
        }

        public DominoRoleProvider()
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.applicationName = "Domino";

            this.roles = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
            roles.Add(UserRoleType.Admin.ToString());
            roles.Add(UserRoleType.Teacher.ToString());
            roles.Add(UserRoleType.Student.ToString());
        }

        public override string[] GetAllRoles()
        {
            return roles.ToArray();
        }

        public override bool RoleExists(string roleName)
        {
            return roles.Contains(roleName);
        }

        public override string[] GetRolesForUser(string username)
        {
            using (var context = Context.Create())
            {
                var user = new User(context);
                user.Load(username);

                var q = from r in user.Roles.Values
                        select r.RoleType.ToString();

                return q.Distinct().ToArray();
            }
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            UserRoleType role;

            if (Enum.TryParse(roleName, out role))
            {
                using (var context = Context.Create())
                {
                    var user = new User(context);
                    user.Load(username);

                    foreach (var r in user.Roles.Values)
                    {
                        if (r.RoleType == role)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }


        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }
    }
}
