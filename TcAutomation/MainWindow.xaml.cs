using System.Windows;
using System.Windows.Forms;
using TcAutomation.Core;

/*
    Manage Dependencies:
        1. Rightclick on Dependencies --> Add COM Reference --> Beckhoff TwinCAT XAE Base 3.3. Type Library
        2. Rightclick on Dependencies --> Manage Nuget Packages --> Browse: envdte --> install envdte and envdte80 by Microsoft
        3. Click on Tools --> Nuget Package Manager --> Manage Nuget Packages for Solution --> Beckhoff.TwinCAT.Ads --> Select solution
 */

namespace TcAutomation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // string vars to be used below
        private string _resultStringFromEngine = string.Empty;
        private string _inputFromTextBox = string.Empty;
        private string _initialTextForStatusLabel = "Status: OK";
        private string _dialogFilter = "Solution Files (*.sln)|*.sln|All Files (*.*)|*.*";

        // initialize the objects here
        private MainWindowCore _mainWindowCore;

        public MainWindow()
        {
            InitializeComponent();

            // Initialize MainWindowCore
            _mainWindowCore = new MainWindowCore();

            //initial text for the label
            label1.Text = _initialTextForStatusLabel;
        }

        /// <summary>
        /// Browse for the solution file button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = _dialogFilter;

                DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    string selectedFilePath = dialog.FileName;
                    string path = _mainWindowCore.SelectFilePath(selectedFilePath); // Set the selected solution file path.
                    FolderPathTextBox.Text = path;
                }
            }
        }

        /// <summary>
        /// Start button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void button1_Click(object sender, RoutedEventArgs e)
        {
            _resultStringFromEngine = _mainWindowCore.Start();
            _mainWindowCore.writer.Write(label1, _resultStringFromEngine);
        }

        /// <summary>
        /// Build Solution button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void button2_Click(object sender, RoutedEventArgs e)
        {
            _resultStringFromEngine = _mainWindowCore.BuildSolution();
            _mainWindowCore.writer.Write(label1, _resultStringFromEngine);
        }

        /// <summary>
        /// Set Target NetId button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void button3_Click(object sender, RoutedEventArgs e)
        {
            _inputFromTextBox = _mainWindowCore.reader.ReadLine(input1);
            _resultStringFromEngine = _mainWindowCore.SetTargetNetId(_inputFromTextBox);
            _mainWindowCore.writer.Write(label1, _resultStringFromEngine);

            input1.Text = "";
        }

        /// <summary>
        /// Activate Configuration button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void button4_Click(object sender, RoutedEventArgs e)
        {
            _resultStringFromEngine = _mainWindowCore.ActivateConfiguration();
            _mainWindowCore.writer.Write(label1, _resultStringFromEngine);
        }

        /// <summary>
        /// Start/Restart TwinCAT button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void button5_Click(object sender, RoutedEventArgs e)
        {
            _resultStringFromEngine = _mainWindowCore.StartRestartTwinCAT();
            _mainWindowCore.writer.Write(label1, _resultStringFromEngine);
        }

        /// <summary>
        /// Read Int From PLC button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void button6_Click(object sender, RoutedEventArgs e)
        {
            _inputFromTextBox = _mainWindowCore.reader.ReadLine(input2);
            _resultStringFromEngine = _mainWindowCore.ReadFromPlc(_inputFromTextBox);
            _mainWindowCore.writer.Write(label1, _resultStringFromEngine);
        }

        /// <summary>
        /// Start/Stop PLC button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void button7_Click(object sender, RoutedEventArgs e)
        {
            _resultStringFromEngine = _mainWindowCore.ToggleStartStop();
            _mainWindowCore.writer.Write(label1, _resultStringFromEngine);
        }

        /// <summary>
        /// Enable/Disable button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void button8_Click(object sender, RoutedEventArgs e)
        {
            _resultStringFromEngine = _mainWindowCore.ToggleEnableDisable();
            _mainWindowCore.writer.Write(label1, _resultStringFromEngine);
        }

        /// <summary>
        /// Exit button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void button9_Click(object sender, RoutedEventArgs e)
        {
            _resultStringFromEngine = _mainWindowCore.Exit();
            _mainWindowCore.writer.Write(label1, _resultStringFromEngine);
        }
    }
}
