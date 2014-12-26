using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Domino.Git
{
    [Serializable]
    public class GitException : Exception
    {
        public GitException(string message)
            : base(message)
        {
        }
    }
}
