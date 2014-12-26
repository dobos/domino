using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Domino.Git
{
    public class Author
    {
        private string name;
        private string email;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public Author()
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            var user = System.DirectoryServices.AccountManagement.UserPrincipal.Current;

            this.name = user.DisplayName ?? Environment.UserName;
            this.email = user.EmailAddress ?? String.Format("{0}@{1}", Environment.UserName, Environment.UserDomainName);
        }

        public override string ToString()
        {
            return String.Format("{0} <{1}>", Name, Email);
        }
    }
}
