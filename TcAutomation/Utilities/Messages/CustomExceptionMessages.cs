namespace TcAutomation.Utilities.Messages
{
    /// <summary>
    /// This class contains custom exception messages, that are used by the other classes.
    /// </summary>
    public static class CustomExceptionMessages
    {
        // for AutomationInterfaceController
        public const string GetTypeFromProductIdError = "Error in GetTypeFromProgID: {0}";
        public const string CreateInstanceError = "Error while creating instance: {0}";
        public const string WindowOptionsError = "Error in window options: {0}";
        public const string OpenSolutionError = "Error in OpenSolution: {0}";
        public const string CreateITcSysManagerError = "Error in CreateITcSysManager: {0}";
        public const string BuildSOlutionError = "Error in BuildSolution: {0}";
        public const string SetTargetNetIdError = "Error in SetTargetNetId: {0}";
        public const string ActivateConfigurationError = "Error in ActivateConfiguration: {0}";
        public const string StartRestartTwinCATError = "Error in StartRestartTwinCAT: {0}";
        public const string CloseSolutionError = "Error in CloseSolution: {0}";
        public const string KillInstanceError = "Error in KillInstance: {0}";

        // for ADS
        public const string InvalidTargetType = "Invalid target type. Expected {0}.";
        public const string CreatingAdsClientError = "Error while creating AdsClient instance: {0}";
        public const string ConnectingToPlcError = "Error while connecting to PLC: {0}";
        public const string ReadingDeviceInfoError = "Error while reading device info from PLC: {0}";
        public const string ReadingVariableError = "Error while reading variable from PLC: {0}";
        public const string EnablingDisablingError = "Error while enabling / disabling: {0}";
        public const string StartingStoppingError = "Error while starting / stopping: {0}";
        public const string DisconnectingError = "Error while disconnecting from PLC: {0}";
    }
}
