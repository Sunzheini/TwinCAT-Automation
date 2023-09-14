using TcAutomation.Core;
using TcAutomation.Core.Contracts;
using TcAutomation.IO.Contracts;


/*
    Dependencies:
        1. Rightclick on Dependencies --> Add COM Reference --> Beckhoff TwinCAT XAE Base 3.3. Type Library
        2. Rightclick on Dependencies --> Manage Nuget Packages --> Browse: envdte --> install envdte and envdte80 by Microsoft
        3. Click on Tools --> Nuget Package Manager --> Manage Nuget Packages for Solution --> Beckhoff.TwinCAT.Ads --> Select solution
 */

/*
    There are hardcoded values in the Engine class. These values are:
        1. productId
        2. solutionPath
        3. defaultAmsNetId
        4. defaultNameOfIntVarToRead
        5. nameOfEnableVar
        6. portForAds
 */


namespace TcAutomation
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Initialize the backend engine
            IEngine engine = new Engine();
            
            // Initialize the forms app
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1(engine));
        }
    }
}