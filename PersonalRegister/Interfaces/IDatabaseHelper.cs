using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalRegister.Interfaces
{
    internal interface IDatabaseHelper
    {
        public DataTable ExecuteQuery(string query, params SQLiteParameter[] parameters);













    }
}
