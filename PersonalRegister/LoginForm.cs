namespace PersonalRegister
{
    public partial class LoginForm : Form
    {
        private readonly Admin admin = new Admin();
        private readonly Register register = new Register();
        private readonly Receptionist receptionist = new Receptionist();

        public LoginForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Kod som ska k�ras n�r formul�ret laddas
            textUsername.Focus();
        }

        private void labelExit_Click(object sender, EventArgs e)
        {
            // St�ng applikationen n�r "Exit"-labeln klickas
            Application.Exit();
        }


        private void buttonLogin_Click(object sender, EventArgs e)
        {
            IRolePermissions userPermissions = null;

            if (admin.CheckLogin(textUsername.Text, textPassword.Text))
            {
                userPermissions = admin;
            }
            else if (register.CheckLogin(textUsername.Text, textPassword.Text))
            {
                userPermissions = register;
            }
            else if (receptionist.CheckLogin(textUsername.Text, textPassword.Text))
            {
                userPermissions = receptionist;
            }
            else
            {
                MessageBox.Show("Anv�ndarnamnet eller l�senordet �r felaktigt.");
                return;
            }

            var crudForm = new CRUDForm(userPermissions);
            crudForm.Show();
            this.Hide();
        }
    }
}
