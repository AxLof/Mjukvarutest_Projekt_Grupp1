using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalRegister.Interfaces
{
    public interface IUserOperations
    {
        void AddEmployee(string uniqueID, string role);
        DataTable GetAllEmployees();
        DataTable GetEmployeeById(string uniqueID);
        void UpdateEmployee(string uniqueID, string newRole);
        void DeleteEmployee(string uniqueID);

        List<string> GetAllEmployeesList();
        Dictionary<string, string> GetAllEmployeesDictionary();
    }


}
