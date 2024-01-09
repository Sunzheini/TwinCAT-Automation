using System;
using System.Windows.Controls;
using TcAutomation.IO.Contracts;


namespace TcAutomation.IO
{
    public class Writer : IWriter
    {
        public void Write(TextBlock labelObject, string message)
        {
            labelObject.Text = message;
        }

        public void WriteLine(TextBlock labelObject, string message)
        {
            labelObject.Text += message + Environment.NewLine;
        }
    }
}
