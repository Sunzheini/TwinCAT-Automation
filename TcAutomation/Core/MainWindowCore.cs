using TcAutomation.Core.Contracts;
using TcAutomation.IO.Contracts;


namespace TcAutomation.Core
{
    /// <summary>
    /// This class contains the core functionality of using the backend engine.
    /// </summary>
    public class MainWindowCore
    {
        // initialize the objects here
        public IEngine engine;  // the backend engine
        public IWriter writer;  // the writer class
        public IReader reader;  // the reader class

        // strings to be used below
        private string _resultStringFromEngine = string.Empty;
        private string _inputFromTextBox = string.Empty;

        public MainWindowCore()
        {
            // Initialize the backend engine
            engine = new Engine();

            // retrieve the instances of the reader and the writer from the engine
            this.writer = engine.GetWriter();
            this.reader = engine.GetReader();
        }

        /// <summary>
        /// Receive the selected file path, sends it to the engine
        /// </summary>
        /// <param name="input"></param>
        /// <returns>
        /// The selected file path
        /// </returns>
        public string SelectFilePath(string input)
        {
            string selectedFilePath = input;
            engine.SolutionPath = selectedFilePath; // Set the selected solution file path.
            return selectedFilePath;
        }

        /// <summary>
        /// Interracts with the engine to start the TwinCAT XAE.
        /// </summary>
        /// <returns>
        /// A string containing the result of the operation.
        /// </returns>
        public string Start()
        {
            _resultStringFromEngine = this.engine.Start();
            return _resultStringFromEngine;
        }

        /// <summary>
        /// Interracts with the engine to build the solution.
        /// </summary>
        /// <returns>
        /// A string containing the result of the operation.
        /// </returns>
        public string BuildSolution()
        {
            _resultStringFromEngine = this.engine.BuildSolution();
            return _resultStringFromEngine;
        }

        /// <summary>
        /// Interracts with the engine to set the target NetId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>
        /// A string containing the result of the operation.
        /// </returns>
        public string SetTargetNetId(string input)
        {
            _inputFromTextBox = input;
            _resultStringFromEngine = this.engine.SetTargetNetId(_inputFromTextBox);
            return _resultStringFromEngine;
        }

        /// <summary>
        /// Interracts with the engine to activate the configuration.
        /// </summary>
        /// <returns>
        /// A string containing the result of the operation.
        /// </returns>
        public string ActivateConfiguration()
        {
            _resultStringFromEngine = this.engine.ActivateConfiguration();
            return _resultStringFromEngine;
        }

        /// <summary>
        /// Interracts with the engine to start or restart the TwinCAT.
        /// </summary>
        /// <returns>
        /// A string containing the result of the operation.
        /// </returns>
        public string StartRestartTwinCAT()
        {
            _resultStringFromEngine = this.engine.StartRestartTwinCAT();
            return _resultStringFromEngine;
        }

        /// <summary>
        /// Interracts with the engine to read from the PLC.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>
        /// A string containing the result of the operation.
        /// </returns>
        public string ReadFromPlc(string input)
        {
            _inputFromTextBox = input;
            _resultStringFromEngine = this.engine.ReadFromPlc(_inputFromTextBox);
            return _resultStringFromEngine;
        }

        /// <summary>
        /// Interracts with the engine to toggle the start/stop of the PLC.
        /// </summary>
        /// <returns>
        /// A string containing the result of the operation.
        /// </returns>
        public string ToggleStartStop()
        {
            _resultStringFromEngine = this.engine.ToggleStartStop();
            return _resultStringFromEngine;
        }

        /// <summary>
        /// Interracts with the engine to toggle the enable/disable of the PLC.
        /// </summary>
        /// <returns>
        /// A string containing the result of the operation.
        /// </returns>
        public string ToggleEnableDisable()
        {
            _resultStringFromEngine = this.engine.ToggleEnableDisable();
            return _resultStringFromEngine;
        }

        /// <summary>
        /// Interracts with the engine to exit the TwinCAT XAE.
        /// </summary>
        /// <returns>
        /// A string containing the result of the operation.
        /// </returns>
        public string Exit()
        {
            _resultStringFromEngine = this.engine.Exit();
            return _resultStringFromEngine;
        }
    }
}
