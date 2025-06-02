using Moq;
using PersonalRegister.GUI;
using PersonalRegister.Interfaces;
using PersonalRegister;
using System.Data.Entity.Core;
using System.Data;
using System.Data.SQLite;


namespace Tests_PersonalRegister
{
    public class TestCRUDForm
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void buttonQuickFetch_Click_Get_NullOrEmpty(string id)
        {
            DataTable testTable = new DataTable();
            testTable.Columns.Add("UniqueID", typeof(string));
            testTable.Columns.Add("Role", typeof(string));

            var mockedPerm = new Mock<IRolePermissions>();
            var mockDatabase = new Mock<IDatabaseHelper>();
            mockDatabase.Setup(db => db.ExecuteQuery("SELECT UniqueID, Role FROM Users"))
                .Returns(testTable);

            var form = new CRUDForm(mockedPerm.Object, mockDatabase.Object);
            Assert.Throws<ArgumentNullException>(() => form.buttonQuickFetch_Click_Get(id));

        }


        [Fact]
        public void buttonQuickFetch_Click_Get_IsInvalidThrowsException()
        {

            DataTable testTable = new DataTable();
            testTable.Columns.Add("UniqueID", typeof(string));
            testTable.Columns.Add("Role", typeof(string));

            var mockedPerm = new Mock<IRolePermissions>();
            var mockDatabase = new Mock<IDatabaseHelper>();

            mockDatabase.Setup(db => db.ExecuteQuery("SELECT UniqueID, Role FROM Users"))
                .Returns(testTable);
            mockDatabase.Setup(db => db.ExecuteQuery("SELECT * FROM Users WHERE UniqueID = @UniqueID",
                It.IsAny<SQLiteParameter[]>()))
                .Returns(testTable);

            var form = new CRUDForm(mockedPerm.Object, mockDatabase.Object);

            Assert.Throws<ObjectNotFoundException>(() => form.buttonQuickFetch_Click_Get("any id"));

        }

        [Fact]
        public void buttonShowDictionary_Click_Get_Insert_data_corretly()
        {
            var mockedPerm = new Mock<IRolePermissions>();
            var test_dict = new Dictionary<string, string>() { { "key", "value" } };
            var testTable = new DataTable();


            testTable.Columns.Add("UniqueID", typeof(string));
            testTable.Columns.Add("Role", typeof(string));

            foreach (var key in test_dict)
            {
                testTable.Rows.Add(key.Key, key.Value);

            }

            var mockDatabase = new Mock<IDatabaseHelper>();

            mockDatabase.Setup(db => db.ExecuteQuery("SELECT UniqueID, Role FROM Users"))
                .Returns(testTable);
            var form = new CRUDForm(mockedPerm.Object, mockDatabase.Object);


            form.buttonShowDictionary_Click_get();
            var RealTable = (DataTable)form.dataGridView.DataSource;

            Assert.Equal(testTable.Columns.Count, RealTable.Columns.Count); //2
            Assert.Equal(testTable.Rows.Count, RealTable.Rows.Count); //1
            Assert.Equal(testTable.Rows[0][0], RealTable.Rows[0][0]);

        }

        [Theory]
        [InlineData("Greg", "Gsreg")] // to fit the structure of => string employeeEntry = $"{uniqueID}: {role}";

        public void buttonShowList_Click_Get_All_Emplyees_Test(string uniqueID, string role)
        {
            var mockedPerm = new Mock<IRolePermissions>();
            var mockOperation = new Mock<AdminUser>(true);

            var testTable = new DataTable();
            var fakelist = new List<string>() { $"{uniqueID}: {role}" };

            testTable.Columns.Add("UniqueID", typeof(string));
            testTable.Columns.Add("Role", typeof(string));
            testTable.Columns.Add("Employee", typeof(string));
            testTable.Rows.Add(fakelist[0]);


            testTable.Rows.Add(fakelist);

            var mockDatabase = new Mock<IDatabaseHelper>();
            mockDatabase.Setup(db => db.ExecuteQuery("SELECT UniqueID, Role FROM Users"))
               .Returns(testTable);

            var form = new CRUDForm(mockedPerm.Object, mockDatabase.Object);
            form.buttonShowList_Click_Get();
            var realtable = (DataTable)form.dataGridView.DataSource;

            Assert.Equal(fakelist[0], realtable.Rows[0][0]);

        }


        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void buttonDelete_Click_Get_Invalid_ID_Should_Throw_NotImplementetEx_Test(string ID)
        {
            var mockedPerm = new Mock<IRolePermissions>();
            var mockDatabase = new Mock<IDatabaseHelper>();
            DataTable testTable = new DataTable();
            testTable.Columns.Add("UniqueID", typeof(string));
            testTable.Columns.Add("Role", typeof(string));
            mockDatabase.Setup(db => db.ExecuteQuery("SELECT UniqueID, Role FROM Users"))
               .Returns(testTable);

            var form = new CRUDForm(mockedPerm.Object, mockDatabase.Object);
            form.textUniqueID.Text = ID;
            Assert.Throws<ArgumentNullException>(() => form.buttonDelete_Click_Get());
        }

        [Fact]
        public void buttonDelete_Click_Get_Invalid_ID_DeletesCorrectEmploye()
        { 

            var mockedPerm = new Mock<IRolePermissions>();
            var mockDatabase = new Mock<IDatabaseHelper>();
            DataTable testTable = new DataTable();
            testTable.Columns.Add("UniqueID", typeof(string));
            testTable.Columns.Add("Role", typeof(string));
            mockDatabase.Setup(db => db.ExecuteQuery("SELECT UniqueID, Role FROM Users"))
               .Returns(testTable);

            var form = new CRUDForm(mockedPerm.Object, mockDatabase.Object);
            form.textUniqueID.Text = "Władysław III";
            Assert.Throws<Exception>(() => form.buttonDelete_Click_Get()); // instead check that targeted "branch" can be reached since same user would be deleted

        }


        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void buttonFetch_Click_Get_NullEmpty_ID_Test(string ID)
        {

            var mockedPerm = new Mock<IRolePermissions>();
            var mockDatabase = new Mock<IDatabaseHelper>();
            DataTable testTable = new DataTable();

            testTable.Columns.Add("Role", typeof(string));

            mockDatabase.Setup(db => db.ExecuteQuery("SELECT UniqueID, Role FROM Users"))
            .Returns(testTable);


            var form = new CRUDForm(mockedPerm.Object, mockDatabase.Object);
            form.textUniqueID.Text = ID;

            Assert.Throws<System.ArgumentNullException>(() => form.buttonDelete_Click_Get());



        }
        [Fact]
        public void LoadEmployeeData_correct_data_test()
        {

            var mockedPerm = new Mock<IRolePermissions>();
            var mockDatabase = new Mock<IDatabaseHelper>();
            DataTable testTable = new DataTable();

            testTable.Columns.Add("Role");
            testTable.Columns.Add("UniqueID");

            testTable.Rows.Add("Olof");

            mockDatabase.Setup(db => db.ExecuteQuery("SELECT UniqueID, Role FROM Users"))
                   .Returns(testTable);

            var form = new CRUDForm(mockedPerm.Object, mockDatabase.Object);
            var realtable = (DataTable)form.dataGridView.DataSource;

            form.LoadEmployeeData();


            Assert.Equal(testTable.Rows[0][0], realtable.Rows[0][0]);


        }


    }
}


