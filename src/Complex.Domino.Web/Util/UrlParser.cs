using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Complex.Domino.Web.Util
{
    public static class UrlParser
    {
        public static int ParseInt(string parameter)
        {
            return int.Parse(parameter);
        }

        public static int ParseInt(string parameter, int defaultValue)
        {
            if (parameter == null)
            {
                return defaultValue;
            }
            else
            {
                return int.Parse(parameter);
            }
        }
    }
}