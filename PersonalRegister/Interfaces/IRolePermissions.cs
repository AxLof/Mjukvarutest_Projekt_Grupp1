using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalRegister.Interfaces
{
    public interface IRolePermissions
    {
        bool CanAddEmployee { get; }
        bool CanUpdateEmployee { get; }
        bool CanDeleteEmployee { get; }
        bool CanViewEmployee { get; }
    }
}
