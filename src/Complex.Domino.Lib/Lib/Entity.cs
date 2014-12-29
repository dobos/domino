using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Complex.Domino.Lib
{
    public abstract class Entity : ContextObject, IDatabaseTableObject
    {
        private int id;
        private string name;
        private bool visible;
        private bool enabled;

        public int ID
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        protected Entity()
        {
            InitializeMembers();
        }

        protected Entity(Context context)
            :base(context)
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.id = -1;
            this.name = null;
            this.visible = true;
            this.enabled = true;
        }

        public virtual void LoadFromDataReader(SqlDataReader reader)
        {
            this.id = reader.GetInt32("ID");
            this.name = reader.GetString("Name");
            this.visible = reader.GetBoolean("Visible");
            this.enabled = reader.GetBoolean("Enabled");
        }
    }
}
