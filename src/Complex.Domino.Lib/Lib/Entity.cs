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
        private bool isLoaded;

        private int id;
        private string name;
        private string description;
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

        public string Description
        {
            get { return description; }
            set { description = value; }
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
            : base(context)
        {
            InitializeMembers();
        }

        protected Entity(Entity old)
            : base(old.Context)
        {
            CopyMembers(old);
        }

        private void InitializeMembers()
        {
            this.isLoaded = false;

            this.id = -1;
            this.name = String.Empty;
            this.description = String.Empty;
            this.visible = true;
            this.enabled = true;
            this.comments = String.Empty;
        }

        private void CopyMembers(Entity old)
        {
            this.isLoaded = old.isLoaded;

            this.id = old.id;
            this.name = old.name;
            this.description = old.description;
            this.visible = old.visible;
            this.enabled = old.enabled;
            this.comments = old.comments;
        }

        public virtual void LoadFromDataReader(SqlDataReader reader)
        {
            this.id = reader.GetInt32("ID");
            this.name = reader.GetString("Name");
            this.description = reader.GetString("Description");
            this.visible = reader.GetBoolean("Visible");
            this.enabled = reader.GetBoolean("Enabled");
            this.comments = reader.GetString("Comments");

            isLoaded = true;
        }

        protected void GetInsertColumnsScript(out string columns, out string values)
        {
            columns = "Name, Description, Visible, Enabled, Comments";
            values = "@Name, @Description, @Visible, @Enabled, @Comments";
        }

        protected void GetUpdateColumnsScript(out string columns)
        {
            columns = @"Name = @Name,
    Description = @Description,
    Visible = @Visible,
    Enabled = @Enabled,
    Comments = @Comments";
        }

        protected virtual void AppendCreateModifyCommandParameters(SqlCommand cmd)
        {
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = description;
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
                string columns;
                GetUpdateColumnsScript(out columns);
                Modify(columns);
            }
            else
            {
                string columns, values;
                GetInsertColumnsScript(out columns, out values);
                Create(columns, values);
            }
        }

        protected abstract void Create(string columns, string values);

        protected abstract void Modify(string columns);

        public void Delete()
        {
            Delete(this.id);
        }

        public abstract void Delete(int id);
    }
}
