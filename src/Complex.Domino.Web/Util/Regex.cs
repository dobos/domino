using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Complex.Domino.Web.Util
{
    public static class Regex
    {
        public static string ExtensionToCaseInsensitive(string pattern)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < pattern.Length; i++)
            {
                if (Char.IsLetter(pattern[i]))
                {
                    sb.Append("[");
                    sb.Append(Char.ToLower(pattern[i]));
                    sb.Append(Char.ToUpper(pattern[i]));
                    sb.Append("]");
                }
                else if (pattern[i] == '.')
                {
                    sb.Append("\\.");
                }
                else
                {
                    sb.Append(pattern[i]);
                }
            }

            return sb.ToString();
        }
    }
}