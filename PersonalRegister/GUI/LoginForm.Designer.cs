namespace PersonalRegister
{
    partial class LoginForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Title = new Label();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            textUsername = new TextBox();
            textPassword = new TextBox();
            buttonLogin = new Button();
            labelExit = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // Title
            // 
            Title.AutoSize = true;
            Title.Font = new Font("Segoe UI", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Title.Location = new Point(203, 90);
            Title.Margin = new Padding(5, 0, 5, 0);
            Title.Name = "Title";
            Title.Size = new Size(394, 65);
            Title.TabIndex = 0;
            Title.Text = "Personalregister";
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImageLayout = ImageLayout.None;
            pictureBox1.Image = Properties.Resources.userIcon;
            pictureBox1.Location = new Point(248, 188);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Padding = new Padding(2);
            pictureBox1.Size = new Size(35, 35);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.pwIcon;
            pictureBox2.Location = new Point(248, 251);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Padding = new Padding(2);
            pictureBox2.Size = new Size(35, 35);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 3;
            pictureBox2.TabStop = false;
            // 
            // textUsername
            // 
            textUsername.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textUsername.Location = new Point(294, 188);
            textUsername.Name = "textUsername";
            textUsername.Size = new Size(250, 35);
            textUsername.TabIndex = 4;
            // 
            // textPassword
            // 
            textPassword.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            textPassword.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textPassword.Location = new Point(294, 251);
            textPassword.Name = "textPassword";
            textPassword.PasswordChar = '*';
            textPassword.Size = new Size(250, 35);
            textPassword.TabIndex = 5;
            textPassword.UseSystemPasswordChar = true;
            // 
            // buttonLogin
            // 
            buttonLogin.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonLogin.Location = new Point(305, 312);
            buttonLogin.Margin = new Padding(0);
            buttonLogin.Name = "buttonLogin";
            buttonLogin.Size = new Size(190, 40);
            buttonLogin.TabIndex = 6;
            buttonLogin.Text = "Logga In";
            buttonLogin.UseVisualStyleBackColor = true;
            buttonLogin.Click += buttonLogin_Click;
            // 
            // labelExit
            // 
            labelExit.AutoSize = true;
            labelExit.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelExit.Location = new Point(378, 374);
            labelExit.Name = "labelExit";
            labelExit.Size = new Size(44, 25);
            labelExit.TabIndex = 7;
            labelExit.Text = "Exit";
            labelExit.Click += labelExit_Click;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(800, 450);
            Controls.Add(labelExit);
            Controls.Add(buttonLogin);
            Controls.Add(textPassword);
            Controls.Add(textUsername);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(Title);
            FormBorderStyle = FormBorderStyle.None;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Title;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private TextBox textUsername;
        private TextBox textPassword;
        private Button buttonLogin;
        private Label labelExit;
    }
}
