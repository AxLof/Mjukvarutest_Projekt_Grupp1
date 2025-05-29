using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PersonalRegister.Interfaces
{
    public interface IRolePermissions
    {
        bool CanAddEmployee { get; }
        bool CanUpdateEmployee { get; }
        bool CanDeleteEmployee { get; }
        bool CanViewEmployee { get; }
        bool CheckLogin(string uName, string pw);


        public string getUser();
        public string getPassword();

    }
}
