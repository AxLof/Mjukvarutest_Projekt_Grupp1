using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalRegister.Interfaces
{
    public interface IStreamReaderHelper
    {
        TextReader Create(string path);
    }
}
