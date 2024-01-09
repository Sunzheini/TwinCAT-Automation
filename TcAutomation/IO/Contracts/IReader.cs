using System.Windows.Controls;


namespace TcAutomation.IO.Contracts
{
    public interface IReader
    {
        //string ReadLine(object fromWhereToRead);
        string ReadLine(TextBox fromWhereToRead);
    }
}
