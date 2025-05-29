using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalRegister.Interfaces
{
    internal interface ISQLiteConnection
    {
        public void open();
        public void close();
        
    }
}
