using System;
using System.Windows.Forms;

using VirtualTerminal.Services;

namespace VirtualTerminal
{
    public partial class MainForm : Form
    {
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
            comboBoxParity.SelectedIndex = 1;   // EVEN
            comboBoxStopBits.SelectedIndex = 1; // 2
        }

        private void SetButtonsEnabledProperty(bool enableButton)
        {
            buttonStartTxProcess.Enabled = enableButton;
            buttonStartRxProcess.Enabled = enableButton;
        }
        #endregion Method(s)
    }
}
