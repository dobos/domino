using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Complex.Domino.Lib
{
    public abstract class EntityFactory<T> : ContextObject
        where T : IDatabaseTableObject, new()
    {
        private static readonly Regex OrderByRegex = new Regex(@"[a-z]+\s*(asc|desc){0,1}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private string name;
        private bool? readOnly;
        private bool? hidden;
        private DateTime filterDate;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public bool? ReadOnly
        {
            get { return readOnly; }
            set { readOnly = value; }
        }

        public bool? Hidden
        {
            get { return hidden; }
            set { hidden = value; }
        }

        public DateTime FilterDate
        {
            get { return filterDate; }
            set { filterDate = value; }
        }

        public EntityFactory(Context context)
            : base(context)
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.name = null;
            this.readOnly = null;
            this.hidden = false;
            this.filterDate = DateTime.Now;
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
            // Prevent any injection attacks
            if (!String.IsNullOrWhiteSpace(orderBy) && !OrderByRegex.Match(orderBy).Success)
            {
                Error.AccessDenied();
            }

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

                return Context.ExecuteCommandAsEnumerable<T>(cmd);
            }
        }

        protected abstract string GetTableQuery();

        protected string BuildWhereClause(SqlCommand cmd)
        {
            var sb = new StringBuilder();

            if (!String.IsNullOrWhiteSpace(name))
            {
                AppendWhereCriterion(sb, "Name = @Name");
                cmd.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar).Value = name;
            }

            cmd.Parameters.Add("@filterDate", SqlDbType.DateTime).Value = filterDate;

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

            if (readOnly.HasValue)
            {
                AppendWhereCriterion(sb, "ReadOnly = @ReadOnly");

                cmd.Parameters.Add("@ReadOnly", SqlDbType.Bit).Value = readOnly.Value;
            }

            if (hidden.HasValue)
            {
                AppendWhereCriterion(sb, "Hidden = @Hidden");

                cmd.Parameters.Add("@Hidden", SqlDbType.Bit).Value = hidden.Value;
            }
        }

        protected void AppendWhereCriterion(StringBuilder sb, string criterion)
        {
            AppendWhereCriterion(sb, criterion, "AND");
        }

        protected void AppendWhereCriterion(StringBuilder sb, string criterion, string op)
        {
            if (sb.Length > 0)
            {
                sb.Append(" ");
                sb.Append(op);
                sb.Append(" ");
            }

            sb.Append("(");
            sb.Append(criterion);
            sb.AppendLine(")");
        }

        protected void AppendDateTimeFilter(StringBuilder sb, string prefix, DateTimeFilter filter)
        {
            if (filter != DateTimeFilter.All)
            {
                var res = new StringBuilder();

                if ((filter & DateTimeFilter.Active) != 0)
                {
                    AppendWhereCriterion(res, String.Format("{0}StartDate <= @filterDate AND @filterDate <= {0}EndDate", prefix));
                }

                if ((filter & DateTimeFilter.Expired) != 0)
                {
                    AppendWhereCriterion(res, String.Format("{0}EndDate < @filterDate", prefix));
                }

                if ((filter & DateTimeFilter.Future) != 0)
                {
                    AppendWhereCriterion(res, String.Format("@filterDate < {0}StartDate", prefix));
                }

                if (res.Length != 0)
                {
                    AppendWhereCriterion(sb, res.ToString());
                }
            }
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
