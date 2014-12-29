using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Domino.Lib
{
    [Serializable]
    public class NoResultsException : Exception
    {
        public NoResultsException(string message)
            : base(message)
        {
        }
    }
}
