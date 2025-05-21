using PersonalRegister.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace PersonalRegister
{
    public class AdminUser : IUserOperations
    {
        private List<string> employeeList = new List<string>(); // Lista för anställda
        private Dictionary<string, string> employeeDictionary = new Dictionary<string, string>(); // Dictionary för anställda

        public AdminUser()
        {
            LoadEmployeesFromDatabase(); // Ladda anställda från databasen när klassen initieras
        }

        // Ladda data från databasen till List och Dictionary
        private void LoadEmployeesFromDatabase()   
        {
            employeeList.Clear();
            employeeDictionary.Clear();

            DataTable employees = DatabaseHelper.ExecuteQuery("SELECT UniqueID, Role FROM Users");
            foreach (DataRow row in employees.Rows)
            {
                string uniqueID = row["UniqueID"]?.ToString() ?? string.Empty;
                string role = row["Role"]?.ToString() ?? string.Empty;
                string employeeEntry = $"{uniqueID}: {role}";

                // Lägg till i både list och dictionary
                if (!employeeList.Contains(employeeEntry))
                {
                    
                    employeeList.Add($"{uniqueID}: {role}");
                    employeeDictionary[uniqueID] = role;
                }
            }
        }

        // CRUD för Employees i databasen
        public void AddEmployee(string uniqueID, string role)
        {
            DatabaseHelper.ExecuteNonQuery(
                "INSERT INTO Users (UniqueID, Role) VALUES (@UniqueID, @Role)",
                new SQLiteParameter("@UniqueID", uniqueID),
                new SQLiteParameter("@Role", role)
            );
            Console.WriteLine("Användare tillagd i databasen.");

            // Uppdatera listan och dictionary efter att användaren lagts till
            employeeList.Add($"{uniqueID}: {role}");
            employeeDictionary[uniqueID] = role;
        }

        public DataTable GetAllEmployees()
        {
            return DatabaseHelper.ExecuteQuery("SELECT * FROM Users");
        }

        public DataTable GetEmployeeById(string uniqueID)
        {
            return DatabaseHelper.ExecuteQuery(
                "SELECT * FROM Users WHERE UniqueID = @UniqueID",
                new SQLiteParameter("@UniqueID", uniqueID)
            );
        }

        public void UpdateEmployee(string uniqueID, string newRole)
        {
            DatabaseHelper.ExecuteNonQuery(
                "UPDATE Users SET Role = @Role WHERE UniqueID = @UniqueID",
                new SQLiteParameter("@Role", newRole),
                new SQLiteParameter("@UniqueID", uniqueID)
            );
            Console.WriteLine("Användare uppdaterad i databasen.");

            // Uppdatera i listan och dictionary efter uppdatering
            for (int i = 0; i < employeeList.Count; i++)
            {
                if (employeeList[i].StartsWith(uniqueID))
                {
                    employeeList[i] = $"{uniqueID}: {newRole}";
                    break;
                }
            }

            if (employeeDictionary.ContainsKey(uniqueID))
            {
                employeeDictionary[uniqueID] = newRole;
            }
        }

        public void DeleteEmployee(string uniqueID)
        {
            DatabaseHelper.ExecuteNonQuery(
                "DELETE FROM Users WHERE UniqueID = @UniqueID",
                new SQLiteParameter("@UniqueID", uniqueID)
            );
            Console.WriteLine("Användare borttagen från databasen.");

            // Ta bort från både list och dictionary
            employeeList.RemoveAll(e => e.StartsWith(uniqueID));
            employeeDictionary.Remove(uniqueID);
        }

        public List<string> GetAllEmployeesList()
        {
            return new List<string>(employeeList);
        }

        public Dictionary<string, string> GetAllEmployeesDictionary()
        {
            return new Dictionary<string, string>(employeeDictionary);
        }
    }
}