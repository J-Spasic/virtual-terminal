using System.IO.Ports;

namespace VirtualTerminal.Services
{
    internal static class VirtualTerminalService
    {
        #region Field(s)
        private static readonly SerialPort serialPort;
        #endregion Field(s)

        #region Constructor(s)
        static VirtualTerminalService()
        {
            VirtualTerminalService.serialPort = new SerialPort();
        }
        #endregion Constructor(s)

        #region Method(s)
        public static string[] GetAvailableSerialPortNames()
        {
            return SerialPort.GetPortNames();
        }

        public static void ConfigureSerialPort(string portName, int baudRate, int parityIndex,
            int dataBits, int stopBit)
        {
            VirtualTerminalService.serialPort.PortName = portName;
            VirtualTerminalService.serialPort.BaudRate = baudRate;
            VirtualTerminalService.serialPort.Parity = (Parity)parityIndex;
            VirtualTerminalService.serialPort.DataBits = dataBits;
            VirtualTerminalService.serialPort.StopBits = (StopBits)stopBit;
        }

        public static void OpenSerialPort()
        {
            VirtualTerminalService.serialPort.Open();
        }

        public static void CloseSerialPort()
        {
            VirtualTerminalService.serialPort.Close();
        }
        #endregion Method(s)
    }
}
