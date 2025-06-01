using Moq;
using PersonalRegister.GUI;
using PersonalRegister.Interfaces;
using PersonalRegister;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Xml;

namespace Tests_PersonalRegister
{
    public class TestCRUDForm
    {
        IUserOperations IUserOperations;
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
            testTable.Columns.Add("UniqueID", typeof(string));
            testTable.Columns.Add("Role", typeof(string));
            testTable.Rows.Add($"{uniqueID}: {role}");

            var mockDatabase = new Mock<IDatabaseHelper>();
            mockDatabase.Setup(db => db.ExecuteQuery("SELECT UniqueID, Role FROM Users"))
               .Returns(testTable);

            var form = new CRUDForm(mockedPerm.Object, mockDatabase.Object);
            form.buttonShowList_Click_Get();
            var realtable = (DataTable)form.dataGridView.DataSource;

            Assert.Equal("Greg: Gsreg", realtable.Rows[0][0]);

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

        //[Fact]
        //public void buttonDelete_Click_Get_Invalid_ID_DeletesCorrectEmploye()
        //{ //lazy test this time need whole new structure

        //    var mockedPerm = new Mock<IRolePermissions>();
        //    var mockOperation = new Mock<AdminUser>(true);
        //    var form = new CRUDForm(mockedPerm.Object, mockOperation.Object);
        //    form.textUniqueID.Text = "Władysław III";
        //    Assert.Throws<Exception>(() => form.buttonDelete_Click_Get()); // instead check that targeted "branch" can be reached since same user would be deleted

        //}

        //[Fact]
        //public void ShowUpdateForm_Correct_update()
        //{

        //    var mockedPerm = new Mock<IRolePermissions>();
        //    var mockOperation = new Mock<AdminUser>(true);

        //    mockOperation.Setup(_ => _.UpdateEmployee("Michinomiya", "Hirohito")).Verifiable();
        //    var form = new CRUDForm(mockedPerm.Object, mockOperation.Object);



        //    form.ShowUpdateForm("Michinomiya", "Hirohito", true);

        //    mockOperation.Verify(_ => _.UpdateEmployee("Michinomiya", "Hirohito"), Times.Once);


        //}

        //[Theory]
        //[InlineData("",null)]
        //public void buttonFetch_Click_Get_Test_NullEmpty_ID(string ID)
        //{





        //}
    }
}
