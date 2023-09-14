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
        private string productId = String.Empty;
        private static Type typeOfInstance = null;
        private static EnvDTE.DTE dte = null;
        private static EnvDTE.Solution solution = null;
        private static EnvDTE.Project project = null;
        private static ITcSysManager15 sysManager = null;

        public AutomationInterfaceController(string productId)
        {
            this.productId = productId;
        }

        public string CreateInstance()
        {
            try
            {
                typeOfInstance = System.Type.GetTypeFromProgID(productId);
            }
            catch (Exception e)
            {
                return $"Error in GetTypeFromProgID: {e}";
            }

            try
            {
                dte = (EnvDTE.DTE)System.Activator.CreateInstance(typeOfInstance);
            }
            catch (Exception e)
            {
                return $"Error while creating instance: {e}";
            }

            try
            {
                dte.SuppressUI = false;        // when true, the console will not be shown
                dte.MainWindow.Visible = true; // when false, twincat will not be shown
            }
            catch (Exception e)
            {
                return $"Error in window options: {e}";
            }

            // Provide a default return value if no exceptions are thrown
            return "Success";
        }

        public string OpenSolution(string solutionPath)
        {
            try
            {
                solution = dte.Solution;
                solution.Open(@solutionPath);
            }
            catch (Exception e)
            {
                return $"Error in OpenSolution: {e}";
            }
            return "Success";
        }

        public string CreateITcSysManager()
        {
            try
            {
                project = solution.Projects.Item(1);
                sysManager = (ITcSysManager15)project.Object;
            }
            catch (Exception e)
            {
                return $"Error in CreateITcSysManager: {e}";
            }
            return "Success";
        }

        public string BuildSolution()
        {
            try
            {
                solution.SolutionBuild.Build(true);
            }
            catch (Exception e)
            {
                return $"Error in BuildSolution: {e}";
            }
            return "Success";
        }

        public string SetTargetNetId(string amsNetId, string defaultAmsNetId)
        {
            try
            {
                sysManager.SetTargetNetId(amsNetId);
            }
            catch (Exception e)
            {
                return $"Error in SetTargetNetId: {e}";
            }
            finally
            {
                sysManager.SetTargetNetId(defaultAmsNetId);
            }
            return "Success";
        }

        public string ActivateConfiguration()
        {
            try
            {
                sysManager.ActivateConfiguration();
            }
            catch (Exception e)
            {
                return $"Error in ActivateConfiguration: {e}";
            }
            return "Success";
        }

        public string StartRestartTwinCAT()
        {
            try
            {
                sysManager.StartRestartTwinCAT();
            }
            catch (Exception e)
            {
                return $"Error in StartRestartTwinCAT: {e}";
            }
            return "Success";
        }

        public string CloseSolution()
        {
            try
            {
                solution.SaveAs(solution.FullName);
                solution.Close();
            }
            catch (Exception e)
            {
                return $"Error in CloseSolution: {e}";
            }
            return "Success";
        }

        public string KillInstance()
        {
            try
            {
                dte.Quit();
            }
            catch (Exception e)
            {
                return $"Error in KillInstance: {e}";
            }
            return "Success";
        }
    }
}
