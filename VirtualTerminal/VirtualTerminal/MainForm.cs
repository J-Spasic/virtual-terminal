using System;
using System.Windows.Forms;

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
            SetInitialStateOfControls();
        }
        #endregion Event(s)

        #region Method(s)
        private void SetInitialStateOfControls()
        {
            SetComboBoxesDefaultValues();

            SetButtonsEnabledProperty(false);

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
