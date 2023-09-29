using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TcAutomation.IO.Contracts
{
    public interface IReader
    {
        //string ReadLine(object fromWhereToRead);
        string ReadLine(TextBox fromWhereToRead);
    }
}
