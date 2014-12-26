using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Complex.Domino.Lib
{
    public interface IDatabaseTableObject
    {
        void LoadFromDataReader(SqlDataReader reader);
    }
}

