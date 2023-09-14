using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcAutomation.IO.Contracts;

namespace TcAutomation.Core.Contracts
{
    public interface IEngine
    {
        IWriter GetWriter();

        IReader GetReader();

        string Start();

        string BuildSolution();

        string SetTargetNetId(string amsNetId);

        string ActivateConfiguration();

        string StartRestartTwinCAT();

        string ReadFromPlc(string nameOfIntVarToRead);

        string ToggleEnableDisable();

        string ToggleStartStop();

        string Exit();
    }
}
