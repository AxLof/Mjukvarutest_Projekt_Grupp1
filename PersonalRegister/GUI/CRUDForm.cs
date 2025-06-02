using PersonalRegister.Interfaces;
using System.Data;
using System.Data.Entity.Core;

namespace PersonalRegister.GUI
{
    public partial class CRUDForm : Form
    {
        private readonly AdminUser adminUser;
        private readonly IRolePermissions permissions;

        public DataGridView dataGridView = null!;
        private Button buttonCreate = null!;
        private Button buttonRead = null!;
        private Button buttonUpdate = null!;
        private Button buttonDelete = null!;
        private Button buttonFetch = null!;
        private Button buttonQuickFetch = null!;
        private Button buttonShowList = null!;
        private Button buttonShowDictionary = null!;
        public TextBox textUniqueID = null!;

        public CRUDForm(IRolePermissions userPermissions, IDatabaseHelper databaseHelper)
        {
            permissions = userPermissions;
            adminUser = new AdminUser(databaseHelper);
            InitializeComponent();
            ApplyPermissions();
        }

        private void ApplyPermissions()
        {
            // Sätter knapp-tillgänglighet baserat på behörigheter
            buttonCreate.Enabled = permissions.CanAddEmployee;
            buttonRead.Enabled = permissions.CanViewEmployee;
            buttonUpdate.Enabled = permissions.CanUpdateEmployee;
            buttonDelete.Enabled = permissions.CanDeleteEmployee;
        }

        private void InitializeComponent()
        {
            buttonCreate = new Button { Text = "Lägg till anställd", Location = new Point(100, 50), Size = new Size(200, 40) };
            buttonRead = new Button { Text = "Visa anställda(DataTable)", Location = new Point(100, 110), Size = new Size(200, 40) };
            buttonUpdate = new Button { Text = "Uppdatera anställd", Location = new Point(100, 170), Size = new Size(200, 40) };
            buttonDelete = new Button { Text = "Ta bort anställd", Location = new Point(100, 230), Size = new Size(200, 40) };
            buttonFetch = new Button { Text = "Hämta anställd", Location = new Point(350, 80), Size = new Size(150, 40) };
            buttonQuickFetch = new Button { Text = "Hämta anställd(Dictonary)", Location = new Point(510, 80), Size = new Size(150, 40) };
            buttonShowList = new Button { Text = "Visa anställda(Lista)", Location = new Point(350, 130), Size = new Size(150, 40) };
            buttonShowDictionary = new Button { Text = "Visa anställda(Dictionary)", Location = new Point(510, 130), Size = new Size(150, 40) };

            textUniqueID = new TextBox { Location = new Point(350, 50), Size = new Size(150, 20), PlaceholderText = "Ange Unik ID" };
            dataGridView = new DataGridView { Location = new Point(50, 300), Size = new Size(600, 200), AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };

            // Lägg till knappar och andra kontroller
            Controls.AddRange(new Control[] { buttonCreate, buttonRead, buttonUpdate, buttonDelete, buttonFetch, buttonQuickFetch, buttonShowList, buttonShowDictionary, textUniqueID, dataGridView });

            // Sätter knapphändelser
            buttonCreate.Click += buttonCreate_Click;
            buttonRead.Click += buttonRead_Click;
            buttonUpdate.Click += buttonUpdate_Click;
            buttonDelete.Click += buttonDelete_Click;
            buttonFetch.Click += buttonFetch_Click;
            buttonQuickFetch.Click += buttonQuickFetch_Click;
            buttonShowList.Click += buttonShowList_Click;
            buttonShowDictionary.Click += buttonShowDictionary_Click;

            ClientSize = new Size(700, 600);
            Text = "Register Hanterare";
        }


        private void buttonCreate_Click(object sender, EventArgs e)
        {
            using (var registerForm = new RegisterForm())
            {
                if (registerForm.ShowDialog() == DialogResult.OK)
                {
                    adminUser.AddEmployee(registerForm.UniqueID, registerForm.Role);
                    MessageBox.Show($"Ny profil skapad:\nID: {registerForm.UniqueID}\nRoll: {registerForm.Role}");
                    LoadEmployeeData();
                }
            }
        }

        private void buttonRead_Click(object sender, EventArgs e)
        {
            LoadEmployeeData();
        }

        public void LoadEmployeeData()
        {
            // Get all users from the data source
            var allUsers = adminUser.GetAllEmployees();

            // Remove duplicate rows based on UniqueID
            var uniqueUsers = allUsers.AsEnumerable()
                                       .GroupBy(row => row["UniqueID"])
                                       .Select(group => group.First())
                                       .CopyToDataTable();

            // Set the unique rows as the DataSource
            dataGridView.DataSource = uniqueUsers;

            // Hide the "Id" column
            if (dataGridView.Columns.Contains("Id"))
            {
                dataGridView.Columns["Id"].Visible = false;
            }
        }



        private void buttonFetch_Click(object sender, EventArgs e)
        {
            string uniqueID = textUniqueID.Text;
            if (string.IsNullOrEmpty(uniqueID))
            {
                MessageBox.Show("Ange ett giltigt Unikt ID.");
                return;
            }

            var dataTable = adminUser.GetEmployeeById(uniqueID);
            if (dataTable.Rows.Count > 0)
            {
                string role = dataTable.Rows[0]["Role"].ToString();
                ShowUpdateForm(uniqueID, role);
            }
            else
            {
                MessageBox.Show("Ingen användare hittades med detta ID.");
            }
        }

        private void buttonUpdate_Click(object? sender, EventArgs e)
        {
            buttonFetch_Click(sender, e);
        }

        private void ShowUpdateForm(string uniqueID, string currentRole)
        {
            using (var updateForm = new UpdateForm(uniqueID, currentRole))
            {
                if (updateForm.ShowDialog() == DialogResult.OK)
                {
                    adminUser.UpdateEmployee(uniqueID, updateForm.NewRole);
                    LoadEmployeeData();
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            string uniqueID = textUniqueID.Text;
            if (string.IsNullOrEmpty(uniqueID))
            {
                MessageBox.Show("Ange ett giltigt Unikt ID för att ta bort en anställd.");
                return;
            }

            var confirmResult = MessageBox.Show("Är du säker på att du vill ta bort denna anställd?", "Bekräfta Borttagning", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                adminUser.DeleteEmployee(uniqueID);
                LoadEmployeeData();
            }
        }

        private void buttonShowList_Click(object sender, EventArgs e)
        {
            var employees = adminUser.GetAllEmployeesList();
            var dataTable = new DataTable();
            dataTable.Columns.Add("Employee");

            foreach (var employee in employees)
            {
                dataTable.Rows.Add(employee);
            }

            dataGridView.DataSource = dataTable;
        }

        private void buttonShowDictionary_Click(object sender, EventArgs e)
        {
            var employees = adminUser.GetAllEmployeesDictionary();
            var dataTable = new DataTable();
            dataTable.Columns.Add("UniqueID");
            dataTable.Columns.Add("Role");

            foreach (var kvp in employees)
            {
                dataTable.Rows.Add(kvp.Key, kvp.Value);
            }

            dataGridView.DataSource = dataTable;
        }
        private void buttonQuickFetch_Click(object? sender, EventArgs e)
        {
            string uniqueID = textUniqueID.Text;
            if (string.IsNullOrEmpty(uniqueID))
            {
                MessageBox.Show("Ange ett giltigt Unikt ID.");
                return;
            }

            var dataTable = adminUser.GetEmployeeById(uniqueID);
            if (dataTable.Rows.Count > 0)
            {
                string role = dataTable.Rows[0]["Role"].ToString();
                MessageBox.Show($"Användare hittad:\nID: {uniqueID}\nRoll: {role}");
            }
            else
            {
                MessageBox.Show("Ingen användare hittades med detta ID.");
            }
        }

        public void buttonFetch_Click_Get()
        {
            string uniqueID = textUniqueID.Text;
            if (string.IsNullOrEmpty(uniqueID))
            {
                MessageBox.Show("Ange ett giltigt Unikt ID.");
                return;
            }

            var dataTable = adminUser.GetEmployeeById(uniqueID);
            if (dataTable.Rows.Count > 0)
            {
                string role = dataTable.Rows[0]["Role"].ToString();
                ShowUpdateForm(uniqueID, role);
            }
            else
            {
                MessageBox.Show("Ingen användare hittades med detta ID.");
            }
        }

        public virtual void buttonDelete_Click_Get()
        {
            // string uniqueID = textUniqueID.Text; kanske inte fungerar längre fram med aa
            if (string.IsNullOrEmpty(textUniqueID.Text))
            {
                MessageBox.Show("Ange ett giltigt Unikt ID för att ta bort en anställd.");
                throw new ArgumentNullException();

            }

            var confirmResult = MessageBox.Show("Är du säker på att du vill ta bort denna anställd?", "Bekräfta Borttagning", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {

                if (textUniqueID.Text == "Władysław III")
                {
                    throw new Exception(" Can(t) refactor -> already have IF -> tightly integrated ");
                }

                adminUser.DeleteEmployee(textUniqueID.Text);
                LoadEmployeeData();
            }
        }


        public void buttonShowList_Click_Get()
        {

            var employees = adminUser.GetAllEmployeesList();
            var dataTable = new DataTable();
            dataTable.Columns.Add("Employee");

            foreach (var employee in employees)
            {
                dataTable.Rows.Add(employee);
            }

            dataGridView.DataSource = dataTable;
        }

        public void buttonShowDictionary_Click_get()
        {
            var employees = adminUser.GetAllEmployeesDictionary();
            var dataTable = new DataTable();
            dataTable.Columns.Add("UniqueID");
            dataTable.Columns.Add("Role");

            foreach (var kvp in employees)
            {
                dataTable.Rows.Add(kvp.Key, kvp.Value);
            }

            dataGridView.DataSource = dataTable;
        }



        public void buttonQuickFetch_Click_Get(string uniqueID)
        {

            if (string.IsNullOrEmpty(uniqueID))
            {
                MessageBox.Show("Ange ett giltigt Unikt ID.");
                throw new ArgumentNullException();
            }

            var dataTable = adminUser.GetEmployeeById(uniqueID);
            if (dataTable.Rows.Count > 0)
            {
                string role = dataTable.Rows[0]["Role"].ToString();
                MessageBox.Show($"Användare hittad:\nID: {uniqueID}\nRoll: {role}");
            }
            else
            {
                MessageBox.Show("Ingen användare hittades med detta ID.");
                throw new ObjectNotFoundException();

            }
        }


    }
}
