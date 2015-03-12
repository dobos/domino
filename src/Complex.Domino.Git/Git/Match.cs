using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Complex.Domino.Git
{
    public class Match
    {
        private string hash;
        private string fileName;
        private int line;
        private string text;

        public string Hash
        {
            get { return hash; }
        }

        public string FileName
        {
            get { return fileName; }
        }

        public int Line
        {
            get { return line; }
        }

        public string Text
        {
            get { return text; }
        }

        public Match()
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.hash = null;
            this.fileName = null;
            this.line = -1;
            this.text = null;
        }

        internal bool Read(TextReader reader)
        {
            var match = reader.ReadLine();

            if (match == null)
            {
                return false;
            }

            if (match.StartsWith("Binary"))
            {
                return false;
            }

            // Parse
            int idx1, idx2;

            idx1 = 0;
            
            idx2 = match.IndexOf(':');
            hash = match.Substring(idx1, idx2 - idx1);
            idx1 = idx2 + 1;

            idx2 = match.IndexOf(':', idx1);
            fileName = match.Substring(idx1, idx2 - idx1);
            idx1 = idx2 + 1;

            idx2 = match.IndexOf(':', idx1);
            line = int.Parse(match.Substring(idx1, idx2 - idx1));
            idx1 = idx2 + 1;

            text = match.Substring(idx1);

            return true;
        }
    }
}
