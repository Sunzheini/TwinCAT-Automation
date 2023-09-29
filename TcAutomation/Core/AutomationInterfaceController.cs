using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCatSysManagerLib;
using TcAutomation.Core.Contracts;
using TcAutomation.IO.Contracts;

namespace TcAutomation.Core
{
    public class AutomationInterfaceController : IAutomationInterfaceController
    {
        private string _productId = String.Empty;
        private static Type _typeOfInstance = null;
        private static EnvDTE.DTE _dte = null;
        private static EnvDTE.Solution _solution = null;
        private static EnvDTE.Project _project = null;
        private static ITcSysManager15 _sysManager = null;

        private string _returnStringWhenSuccess = "Success";

        public AutomationInterfaceController(string productId)
        {
            this._productId = productId;
        }

        // catch exception in Engine.cs, check if exception is the same!
        public string CreateInstance()
        {
            try
            {
                _typeOfInstance = System.Type.GetTypeFromProgID(_productId);
            }
            catch (Exception e)
            {
                return $"Error in GetTypeFromProgID: {e}";
            }

            try
            {
                _dte = (EnvDTE.DTE)System.Activator.CreateInstance(_typeOfInstance);
            }
            catch (Exception e)
            {
                return $"Error while creating instance: {e}";
            }

            try
            {
                _dte.SuppressUI = false;        // when true, the console will not be shown
                _dte.MainWindow.Visible = true; // when false, twincat will not be shown
            }
            catch (Exception e)
            {
                return $"Error in window options: {e}";
            }

            return _returnStringWhenSuccess;
        }

        public string OpenSolution(string solutionPath)
        {
            try
            {
                _solution = _dte.Solution;
                _solution.Open(@solutionPath);
            }
            catch (Exception e)
            {
                return $"Error in OpenSolution: {e}";
            }
            return _returnStringWhenSuccess;
        }

        public string CreateITcSysManager()
        {
            try
            {
                _project = _solution.Projects.Item(1);
                _sysManager = (ITcSysManager15)_project.Object;
            }
            catch (Exception e)
            {
                return $"Error in CreateITcSysManager: {e}";
            }
            return _returnStringWhenSuccess;
        }

        public string BuildSolution()
        {
            try
            {
                _solution.SolutionBuild.Build(true);
            }
            catch (Exception e)
            {
                return $"Error in BuildSolution: {e}";
            }
            return _returnStringWhenSuccess;
        }

        public string SetTargetNetId(string amsNetId, string defaultAmsNetId)
        {
            try
            {
                _sysManager.SetTargetNetId(amsNetId);
            }
            catch (Exception e)
            {
                return $"Error in SetTargetNetId: {e}";
            }
            finally
            {
                _sysManager.SetTargetNetId(defaultAmsNetId);
            }
            return _returnStringWhenSuccess;
        }

        public string ActivateConfiguration()
        {
            try
            {
                _sysManager.ActivateConfiguration();
            }
            catch (Exception e)
            {
                return $"Error in ActivateConfiguration: {e}";
            }
            return _returnStringWhenSuccess;
        }

        public string StartRestartTwinCAT()
        {
            try
            {
                _sysManager.StartRestartTwinCAT();
            }
            catch (Exception e)
            {
                return $"Error in StartRestartTwinCAT: {e}";
            }
            return _returnStringWhenSuccess;
        }

        public string CloseSolution()
        {
            try
            {
                _solution.SaveAs(_solution.FullName);
                _solution.Close();
            }
            catch (Exception e)
            {
                return $"Error in CloseSolution: {e}";
            }
            return _returnStringWhenSuccess;
        }

        public string KillInstance()
        {
            try
            {
                _dte.Quit();
            }
            catch (Exception e)
            {
                return $"Error in KillInstance: {e}";
            }
            return _returnStringWhenSuccess;
        }
    }
}
