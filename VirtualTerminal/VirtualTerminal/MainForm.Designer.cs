namespace VirtualTerminal
{
    partial class MainForm
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
            this.panelSettings = new System.Windows.Forms.Panel();
            this.richTextBoxTerminal = new System.Windows.Forms.RichTextBox();
            this.buttonStartRxProcess = new System.Windows.Forms.Button();
            this.buttonStartTxProcess = new System.Windows.Forms.Button();
            this.groupBoxBuffer = new System.Windows.Forms.GroupBox();
            this.radioButtonHexMode = new System.Windows.Forms.RadioButton();
            this.radioButtonTextMode = new System.Windows.Forms.RadioButton();
            this.comboBoxStopBits = new System.Windows.Forms.ComboBox();
            this.labelStopBits = new System.Windows.Forms.Label();
            this.comboBoxParity = new System.Windows.Forms.ComboBox();
            this.labelParity = new System.Windows.Forms.Label();
            this.comboBoxDataBits = new System.Windows.Forms.ComboBox();
            this.labelDataBits = new System.Windows.Forms.Label();
            this.comboBoxBaudRate = new System.Windows.Forms.ComboBox();
            this.labelBaudRate = new System.Windows.Forms.Label();
            this.comboBoxPort = new System.Windows.Forms.ComboBox();
            this.labelPort = new System.Windows.Forms.Label();
            this.panelSettings.SuspendLayout();
            this.groupBoxBuffer.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSettings
            // 
            this.panelSettings.Controls.Add(this.richTextBoxTerminal);
            this.panelSettings.Controls.Add(this.buttonStartRxProcess);
            this.panelSettings.Controls.Add(this.buttonStartTxProcess);
            this.panelSettings.Controls.Add(this.groupBoxBuffer);
            this.panelSettings.Controls.Add(this.comboBoxStopBits);
            this.panelSettings.Controls.Add(this.labelStopBits);
            this.panelSettings.Controls.Add(this.comboBoxParity);
            this.panelSettings.Controls.Add(this.labelParity);
            this.panelSettings.Controls.Add(this.comboBoxDataBits);
            this.panelSettings.Controls.Add(this.labelDataBits);
            this.panelSettings.Controls.Add(this.comboBoxBaudRate);
            this.panelSettings.Controls.Add(this.labelBaudRate);
            this.panelSettings.Controls.Add(this.comboBoxPort);
            this.panelSettings.Controls.Add(this.labelPort);
            this.panelSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSettings.Location = new System.Drawing.Point(0, 0);
            this.panelSettings.Name = "panelSettings";
            this.panelSettings.Size = new System.Drawing.Size(484, 211);
            this.panelSettings.TabIndex = 0;
            // 
            // richTextBoxTerminal
            // 
            this.richTextBoxTerminal.BackColor = System.Drawing.Color.Black;
            this.richTextBoxTerminal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBoxTerminal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxTerminal.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.richTextBoxTerminal.ForeColor = System.Drawing.Color.Lime;
            this.richTextBoxTerminal.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxTerminal.Name = "richTextBoxTerminal";
            this.richTextBoxTerminal.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBoxTerminal.Size = new System.Drawing.Size(484, 211);
            this.richTextBoxTerminal.TabIndex = 13;
            this.richTextBoxTerminal.Text = "> Note: Enter vt -quit to go back to main screen.\n> ";
            this.richTextBoxTerminal.TextChanged += new System.EventHandler(this.TerminalRichTextBox_TextChanged);
            this.richTextBoxTerminal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TerminalRichTextBox_KeyDown);
            // 
            // buttonStartRxProcess
            // 
            this.buttonStartRxProcess.Location = new System.Drawing.Point(255, 165);
            this.buttonStartRxProcess.Name = "buttonStartRxProcess";
            this.buttonStartRxProcess.Size = new System.Drawing.Size(120, 35);
            this.buttonStartRxProcess.TabIndex = 12;
            this.buttonStartRxProcess.Text = "Start Rx Process";
            this.buttonStartRxProcess.UseVisualStyleBackColor = true;
            // 
            // buttonStartTxProcess
            // 
            this.buttonStartTxProcess.Location = new System.Drawing.Point(115, 165);
            this.buttonStartTxProcess.Name = "buttonStartTxProcess";
            this.buttonStartTxProcess.Size = new System.Drawing.Size(120, 35);
            this.buttonStartTxProcess.TabIndex = 11;
            this.buttonStartTxProcess.Text = "Start Tx Process";
            this.buttonStartTxProcess.UseVisualStyleBackColor = true;
            this.buttonStartTxProcess.Click += new System.EventHandler(this.StartTxProcessButton_Click);
            // 
            // groupBoxBuffer
            // 
            this.groupBoxBuffer.Controls.Add(this.radioButtonHexMode);
            this.groupBoxBuffer.Controls.Add(this.radioButtonTextMode);
            this.groupBoxBuffer.Location = new System.Drawing.Point(285, 30);
            this.groupBoxBuffer.Name = "groupBoxBuffer";
            this.groupBoxBuffer.Size = new System.Drawing.Size(187, 100);
            this.groupBoxBuffer.TabIndex = 10;
            this.groupBoxBuffer.TabStop = false;
            this.groupBoxBuffer.Text = "Buffer";
            // 
            // radioButtonHexMode
            // 
            this.radioButtonHexMode.AutoSize = true;
            this.radioButtonHexMode.Location = new System.Drawing.Point(60, 55);
            this.radioButtonHexMode.Name = "radioButtonHexMode";
            this.radioButtonHexMode.Size = new System.Drawing.Size(80, 19);
            this.radioButtonHexMode.TabIndex = 1;
            this.radioButtonHexMode.Text = "Hex Mode";
            this.radioButtonHexMode.UseVisualStyleBackColor = true;
            // 
            // radioButtonTextMode
            // 
            this.radioButtonTextMode.AutoSize = true;
            this.radioButtonTextMode.Checked = true;
            this.radioButtonTextMode.Location = new System.Drawing.Point(60, 30);
            this.radioButtonTextMode.Name = "radioButtonTextMode";
            this.radioButtonTextMode.Size = new System.Drawing.Size(80, 19);
            this.radioButtonTextMode.TabIndex = 0;
            this.radioButtonTextMode.TabStop = true;
            this.radioButtonTextMode.Text = "Text Mode";
            this.radioButtonTextMode.UseVisualStyleBackColor = true;
            // 
            // comboBoxStopBits
            // 
            this.comboBoxStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStopBits.FormattingEnabled = true;
            this.comboBoxStopBits.Items.AddRange(new object[] {
            "1",
            "2"});
            this.comboBoxStopBits.Location = new System.Drawing.Point(125, 128);
            this.comboBoxStopBits.Name = "comboBoxStopBits";
            this.comboBoxStopBits.Size = new System.Drawing.Size(121, 23);
            this.comboBoxStopBits.TabIndex = 9;
            // 
            // labelStopBits
            // 
            this.labelStopBits.AutoSize = true;
            this.labelStopBits.Location = new System.Drawing.Point(12, 131);
            this.labelStopBits.Name = "labelStopBits";
            this.labelStopBits.Size = new System.Drawing.Size(56, 15);
            this.labelStopBits.TabIndex = 8;
            this.labelStopBits.Text = "Stop Bits:";
            // 
            // comboBoxParity
            // 
            this.comboBoxParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxParity.FormattingEnabled = true;
            this.comboBoxParity.Items.AddRange(new object[] {
            "NONE",
            "ODD",
            "EVEN",
            "MARK",
            "SPACE"});
            this.comboBoxParity.Location = new System.Drawing.Point(125, 99);
            this.comboBoxParity.Name = "comboBoxParity";
            this.comboBoxParity.Size = new System.Drawing.Size(121, 23);
            this.comboBoxParity.TabIndex = 7;
            // 
            // labelParity
            // 
            this.labelParity.AutoSize = true;
            this.labelParity.Location = new System.Drawing.Point(12, 102);
            this.labelParity.Name = "labelParity";
            this.labelParity.Size = new System.Drawing.Size(40, 15);
            this.labelParity.TabIndex = 6;
            this.labelParity.Text = "Parity:";
            // 
            // comboBoxDataBits
            // 
            this.comboBoxDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDataBits.FormattingEnabled = true;
            this.comboBoxDataBits.Items.AddRange(new object[] {
            "7",
            "8"});
            this.comboBoxDataBits.Location = new System.Drawing.Point(125, 70);
            this.comboBoxDataBits.Name = "comboBoxDataBits";
            this.comboBoxDataBits.Size = new System.Drawing.Size(121, 23);
            this.comboBoxDataBits.TabIndex = 5;
            // 
            // labelDataBits
            // 
            this.labelDataBits.AutoSize = true;
            this.labelDataBits.Location = new System.Drawing.Point(12, 73);
            this.labelDataBits.Name = "labelDataBits";
            this.labelDataBits.Size = new System.Drawing.Size(56, 15);
            this.labelDataBits.TabIndex = 4;
            this.labelDataBits.Text = "Data Bits:";
            // 
            // comboBoxBaudRate
            // 
            this.comboBoxBaudRate.FormattingEnabled = true;
            this.comboBoxBaudRate.Items.AddRange(new object[] {
            "50",
            "110",
            "300",
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600"});
            this.comboBoxBaudRate.Location = new System.Drawing.Point(125, 41);
            this.comboBoxBaudRate.Name = "comboBoxBaudRate";
            this.comboBoxBaudRate.Size = new System.Drawing.Size(121, 23);
            this.comboBoxBaudRate.TabIndex = 3;
            // 
            // labelBaudRate
            // 
            this.labelBaudRate.AutoSize = true;
            this.labelBaudRate.Location = new System.Drawing.Point(12, 44);
            this.labelBaudRate.Name = "labelBaudRate";
            this.labelBaudRate.Size = new System.Drawing.Size(63, 15);
            this.labelBaudRate.TabIndex = 2;
            this.labelBaudRate.Text = "Baud Rate:";
            // 
            // comboBoxPort
            // 
            this.comboBoxPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPort.FormattingEnabled = true;
            this.comboBoxPort.Location = new System.Drawing.Point(125, 12);
            this.comboBoxPort.Name = "comboBoxPort";
            this.comboBoxPort.Size = new System.Drawing.Size(121, 23);
            this.comboBoxPort.TabIndex = 1;
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(12, 15);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(32, 15);
            this.labelPort.TabIndex = 0;
            this.labelPort.Text = "Port:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 211);
            this.Controls.Add(this.panelSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Virtual Terminal";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelSettings.ResumeLayout(false);
            this.panelSettings.PerformLayout();
            this.groupBoxBuffer.ResumeLayout(false);
            this.groupBoxBuffer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSettings;
        private System.Windows.Forms.RichTextBox richTextBoxTerminal;
        private System.Windows.Forms.Button buttonStartRxProcess;
        private System.Windows.Forms.Button buttonStartTxProcess;
        private System.Windows.Forms.GroupBox groupBoxBuffer;
        private System.Windows.Forms.RadioButton radioButtonHexMode;
        private System.Windows.Forms.RadioButton radioButtonTextMode;
        private System.Windows.Forms.ComboBox comboBoxStopBits;
        private System.Windows.Forms.Label labelStopBits;
        private System.Windows.Forms.ComboBox comboBoxParity;
        private System.Windows.Forms.Label labelParity;
        private System.Windows.Forms.ComboBox comboBoxDataBits;
        private System.Windows.Forms.Label labelDataBits;
        private System.Windows.Forms.ComboBox comboBoxBaudRate;
        private System.Windows.Forms.Label labelBaudRate;
        private System.Windows.Forms.ComboBox comboBoxPort;
        private System.Windows.Forms.Label labelPort;
    }
}
