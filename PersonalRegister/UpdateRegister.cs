using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Windows.Forms;

namespace PersonalRegister
{
   
        public partial class UpdateForm : Form
        {
            public string NewRole { get; private set; }

            public UpdateForm(string uniqueID, string currentRole)
            {
                InitializeComponent();
                textUniqueID.Text = uniqueID;  // Visa ID som ej redigerbart
                textRole.Text = currentRole;
            }

            private void InitializeComponent()
            {
                this.labelID = new Label();
                this.textUniqueID = new TextBox();
                this.labelRole = new Label();
                this.textRole = new TextBox();
                this.buttonSave = new Button();

                // Label ID
                this.labelID.Text = "Unikt ID:";
                this.labelID.Location = new System.Drawing.Point(10, 10);
                this.labelID.Size = new System.Drawing.Size(100, 20);

                // TextBox ID (ej redigerbart)
                this.textUniqueID.Location = new System.Drawing.Point(120, 10);
                this.textUniqueID.Size = new System.Drawing.Size(150, 20);
                this.textUniqueID.ReadOnly = true;

                // Label Roll
                this.labelRole.Text = "Ny Roll:";
                this.labelRole.Location = new System.Drawing.Point(10, 40);
                this.labelRole.Size = new System.Drawing.Size(100, 20);

                // TextBox Roll
                this.textRole.Location = new System.Drawing.Point(120, 40);
                this.textRole.Size = new System.Drawing.Size(150, 20);

                // Save-knappen
                this.buttonSave.Text = "Spara";
                this.buttonSave.Location = new System.Drawing.Point(120, 70);
                this.buttonSave.Click += new EventHandler(this.buttonSave_Click);

                // UpdateForm
                this.ClientSize = new System.Drawing.Size(300, 120);
                this.Controls.Add(this.labelID);
                this.Controls.Add(this.textUniqueID);
                this.Controls.Add(this.labelRole);
                this.Controls.Add(this.textRole);
                this.Controls.Add(this.buttonSave);
                this.Text = "Uppdatera Användare";
            }

            private void buttonSave_Click(object sender, EventArgs e)
            {
                NewRole = textRole.Text;
                if (string.IsNullOrWhiteSpace(NewRole))
                {
                    MessageBox.Show("Skriv in en ny roll.");
                    return;
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }

            private Label labelID;
            private TextBox textUniqueID;
            private Label labelRole;
            private TextBox textRole;
            private Button buttonSave;
        }
    

}
