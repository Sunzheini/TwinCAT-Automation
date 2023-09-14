using TcAutomation.Core;
using TcAutomation.Core.Contracts;
using TcAutomation.IO.Contracts;

namespace TcAutomation

{
    public partial class Form1 : Form
    {
        private IEngine engine;
        private IWriter writer;
        private IReader reader;
        private string resultStringFromEngine = string.Empty;
        private string inputFromTextBox = string.Empty;

        public Form1(IEngine engine)
        {
            InitializeComponent();
            this.engine = engine;

            // retrieve the instances from the engine
            this.writer = engine.GetWriter();
            this.reader = engine.GetReader();
        }

        // Start
        private void button1_Click(object sender, EventArgs e)
        {
            resultStringFromEngine = this.engine.Start();
            writer.Write(label3, resultStringFromEngine);
        }

        // Build Solution
        private void button2_Click(object sender, EventArgs e)
        {
            resultStringFromEngine = this.engine.BuildSolution();
            writer.Write(label3, resultStringFromEngine);
        }

        // Set Target NetId
        private void button3_Click(object sender, EventArgs e)
        {
            inputFromTextBox = reader.ReadLine(textBox1);
            resultStringFromEngine = this.engine.SetTargetNetId(inputFromTextBox);
            writer.Write(label3, resultStringFromEngine);

            textBox1.Text = "";
        }

        // Activate Configuration
        private void button4_Click(object sender, EventArgs e)
        {
            resultStringFromEngine = this.engine.ActivateConfiguration();
            writer.Write(label3, resultStringFromEngine);
        }

        // Start/Restart TwinCAT
        private void button5_Click(object sender, EventArgs e)
        {
            resultStringFromEngine = this.engine.StartRestartTwinCAT();
            writer.Write(label3, resultStringFromEngine);
        }

        // Exit
        private void button6_Click(object sender, EventArgs e)
        {
            resultStringFromEngine = this.engine.Exit();
            writer.Write(label3, resultStringFromEngine);
        }

        // Read Int From PLC
        private void button7_Click(object sender, EventArgs e)
        {
            inputFromTextBox = reader.ReadLine(textBox2);
            resultStringFromEngine = this.engine.ReadFromPlc(inputFromTextBox);
            writer.Write(label3, resultStringFromEngine);
        }

        // Start/Stop PLC
        private void button8_Click(object sender, EventArgs e)
        {
            resultStringFromEngine = this.engine.ToggleStartStop();
            writer.Write(label3, resultStringFromEngine);
        }

        // Enable/Disable
        private void button9_Click(object sender, EventArgs e)
        {
            resultStringFromEngine = this.engine.ToggleEnableDisable();
            writer.Write(label3, resultStringFromEngine);
        }

        // not used
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
    }
}