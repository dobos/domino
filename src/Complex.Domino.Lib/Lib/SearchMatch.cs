using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Domino.Lib
{
    public class SearchMatch
    {
        private User student;
        private Assignment assignment;
        private Submission submission;
        private string fileName;
        private int line;
        private string text;

        public User Student
        {
            get { return student; }
            set { student = value; }
        }

        public Assignment Assignment
        {
            get { return assignment; }
            set { assignment = value; }
        }

        public Submission Submission
        {
            get { return submission; }
            set { submission = value; }
        }

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        public int Line
        {
            get { return line; }
            set { line = value; }
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public SearchMatch()
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.student = null;
            this.assignment = null;
            this.submission = null;
            this.fileName = null;
            this.line = -1;
            this.text = null;

        }
    }
}
