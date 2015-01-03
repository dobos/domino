using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Complex.Domino.Lib
{
    public abstract class EntityFactory : ContextObject
    {
        private string name;
        private bool? enabled;
        private bool? visible;
        private string orderBy;

        protected abstract string TableName { get; }

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

        public string OrderBy
        {
            get { return orderBy; }
            set { orderBy = value; }
        }

        public EntityFactory(Context context)
            :base(context)
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.name = null;
            this.enabled = null;
            this.visible = null;
            this.orderBy = null;
        }

        public int Count()
        {
            using (var cmd = Context.CreateCommand())
            {
                string sql = @"
SELECT COUNT(*) FROM [{0}]
{1}";

                var where = BuildWhereClause(cmd);

                cmd.CommandText = String.Format(sql, TableName, where);

                return Context.ExecuteCommandScalar(cmd);
            }
        }

        protected IEnumerable<T> Find<T>(int max, int from)
            where T : IDatabaseTableObject, new()
        {
            using (var cmd = Context.CreateCommand())
            {
                string sql = @"
WITH q AS
(
    SELECT [{0}].*, ROW_NUMBER() OVER({1}) AS rn
    FROM [{0}]
    {2}
)
SELECT * FROM q
WHERE rn BETWEEN @from AND @to
{1}
";

                var where = BuildWhereClause(cmd);
                var orderby = BuildOrderByClause();

                cmd.CommandText = String.Format(sql, TableName, orderby, where);

                cmd.Parameters.Add("@from", SqlDbType.Int).Value = from;
                cmd.Parameters.Add("@to", SqlDbType.Int).Value = from + max;

                return Context.ExecuteCommandReader<T>(cmd);
            }
        }

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

        protected string BuildOrderByClause()
        {
            if (!String.IsNullOrWhiteSpace(orderBy))
            {
                return String.Format("ORDER BY {0}", orderBy);
            }
            else
            {
                return "ORDER BY ID ASC";
            }
        }
    }
}
