using System.Windows.Controls;


namespace TcAutomation.IO.Contracts
{
    public interface IWriter
    {
        void Write(TextBlock whereToWrite, string message);

        void WriteLine(TextBlock whereToWrite, string message);
    }
}
