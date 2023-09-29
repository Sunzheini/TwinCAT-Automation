using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TcAutomation.IO.Contracts;
using TcAutomation.Utilities.Messages;

namespace TcAutomation.IO
{
    public class Reader : IReader
    {
        public string ReadLine(TextBox textBoxObject)
        {
            return textBoxObject.Text;
        }
    }
}
