using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Domino.Lib
{
    public class Search : ContextObject
    {
        private string pattern;

        // TODO: add criteria, like course, assignment, search depth etc.

        public string Pattern
        {
            get { return pattern; }
            set { pattern = value; }
        }

        public Search(Context context)
            :base(context)
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.pattern = null;
        }

        public IEnumerable<SearchMatch> Find()
        {
            // Find all submissions
            var sf = new SubmissionFactory(Context);
            var submissions = sf.Find();

            foreach (var s in submissions)
            {
                var a = new Lib.Assignment(Context);
                a.Load(s.AssignmentID);

                var u = new Lib.User(Context);
                u.Load(s.StudentID);

                var gh = new GitHelper()
                {
                    Assignment = a,
                    Submission = s,
                    Student = u,
                };

                var matches = gh.Search(pattern);

                foreach (var m in matches)
                {
                    yield return new SearchMatch()
                    {
                        Assignment = a,
                        Submission = s,
                        Student = u,
                        FileName = m.FileName,
                        Line = m.Line,
                        Text = m.Text
                    };
                }
            }
        }
    }
}
