namespace TcAutomation
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label3 = new Label();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            textBox1 = new TextBox();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            button9 = new Button();
            button8 = new Button();
            textBox2 = new TextBox();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(131, 640);
            label3.Name = "label3";
            label3.Size = new Size(150, 20);
            label3.TabIndex = 2;
            label3.Text = "Info is displayed here";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            label3.Click += label3_Click;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ControlLight;
            button1.Location = new Point(131, 57);
            button1.Name = "button1";
            button1.Size = new Size(157, 29);
            button1.TabIndex = 3;
            button1.Text = "Start";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(131, 105);
            button2.Name = "button2";
            button2.Size = new Size(157, 29);
            button2.TabIndex = 4;
            button2.Text = "Build Solution";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(131, 188);
            button3.Name = "button3";
            button3.Size = new Size(157, 29);
            button3.TabIndex = 5;
            button3.Text = "Set Target NetId";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(131, 155);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(157, 27);
            textBox1.TabIndex = 6;
            textBox1.TextAlign = HorizontalAlignment.Center;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // button4
            // 
            button4.Location = new Point(131, 237);
            button4.Name = "button4";
            button4.Size = new Size(157, 29);
            button4.TabIndex = 7;
            button4.Text = "Activate Configuration";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(131, 288);
            button5.Name = "button5";
            button5.Size = new Size(157, 29);
            button5.TabIndex = 8;
            button5.Text = "Start/Restart TwinCAT";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.Location = new Point(131, 582);
            button6.Name = "button6";
            button6.Size = new Size(157, 29);
            button6.TabIndex = 9;
            button6.Text = "Exit";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // button7
            // 
            button7.Location = new Point(131, 371);
            button7.Name = "button7";
            button7.Size = new Size(157, 29);
            button7.TabIndex = 10;
            button7.Text = "Read int from PLC";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // button9
            // 
            button9.Location = new Point(131, 475);
            button9.Name = "button9";
            button9.Size = new Size(157, 29);
            button9.TabIndex = 12;
            button9.Text = "Enable/Disable";
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click;
            // 
            // button8
            // 
            button8.Location = new Point(131, 423);
            button8.Name = "button8";
            button8.Size = new Size(157, 29);
            button8.TabIndex = 11;
            button8.Text = "Start/Stop PLC";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(131, 338);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(157, 27);
            textBox2.TabIndex = 13;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            ClientSize = new Size(413, 760);
            Controls.Add(textBox2);
            Controls.Add(button9);
            Controls.Add(button8);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(textBox1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label3);
            Name = "Form1";
            Text = "TwinCAT Controller";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label3;
        private Button button1;
        private Button button2;
        private Button button3;
        private TextBox textBox1;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button9;
        private Button button8;
        private TextBox textBox2;
    }
}