using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace PersonalRegister
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
            this.labelID = new Label();
            this.textID = new TextBox();
            this.labelRole = new Label();
            this.textRole = new TextBox();
            this.buttonSave = new Button();

            // labelID
            this.labelID.Text = "Unik ID:";
            this.labelID.Location = new System.Drawing.Point(10, 10);
            this.labelID.Size = new System.Drawing.Size(100, 20);

            // textID
            this.textID.Location = new System.Drawing.Point(120, 10);
            this.textID.Size = new System.Drawing.Size(100, 20);
            this.textID.ReadOnly = true;

            // labelRole
            this.labelRole.Text = "Roll:";
            this.labelRole.Location = new System.Drawing.Point(10, 40);
            this.labelRole.Size = new System.Drawing.Size(100, 20);

            // textRole
            this.textRole.Location = new System.Drawing.Point(120, 40);
            this.textRole.Size = new System.Drawing.Size(100, 20);

            // buttonSave
            this.buttonSave.Text = "Spara";
            this.buttonSave.Location = new System.Drawing.Point(120, 70);
            this.buttonSave.Click += new EventHandler(this.buttonSave_Click!);

            // RegisterForm
            this.ClientSize = new System.Drawing.Size(250, 120);
            this.Controls.Add(this.labelID);
            this.Controls.Add(this.textID);
            this.Controls.Add(this.labelRole);
            this.Controls.Add(this.textRole);
            this.Controls.Add(this.buttonSave);
            this.Text = "Registrera ny användare";
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

            this.DialogResult = DialogResult.OK;
            this.Close();
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
