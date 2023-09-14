using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
