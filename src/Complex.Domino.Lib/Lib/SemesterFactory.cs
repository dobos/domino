using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Domino.Lib
{
    public class SemesterFactory : EntityFactory
    {
        public SemesterFactory(Context context)
            :base(context)
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {

        }

        public IEnumerable<Semester> Find()
        {
            return Find(-1, -1, null);
        }

        public IEnumerable<Semester> Find(int max, int from, string orderBy)
        {
            return base.Find<Semester>(max, from, orderBy);
        }

        protected override string GetTableQuery()
        {
            return "Semester";
        }
    }
}
