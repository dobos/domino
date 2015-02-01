using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Domino.Lib
{
    public class SemesterFactory : EntityFactory<Semester>
    {
        public SemesterFactory(Context context)
            :base(context)
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {

        }

        protected override string GetTableQuery()
        {
            return "Semester";
        }
    }
}
