using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Contra
{
    /// <summary>
    /// Reading and writing of the contraZH engine settings in Options.ini.
    ///
    /// The existing EnableDisableSettings in OptionsForm only rewrites a line that is already
    /// present, which is fine for the retail keys because the game writes them all out itself.
    /// None of the settings below exist in a fresh Options.ini, so they need an upsert: replace
    /// the line when it is there, append it when it is not.
    /// </summary>
    internal static class GameOptionsIni
    {
        private static string IniPath
        {
            get { return Globals.myDocPath + "Options.ini"; }
        }

        public static bool Exists()
        {
            return Directory.Exists(Globals.myDocPath) && File.Exists(IniPath);
        }

        public static string ReadAll()
        {
            if (!Exists())
            {
                return string.Empty;
            }

            return File.ReadAllText(IniPath);
        }

        /// <summary>
        /// Value of a key, or the supplied fallback when the key is absent or empty.
        /// Matched at the start of a line so a key is never picked up from inside another one --
        /// "GridHotkeys" would otherwise also match "GridHotkeyColumns".
        /// </summary>
        public static string GetValue(string contents, string key, string fallback)
        {
            if (string.IsNullOrEmpty(contents))
            {
                return fallback;
            }

            Match m = Regex.Match(contents,
                @"^[ \t]*" + Regex.Escape(key) + @"[ \t]*=[ \t]*(.*)$",
                RegexOptions.IgnoreCase | RegexOptions.Multiline);

            if (!m.Success)
            {
                return fallback;
            }

            string value = m.Groups[1].Value.Trim();
            return value.Length == 0 ? fallback : value;
        }

        public static bool GetBool(string contents, string key, bool fallback)
        {
            string value = GetValue(contents, key, fallback ? "Yes" : "No");
            return string.Equals(value, "Yes", StringComparison.OrdinalIgnoreCase);
        }

        public static int GetInt(string contents, string key, int fallback)
        {
            int parsed;
            if (int.TryParse(GetValue(contents, key, string.Empty), out parsed))
            {
                return parsed;
            }

            return fallback;
        }

        /// <summary>
        /// Set a key, replacing the existing line or appending a new one. Returns the updated text
        /// rather than writing, so a whole page of settings costs one file write instead of one per
        /// setting.
        /// </summary>
        public static string SetValue(string contents, string key, string value)
        {
            if (contents == null)
            {
                contents = string.Empty;
            }

            // Keep whatever line ending the file already uses. Options.ini is written by the game
            // as CRLF, but a hand edited file may not be, and mixing the two upsets some parsers.
            string newLine = contents.Contains("\r\n") ? "\r\n" : "\n";

            Regex existing = new Regex(
                @"^[ \t]*" + Regex.Escape(key) + @"[ \t]*=.*$",
                RegexOptions.IgnoreCase | RegexOptions.Multiline);

            if (existing.IsMatch(contents))
            {
                return existing.Replace(contents, key + " = " + value, 1);
            }

            if (contents.Length > 0 && !contents.EndsWith("\n"))
            {
                contents += newLine;
            }

            return contents + key + " = " + value + newLine;
        }

        public static string SetBool(string contents, string key, bool value)
        {
            return SetValue(contents, key, value ? "Yes" : "No");
        }

        public static string SetInt(string contents, string key, int value)
        {
            return SetValue(contents, key, value.ToString());
        }

        public static void WriteAll(string contents)
        {
            if (!Directory.Exists(Globals.myDocPath))
            {
                return;
            }

            File.WriteAllText(IniPath, contents);
        }
    }
}
