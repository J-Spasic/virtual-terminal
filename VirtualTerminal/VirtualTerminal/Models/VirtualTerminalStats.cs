using VirtualTerminal.Enums;

namespace VirtualTerminal.Models
{
    internal static class VirtualTerminalStats
    {
        #region Properties
        public static bool IsActive { get; set; } = false;
        public static VirtualTerminalBufferMode BufferMode { get; set; }
            = VirtualTerminalBufferMode.Text;
        public static int NoEnteredSymbols { get; set; } = 0;
        #endregion Properties
    }
}
