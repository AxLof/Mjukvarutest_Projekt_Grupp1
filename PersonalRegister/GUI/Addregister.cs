using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace PersonalRegister.GUI
{
    public partial class RegisterForm : Form
    {
        public string UniqueID { get; private set; } = string.Empty;
        public string Role { get; private set; } = string.Empty;

        private Label labelID = null!;
        private TextBox textID = null!;
        private Label labelRole = null!;
        private TextBox textRole = null!;
        private Button buttonSave = null!;

        public RegisterForm()
        {
            InitializeComponent();
            GenerateUniqueID();
        }

        private void InitializeComponent()
        {
            labelID = new Label();
            textID = new TextBox();
            labelRole = new Label();
            textRole = new TextBox();
            buttonSave = new Button();

            // labelID
            labelID.Text = "Unik ID:";
            labelID.Location = new Point(10, 10);
            labelID.Size = new Size(100, 20);

            // textID
            textID.Location = new Point(120, 10);
            textID.Size = new Size(100, 20);
            textID.ReadOnly = true;

            // labelRole
            labelRole.Text = "Roll:";
            labelRole.Location = new Point(10, 40);
            labelRole.Size = new Size(100, 20);

            // textRole
            textRole.Location = new Point(120, 40);
            textRole.Size = new Size(100, 20);

            // buttonSave
            buttonSave.Text = "Spara";
            buttonSave.Location = new Point(120, 70);
            buttonSave.Click += new EventHandler(this.buttonSave_Click!);

            // RegisterForm
            ClientSize = new Size(250, 120);
            Controls.Add(labelID);
            Controls.Add(textID);
            Controls.Add(labelRole);
            Controls.Add(textRole);
            Controls.Add(buttonSave);
            Text = "Registrera ny användare";
        }

        private void GenerateUniqueID()
        {
            Random rnd = new Random();
            int number = rnd.Next(1000, 9999); // Fyrsiffrigt nummer
            char letter = (char)rnd.Next('A', 'Z'); // Slumpmässig bokstav

            UniqueID = $"{number}{letter}";
            textID.Text = UniqueID; // Visa det genererade ID:t i textID
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Role = textRole.Text;

            if (string.IsNullOrWhiteSpace(Role))
            {
                MessageBox.Show("Skriv in din roll");
                return;
            }

            buttonSave.Enabled = false; // Inaktivera knappen för att förhindra dubbletter
            SaveEmployeeToDatabase(UniqueID, Role);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void SaveEmployeeToDatabase(string uniqueID, string role)
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string insertQuery = "INSERT INTO Users (UniqueID, Role) VALUES (@UniqueID, @Role)";
                    using (var command = new SQLiteCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@UniqueID", uniqueID);
                        command.Parameters.AddWithValue("@Role", role);
                        command.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Anställd har lagts till :) ");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fel vid sparande av anställd: {ex.Message}");
            }
        }
    }
}
