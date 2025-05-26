using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalRegister.Interfaces
{
    public interface IDatabaseHelper
    {
        void ExecuteNonQuery(string query, params SQLiteParameter[] parameters);
        DataTable ExecuteQuery(string query, params SQLiteParameter[] parameters);
    }
}
