namespace TcAutomation.Utilities.Constants
{
    /// <summary>
    /// This static class holds the constants needed by the program to work. Also holds some default values of parameteres, which however can be changed by the user in the GUI.
    /// </summary>
    public static class CustomConstants
    {
        public const string productId = "TcXaeShell.DTE.15.0";
        public const int portForAds = 851;

        public const string defaultSolutionPath = "C:\\Appl\\Projects\\TwinCAT\\Test_Counter\\Test_Counter.sln";
        public const string defaultAmsNetId = "5.29.223.252.1.1"; // PLC
        public const string defaultNameOfIntVarToRead = "MAIN.uiCounter";

        public const string nameOfEnableVar = "MAIN.boEnable";
    }
}
