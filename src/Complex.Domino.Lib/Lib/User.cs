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
        private byte[] passwordHash;

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

        public User(Context context)
            :base(context)
        {

        }

        private void InitializeMembers()
        {
            this.email = null;
            this.username = null;
            this.activationCode = null;
            this.passwordHash = null;
        }

        public override void LoadFromDataReader(SqlDataReader reader)
        {
            base.LoadFromDataReader(reader);

            this.email = reader.GetString(reader.GetOrdinal("Email"));
            this.username = reader.GetString(reader.GetOrdinal("Username"));
            this.activationCode = reader.GetString(reader.GetOrdinal("ActivationCode"));
            this.passwordHash = reader.GetSqlBytes(reader.GetOrdinal("PasswordHash")).Value;
        }

        public void SetPassword(string password)
        {
            this.passwordHash = HashPassword(password);
        }

        public bool VerifyPassword(string password)
        {
            var hash = HashPassword(password);

            if (hash.Length != passwordHash.Length)
            {
                return false;
            }

            for (int i = 0; i < hash.Length; i++)
            {
                if (hash[i] != passwordHash[i])
                {
                    return false;
                }
            }

            return true;
        }

        private byte[] HashPassword(string password)
        {
            HashAlgorithm hashalg = new SHA512Managed();
            return hashalg.ComputeHash(Encoding.Unicode.GetBytes(password));
        }
    }
}
