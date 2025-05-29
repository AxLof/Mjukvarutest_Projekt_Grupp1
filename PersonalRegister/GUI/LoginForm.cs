using PersonalRegister.GUI;
using PersonalRegister.Interfaces;

namespace PersonalRegister
{
    public partial class LoginForm : Form
    {
 
        public IRolePermissions userPermissions;
        public int test = 0;
        Admin admin = new Admin();
        Register register = new Register();
        Receptionist receptionist = new Receptionist();
        public bool passedTest = false;
        public LoginForm()
        {
            InitializeComponent();
        }

        public LoginForm(IRolePermissions IRolePermissionss) //for test
        {
            InitializeComponent();
            userPermissions = IRolePermissionss;

            textUsername.Text = userPermissions.getUser();
            textPassword.Text = userPermissions.getPassword();

            if (userPermissions is Admin || textUsername.Text == "password")
            {
                test = 1;
            }
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            // Kod som ska köras när formuläret laddas
            textUsername.Focus();
        }

        public void labelExit_Click(object sender, EventArgs e)
        {
            // Stäng applikationen när "Exit"-labeln klickas
            Application.Exit();
        }

        public void buttonLogin_Click(object sender, EventArgs e)
        {

            buttonLogin_Click_Interface();
        }

        // create interface between b
        public void buttonLogin_Click_Interface()
        {


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

            else if  (userPermissions is Admin || userPermissions is Receptionist|| userPermissions is Register)
            {
                passedTest = true;
                return;
            }
            else
            {
                
                
                MessageBox.Show("Användarnamnet eller lösenordet är felaktigt.");
                return;
            }
          

            var crudForm = new CRUDForm(userPermissions);
            crudForm.Show();
            this.Hide();
        }

        public void startCrud() { }

        //test help

        }
    }//test end help

