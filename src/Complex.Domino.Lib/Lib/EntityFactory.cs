﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Complex.Domino.Lib
{
    public abstract class EntityFactory<T> : ContextObject
        where T : IDatabaseTableObject, new()
    {
        private string name;
        private bool? enabled;
        private bool? visible;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public bool? Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        public bool? Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        public EntityFactory(Context context)
            : base(context)
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.name = null;
            this.enabled = null;
            this.visible = true;
        }

        public int Count()
        {
            using (var cmd = Context.CreateCommand())
            {
                string sql = @"
SELECT COUNT(*) FROM {0} AS entities
{1}";

                var where = BuildWhereClause(cmd);
                var tableQuery = GetTableQuery();

                cmd.CommandText = String.Format(sql, GetTableQuery(), where);

                return Context.ExecuteCommandScalar(cmd);
            }
        }

        public IEnumerable<T> Find()
        {
            return Find(-1, -1, null);
        }

        public IEnumerable<T> Find(int max, int from, string orderBy)
        {
            using (var cmd = Context.CreateCommand())
            {
                string sql = @"
WITH q AS
(
    SELECT entities.*, ROW_NUMBER() OVER({1}) AS rn
    FROM {0} AS entities
    {2}
)
SELECT * FROM q
{3}
{1}
";

                var where = BuildWhereClause(cmd);
                var orderby = BuildOrderByClause(orderBy);

                var limit = from > 0 || max > 0 ? "WHERE rn BETWEEN @from AND @to" : "";

                var table = GetTableQuery();

                cmd.CommandText = String.Format(sql, table, orderby, where, limit);

                cmd.Parameters.Add("@from", SqlDbType.Int).Value = from;
                cmd.Parameters.Add("@to", SqlDbType.Int).Value = from + max;

                return Context.ExecuteCommandReader<T>(cmd);
            }
        }

        protected abstract string GetTableQuery();

        protected string BuildWhereClause(SqlCommand cmd)
        {
            var sb = new StringBuilder();

            AppendWhereCriteria(sb, cmd);

            if (sb.Length > 0)
            {
                sb.Insert(0, "WHERE ");
            }

            return sb.ToString();
        }

        protected virtual void AppendWhereCriteria(StringBuilder sb, SqlCommand cmd)
        {
            if (name != null)
            {
                AppendWhereCriterion(sb, "Name LIKE @Name");

                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = '%' + name + '%';
            }

            if (enabled.HasValue)
            {
                AppendWhereCriterion(sb, "Enabled = @Enabled");

                cmd.Parameters.Add("@Enabled", SqlDbType.Bit).Value = enabled.Value;
            }

            if (visible.HasValue)
            {
                AppendWhereCriterion(sb, "Visible = @Visible");

                cmd.Parameters.Add("@Visible", SqlDbType.Bit).Value = visible.Value;
            }
        }

        protected void AppendWhereCriterion(StringBuilder sb, string criterion)
        {
            if (sb.Length > 0)
            {
                sb.Append(" AND ");
            }

            sb.Append("(");
            sb.Append(criterion);
            sb.AppendLine(")");
        }

        protected virtual string GetDefaultOrderBy()
        {
            return "ID ASC";
        }

        protected string BuildOrderByClause(string orderBy)
        {
            if (!String.IsNullOrWhiteSpace(orderBy))
            {
                return String.Format("ORDER BY {0}", orderBy);
            }
            else
            {
                return "ORDER BY " + GetDefaultOrderBy();
            }
        }
    }
}
