using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Complex.Domino.Git
{
    public class Git
    {
        public static GitConfiguration Configuration
        {
            get
            {
                return (GitConfiguration)ConfigurationManager.GetSection("git");
            }
        }

    }
}
