using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

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

//        public static string playersOnlineLabel_PassFromForm2;

        //public string LabelText
        //{
        //    get
        //    {
        //        return this.playersOnlineLabel.Text;
        //    }
        //    set
        //    {
        //        this.playersOnlineLabel.Text = value;
        //    }
        //}

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
