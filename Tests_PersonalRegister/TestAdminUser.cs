using Microsoft.VisualBasic.ApplicationServices;
using Moq;
using PersonalRegister;
using PersonalRegister.Interfaces;
using System.Collections.Generic;
using System.Data;
using static System.Data.Entity.Infrastructure.Design.Executor;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tests_PersonalRegister
{
    public class TestAdminUser
    {
        [Fact]
        public void LoadEmployeesFromDatabase_LoadsAndReturnsCorrectEmployees()
        {
            
            DataTable testTable = new DataTable();
            testTable.Columns.Add("UniqueID", typeof(string));
            testTable.Columns.Add("Role", typeof(string));   
            
            testTable.Rows.Add("7462E", "Bee");
            testTable.Rows.Add("3836Q", "Ant");
            testTable.Rows.Add("8444I", "Bee");
            testTable.Rows.Add("6536V", "Ant");
            testTable.Rows.Add("3098W", "Ant");
            testTable.Rows.Add("8031L", "Bee");

            var mockDatabaseHelper = new Mock<IDatabaseHelper>();
            mockDatabaseHelper.Setup(db => db.ExecuteQuery("SELECT UniqueID, Role FROM Users"))
                .Returns(testTable);

            AdminUser testUser = new AdminUser(mockDatabaseHelper.Object);

            var expectedDictResult = new Dictionary<string, string>
            {
                { "7462E", "Bee" },
                { "3836Q", "Ant" },
                { "8444I", "Bee" },
                { "6536V", "Ant" },
                { "3098W", "Ant" },
                { "8031L", "Bee" }
            };
            var expectedListResult = new List<string> 
            {
                "7462E: Bee",
                "3836Q: Ant",
                "8444I: Bee",
                "6536V: Ant",
                "3098W: Ant",
                "8031L: Bee"
            };

            var listResult = testUser.GetAllEmployeesList();
            var dictResult = testUser.GetAllEmployeesDictionary();
            
            Assert.Equal(listResult, expectedListResult);
            Assert.Equal(dictResult, expectedDictResult);
        }

        [Fact]
        public void AddEmployee_AddsCorrectEmployee()
        {
            DataTable testTable = new DataTable();
            testTable.Columns.Add("UniqueID", typeof(string));
            testTable.Columns.Add("Role", typeof(string));

            var mockDatabaseHelper = new Mock<IDatabaseHelper>();
            mockDatabaseHelper.Setup(db => db.ExecuteQuery("SELECT UniqueID, Role FROM Users"))
                .Returns(testTable);
            mockDatabaseHelper.Setup(db => db.ExecuteNonQuery("INSERT INTO Users (UniqueID, Role) " +
                                                              "VALUES (@UniqueID, @Role)"));
                
            AdminUser testUser = new AdminUser(mockDatabaseHelper.Object);
            testUser.AddEmployee("TEST123", "APA");
            var employeeList = testUser.GetAllEmployeesList();
            var employeeDict = testUser.GetAllEmployeesDictionary();

            Assert.Contains("TEST123: APA", employeeList);
            Assert.Contains("TEST123", employeeDict.Keys);
            Assert.Contains("APA", employeeDict.Values);
        }

        [Fact]
        public void UpdateEmployee_UpdatesCorrectEmployee()
        {
            DataTable testTable = new DataTable();
            testTable.Columns.Add("UniqueID", typeof(string));
            testTable.Columns.Add("Role", typeof(string));
            testTable.Rows.Add("7462E", "Bee");

            var mockDatabaseHelper = new Mock<IDatabaseHelper>();
            mockDatabaseHelper.Setup(db => db.ExecuteQuery("SELECT UniqueID, Role FROM Users"))
            .Returns(testTable);
            mockDatabaseHelper.Setup(db => db.ExecuteNonQuery("UPDATE Users SET Role = @Role " +
                                                              "WHERE UniqueID = @UniqueID"));

            AdminUser testUser = new AdminUser(mockDatabaseHelper.Object);
            testUser.UpdateEmployee("7462E", "APA");
            var employeeList = testUser.GetAllEmployeesList();
            var employeeDict = testUser.GetAllEmployeesDictionary();

            Assert.Equal("7462E: APA", employeeList[0]);
            Assert.Equal("7462E", employeeDict.Keys.First());
            Assert.Equal("APA", employeeDict.Values.First());
        }

        [Fact]
        public void DeleteEmployee_DeletesCorrectEmployee()
        {
            DataTable testTable = new DataTable();
            testTable.Columns.Add("UniqueID", typeof(string));
            testTable.Columns.Add("Role", typeof(string));
            testTable.Rows.Add("7462E", "Bee");

            var mockDatabaseHelper = new Mock<IDatabaseHelper>();
            mockDatabaseHelper.Setup(db => db.ExecuteQuery("SELECT UniqueID, Role FROM Users"))
            .Returns(testTable);
            mockDatabaseHelper.Setup(db => db.ExecuteNonQuery("DELETE FROM Users " +
                                                              "WHERE UniqueID = @UniqueID"));

            AdminUser testUser = new AdminUser(mockDatabaseHelper.Object);
            testUser.DeleteEmployee("7462E");
            var employeeList = testUser.GetAllEmployeesList();
            var employeeDict = testUser.GetAllEmployeesDictionary();

            Assert.Empty(employeeList);
            Assert.Empty(employeeDict);
        }
    }
}