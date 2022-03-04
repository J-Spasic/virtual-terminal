namespace VirtualTerminal.Models
{
    internal static class VirtualTerminalCommands
    {
        #region Properties
        public static string Quit { get; } = "vt -quit";
        public static string Clear { get; } = "vt -clear";
        #endregion Properties
    }
}
