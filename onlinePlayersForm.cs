using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Win32;

namespace Contra
{
    public partial class onlinePlayersForm : Form
    {
        public onlinePlayersForm()
        {
            InitializeComponent();
            IP_Label.Text = Properties.Settings.Default.IP_Label;
            refreshTimer.Enabled = true;

            if (Globals.GB_Checked == true)
            {
                toolTip1.SetToolTip(refreshOnlinePlayersBtn, "Refresh online players");
            }
            else if (Globals.RU_Checked == true)
            {
                toolTip1.SetToolTip(refreshOnlinePlayersBtn, "Обновить игроков онлайн");
            }
            else if (Globals.UA_Checked == true)
            {
                toolTip1.SetToolTip(refreshOnlinePlayersBtn, "Оновити гравців онлайн");
            }
            else if (Globals.BG_Checked == true)
            {
                toolTip1.SetToolTip(refreshOnlinePlayersBtn, "Обнови");
            }
            else if (Globals.DE_Checked == true)
            {
                toolTip1.SetToolTip(refreshOnlinePlayersBtn, "Aktualisiere Spieler die online sind");
            }
        }

        string vpnconfig = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Contra\\vpnconfig\\contravpn";


        //**********DRAG FORM CODE START**********
        const int WM_NCLBUTTONDBLCLK = 0xA3;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_NCLBUTTONDBLCLK)
                return;

            base.WndProc(ref m);
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                        m.Result = (IntPtr)0x2;
                    return;
            }
            base.WndProc(ref m);
        }
        //**********DRAG FORM CODE END**********


        public static string userDataLeafName()
        {
            //o gets "Command and Conquer Generals Zero Hour Data" from registry. It varies depending on language
            var o = string.Empty;
            if (Globals.userOS == "32")
            {
                var userDataRegistryPath = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Electronic Arts\EA Games\Command and Conquer Generals Zero Hour");
                if (userDataRegistryPath != null)
                {
                    o = userDataRegistryPath.GetValue("UserDataLeafName") as string;
                }
            }
            else if (Globals.userOS == "64")
            {
                var userDataRegistryPath = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Electronic Arts\EA Games\Command and Conquer Generals Zero Hour");
                if (userDataRegistryPath != null)
                {
                    o = userDataRegistryPath.GetValue("UserDataLeafName") as string;
                }
            }
            if (o != null)
            {
                return System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + o + @"\";
            }
            else
            {
                return null;
            }
        }

        public static string myDocPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Command and Conquer Generals Zero Hour Data\";

        private static void vpnIP()
        {
            Process netsh = new Process();
            netsh.StartInfo.FileName = "netsh.exe";
            netsh.StartInfo.UseShellExecute = false;
            netsh.StartInfo.RedirectStandardInput = true;
            netsh.StartInfo.RedirectStandardOutput = true;
            netsh.StartInfo.RedirectStandardError = true;
            netsh.StartInfo.CreateNoWindow = true;
            netsh.StartInfo.Arguments = "interface ip show addresses ContraVPN";
            try
            {
                netsh.Start();
                netsh.WaitForExit();
                // Match vpn DHCP pool range 10.10.10.[11-254]
                string ip = Regex.Match(netsh.StandardOutput.ReadToEnd(), "10.10.10.(1[1-9]|[2-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-4])?\r?\n").Value.Trim();

                if (!string.IsNullOrWhiteSpace(ip))
                {
                    Properties.Settings.Default.IP_Label = "ContraVPN IP: " + ip;
                    void writeIPAddress(string path)
                    {
                        File.WriteAllText(path, Regex.Replace(File.ReadAllText(path), "^IPAddress.*\\S+", $"IPAddress = {ip}", RegexOptions.Multiline));
                    }
                    if (File.Exists(userDataLeafName() + "Options.ini"))
                    {
                        writeIPAddress(userDataLeafName() + "Options.ini");
                    }
                    else if (File.Exists(myDocPath + "Options.ini"))
                    {
                        writeIPAddress(myDocPath + "Options.ini");
                    }
                    else
                    {
                        var cannotsaveip_lang = new Dictionary<string, bool>
                        {
                            {"Options.ini not found!\nCannot write IPAddress.", Globals.GB_Checked},
                            {"Файл Options.ini не найден!\nНевозможно записать IPAddress.", Globals.RU_Checked},
                            {"Файл Options.ini не знайдений!\nНеможливо написати IPAddress.", Globals.UA_Checked},
                            {"Options.ini не беше намерен!\nНе може запише IPAddress.", Globals.BG_Checked},
                            {"Options.ini nicht gefunden!\nIPAddress kann nicht geschrieben werden.", Globals.DE_Checked},
                        };
                        //Too spammy
                        //MessageBox.Show(cannotsaveip_lang.Single(l => l.Value).Key, null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Console.Error.WriteLine(cannotsaveip_lang.Single(l => l.Value).Key);
                    }
                }
                else
                {
                    var iplabel_lang = new Dictionary<string, bool>
                    {
                        {"Not compatible", Globals.GB_Checked},
                        {"несовместимый", Globals.RU_Checked},
                        {"несумісні", Globals.UA_Checked},
                        {"несъвместим", Globals.BG_Checked},
                        {"Nicht Kompatibel", Globals.DE_Checked},
                    };
                    Properties.Settings.Default.IP_Label = "ContraVPN IP: " + iplabel_lang.Single(l => l.Value).Key;
                }
            }
            catch (Exception ex) { Console.Error.WriteLine(ex); }
        }

        private void refreshOnlinePlayers()
        {
            try
            {
                string tincd = "tincd.exe";
                Process[] tincdByName = Process.GetProcessesByName(tincd.Substring(0, tincd.LastIndexOf('.')));
                if (tincdByName.Length > 0) //if tincd is already running
                {
                    IP_Label.Text = Properties.Settings.Default.IP_Label;
                    Process onlinePlayers = new Process();
                    onlinePlayers.StartInfo.Arguments = "--config=\"" + vpnconfig + "\" --pidfile=\"" + vpnconfig + "\\tinc.pid\"";
                    onlinePlayers.StartInfo.FileName = Environment.CurrentDirectory + @"\contra\vpn\" + Globals.userOS + @"\tinc.exe";
                    onlinePlayers.StartInfo.UseShellExecute = false;
                    onlinePlayers.StartInfo.RedirectStandardInput = true;
                    onlinePlayers.StartInfo.RedirectStandardOutput = true;
                    onlinePlayers.StartInfo.CreateNoWindow = true;
                    onlinePlayers.Start();
                    onlinePlayers.StandardInput.WriteLine("dump reachable nodes");
                    onlinePlayers.StandardInput.Flush();
                    onlinePlayers.StandardInput.Close();
                    string s = onlinePlayers.StandardOutput.ReadToEnd();

                    // Match up to first whitespace then go to next line
                    Regex pattern = new Regex(@"^\S+", RegexOptions.Multiline);
                    string s_concat = string.Empty;
                    bool first = true;

                    foreach (var match in pattern.Matches(s))
                    {
                        if (first)
                        {
                            s_concat = s_concat.TrimEnd('_') + "- ";
                            first = false;
                        }
                        string matchString = match.ToString();
                        if (!matchString.Contains("contravpn") && (!matchString.Contains("ctrvpntest9")))
                        {
                            s_concat += matchString;
                            s_concat = s_concat.TrimEnd('_') + Environment.NewLine + "- ";
                        }
                    }

                    // Remove newline and dash from result string
                    s_concat = s_concat.Remove(s_concat.Length - Environment.NewLine.Length - 2);
                    onlinePlayersLabel.Text = s_concat;

                    // We count all newlines and add one for the first name that's online
                    int s2 = Regex.Matches(s_concat, Environment.NewLine).Count + 1;
                    onlinePlayers.WaitForExit();
                    onlinePlayers.Close();

                    var labelLang = new Dictionary<string, bool>
                    {
                        {$"Players online: {s2}",   Globals.GB_Checked},
                        {$"Игроки онлайн: {s2}",    Globals.RU_Checked},
                        {$"Гравці в мережі: {s2}",  Globals.UA_Checked},
                        {$"Играчи на линия: {s2}",  Globals.BG_Checked},
                        {$"Spieler online: {s2}",   Globals.DE_Checked},
                    };
                    Globals.playersOnlineLabel = labelLang.Single(l => l.Value).Key;
                    playersOnlineLabel.Text = Globals.playersOnlineLabel;
                    vpnIP();
                }
            }
            catch (Exception ex) { Console.Error.WriteLine(ex); }
        }

        private void onlinePlayersForm_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.playersOnlineWindowLocation != null)
            {
                this.Location = Properties.Settings.Default.playersOnlineWindowLocation;
            }
            refreshOnlinePlayers();
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.playersOnlineWindowLocation = this.Location;
            }
            else
            {
                Properties.Settings.Default.playersOnlineWindowLocation = this.RestoreBounds.Location;
            }
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            OnApplicationExit(sender, e);
        }

        int i = 0;
        private void refreshTimer_Tick(object sender, EventArgs e) //refresh 4 times, each 2.5 sec
        {
            refreshOnlinePlayersBtn.PerformClick();
            i++;
            if (i == 4)
            {
                refreshTimer.Enabled = false;
            }
            //MessageBox.Show("timer 2 ticked");
        }

        private void refreshOnlinePlayersBtn_Click(object sender, EventArgs e)
        {
            IP_Label.Text = Properties.Settings.Default.IP_Label;
            string tincd = "tincd.exe";
            Process[] tincdByName = Process.GetProcessesByName(tincd.Substring(0, tincd.LastIndexOf('.')));
            if (tincdByName.Length > 0) //if tincd is already running
            {
                refreshOnlinePlayers();
            }
            else
            {
                if (Globals.GB_Checked == true)
                {
                    playersOnlineLabel.Text = "ContraVPN is disabled";
                }
                else if (Globals.RU_Checked == true)
                {
                    playersOnlineLabel.Text = "ContraVPN выключено";
                }
                else if (Globals.UA_Checked == true)
                {
                    playersOnlineLabel.Text = "ContraVPN вимкнено";
                }
                else if (Globals.BG_Checked == true)
                {
                    playersOnlineLabel.Text = "ContraVPN е изключен";
                }
                else if (Globals.DE_Checked == true)
                {
                    playersOnlineLabel.Text = "ContraVPN deaktiviert";
                }
                onlinePlayersLabel.Text = "";
            }
        }

        private void onlinePlayersBtn_MouseDown(object sender, MouseEventArgs e)
        {
            refreshOnlinePlayersBtn.BackgroundImage = (System.Drawing.Image)(Properties.Resources.refresh2);
            refreshOnlinePlayersBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }

        private void refreshOnlinePlayersBtn_MouseUp(object sender, MouseEventArgs e)
        {
            refreshOnlinePlayersBtn.BackgroundImage = (System.Drawing.Image)(Properties.Resources.refresh);
            refreshOnlinePlayersBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }

        private void onlinePlayersBtn_MouseEnter(object sender, EventArgs e)
        {
            refreshOnlinePlayersBtn.BackgroundImage = (System.Drawing.Image)(Properties.Resources.refresh_tr);
            refreshOnlinePlayersBtn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            refreshOnlinePlayersBtn.FlatAppearance.MouseDownBackColor = Color.Transparent;
            refreshOnlinePlayersBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }

        private void onlinePlayersBtn_MouseLeave(object sender, EventArgs e)
        {
            refreshOnlinePlayersBtn.BackgroundImage = (System.Drawing.Image)(Properties.Resources.refresh);
            refreshOnlinePlayersBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }

        private void button17_MouseEnter(object sender, EventArgs e)
        {
            button17.BackgroundImage = (System.Drawing.Image)(Properties.Resources.min11);
        }

        private void button17_MouseLeave(object sender, EventArgs e)
        {
            button17.BackgroundImage = (System.Drawing.Image)(Properties.Resources.min);
        }

        private void button18_MouseEnter(object sender, EventArgs e)
        {
            button18.BackgroundImage = (System.Drawing.Image)(Properties.Resources.exit11);
        }

        private void button18_MouseLeave(object sender, EventArgs e)
        {
            button18.BackgroundImage = (System.Drawing.Image)(Properties.Resources.exit1);
        }
    }
}
