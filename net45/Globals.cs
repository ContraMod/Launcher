namespace Contra
{
    public static class Globals
    {
        public static bool GB_Checked = false;
        public static bool RU_Checked = false;
        public static bool UA_Checked = false;
        public static bool BG_Checked = false;
        public static bool DE_Checked = false;
        public static string userOS;
        public static string playersOnlineLabel;
        public static int ZTReady = 0; // Integer checking if all 4 ZT installation steps are done. If ZTReady == 4, ZT is installed successfully
        public static bool ZTDriverUninstallSuccessful = false;
        public static int cpuSpeed;
    }
}