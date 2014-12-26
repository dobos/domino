using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Complex.Domino.Git
{
    public class User
    {
        private static readonly Regex authorRegex = new Regex(@"([^\<]*)\<([A-Z0-9._%+-]+@[A-Z0-9.-]+)\>", RegexOptions.IgnoreCase);

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

        public static User Current
        {
            get
            {
                var u = new User();
                var user = System.DirectoryServices.AccountManagement.UserPrincipal.Current;

                u.name = user.DisplayName ?? Environment.UserName;
                u.email = user.EmailAddress ?? String.Format("{0}@{1}", Environment.UserName, Environment.UserDomainName);

                return u;
            }
        }

        public User()
        {
            InitializeMembers();
        }

        public static User Parse(string text)
        {
            var a = new User();
            a.ParseImpl(text);

            return a;
        }

        private void InitializeMembers()
        {
            this.name = null;
            this.email = null;
        }

        private void ParseImpl(string text)
        {
            var m = authorRegex.Match(text);

            if (!m.Success)
            {
                throw new ArgumentException(ExceptionMessages.WrongAuthorFormat, "text");
            }

            this.name = m.Groups[1].Value.Trim();
            this.email = m.Groups[2].Value;
        }

        public override string ToString()
        {
            return String.Format("{0} <{1}>", Name, Email);
        }
    }
}
