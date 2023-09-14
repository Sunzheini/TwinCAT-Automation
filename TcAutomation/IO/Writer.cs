using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcAutomation.IO.Contracts;
using TcAutomation.Utilities.Messages;

namespace TcAutomation.IO
{
    public class Writer : IWriter
    {
        private readonly Label outputLabel;

        public void Write(object labelObject, string message)
        {
            if (labelObject is Label label)
            {
                label.Text = message;
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
                      nameof(labelObject)
                 );
            }
        }

        public void WriteLine(object labelObject, string message)
        {
            if (labelObject is Label label)
            {
                // If you want to append a new line character after writing
                label.Text += message + Environment.NewLine;
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
                      nameof(labelObject)
                 );
            }
        }
    }
}
