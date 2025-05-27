using Microsoft.VisualBasic.ApplicationServices;
using Moq;
using PersonalRegister;
using PersonalRegister.Interfaces;
using System.Collections.Generic;
using System.Data;
using static System.Data.Entity.Infrastructure.Design.Executor;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data.SQLite;
using System.Windows.Forms;
using NuGet.Frameworks;

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


        // https://stackoverflow.com/questions/45017295/assert-an-exception-using-xunit
        [Theory]
        [InlineData("7462E")]
        [InlineData("")]
        public void AddEmplyeeBadInput_ThrowsSQLException(string key)
        {
            DataTable testTable = new DataTable();
            testTable.Columns.Add("UniqueID", typeof(string));
            testTable.Columns.Add("Role", typeof(string));
            testTable.Rows.Add("7462E", "Bee");

            var mockDatabaseHelper = new Mock<IDatabaseHelper>();
            mockDatabaseHelper.Setup(db => db.ExecuteQuery("SELECT UniqueID, Role FROM Users"))
            .Returns(testTable);

            mockDatabaseHelper.Setup(db => db.ExecuteNonQuery("INSERT INTO Users (UniqueID, Role) " +
                                                             "VALUES (@UniqueID, @Role)",
                                                              It.IsAny<SQLiteParameter[]>()))
                .Throws(new SQLiteException("UNIQCUE CONSTRAIANT Users.UniqueID.PK VIOLATED"));

            AdminUser testUser = new AdminUser(mockDatabaseHelper.Object);

            var thrownExceptions = Record.Exception(() => testUser.AddEmployee(key, "APA"));
            Assert.Null(thrownExceptions);

        }

        // vet ej vad jag ska döpa denna till
        [Fact]
        public void UpdateEmployeeBadInput_NoUppdateConfirmation()
        {
            DataTable testTable = new DataTable();
            testTable.Columns.Add("UniqueID", typeof(string));
            testTable.Columns.Add("Role", typeof(string));

            var mockDatabaseHelper = new Mock<IDatabaseHelper>();
            mockDatabaseHelper.Setup(db => db.ExecuteQuery("SELECT UniqueID, Role FROM Users"))
            .Returns(testTable);

            mockDatabaseHelper.Setup(db => db.ExecuteNonQuery("UPDATE Users SET Role = @Role " +
                                                              "WHERE UniqueID = @UniqueID",
                                                              It.IsAny<SQLiteParameter[]>()));

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            AdminUser testUser = new AdminUser(mockDatabaseHelper.Object);
            testUser.UpdateEmployee("BJE43E", "APA");

            // Kollar så att inget "success" meddelande visas om man uppdaterar en användare som inte finns.
            string lowercaseOutput = consoleOutput.ToString().ToLower();
            Assert.False(lowercaseOutput.Contains("uppdaterad") || lowercaseOutput.Contains("uppdaterades")
                        || lowercaseOutput.Contains("lyckades") || lowercaseOutput.Contains("lyckad"));
            
        }

        [Fact]
        public void DeleteEmployeeBadInput_NoDeleteConfirmation()
        {
            DataTable testTable = new DataTable();
            testTable.Columns.Add("UniqueID", typeof(string));
            testTable.Columns.Add("Role", typeof(string));

            var mockDatabaseHelper = new Mock<IDatabaseHelper>();
            mockDatabaseHelper.Setup(db => db.ExecuteQuery("SELECT UniqueID, Role FROM Users"))
            .Returns(testTable);

            mockDatabaseHelper.Setup(db => db.ExecuteNonQuery("DELETE FROM Users " +
                                                              "WHERE UniqueID = @UniqueID",
                                                              It.IsAny<SQLiteParameter[]>()));

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            AdminUser testUser = new AdminUser(mockDatabaseHelper.Object);
            testUser.DeleteEmployee("BJE43E");

            // Kollar så att inget "success" meddelande visas om man uppdaterar en användare som inte finns.
            string lowercaseOutput = consoleOutput.ToString().ToLower();
            Assert.False(lowercaseOutput.Contains("borttagen") || lowercaseOutput.Contains("raderad")
                        || lowercaseOutput.Contains("togs bort") || lowercaseOutput.Contains("raderades"));

        }

        [Fact]
        public void TestConnectionRefused_ThrowsSQLError()
        {
            var mockDatabaseHelper = new Mock<IDatabaseHelper>();
            mockDatabaseHelper.Setup(db => db.ExecuteQuery("SELECT UniqueID, Role FROM Users"))
            .Throws(new SQLiteException("Connection failed"));

            var thrownException = Record.Exception(() => new AdminUser(mockDatabaseHelper.Object));

            Assert.Null(thrownException);
        }
    }
}