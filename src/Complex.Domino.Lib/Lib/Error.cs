using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;

namespace Complex.Domino.Lib
{
    public static class Error
    {
        public static NoResultsException NoResults(int expected)
        {
            return new NoResultsException(String.Format(ErrorMessages.NoResults, expected));
        }

        public static SecurityException InvalidUsernameOrPassword()
        {
            return new SecurityException(ErrorMessages.InvalidUsernameOrPassword);
        }

        public static SecurityException InvalidUsernameOrPassword(Exception inner)
        {
            return new SecurityException(ErrorMessages.InvalidUsernameOrPassword, inner);
        }

        public static ArgumentException InvalidUserID()
        {
            return new ArgumentException(ErrorMessages.InvalidUserID);
        }

        public static ArgumentException InvalidUserName(string input)
        {
            var message = String.Format(ErrorMessages.InvalidUserName, input);
            return new ArgumentException(message);
        }

        public static SecurityException InvalidUserEmail()
        {
            return new SecurityException(ErrorMessages.InvalidUserEmail);
        }

        public static SecurityException InvalidActivationCode()
        {
            return new SecurityException(ErrorMessages.InvalidActivationCode);
        }

        public static SecurityException AccessDenied()
        {
            return new SecurityException(ErrorMessages.AccessDenied);
        }
    }
}
