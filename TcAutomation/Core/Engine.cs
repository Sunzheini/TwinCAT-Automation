using TcAutomation.Core.Contracts;
using TcAutomation.IO;
using TcAutomation.IO.Contracts;
using TcAutomation.Utilities.Constants;
using TcAutomation.Utilities.Messages;


/// <summary>
/// This is the backend engine of the solution. It contains all the logic and methods that are needed to manipulate the Automation interface and the ADS library.
/// Combines 1 or more methods by the Automation interface and the ADS library to a single method and exposes it for the front end.
/// </summary>
namespace TcAutomation.Core
{
    public class Engine : IEngine
    {
        // this is needed for correct work of the program and is not a subject of change
        private string _productId = CustomConstants.productId;
        private int _portForAds = CustomConstants.portForAds;

        //these are default values, however they can be changed by the user in the GUI
        private string _defaultSolutionPath = CustomConstants.defaultSolutionPath;
        private string _defaultAmsNetId = CustomConstants.defaultAmsNetId;
        private string _defaultNameOfIntVarToRead = CustomConstants.defaultNameOfIntVarToRead;

        //these is hardcoded inside the program of the PLC and is not a subject of change
        private string _nameOfEnableVar = CustomConstants.nameOfEnableVar;

        // instances of the classes that are needed to: Read, Write and manipulate the Automation interface and the ADS library
        private IReader reader;
        private IWriter writer;
        private IAutomationInterfaceController automationInterfaceController;
        private IAdsController adsController;

        // string for the result of the method execution, which is updated after each method execution
        private string _resultStringFromMethodExecution = string.Empty;

        public Engine()
        {
            reader = new Reader();
            writer = new Writer();

            // Initialize the controller objects and pass the needed parameters
            automationInterfaceController = new AutomationInterfaceController
            (
                _productId
            );
            adsController = new AdsController
            (
                _defaultAmsNetId,
                _portForAds,
                _nameOfEnableVar
            );
        }

        /// <summary>
        /// Sets the path of the solution file.
        /// </summary>
        public string SolutionPath
        {
            get { return _defaultSolutionPath; }
            set { _defaultSolutionPath = value; }
        }

        /// <summary>
        /// Returns the instance of the writer.
        /// </summary>
        /// <returns></returns>
        public IWriter GetWriter()
        {
            return writer;
        }

        /// <summary>
        /// Returns the instance of the reader.
        /// </summary>
        /// <returns></returns>
        public IReader GetReader()
        {
            return reader;
        }

        /// <summary>
        /// Executes all methods in the correct order which are bound to the start button. Starts TwinCAT, opens a solution, initiates ads client.
        /// </summary>
        /// <returns></returns>
        public string Start()
        {
            // method #1
            _resultStringFromMethodExecution = automationInterfaceController.CreateInstance();
            if (_resultStringFromMethodExecution != CustomReturnMessages.returnStringWhenSuccess) return _resultStringFromMethodExecution;

            // method #2
            _resultStringFromMethodExecution = automationInterfaceController.OpenSolution(_defaultSolutionPath);
            if (_resultStringFromMethodExecution != CustomReturnMessages.returnStringWhenSuccess) return _resultStringFromMethodExecution;

            // method #3
            _resultStringFromMethodExecution = automationInterfaceController.CreateITcSysManager();
            if (_resultStringFromMethodExecution != CustomReturnMessages.returnStringWhenSuccess) return _resultStringFromMethodExecution;

            // method #4
            _resultStringFromMethodExecution = adsController.CreateInstance();
            if (_resultStringFromMethodExecution != CustomReturnMessages.returnStringWhenSuccess) return _resultStringFromMethodExecution;

            // method #5
            _resultStringFromMethodExecution = adsController.ClientConnect();
            if (_resultStringFromMethodExecution != CustomReturnMessages.returnStringWhenSuccess) return _resultStringFromMethodExecution;

            // end
            return CustomReturnMessages.returnStringStart;
        }

        /// <summary>
        /// Executes all methods in the correct order which are bound to the build button. Currently only builds the solution.
        /// </summary>
        /// <returns></returns>
        public string BuildSolution()
        {
            // method #1
            _resultStringFromMethodExecution = automationInterfaceController.BuildSolution();
            if (_resultStringFromMethodExecution != CustomReturnMessages.returnStringWhenSuccess) return _resultStringFromMethodExecution;

            // end
            return CustomReturnMessages.returnStringBuild;
        }

        /// <summary>
        /// Executes all methods in the correct order which are bound to the set target net id button. Currently only sets the target net id.
        /// </summary>
        /// <param name="amsNetId"></param>
        /// <returns></returns>
        public string SetTargetNetId(string amsNetId)
        {
            // method #1
            _resultStringFromMethodExecution = automationInterfaceController.SetTargetNetId(amsNetId, _defaultAmsNetId);
            if (_resultStringFromMethodExecution != CustomReturnMessages.returnStringWhenSuccess) return _resultStringFromMethodExecution;

            // end
            return CustomReturnMessages.returnStringSetTargetNetId;
        }

        /// <summary>
        /// Executes all methods in the correct order which are bound to the activate configuration button. Currently only activates the configuration.
        /// </summary>
        /// <returns></returns>
        public string ActivateConfiguration()
        {
            // method #1
            _resultStringFromMethodExecution = automationInterfaceController.ActivateConfiguration();
            if (_resultStringFromMethodExecution != CustomReturnMessages.returnStringWhenSuccess) return _resultStringFromMethodExecution;

            // end
            return CustomReturnMessages.returnStringActivateConfiguration;
        }

        /// <summary>
        /// Executes all methods in the correct order which are bound to the start/restart TwinCAT button. Currently only starts/restarts TwinCAT.
        /// </summary>
        /// <returns></returns>
        public string StartRestartTwinCAT()
        {
            // method #1
            _resultStringFromMethodExecution = automationInterfaceController.StartRestartTwinCAT();
            if (_resultStringFromMethodExecution != CustomReturnMessages.returnStringWhenSuccess) return _resultStringFromMethodExecution;

            // end
            return CustomReturnMessages.returnStringStartRestartTwinCAT;
        }

        /// <summary>
        /// Executes all methods in the correct order which are bound to the read from plc button. Currently only reads the value of the int variable.
        /// </summary>
        /// <param name="nameOfIntVarToRead"></param>
        /// <returns></returns>
        public string ReadFromPlc(string nameOfIntVarToRead)
        {
            // method #1
            _resultStringFromMethodExecution = adsController.AdsReadFromPlc(nameOfIntVarToRead, _defaultNameOfIntVarToRead);
            if (_resultStringFromMethodExecution != CustomReturnMessages.returnStringWhenSuccess) return _resultStringFromMethodExecution;

            // end
            return _resultStringFromMethodExecution;
        }

        /// <summary>
        /// Executes all methods in the correct order which are bound to the enable/disable button. Currently only toggles the enable/disable variable, which is inside the test plc program.
        /// </summary>
        /// <returns></returns>
        public string ToggleEnableDisable()
        {
            // method #1
            _resultStringFromMethodExecution = adsController.ToggleEnable();
            if (_resultStringFromMethodExecution != CustomReturnMessages.returnStringWhenSuccess) return _resultStringFromMethodExecution;

            // end
            return _resultStringFromMethodExecution;
        }

        /// <summary>
        /// Executes all methods in the correct order which are bound to the start/stop plc button. Currently only toggles the start/stop variable, which is inside the test plc program.
        /// </summary>
        /// <returns></returns>
        public string ToggleStartStop()
        {
            // method #1
            _resultStringFromMethodExecution = adsController.ToggleStartStop();
            if (_resultStringFromMethodExecution != CustomReturnMessages.returnStringWhenSuccess) return _resultStringFromMethodExecution;

            // end
            return _resultStringFromMethodExecution;
        }

        /// <summary>
        /// Executes all methods in the correct order which are bound to the exit button. Closes the solution, kills the instance and disconnects the client.
        /// </summary>
        /// <returns></returns>
        public string Exit()
        {
            // method #1
            _resultStringFromMethodExecution = automationInterfaceController.CloseSolution();
            if (_resultStringFromMethodExecution != CustomReturnMessages.returnStringWhenSuccess) return _resultStringFromMethodExecution;

            // method #2
            _resultStringFromMethodExecution = automationInterfaceController.KillInstance();
            if (_resultStringFromMethodExecution != CustomReturnMessages.returnStringWhenSuccess) return _resultStringFromMethodExecution;

            // method #3
            _resultStringFromMethodExecution = adsController.ClientDisconnect();
            if (_resultStringFromMethodExecution != CustomReturnMessages.returnStringWhenSuccess) return _resultStringFromMethodExecution;

            // end
            return CustomReturnMessages.returnStringExit;
        }
    }
}
