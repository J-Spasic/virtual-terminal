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
        public static string[] GetAvailableSerialPortNames() { return SerialPort.GetPortNames(); }

        public static void SetPortName(string portName)
        {
            VirtualTerminalService.serialPort.PortName = portName;
        }

        public static void SetBaudRate(int baudRate) { VirtualTerminalService.serialPort.BaudRate = baudRate; }

        public static void SetDataBits(int dataBits) { VirtualTerminalService.serialPort.DataBits = dataBits; }

        public static void SetParity(int parityIndex)
        {
            VirtualTerminalService.serialPort.Parity = (Parity)parityIndex;
        }

        public static void SetStopBits(int stopBits)
        {
            VirtualTerminalService.serialPort.StopBits = (StopBits)stopBits;
        }

        public static void OpenSerialPort() { VirtualTerminalService.serialPort.Open(); }

        public static void CloseSerialPort() { VirtualTerminalService.serialPort.Close(); }

        public static void SendData(char[] characters)
        {
            VirtualTerminalService.serialPort.Write(characters, 0, characters.Length);
        }

        public static void SendData(byte[] hexValues)
        {
            VirtualTerminalService.serialPort.Write(hexValues, 0, hexValues.Length);
        }
        #endregion Method(s)
    }
}
