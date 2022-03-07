using System;
using System.Globalization;
using System.IO.Ports;
using System.Windows.Forms;

using VirtualTerminal.Enums;
using VirtualTerminal.Models;

namespace VirtualTerminal
{
    public partial class MainForm : Form
    {
        #region Field(s)
        private static readonly SerialPort serialPort;

        private static bool isBackspacePressed;

        private static event SerialPortDataReceivedDelegate? SerialPortDataReceived;
        #endregion Field(s)

        #region Constructor(s)
        public MainForm()
        {
            InitializeComponent();
        }

        static MainForm()
        {
            MainForm.serialPort = new SerialPort();
            MainForm.serialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);

            MainForm.isBackspacePressed = false;
        }
        #endregion Constructor(s)

        #region Delegate(s)
        private delegate void SerialPortDataReceivedDelegate(string data);
        #endregion Delegate(s)

        #region Event(s)
        private void MainForm_Load(object sender, EventArgs e)
        {
            SetPortComboBox();
            SetInitialStateOfControls();

            MainForm.SerialPortDataReceived += delegate (string data)
            {
                Invoke((MethodInvoker)delegate () { WriteReceivedDataToTerminalWindow(data); });
            };
        }

        private void PortComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string? portName = comboBoxPort.Items[comboBoxPort.SelectedIndex]?.ToString();

            MainForm.serialPort.PortName = portName ?? string.Empty;
        }

        private void BaudRateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int baudRate = Convert.ToInt32(comboBoxBaudRate.Items[comboBoxBaudRate.SelectedIndex].ToString());

            MainForm.serialPort.BaudRate = baudRate;
        }

        private void DataBitsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int dataBits = Convert.ToInt32(comboBoxDataBits.Items[comboBoxDataBits.SelectedIndex].ToString());

            MainForm.serialPort.DataBits = dataBits;
        }

        private void ParityComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int parityIndex = comboBoxParity.SelectedIndex;

            MainForm.serialPort.Parity = (Parity)parityIndex;
        }

        private void StopBitsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int stopBits = Convert.ToInt32(comboBoxStopBits.Items[comboBoxStopBits.SelectedIndex].ToString());

            MainForm.serialPort.StopBits = (StopBits)stopBits;
        }

        private void TextModeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            VirtualTerminalStats.BufferMode = (radioButtonTextMode.Checked) ?
                VirtualTerminalBufferMode.Text : VirtualTerminalBufferMode.Hex;
        }

        private void StartTxProcessButton_Click(object sender, EventArgs e)
        {
            StartProcess(VirtualTerminalProcessType.Transmit);
        }

        private void StartRxProcessButton_Click(object sender, EventArgs e)
        {
            StartProcess(VirtualTerminalProcessType.Receive);
        }

        private static void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (sender is SerialPort sourceSerialPort)
            {
                string receivedData = sourceSerialPort.ReadExisting();

                MainForm.SerialPortDataReceived?.Invoke(receivedData);
            }
        }

        private void TerminalRichTextBox_Click(object sender, EventArgs e)
        {
            richTextBoxTerminal.Select(richTextBoxTerminal.TextLength, 0);
        }

        private void TerminalRichTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (MainForm.IsArrowKeyPressed(e.KeyCode))
            {
                e.Handled = true;
            }
            else if (VirtualTerminalStats.NoEnteredSymbols == 0)
            {
                if (e.KeyCode.Equals(Keys.Back))
                {
                    e.Handled = true;
                }
            }
            else
            {
                HandlePressedKeyInTerminalWindow(e);
            }
        }

        private void TerminalRichTextBox_TextChanged(object sender, EventArgs e)
        {
            if (richTextBoxTerminal.TextLength != 0 && !MainForm.isBackspacePressed)
            {
                VirtualTerminalStats.NoEnteredSymbols++;
            }
        }
        #endregion Event(s)

        #region Method(s)

        #region Form Load Method(s)
        private void SetPortComboBox()
        {
            string[] serialPortNames = SerialPort.GetPortNames();

            if (serialPortNames != null)
            {
                foreach (string portName in serialPortNames)
                {
                    comboBoxPort.Items.Add(portName);
                }

                comboBoxPort.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("You do not have available serial ports.", "No serial ports available",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetInitialStateOfControls()
        {
            SetComboBoxesDefaultValues();

            if (comboBoxPort.Items.Count > 0)
            {
                SetButtonsEnabledProperty(true);
            }
            else
            {
                SetButtonsEnabledProperty(false);
            }

            richTextBoxTerminal.Visible = false;
        }

        private void SetComboBoxesDefaultValues()
        {
            comboBoxBaudRate.SelectedIndex = 3; // 1200
            comboBoxDataBits.SelectedIndex = 1; // 8
            comboBoxParity.SelectedIndex = 2;   // EVEN
            comboBoxStopBits.SelectedIndex = 1; // 2
        }

        private void SetButtonsEnabledProperty(bool enableButton)
        {
            buttonStartTxProcess.Enabled = buttonStartRxProcess.Enabled = enableButton;
        }
        #endregion Form Load Method(s)

        private static bool IsArrowKeyPressed(Keys keyCode)
        {
            return keyCode.Equals(Keys.Up) || keyCode.Equals(Keys.Down) ||
                keyCode.Equals(Keys.Left) || keyCode.Equals(Keys.Right);
        }

        private void HandlePressedKeyInTerminalWindow(KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Back))
            {
                MainForm.isBackspacePressed = true;

                VirtualTerminalStats.NoEnteredSymbols--;
            }
            else
            {
                MainForm.isBackspacePressed = false;

                if (e.KeyCode.Equals(Keys.Enter))
                {
                    e.Handled = true;

                    HandleEnteredCommand();
                }
            }
        }

        private void HandleEnteredCommand()
        {
            int commandIndex = richTextBoxTerminal.TextLength - VirtualTerminalStats.NoEnteredSymbols;
            string command = richTextBoxTerminal.Text[commandIndex..];

            if (command.ToLower().Equals(VirtualTerminalCommands.Quit))
            {
                MainForm.serialPort.Close();

                VirtualTerminalStats.IsActive = false;

                ExitTerminalWindow();
            }
            else if (command.ToLower().Equals(VirtualTerminalCommands.Clear))
            {
                VirtualTerminalStats.NoEnteredSymbols = 0;

                ClearTerminalWindow();
            }
            else
            {
                if (VirtualTerminalStats.ProcessType.Equals(VirtualTerminalProcessType.Transmit))
                {
                    MainForm.TransmitData(command);
                }

                TryToClearTerminalWindow();
            }

            VirtualTerminalStats.NoEnteredSymbols = 0;
        }

        #region Terminal Window Method(s)
        private void ActivateTerminalWindow()
        {
            richTextBoxTerminal.Visible = true;

            richTextBoxTerminal.Focus();
            richTextBoxTerminal.Select(richTextBoxTerminal.TextLength, 0);
        }

        private void WriteReceivedDataToTerminalWindow(string data)
        {
            if (VirtualTerminalStats.ProcessType.Equals(VirtualTerminalProcessType.Receive))
            {
                if (VirtualTerminalStats.BufferMode.Equals(VirtualTerminalBufferMode.Text))
                {
                    richTextBoxTerminal.Text += data + "\n" + "> ";
                }
                else if (byte.TryParse(data, NumberStyles.HexNumber, null, out byte hexValue))
                {
                    richTextBoxTerminal.Text += hexValue + "\n" + "> ";
                }

                VirtualTerminalStats.NoEnteredSymbols = 0;

                richTextBoxTerminal.Select(richTextBoxTerminal.TextLength, 0);
            }
        }

        private void ExitTerminalWindow()
        {
            richTextBoxTerminal.Text = "> ";
            richTextBoxTerminal.Visible = false;

            comboBoxPort.Focus();
        }

        private void ClearTerminalWindow()
        {
            richTextBoxTerminal.Text = "> ";

            richTextBoxTerminal.Select(richTextBoxTerminal.TextLength, 0);
        }

        private void TryToClearTerminalWindow()
        {
            if (richTextBoxTerminal.TextLength >= short.MaxValue)
            {
                richTextBoxTerminal.Text = "> ";
            }
            else
            {
                richTextBoxTerminal.Text += "\n" + "> ";
            }

            richTextBoxTerminal.Select(richTextBoxTerminal.TextLength, 0);
        }
        #endregion Terminal Window Method(s)

        #region Serial Communication Method(s)
        private void StartProcess(VirtualTerminalProcessType processType)
        {
            try
            {
                TryToChangeBaudRate();

                MainForm.serialPort.Open();

                VirtualTerminalStats.IsActive = true;
                VirtualTerminalStats.ProcessType = processType;

                ActivateTerminalWindow();
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("You do not have access to this port. Please try another one.",
                    "Unauthorized access", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // To-Do: Replace with Logger.
                Console.WriteLine(ex.Message);
            }
        }

        private void TryToChangeBaudRate()
        {
            if (int.TryParse(comboBoxBaudRate.Text, out int baudRate))
            {
                MainForm.serialPort.BaudRate = baudRate;
            }
        }

        private static void TransmitData(string data)
        {
            if (VirtualTerminalStats.BufferMode.Equals(VirtualTerminalBufferMode.Text))
            {
                MainForm.serialPort.Write(new char[] { data[0] }, 0, 1);
            }
            else if (byte.TryParse(data, NumberStyles.HexNumber, null, out byte hexValue))
            {
                MainForm.serialPort.Write(new byte[] { hexValue }, 0, 1);
            }
        }
        #endregion Serial Communication Method(s)

        #endregion Method(s)
    }
}
