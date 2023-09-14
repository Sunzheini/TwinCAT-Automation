using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcAutomation.IO.Contracts;
using TcAutomation.Utilities.Messages;

namespace TcAutomation.IO
{
    public class Reader : IReader
    {
        public string ReadLine(object textBoxObject)
        {
            if (textBoxObject is TextBox textBox)
            {
                return textBox.Text;
            }
            else
            {
                throw new ArgumentException
                (
                    string.Format
                      (
                          CustomExceptionMessages.InvalidTargetType,
                          nameof(Label)
                      ),
                      nameof(textBoxObject)
                 );
            }
        }
    }
}
