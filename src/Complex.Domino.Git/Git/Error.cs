using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Domino.Git
{
    static class Error
    {
        public static ArgumentException InvalidAuthorFormat(string input, string parameter)
        {
            var message = String.Format(ExceptionMessages.WrongAuthorFormat, input);
            return new ArgumentException(message, parameter);
        }
    }
}
