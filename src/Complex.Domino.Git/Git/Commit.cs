using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Complex.Domino.Git
{
    public class Commit
    {
        private static readonly DateTime UnixTimeOrigin = new DateTime(1970, 1, 1);
        private static readonly Regex authorHeaderRegex = new Regex(@"([^\<]*)\<([A-Z0-9._%+-]+@[A-Z0-9.-]+)\>\s+([0-9]+)\s+([\+\-0-9]+)", RegexOptions.IgnoreCase);

        private string hash;
        private User author;
        private DateTime date;
        private string message;

        public string Hash
        {
            get { return hash; }
            set { hash = value; }
        }

        public User Author
        {
            get { return author; }
            set { author = value; }
        }

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public string Message
        {
            get { return Message; }
            set { message = value; }
        }

        public Commit()
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.hash = null;
            this.author = null;
            this.date = DateTime.MinValue;
            this.message = null;
        }

        internal bool Read(TextReader text)
        {
            bool hascontent = false;
            bool inmessage = false;
            var msgsb = new StringBuilder();

            while (true)
            {
                var line = text.ReadLine();

                if (line == null)
                {
                    break;
                }
                else if (line.Length == 0)
                {
                    if (inmessage)
                    {
                        break;
                    }
                    else
                    {
                        inmessage = true;
                    }
                }
                else if (line.StartsWith("    "))
                {
                    msgsb.AppendLine(line.Substring(4));
                }
                else
                {
                    ParseHeader(line);
                }

                hascontent = true;
            }

            message = msgsb.ToString();
            return hascontent;
        }

        private void ParseHeader(string line)
        {
            if (line.StartsWith("commit ", StringComparison.InvariantCultureIgnoreCase))
            {
                hash = line.Substring(7);
            }
            else if (line.StartsWith("author ", StringComparison.InvariantCultureIgnoreCase))
            {
                ParseAuthorHeader(line.Substring(7), out author, out date);
            }
            else if (line.StartsWith("\t"))
            {
            }
        }

        private void ParseAuthorHeader(string text, out User user, out DateTime date)
        {
            var m = authorHeaderRegex.Match(text);

            if (!m.Success)
            {
                throw new ArgumentException(ExceptionMessages.WrongAuthorFormat, "text");
            }

            user = new User()
            {
                Name = m.Groups[1].Value.Trim(),
                Email = m.Groups[2].Value.Trim(),
            };

            var unixtime = long.Parse(m.Groups[3].Value);
            var timezone = int.Parse(m.Groups[4].Value);

            // UTC
            date = UnixTimeOrigin.AddSeconds(unixtime);
        }
    }
}
