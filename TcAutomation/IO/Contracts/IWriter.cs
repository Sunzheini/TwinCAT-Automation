using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcAutomation.IO.Contracts
{
    public interface IWriter
    {
        void Write(object whereToWrite, string message);

        void WriteLine(object whereToWrite, string message);
    }
}
