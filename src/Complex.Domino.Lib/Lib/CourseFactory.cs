using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Domino.Lib
{
    public class CourseFactory : EntityFactory
    {
        protected override string TableName
        {
            get { return "Course"; }
        }

        public CourseFactory(Context context)
            :base(context)
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {

        }

        public IEnumerable<Course> Find()
        {
            return Find(-1, -1);
        }

        public IEnumerable<Course> Find(int max, int from)
        {
            return base.Find<Course>(max, from);
        }
    }
}
