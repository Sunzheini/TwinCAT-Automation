using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TcAutomation.Core;
using TcAutomation.Core.Contracts;
using TcAutomation.IO.Contracts;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

/*
    Dependencies:
        1. Rightclick on Dependencies --> Add COM Reference --> Beckhoff TwinCAT XAE Base 3.3. Type Library
        2. Rightclick on Dependencies --> Manage Nuget Packages --> Browse: envdte --> install envdte and envdte80 by Microsoft
        3. Click on Tools --> Nuget Package Manager --> Manage Nuget Packages for Solution --> Beckhoff.TwinCAT.Ads --> Select solution
 */

/*
    There are hardcoded values in the Engine class
 */

namespace TcAutomation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IEngine engine;
        private IWriter writer;
        private IReader reader;
        private string _resultStringFromEngine = string.Empty;
        private string _inputFromTextBox = string.Empty;
        private string _initialTextForStatusLabel = "Status: OK";

        public MainWindow()
        {
            InitializeComponent();

            // Initialize the backend engine
            engine = new Engine();

            // retrieve the instances from the engine
            this.writer = engine.GetWriter();
            this.reader = engine.GetReader();

            //initial text for the label
            label1.Text = _initialTextForStatusLabel;
        }

        // browse
        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Solution Files (*.sln)|*.sln|All Files (*.*)|*.*";

                DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    string selectedFilePath = dialog.FileName;
                    FolderPathTextBox.Text = selectedFilePath;
                    engine.SolutionPath = selectedFilePath; // Set the selected solution file path.
                }
            }
        }

        // Start
        public void button1_Click(object sender, RoutedEventArgs e)
        {
            _resultStringFromEngine = this.engine.Start();
            writer.Write(label1, _resultStringFromEngine);
        }

        // Build Solution
        public void button2_Click(object sender, RoutedEventArgs e)
        {
            _resultStringFromEngine = this.engine.BuildSolution();
            writer.Write(label1, _resultStringFromEngine);
        }

        // Set Target NetId
        public void button3_Click(object sender, RoutedEventArgs e)
        {
            _inputFromTextBox = reader.ReadLine(input1);
            _resultStringFromEngine = this.engine.SetTargetNetId(_inputFromTextBox);
            writer.Write(label1, _resultStringFromEngine);

            input1.Text = "";
        }

        // Activate Configuration
        public void button4_Click(object sender, RoutedEventArgs e)
        {
            _resultStringFromEngine = this.engine.ActivateConfiguration();
            writer.Write(label1, _resultStringFromEngine);
        }

        // Start/Restart TwinCAT
        public void button5_Click(object sender, RoutedEventArgs e)
        {
            _resultStringFromEngine = this.engine.StartRestartTwinCAT();
            writer.Write(label1, _resultStringFromEngine);
        }

        // Read Int From PLC
        public void button6_Click(object sender, RoutedEventArgs e)
        {
            _inputFromTextBox = reader.ReadLine(input2);
            _resultStringFromEngine = this.engine.ReadFromPlc(_inputFromTextBox);
            writer.Write(label1, _resultStringFromEngine);
        }

        // Start/Stop PLC
        public void button7_Click(object sender, RoutedEventArgs e)
        {
            _resultStringFromEngine = this.engine.ToggleStartStop();
            writer.Write(label1, _resultStringFromEngine);
        }

        // Enable/Disable
        public void button8_Click(object sender, RoutedEventArgs e)
        {
            _resultStringFromEngine = this.engine.ToggleEnableDisable();
            writer.Write(label1, _resultStringFromEngine);
        }

        // Exit
        public void button9_Click(object sender, RoutedEventArgs e)
        {
            _resultStringFromEngine = this.engine.Exit();
            writer.Write(label1, _resultStringFromEngine);
        }
    }
}
