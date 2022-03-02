namespace VirtualTerminal.Models
{
    internal enum VirtualTerminalMode
    {
        Text,
        Hex
    };

    internal static class VirtualTerminalStats
    {
        #region Properties
        public static int NoEnteredSymbols { get; set; } = 0;
        public static VirtualTerminalMode CurrentMode { get; set; } = VirtualTerminalMode.Text;
        public static bool IsActive { get; set; } = false;
        #endregion Properties
    }
}
