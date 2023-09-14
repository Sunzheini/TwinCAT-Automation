using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCatSysManagerLib;

namespace TcAutomation.Core.Contracts
{
    public interface IAutomationInterfaceController
    {
        string CreateInstance();

        string OpenSolution(string solutionPath);

        string CreateITcSysManager();

        string BuildSolution();

        string SetTargetNetId(string amsNetId, string defaultAmsNetId);

        string ActivateConfiguration();

        string StartRestartTwinCAT();

        string CloseSolution();

        string KillInstance();
    }
}
