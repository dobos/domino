using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Complex.Domino.Lib
{
    public static class SqlDataReaderExtensions
    {
        public static IEnumerable<T> AsEnumerable<T>(this SqlDataReader reader)
            where T : IDatabaseTableObject, new()
        {
            return new DataReaderEnumerator<T>(reader);
        }

        public static T AsSingleObject<T>(this SqlDataReader reader)
            where T : IDatabaseTableObject, new()
        {
            if (!reader.Read())
            {
                throw Error.NoResults(1);
            }

            var o = new T();
            o.LoadFromDataReader(reader);

            return o;
        }

        public static void AsSingleObject<T>(this SqlDataReader reader, T o)
            where T : IDatabaseTableObject
        {
            if (!reader.Read())
            {
                throw Error.NoResults(1);
            }

            o.LoadFromDataReader(reader);
        }

        public static int GetInt32(this SqlDataReader reader, string key)
        {
            var o = reader.GetOrdinal(key);
            return reader.GetInt32(o);
        }

        public static bool GetBoolean(this SqlDataReader reader, string key)
        {
            var o = reader.GetOrdinal(key);
            return reader.GetBoolean(o);
        }

        public static string GetString(this SqlDataReader reader, string key)
        {
            var o = reader.GetOrdinal(key);

            if (reader.IsDBNull(o))
            {
                return null;
            }
            else
            {
                return reader.GetString(o);
            }
        }
    }
}
