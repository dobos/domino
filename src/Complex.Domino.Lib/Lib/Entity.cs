using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Complex.Domino.Lib
{
    public abstract class Entity : IDatabaseTableObject
    {
        private Context context;
        private int id;
        private string name;
        private bool visible;
        private bool enabled;

        public Context Context
        {
            get { return context; }
            set { context = value; }
        }

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

        protected Entity(Context context)
        {
            InitializeMembers();

            this.context = context;
        }

        private void InitializeMembers()
        {
            this.context = null;
            this.id = -1;
            this.name = null;
            this.visible = true;
            this.enabled = true;
        }

        public virtual void LoadFromDataReader(SqlDataReader reader)
        {
            this.id = reader.GetInt32(reader.GetOrdinal("ID"));
            this.name = reader.GetString(reader.GetOrdinal("Name"));
            this.visible = reader.GetBoolean(reader.GetOrdinal("Visible"));
            this.enabled = reader.GetBoolean(reader.GetOrdinal("Enabled"));
        }
    }
}
