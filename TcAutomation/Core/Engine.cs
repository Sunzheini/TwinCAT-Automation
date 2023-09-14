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
        private string productId = "TcXaeShell.DTE.15.0";
        private string solutionPath = "C:\\Appl\\Projects\\TwinCAT\\Test_Counter\\Test_Counter.sln";                                       // my path
        private string defaultAmsNetId = "5.29.223.252.1.1"; // PLC
        private string defaultNameOfIntVarToRead = "MAIN.iCounter";
        private string nameOfEnableVar = "MAIN.boEnable";
        private int portForAds = 851;

        private IReader reader;
        private IWriter writer;
        private IAutomationInterfaceController automationInterfaceController;
        private IAdsController adsController;

        private string resultStringFromMethodExecution = string.Empty;

        public Engine()
        {
            reader = new Reader();
            writer = new Writer();
            automationInterfaceController = new AutomationInterfaceController
            (
                productId
            );
            adsController = new AdsController
            (
                defaultAmsNetId,
                portForAds,
                nameOfEnableVar
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

        public string Start()
        {
            // method #1
            resultStringFromMethodExecution = automationInterfaceController.CreateInstance();
            if (resultStringFromMethodExecution != "Success") return resultStringFromMethodExecution;
            
            // method #2
            resultStringFromMethodExecution = automationInterfaceController.OpenSolution(solutionPath);
            if (resultStringFromMethodExecution != "Success") return resultStringFromMethodExecution;

            // method #3
            resultStringFromMethodExecution = automationInterfaceController.CreateITcSysManager();
            if (resultStringFromMethodExecution != "Success") return resultStringFromMethodExecution;

            // method #4
            resultStringFromMethodExecution = adsController.CreateInstance();
            if (resultStringFromMethodExecution != "Success") return resultStringFromMethodExecution;

            // method #5
            resultStringFromMethodExecution = adsController.ClientConnect();
            if (resultStringFromMethodExecution != "Success") return resultStringFromMethodExecution;

            // end
            return resultStringFromMethodExecution;
        }

        public string BuildSolution()
        {
            // method #1
            resultStringFromMethodExecution = automationInterfaceController.BuildSolution();
            if (resultStringFromMethodExecution != "Success") return resultStringFromMethodExecution;

            // end
            return resultStringFromMethodExecution;
        }

        public string SetTargetNetId(string amsNetId)
        {
            // method #1
            resultStringFromMethodExecution = automationInterfaceController.SetTargetNetId(amsNetId, defaultAmsNetId);
            if (resultStringFromMethodExecution != "Success") return resultStringFromMethodExecution;

            // end
            return resultStringFromMethodExecution;
        }

        public string ActivateConfiguration()
        {
            // method #1
            resultStringFromMethodExecution = automationInterfaceController.ActivateConfiguration();
            if (resultStringFromMethodExecution != "Success") return resultStringFromMethodExecution;

            // end
            return resultStringFromMethodExecution;
        }

        public string StartRestartTwinCAT()
        {
            // method #1
            resultStringFromMethodExecution = automationInterfaceController.StartRestartTwinCAT();
            if (resultStringFromMethodExecution != "Success") return resultStringFromMethodExecution;

            // end
            return resultStringFromMethodExecution;
        }

        public string ReadFromPlc(string nameOfIntVarToRead)
        {
            // method #1
            resultStringFromMethodExecution = adsController.AdsReadFromPlc(nameOfIntVarToRead, defaultNameOfIntVarToRead);
            if (resultStringFromMethodExecution != "Success") return resultStringFromMethodExecution;

            // end
            return resultStringFromMethodExecution;
        }

        public string ToggleEnableDisable()
        {
            // method #1
            resultStringFromMethodExecution = adsController.ToggleEnable();
            if (resultStringFromMethodExecution != "Success") return resultStringFromMethodExecution;

            // end
            return resultStringFromMethodExecution;
        }

        public string ToggleStartStop()
        {
            // method #1
            resultStringFromMethodExecution = adsController.ToggleStartStop();
            if (resultStringFromMethodExecution != "Success") return resultStringFromMethodExecution;

            // end
            return resultStringFromMethodExecution;
        }

        public string Exit()
        {
            // method #1
            resultStringFromMethodExecution = automationInterfaceController.CloseSolution();
            if (resultStringFromMethodExecution != "Success") return resultStringFromMethodExecution;

            // method #2
            resultStringFromMethodExecution = automationInterfaceController.KillInstance();
            if (resultStringFromMethodExecution != "Success") return resultStringFromMethodExecution;

            // method #3
            resultStringFromMethodExecution = adsController.ClientDisconnect();
            if (resultStringFromMethodExecution != "Success") return resultStringFromMethodExecution;

            // end
            return resultStringFromMethodExecution;
        }
    }
}
