using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Complex.Domino.Lib
{
    [Serializable]
    public abstract class Entity : ContextObject, IDatabaseTableObject
    {
        private bool isLoaded;
        private Access access;

        private int id;
        private string name;
        private string description;
        private bool hidden;
        private bool readOnly;
        private DateTime createdDate;
        private DateTime modifiedDate;
        private string comments;

        public Access Access
        {
            get
            {
                if (access == null)
                {
                    access = GetAccess();
                }

                return access;
            }
        }

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

        public bool Hidden
        {
            get { return hidden; }
            set { hidden = value; }
        }

        public bool ReadOnly
        {
            get { return readOnly; }
            set { readOnly = value; }
        }

        public DateTime CreatedDate
        {
            get { return createdDate; }
        }

        public DateTime ModifiedDate
        {
            get { return modifiedDate; }
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
            this.hidden = false;
            this.readOnly = false;
            this.createdDate = DateTime.Now;
            this.modifiedDate = DateTime.Now;
            this.comments = String.Empty;
        }

        private void CopyMembers(Entity old)
        {
            this.isLoaded = old.isLoaded;

            this.id = old.id;
            this.name = old.name;
            this.description = old.description;
            this.hidden = old.hidden;
            this.readOnly = old.readOnly;
            this.createdDate = old.createdDate;
            this.modifiedDate = old.modifiedDate;
            this.comments = old.comments;
        }

        public virtual void LoadFromDataReader(SqlDataReader reader)
        {
            this.id = reader.GetInt32("ID");
            this.name = reader.GetString("Name");
            this.description = reader.GetString("Description");
            this.hidden = reader.GetBoolean("Hidden");
            this.readOnly = reader.GetBoolean("ReadOnly");
            this.createdDate = reader.GetDateTime("CreatedDate");
            this.modifiedDate = reader.GetDateTime("ModifiedDate");
            this.comments = reader.GetString("Comments");

            isLoaded = true;
        }

        protected abstract Access GetAccess();

        protected void GetInsertColumnsScript(out string columns, out string values)
        {
            columns = "Name, Description, Hidden, ReadOnly, CreatedDate, ModifiedDate, Comments";
            values = "@Name, @Description, @Hidden, @ReadOnly, @CreatedDate, @ModifiedDate, @Comments";
        }

        protected void GetUpdateColumnsScript(out string columns)
        {
            columns = @"Name = @Name,
    Description = @Description,
    Hidden = @Hidden,
    ReadOnly = @ReadOnly,
    CreatedDate = @CreatedDate,
    ModifiedDate = @ModifiedDate,
    Comments = @Comments";
        }

        protected virtual void AppendCreateModifyCommandParameters(SqlCommand cmd)
        {
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = description;
            cmd.Parameters.Add("@Hidden", SqlDbType.Bit).Value = hidden;
            cmd.Parameters.Add("@ReadOnly", SqlDbType.Bit).Value = readOnly;
            cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = createdDate;
            cmd.Parameters.Add("@ModifiedDate", SqlDbType.DateTime).Value = modifiedDate;
            cmd.Parameters.Add("@Comments", SqlDbType.NVarChar).Value = comments;
        }

        public void Load()
        {
            Load(this.id);
        }

        public void Load(int id)
        {
            OnLoad(id);

            // Check access
            var access = GetAccess();

            if (!access.Read)
            {
                throw Error.AccessDenied();
            }
        }

        protected abstract void OnLoad(int id);

        public void Save()
        {
            var access = GetAccess();

            if (IsExisting)
            {
                if (!access.Update)
                {
                    throw Error.AccessDenied();
                }

                string columns;
                GetUpdateColumnsScript(out columns);

                modifiedDate = DateTime.Now;
                OnModify(columns);
            }
            else
            {
                // TODO: add access control call
                if (!access.Create)
                {
                    throw Error.AccessDenied();
                }

                string columns, values;
                GetInsertColumnsScript(out columns, out values);

                createdDate = modifiedDate = DateTime.Now;
                OnCreate(columns, values);
            }
        }

        protected abstract void OnCreate(string columns, string values);

        protected abstract void OnModify(string columns);

        public void Delete()
        {
            Delete(this.id);
        }

        public void Delete(int id)
        {
            var access = GetAccess();

            if (!access.Delete)
            {
                throw Error.AccessDenied();
            }

            OnDelete(id);
        }

        protected abstract void OnDelete(int id);
    }
}
