using System.Windows.Controls;
using TcAutomation.IO.Contracts;


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
