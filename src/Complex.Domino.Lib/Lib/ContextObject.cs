using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Domino.Lib
{
    public abstract class ContextObject
    {
        private Context context;

        public Context Context
        {
            get { return context; }
            set { context = value; }
        }

        protected ContextObject()
        {
            InitializeMembers();
        }

        protected ContextObject(Context context)
        {
            InitializeMembers();

            this.context = context;
        }

        private void InitializeMembers()
        {
            this.context = null;
        }
    }
}
