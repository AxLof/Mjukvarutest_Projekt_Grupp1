using PersonalRegister.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalRegister
{
    public class Admin : IRolePermissions
    {
        private readonly string username = "admin";
        private readonly string password = "password";

        public bool CanAddEmployee => true;
        public bool CanUpdateEmployee => true;
        public bool CanDeleteEmployee => true;
        public bool CanViewEmployee => true;

        public string getUser() {  return username; }
        public string getPassword() { return password; }

        public bool CheckLogin(string uName, string pw) => uName == username && pw == password;
    }


    public class Register : IRolePermissions
    {
        private readonly string username = "register";
        private readonly string password = "password";

        public bool CanAddEmployee => true;
        public bool CanUpdateEmployee => true;
        public bool CanDeleteEmployee => true;
        public bool CanViewEmployee => true;

        public string getUser() { return username; }
        public string getPassword() { return password; }

        public bool CheckLogin(string uName, string pw) => uName == username && pw == password;
    }

    public class Receptionist : IRolePermissions
    {
        private readonly string username = "receptionist";
        private readonly string password = "password";

        public bool CanAddEmployee => true;
        public bool CanUpdateEmployee => false;
        public bool CanDeleteEmployee => false;
        public bool CanViewEmployee => true;

        public string getUser() { return username; }
        public string getPassword() { return password; }

        public bool CheckLogin(string uName, string pw) => uName == username && pw == password;
    }
}
