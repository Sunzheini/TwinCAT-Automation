using System;
using TCatSysManagerLib;
using TcAutomation.Core.Contracts;
using TcAutomation.Utilities.Messages;


/// <summary>
/// Used to manipulate the Automation interface of TwinCAT.
/// </summary>
namespace TcAutomation.Core
{
    public class AutomationInterfaceController : IAutomationInterfaceController
    {
        // Needed for the Automation interface
        private string _productId = String.Empty;
        private static Type _typeOfInstance = null;
        private static EnvDTE.DTE _dte = null;
        private static EnvDTE.Solution _solution = null;
        private static EnvDTE.Project _project = null;
        private static ITcSysManager15 _sysManager = null;

        public AutomationInterfaceController(string productId)
        {
            this._productId = productId;
        }

        /// <summary>
        /// Creates an instance of the Automation interface.
        /// </summary>
        /// <returns></returns>
        public string CreateInstance()
        {
            try
            {
                _typeOfInstance = System.Type.GetTypeFromProgID(_productId);
            }
            catch (Exception e)
            {
                return string.Format(CustomExceptionMessages.GetTypeFromProductIdError, e);
            }

            try
            {
                _dte = (EnvDTE.DTE)System.Activator.CreateInstance(_typeOfInstance);
            }
            catch (Exception e)
            {
                return string.Format(CustomExceptionMessages.CreateInstanceError, e);
            }

            try
            {
                _dte.SuppressUI = false;        // when true, the console will not be shown
                _dte.MainWindow.Visible = true; // when false, twincat will not be shown
            }
            catch (Exception e)
            {
                return string.Format(CustomExceptionMessages.WindowOptionsError, e);
            }

            return CustomReturnMessages.returnStringWhenSuccess;
        }

        /// <summary>
        /// Opens a solution in TwinCAT
        /// </summary>
        /// <param name="solutionPath"></param>
        /// <returns></returns>
        public string OpenSolution(string solutionPath)
        {
            try
            {
                _solution = _dte.Solution;
                _solution.Open(@solutionPath);
            }
            catch (Exception e)
            {
                return string.Format(CustomExceptionMessages.OpenSolutionError, e);
            }
            return CustomReturnMessages.returnStringWhenSuccess; ;
        }

        /// <summary>
        /// Creates an instance of the ITcSysManager interface
        /// </summary>
        /// <returns></returns>
        public string CreateITcSysManager()
        {
            try
            {
                _project = _solution.Projects.Item(1);
                _sysManager = (ITcSysManager15)_project.Object;
            }
            catch (Exception e)
            {
                return string.Format(CustomExceptionMessages.CreateITcSysManagerError, e);
            }
            return CustomReturnMessages.returnStringWhenSuccess; ;
        }

        /// <summary>
        /// Builds the solution
        /// </summary>
        /// <returns></returns>
        public string BuildSolution()
        {
            try
            {
                _solution.SolutionBuild.Build(true);
            }
            catch (Exception e)
            {
                return string.Format(CustomExceptionMessages.BuildSOlutionError, e);
            }
            return CustomReturnMessages.returnStringWhenSuccess; ;
        }

        /// <summary>
        /// Sets the target net id
        /// </summary>
        /// <param name="amsNetId"></param>
        /// <param name="defaultAmsNetId"></param>
        /// <returns></returns>
        public string SetTargetNetId(string amsNetId, string defaultAmsNetId)
        {
            try
            {
                _sysManager.SetTargetNetId(amsNetId);
            }
            catch (Exception e)
            {
                return string.Format(CustomExceptionMessages.SetTargetNetIdError, e);
            }
            finally
            {
                _sysManager.SetTargetNetId(defaultAmsNetId);
            }
            return CustomReturnMessages.returnStringWhenSuccess; ;
        }

        /// <summary>
        /// Activates the configuration
        /// </summary>
        /// <returns></returns>
        public string ActivateConfiguration()
        {
            try
            {
                _sysManager.ActivateConfiguration();
            }
            catch (Exception e)
            {
                return string.Format(CustomExceptionMessages.ActivateConfigurationError, e);
            }
            return CustomReturnMessages.returnStringWhenSuccess; ;
        }

        /// <summary>
        /// Starts or restarts TwinCAT
        /// </summary>
        /// <returns></returns>
        public string StartRestartTwinCAT()
        {
            try
            {
                _sysManager.StartRestartTwinCAT();
            }
            catch (Exception e)
            {
                return string.Format(CustomExceptionMessages.StartRestartTwinCATError, e);
            }
            return CustomReturnMessages.returnStringWhenSuccess; ;
        }

        /// <summary>
        /// Closes the solution
        /// </summary>
        /// <returns></returns>
        public string CloseSolution()
        {
            try
            {
                _solution.SaveAs(_solution.FullName);
                _solution.Close();
            }
            catch (Exception e)
            {
                return string.Format(CustomExceptionMessages.CloseSolutionError, e);
            }
            return CustomReturnMessages.returnStringWhenSuccess; ;
        }

        /// <summary>
        /// Kills the instance of TwinCAT
        /// </summary>
        /// <returns></returns>
        public string KillInstance()
        {
            try
            {
                _dte.Quit();
            }
            catch (Exception e)
            {
                return string.Format(CustomExceptionMessages.KillInstanceError, e);
            }
            return CustomReturnMessages.returnStringWhenSuccess; ;
        }
    }
}
