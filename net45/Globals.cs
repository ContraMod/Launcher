using System.Drawing;

namespace Contra
{
    public static class Globals
    {
        public static bool GB_Checked = false;
        public static bool RU_Checked = false;
        public static bool UA_Checked = false;
        public static bool BG_Checked = false;
        public static bool DE_Checked = false;
        public static string currentLanguage;
        public static string userOS;
        public static string myDocPath;
        public static int cpuSpeed;
        public static Color buttonHighlight = Color.FromArgb(244, 250, 175);
        public static Color disabledTextColor = Color.FromArgb(128, 128, 128); // Gray color for disabled text
    }
}