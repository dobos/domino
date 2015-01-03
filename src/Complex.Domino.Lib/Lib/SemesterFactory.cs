using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Domino.Lib
{
    public class SemesterFactory : EntityFactory
    {
        protected override string TableName
        {
            get { return "Semester"; }
        }

        public SemesterFactory(Context context)
            :base(context)
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {

        }

        public IEnumerable<Semester> Find(int max, int from)
        {
            return base.Find<Semester>(max, from);
        }
    }
}
