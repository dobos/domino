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
        private string comments;

        public int ID
        {
            get { return id; }
            set { id = value; }
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

        public string Comments
        {
            get { return comments; }
            set { comments = value; }
        }

        public bool IsExisting
        {
            get { return id > 0; }
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
            this.name = String.Empty;
            this.visible = true;
            this.enabled = true;
            this.comments = String.Empty;
        }

        public virtual void LoadFromDataReader(SqlDataReader reader)
        {
            this.id = reader.GetInt32("ID");
            this.name = reader.GetString("Name");
            this.visible = reader.GetBoolean("Visible");
            this.enabled = reader.GetBoolean("Enabled");
            this.comments = reader.GetString("Comments");
        }

        protected virtual void AppendCreateModifyCommandParameters(SqlCommand cmd)
        {
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
            cmd.Parameters.Add("@Visible", SqlDbType.Bit).Value = visible;
            cmd.Parameters.Add("@Enabled", SqlDbType.Bit).Value = enabled;
            cmd.Parameters.Add("@Comments", SqlDbType.NVarChar).Value = comments;
        }

        public void Load()
        {
            Load(this.id);
        }

        public abstract void Load(int id);

        public virtual void Save()
        {
            if (IsExisting)
            {
                Modify();
            }
            else
            {
                Create();
            }
        }

        protected abstract void Create();

        protected abstract void Modify();

        public void Delete()
        {
            Delete(this.id);
        }

        public abstract void Delete(int id);
    }
}
