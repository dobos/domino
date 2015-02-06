using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Domino.Lib
{
    public class Access
    {
        private bool create;
        private bool read;
        private bool update;
        private bool delete;

        public bool Create
        {
            get { return create; }
        }

        public bool Read
        {
            get { return read; }
        }

        public bool Update
        {
            get { return update; }
        }

        public bool Delete
        {
            get { return delete; }
        }

        internal Access(bool create, bool read, bool update, bool delete)
        {
            this.create = create;
            this.read = read;
            this.update = update;
            this.delete = delete;
        }

        internal static Access All
        {
            get { return new Access(true, true, true, true); }
        }

        internal static Access None
        {
            get { return new Access(false, false, false, false); }
        }
    }
}
