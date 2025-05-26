using Moq;
using PersonalRegister;
using PersonalRegister.Interfaces;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tests_PersonalRegister
{
    public class UnitTest1
    {
        [Fact]
        public void LoadEmployeesFromDatabase_LoadsAndReturnsCorrectEmployees()
        {
            // Arrange
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
    }
}