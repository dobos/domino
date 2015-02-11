using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Complex.Domino.Lib
{
    public class Installer : ContextObject
    {
        #region Constructors and initializers

        public Installer(Context context)
            : base(context)
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
        }

        #endregion

        public void DropSchema()
        {
            var sql = SplitScript(SqlScripts.DropSchema);
            ExecuteScripts(sql);
        }

        public void CreateSchema()
        {
            var sql = SplitScript(SqlScripts.CreateSchema);
            ExecuteScripts(sql);
        }

        public void InitializeData()
        {
            var sql = SplitScript(SqlScripts.InitData);
            ExecuteScripts(sql);
        }

        /// <summary>
        /// Split a string into parts between GO lines that should be executed individually.
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private string[] SplitScript(string sql)
        {
            var parts = sql.Split(new string[] { "\r\nGO", "\nGO" }, StringSplitOptions.RemoveEmptyEntries);

            // now remove all USE [database] statements

            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i].IndexOf("USE ", StringComparison.InvariantCultureIgnoreCase) >= 0)
                {
                    parts[i] = null;
                }
            }

            return parts;
        }

        private void ExecuteScripts(string[] sql)
        {
            for (int i = 0; i < sql.Length; i++)
            {
                if (sql[i] != null)
                {
                    using (var cn = Context.OpenConnection())
                    {
                        using (var cmd = new SqlCommand(sql[i], cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
    }
}
