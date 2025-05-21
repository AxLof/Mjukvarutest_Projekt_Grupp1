using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Windows.Forms;

namespace PersonalRegister.GUI
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
                labelID = new Label();
                textUniqueID = new TextBox();
                labelRole = new Label();
                textRole = new TextBox();
                buttonSave = new Button();

                // Label ID
                labelID.Text = "Unikt ID:";
                labelID.Location = new Point(10, 10);
                labelID.Size = new Size(100, 20);

                // TextBox ID (ej redigerbart)
                textUniqueID.Location = new Point(120, 10);
                textUniqueID.Size = new Size(150, 20);
                textUniqueID.ReadOnly = true;

                // Label Roll
                labelRole.Text = "Ny Roll:";
                labelRole.Location = new Point(10, 40);
                labelRole.Size = new Size(100, 20);

                // TextBox Roll
                textRole.Location = new Point(120, 40);
                textRole.Size = new Size(150, 20);

                // Save-knappen
                buttonSave.Text = "Spara";
                buttonSave.Location = new Point(120, 70);
                buttonSave.Click += new EventHandler(buttonSave_Click);

                // UpdateForm
                ClientSize = new Size(300, 120);
                Controls.Add(labelID);
                Controls.Add(textUniqueID);
                Controls.Add(labelRole);
                Controls.Add(textRole);
                Controls.Add(buttonSave);
                Text = "Uppdatera Användare";
            }

            private void buttonSave_Click(object sender, EventArgs e)
            {
                NewRole = textRole.Text;
                if (string.IsNullOrWhiteSpace(NewRole))
                {
                    MessageBox.Show("Skriv in en ny roll.");
                    return;
                }

                DialogResult = DialogResult.OK;
                Close();
            }

            private Label labelID;
            private TextBox textUniqueID;
            private Label labelRole;
            private TextBox textRole;
            private Button buttonSave;
        }
    

}
