using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Domino.Git
{
    class Arguments
    {
        private StringBuilder args;
        private int count;

        public Arguments()
        {
            this.args = new StringBuilder();
            this.count = 0;
        }

        public void Append(string key)
        {
            if (count > 0)
            {
                args.Append(' ');
            }

            args.Append(key);

            count++;
        }

        public void Append(string key, string value)
        {
            Append(key + "=" + QuoteValue(value));
        }

        public void AppendIfTrue(bool value, string argument)
        {
            if (value)
            {
                Append(argument);
            }
        }

        public void AppendIfNotNull(string value)
        {
            if (!String.IsNullOrWhiteSpace(value))
            {
                Append(value);
            }
        }

        private string QuoteValue(string value)
        {
            if (value.IndexOf(' ') >= 0)
            {
                return "\"" + value + "\"";
            }
            else
            {
                return value;
            }
        }

        public override string ToString()
        {
            return args.ToString();
        }
    }
}
