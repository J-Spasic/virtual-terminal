using System;
using System.Globalization;
using System.Windows.Forms;

using VirtualTerminal.Models;
using VirtualTerminal.Services;

namespace VirtualTerminal
{
    public partial class MainForm : Form
    {
        #region Field(s)
        private bool isBackspacePressed;
        #endregion Field(s)

        #region Constructor(s)
        public MainForm()
        {
            InitializeComponent();
        }
        #endregion Constructor(s)

        #region Event(s)
        private void MainForm_Load(object sender, EventArgs e)
        {
            SetPortComboBox();
            SetInitialStateOfControls();
        }

        private void PortComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string? portName = comboBoxPort.Items[comboBoxPort.SelectedIndex]?.ToString();

            VirtualTerminalService.SetPortName((portName is not null) ? portName : string.Empty);
        }

        private void BaudRateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int baudRate = Convert.ToInt32(comboBoxBaudRate.Items[comboBoxBaudRate.SelectedIndex].ToString());

            VirtualTerminalService.SetBaudRate(baudRate);
        }

        private void DataBitsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int dataBits = Convert.ToInt32(comboBoxDataBits.Items[comboBoxDataBits.SelectedIndex].ToString());

            VirtualTerminalService.SetDataBits(dataBits);
        }

        private void ParityComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int parityIndex = comboBoxParity.SelectedIndex;

            VirtualTerminalService.SetParity(parityIndex);
        }

        private void StopBitsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int stopBits = Convert.ToInt32(comboBoxStopBits.Items[comboBoxStopBits.SelectedIndex].ToString());

            VirtualTerminalService.SetStopBits(stopBits);
        }

        private void TextModeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonTextMode.Checked) { VirtualTerminalStats.CurrentMode = VirtualTerminalMode.Text; }
            else { VirtualTerminalStats.CurrentMode = VirtualTerminalMode.Hex; }
        }

        private void StartTxProcessButton_Click(object sender, EventArgs e)
        {
            try
            {
                VirtualTerminalService.OpenSerialPort();

                VirtualTerminalStats.IsActive = true;

                ActivateTerminal();
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("You do not have access to this port. Please try another one.",
                    "Unauthorized access", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // To-Do: Replace with Logger.
                Console.WriteLine(ex.Message);
            }
        }

        private void TerminalRichTextBox_Click(object sender, EventArgs e)
        {
            richTextBoxTerminal.Select(richTextBoxTerminal.TextLength, 0);
        }

        private void TerminalRichTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (MainForm.IsArrowKeyPressed(e.KeyCode)) { e.Handled = true; }
            else if (VirtualTerminalStats.NoEnteredSymbols == 0)
            {
                if (e.KeyCode == Keys.Back) { e.Handled = true; }
            }
            else
            {
                if (e.KeyCode == Keys.Back)
                {
                    isBackspacePressed = true;

                    VirtualTerminalStats.NoEnteredSymbols--;
                }
                else
                {
                    isBackspacePressed = false;

                    if (e.KeyCode == Keys.Enter)
                    {
                        e.Handled = true;

                        HandleEnteredCommand();
                    }
                }
            }
        }

        private void TerminalRichTextBox_TextChanged(object sender, EventArgs e)
        {
            if (richTextBoxTerminal.TextLength != 0 && !isBackspacePressed)
            {
                VirtualTerminalStats.NoEnteredSymbols++;
            }
        }
        #endregion Event(s)

        #region Method(s)
        private void SetPortComboBox()
        {
            string[] serialPortNames = VirtualTerminalService.GetAvailableSerialPortNames();

            if (serialPortNames.Length == 0)
            {
                MessageBox.Show("You do not have available serial ports.", "No serial ports available",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                foreach (string portName in serialPortNames) { comboBoxPort.Items.Add(portName); }

                comboBoxPort.SelectedIndex = 0;
            }
        }

        private void SetInitialStateOfControls()
        {
            SetComboBoxesDefaultValues();

            if (comboBoxPort.Items.Count > 0) { SetButtonsEnabledProperty(true); }
            else { SetButtonsEnabledProperty(false); }

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

        private void ActivateTerminal()
        {
            richTextBoxTerminal.Visible = true;

            richTextBoxTerminal.Focus();
            richTextBoxTerminal.Select(richTextBoxTerminal.TextLength, 0);
        }

        private static bool IsArrowKeyPressed(Keys keyCode)
        {
            return keyCode == Keys.Up || keyCode == Keys.Down ||
                keyCode == Keys.Left || keyCode == Keys.Right;
        }

        private void HandleEnteredCommand()
        {
            int commandIndex = richTextBoxTerminal.TextLength - VirtualTerminalStats.NoEnteredSymbols;
            string command = richTextBoxTerminal.Text[commandIndex..];

            if (command.ToLower().Equals(VirtualTerminalCommands.Quit))
            {
                VirtualTerminalService.CloseSerialPort();

                VirtualTerminalStats.IsActive = false;

                ResetAndExitTerminalWindow();
            }
            else if (command.ToLower().Equals(VirtualTerminalCommands.Clear))
            {
                ClearTerminalWindow();
            }
            else
            {
                if (VirtualTerminalStats.CurrentMode == VirtualTerminalMode.Text)
                {
                    VirtualTerminalService.SendData(new char[] { command[0] });
                }
                else
                {
                    if (byte.TryParse(command, NumberStyles.HexNumber, null, out byte hexValue))
                    {
                        VirtualTerminalService.SendData(new byte[] { hexValue });
                    }
                }

                richTextBoxTerminal.Text += "\n> ";
                richTextBoxTerminal.Select(richTextBoxTerminal.TextLength, 0);
            }

            VirtualTerminalStats.NoEnteredSymbols = 0;
        }

        private void ResetAndExitTerminalWindow()
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
        #endregion Method(s)
    }
}
