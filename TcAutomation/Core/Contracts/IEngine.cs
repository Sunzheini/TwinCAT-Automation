using TcAutomation.IO.Contracts;


namespace TcAutomation.Core.Contracts
{
    public interface IEngine
    {
        string SolutionPath { get; set; }

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
