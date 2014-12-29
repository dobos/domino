﻿using System;
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
    }
}
