using System;
using System.Windows.Forms;

using VirtualTerminal.Services;

namespace VirtualTerminal
{
    public partial class MainForm : Form
    {
        #region Field(s)
        private int numberOfEnteredSymbolsInTerminal;
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

        private void StartTxProcessButton_Click(object sender, EventArgs e)
        {
            string? portName = comboBoxPort.Items[comboBoxPort.SelectedIndex]?.ToString();
            int baudRate = Convert.ToInt32(comboBoxBaudRate.Items[comboBoxBaudRate.SelectedIndex].ToString());
            int dataBits = Convert.ToInt32(comboBoxDataBits.Items[comboBoxDataBits.SelectedIndex].ToString());
            int parityIndex = comboBoxParity.SelectedIndex;
            int stopBit = Convert.ToInt32(comboBoxStopBits.Items[comboBoxStopBits.SelectedIndex].ToString());

            VirtualTerminalService.ConfigureSerialPort((portName is not null) ? portName : string.Empty,
                baudRate, parityIndex, dataBits, stopBit);

            try
            {
                VirtualTerminalService.OpenSerialPort();

                numberOfEnteredSymbolsInTerminal = 0;

                richTextBoxTerminal.Visible = true;
                richTextBoxTerminal.Focus();
                richTextBoxTerminal.Select(richTextBoxTerminal.Text.Length, 0);
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
            richTextBoxTerminal.Select(richTextBoxTerminal.Text.Length, 0);
        }

        private void TerminalRichTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                isBackspacePressed = true;

                TryToDeleteEnteredSymbol(e);
            }
            else
            {
                isBackspacePressed = false;

                if (MainForm.IsArrowKeyPressed(e.KeyCode))
                {
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;

                    HandleEnteredCommand();
                }
            }
        }

        private void TerminalRichTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!richTextBoxTerminal.Text.Equals(string.Empty) && !isBackspacePressed)
            {
                numberOfEnteredSymbolsInTerminal++;
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
                foreach (string portName in serialPortNames)
                {
                    comboBoxPort.Items.Add(portName);
                }

                comboBoxPort.SelectedIndex = 0;
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
            buttonStartTxProcess.Enabled = enableButton;
            buttonStartRxProcess.Enabled = enableButton;
        }

        private void TryToDeleteEnteredSymbol(KeyEventArgs e)
        {
            if (numberOfEnteredSymbolsInTerminal == 0)
            {
                e.Handled = true;
            }
            else
            {
                numberOfEnteredSymbolsInTerminal--;
            }
        }

        private static bool IsArrowKeyPressed(Keys keyCode)
        {
            return keyCode == Keys.Up || keyCode == Keys.Down || keyCode == Keys.Left ||
                keyCode == Keys.Right;
        }

        private void HandleEnteredCommand()
        {
            int commandLength = richTextBoxTerminal.Text.Length - numberOfEnteredSymbolsInTerminal;
            string command = richTextBoxTerminal.Text[commandLength..];

            if (command.ToLower().Equals("vt -quit"))
            {
                VirtualTerminalService.CloseSerialPort();

                richTextBoxTerminal.Text = "> ";
                richTextBoxTerminal.Visible = false;

                comboBoxPort.Focus();
            }
            else
            {
                richTextBoxTerminal.Text += "\n> ";
                richTextBoxTerminal.Select(richTextBoxTerminal.Text.Length, 0);
            }

            numberOfEnteredSymbolsInTerminal = 0;
        }
        #endregion Method(s)
    }
}
