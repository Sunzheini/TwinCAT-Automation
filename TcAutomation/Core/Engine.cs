using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcAutomation.Core.Contracts;
using TcAutomation.IO;
using TcAutomation.IO.Contracts;
using TCatSysManagerLib;

namespace TcAutomation.Core
{
    public class Engine : IEngine
    {
        // hardcoded values!!!
        private string _productId = "TcXaeShell.DTE.15.0";
        private string _solutionPath = "C:\\Appl\\Projects\\TwinCAT\\Test_Counter\\Test_Counter.sln"; // my path
                                                                                                      // environment variables in visual studio to fetect,

        // private string test = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        // private string test2 = System.Reflection.Assembly.GetExecutingAssembly().Location; // location of the exe 

        // either config file or inputs from the front end
        private string _defaultAmsNetId = "5.29.223.252.1.1"; // PLC
        private string _defaultNameOfIntVarToRead = "MAIN.uiCounter";
        private string _nameOfEnableVar = "MAIN.boEnable";
        private int _portForAds = 851;

        private IReader reader;
        private IWriter writer;
        private IAutomationInterfaceController automationInterfaceController;
        private IAdsController adsController;

        private string _resultStringFromMethodExecution = string.Empty;

        // __init__ method in python
        // standard guide how to name variables in Festo due to Resharper
        // _name for local variables
        public Engine()
        {
            reader = new Reader();
            writer = new Writer();
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

        public IWriter GetWriter()
        {
            return writer;
        }

        public IReader GetReader()
        {
            return reader;
        }

        // magic string!
        // method1: catch the exception here to reduce the code
        // 2: enumeration
        public string Start()
        {
            // method #1
            _resultStringFromMethodExecution = automationInterfaceController.CreateInstance();
            if (_resultStringFromMethodExecution != "Success") return _resultStringFromMethodExecution;

            // method #2
            _resultStringFromMethodExecution = automationInterfaceController.OpenSolution(_solutionPath);
            if (_resultStringFromMethodExecution != "Success") return _resultStringFromMethodExecution;

            // method #3
            _resultStringFromMethodExecution = automationInterfaceController.CreateITcSysManager();
            if (_resultStringFromMethodExecution != "Success") return _resultStringFromMethodExecution;

            // method #4
            _resultStringFromMethodExecution = adsController.CreateInstance();
            if (_resultStringFromMethodExecution != "Success") return _resultStringFromMethodExecution;

            // method #5
            _resultStringFromMethodExecution = adsController.ClientConnect();
            if (_resultStringFromMethodExecution != "Success") return _resultStringFromMethodExecution;

            // end
            return "Starting Finished";
        }

        public string BuildSolution()
        {
            // method #1
            _resultStringFromMethodExecution = automationInterfaceController.BuildSolution();
            if (_resultStringFromMethodExecution != "Success") return _resultStringFromMethodExecution;

            // end
            return "Build Finished";
        }

        public string SetTargetNetId(string amsNetId)
        {
            // method #1
            _resultStringFromMethodExecution = automationInterfaceController.SetTargetNetId(amsNetId, _defaultAmsNetId);
            if (_resultStringFromMethodExecution != "Success") return _resultStringFromMethodExecution;

            // end
            return "Set TargetNetID Finished";
        }

        public string ActivateConfiguration()
        {
            // method #1
            _resultStringFromMethodExecution = automationInterfaceController.ActivateConfiguration();
            if (_resultStringFromMethodExecution != "Success") return _resultStringFromMethodExecution;

            // end
            return "Configuration Activated";
        }

        public string StartRestartTwinCAT()
        {
            // method #1
            _resultStringFromMethodExecution = automationInterfaceController.StartRestartTwinCAT();
            if (_resultStringFromMethodExecution != "Success") return _resultStringFromMethodExecution;

            // end
            return "TwinCAT (re)started";
        }

        public string ReadFromPlc(string nameOfIntVarToRead)
        {
            // method #1
            _resultStringFromMethodExecution = adsController.AdsReadFromPlc(nameOfIntVarToRead, _defaultNameOfIntVarToRead);
            if (_resultStringFromMethodExecution != "Success") return _resultStringFromMethodExecution;

            // end
            return _resultStringFromMethodExecution;
        }

        public string ToggleEnableDisable()
        {
            // method #1
            _resultStringFromMethodExecution = adsController.ToggleEnable();
            if (_resultStringFromMethodExecution != "Success") return _resultStringFromMethodExecution;

            // end
            return _resultStringFromMethodExecution;
        }

        public string ToggleStartStop()
        {
            // method #1
            _resultStringFromMethodExecution = adsController.ToggleStartStop();
            if (_resultStringFromMethodExecution != "Success") return _resultStringFromMethodExecution;

            // end
            return _resultStringFromMethodExecution;
        }

        public string Exit()
        {
            // method #1
            _resultStringFromMethodExecution = automationInterfaceController.CloseSolution();
            if (_resultStringFromMethodExecution != "Success") return _resultStringFromMethodExecution;

            // method #2
            _resultStringFromMethodExecution = automationInterfaceController.KillInstance();
            if (_resultStringFromMethodExecution != "Success") return _resultStringFromMethodExecution;

            // method #3
            _resultStringFromMethodExecution = adsController.ClientDisconnect();
            if (_resultStringFromMethodExecution != "Success") return _resultStringFromMethodExecution;

            // end
            return "Exited";
        }
    }
}
