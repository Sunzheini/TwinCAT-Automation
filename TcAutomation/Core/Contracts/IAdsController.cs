namespace TcAutomation.Core.Contracts
{
    public interface IAdsController
    {
        string CreateInstance();

        string ClientConnect();

        string AdsReadFromPlc(string nameOfIntVarToRead, string defaultNameOfIntVarToRead);

        string ToggleEnable();

        string ToggleStartStop();

        string ClientDisconnect();
    }
}
