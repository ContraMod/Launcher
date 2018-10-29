using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Timers;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;

namespace Contra
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.InitializeComponent();
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
            button1.TabStop = false;
            button2.TabStop = false;
            button3.TabStop = false;
            button5.TabStop = false;
            button6.TabStop = false;
            button17.TabStop = false;
            button18.TabStop = false;
            buttonChat.TabStop = false;
            helpbutton.TabStop = false;
            websitebutton.TabStop = false;
            RadioLocQuotes.TabStop = false;
            RadioEN.TabStop = false;
            MNew.TabStop = false;
            DefaultPics.TabStop = false;
            QSCheckBox.TabStop = false;
            WinCheckBox.TabStop = false;
            buttonVPNstart.TabStop = false;
            VPNMoreButton.TabStop = false;
            radioFlag_GB.TabStop = false;
            radioFlag_RU.TabStop = false;
            radioFlag_UA.TabStop = false;
            radioFlag_BG.TabStop = false;
            refreshOnlinePlayersBtn.TabStop = false;
            vpn_start.FlatAppearance.MouseOverBackColor = Color.Transparent;
            vpn_start.FlatAppearance.MouseDownBackColor = Color.Transparent;
            vpn_start.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            refreshOnlinePlayersBtn.Hide();
            whoIsOnline.Hide();
            if (Globals.GB_Checked == true)
            {
                Properties.Settings.Default.IP_Label = "ContraVPN IP: unknown";
            }
            else if (Globals.RU_Checked == true)
            {
                Properties.Settings.Default.IP_Label = "ContraVPN IP: ����������";
            }
            else if (Globals.UA_Checked == true)
            {
                Properties.Settings.Default.IP_Label = "ContraVPN IP: �������";
            }
            else if (Globals.BG_Checked == true)
            {
                Properties.Settings.Default.IP_Label = "ContraVPN IP: ����������";
            }
            else if (Globals.DE_Checked == true)
            {
                Properties.Settings.Default.IP_Label = "ContraVPN IP: unbekannt";
            }

            //Determine OS bitness
            if (IntPtr.Size == 8)
            {
                Globals.userOS = "64";
            }
            else
            {
                Globals.userOS = "32";
            }

            delTmpChunk();

            //Delete tinc.log if file size > 5 MB
            Process tincLog = new Process();
            tincLog.StartInfo.FileName = Environment.CurrentDirectory + @"\contra\vpn\tinc.log";
            if (File.Exists(Environment.CurrentDirectory + @"\contra\vpn\tinc.log"))
            {
                double fileSize = new FileInfo(tincLog.StartInfo.FileName).Length;
                if ((fileSize / 1048576.0) > 5)
                {
                    File.Delete(Environment.CurrentDirectory + @"\contra\vpn\tinc.log");
                }
            }

            //Load DiscordRPC.dll
            if (File.Exists((@"contra\DiscordRPC.dll")))
            {
                //Assembly assembly = Assembly.LoadFrom("DiscordRPC.dll");
                Assembly assembly = Assembly.LoadFrom(@"contra\DiscordRPC.dll");
                Type type = assembly.GetType("DiscordRPC.Program");
                object instance = Activator.CreateInstance(type);
                MethodInfo[] methods = type.GetMethods();
            }
        }

        string newVersion = "";

        public void DownloadUpdate()
        {
            //URL of the updated file
            string url = "https://github.com/ThePredatorBG/contra-launcher/raw/master/Contra/bin/Release/Contra_Launcher.exe";

            //Declare new WebClient object
            WebClient wc = new WebClient();
            wc.DownloadFileCompleted += new AsyncCompletedEventHandler(wc_DownloadFileCompleted);
            wc.DownloadFileAsync(new Uri(url), Application.StartupPath + "/ContraLauncher(1).exe");
        }

        void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            //Show a message when the download has completed
            MessageBox.Show("Your application is now up-to-date!\n\nThe application will now restart!", "Update Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Restart();
        }

        //Create method to check for an update
        public void GetUpdate()
        {
            //Declare new WebClient object
            WebClient wc = new WebClient();
            string textFile = wc.DownloadString("https://gist.githubusercontent.com/ThePredatorBG/65e9e36d85c5def6adf7a0a5c73fb15a/raw/gistfile1.txt");
            string versionText = textFile.Substring(textFile.LastIndexOf("Version: ") + 9);
            string versionText2 = versionText.Substring(0, versionText.IndexOf("#"));
            //    ThreadHelperClass.SetText(this, verLabel, versionText2); //setting verLabel to latest ver - unused
            newVersion = versionText2;

            //verLabel.Text = verLabel.Text + Application.ProductVersion;
            //verLabel.Text = verLabel.Text + newVersion;

            //If there is a new version, call the DownloadUpdate method
            if (newVersion != Application.ProductVersion)
            {
                MessageBox.Show("Contra Launcher version " + versionText2 + " is available! Click OK to update and restart!", "Update Available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DownloadUpdate();
            }
        }

        public static string userDataLeafName()
        {
            var o = string.Empty;
            var userDataRegistryPath = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Electronic Arts\EA Games\Command and Conquer Generals Zero Hour");
            if (userDataRegistryPath != null)
            {
                o = userDataRegistryPath.GetValue("UserDataLeafName") as string;
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


        public static bool wait = true;

        public static bool adapterInstalled = false;


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


        private void checkInstallDir()
        {
            if (!Environment.CurrentDirectory.Contains("generals.exe"))
            {
                if (Globals.GB_Checked == true)
                {
                    MessageBox.Show("You have installed Contra in the wrong folder. Install it in the Zero Hour folder which contains the \"generals.exe\".", "Error");
                }
                else if (Globals.RU_Checked == true)
                {
                    MessageBox.Show("�� ���������� Contra � ������������ �����. ���������� ��� � ����� Zero Hour, ������� �������� ���� \"generals.exe\".", "������");
                }
                else if (Globals.UA_Checked == true)
                {
                    MessageBox.Show("�� ���������� Contra � ������������ �����. ��������� �� � ����� Zero Hour, ��� ������ \"generals.exe\".", "�������");
                }
                else if (Globals.BG_Checked == true)
                {
                    MessageBox.Show("����������� ��� Contra � �������� �����. ������������ � Zero Hour �������, ����� ������� \"generals.exe\".", "������");
                }
                else if (Globals.DE_Checked == true)
                {
                    MessageBox.Show("Du hast Contra im falschen ordner installiert. Installiere es in dem Zero Hour ordner in dem die \"generals.exe\" ist.", "Fehler");
                }
            }
        }

        private void delTmpChunk()
        {
            if (File.Exists(userDataLeafName() + "_tmpChunk.dat"))
            {
                File.Delete(userDataLeafName() + "_tmpChunk.dat");
            }
            else if (File.Exists(myDocPath + "_tmpChunk.dat"))
            {
                File.Delete(myDocPath + "_tmpChunk.dat");
            }
        }

        public static string StartGenerals;
        private void StartFile(string FileName)
        {
            try
            {
                Process generals = new Process();
                generals.StartInfo.FileName = "generals.exe";
                generals.StartInfo.Verb = "runas";
                if (File.Exists("generals.exe"))
                {
                    generals.StartInfo.WorkingDirectory = Path.GetDirectoryName("generals.exe");
                }
                else if (File.Exists(@"..\generals.exe"))
                {
                    generals.StartInfo.WorkingDirectory = Path.GetDirectoryName(@"..\generals.exe");
                }
                generals.Start();
            }
            catch
            {
                checkInstallDir();
            }
        }

        private void StartFileQS(string FileName)
        {
            try
            {
                Process generals = new Process();
                generals.StartInfo.FileName = "generals.exe";
                generals.StartInfo.Arguments = "-quickstart";
                generals.StartInfo.Verb = "runas";
                if (File.Exists("generals.exe"))
                {
                    generals.StartInfo.WorkingDirectory = Path.GetDirectoryName("generals.exe");
                }
                else if (File.Exists(@"..\generals.exe"))
                {
                    generals.StartInfo.WorkingDirectory = Path.GetDirectoryName(@"..\generals.exe");
                }
                generals.Start();
            }
            catch
            {
                checkInstallDir();
            }
        }

        private void StartFileWin(string FileName)
        {
            try
            {
                Process generals = new Process();
                generals.StartInfo.FileName = "generals.exe";
                generals.StartInfo.Arguments = "-win";
                generals.StartInfo.Verb = "runas";
                if (File.Exists("generals.exe"))
                {
                    generals.StartInfo.WorkingDirectory = Path.GetDirectoryName("generals.exe");
                }
                else if (File.Exists(@"..\generals.exe"))
                {
                    generals.StartInfo.WorkingDirectory = Path.GetDirectoryName(@"..\generals.exe");
                }
                generals.Start();
            }
            catch
            {
                checkInstallDir();
            }
        }

        private void StartFileWinQS(string FileName)
        {
            try
            {
                Process generals = new Process();
                generals.StartInfo.FileName = "generals.exe";
                generals.StartInfo.Arguments = "-win -quickstart";
                generals.StartInfo.Verb = "runas";
                if (File.Exists("generals.exe"))
                {
                    generals.StartInfo.WorkingDirectory = Path.GetDirectoryName("generals.exe");
                }
                else if (File.Exists(@"..\generals.exe"))
                {
                    generals.StartInfo.WorkingDirectory = Path.GetDirectoryName(@"..\generals.exe");
                }
                generals.Start();
            }
            catch
            {
                checkInstallDir();
            }
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.BackgroundImage = (System.Drawing.Image)(Properties.Resources._button_exit_text);
            button2.ForeColor = SystemColors.ButtonHighlight;
            button2.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackgroundImage = (System.Drawing.Image)(Properties.Resources._button_exit);
            button2.ForeColor = SystemColors.ButtonHighlight;
            button2.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            button2.BackgroundImage = (System.Drawing.Image)(Properties.Resources._button_highlight);
            button2.ForeColor = SystemColors.ButtonHighlight;
            button2.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }

        private void button2_Click(object sender, EventArgs e) //ExitButton
        {
            string wb = "worldbuilder_ctr.exe";
            Process[] wbByName = Process.GetProcessesByName(wb.Substring(0, wb.LastIndexOf('.')));
            if (wbByName.Length > 0) //if wb is already running
            {
                if (Globals.GB_Checked == true)
                {
                    MessageBox.Show("Mod files could not be unloaded since they are currently in use by World Builder. If you want to unload mod files, close World Builder and run the launcher again. Closing the launcher anyway.", "Error");
                }
                else if (Globals.RU_Checked == true)
                {
                    MessageBox.Show("����� ���� �� ����� ���� ���������, ��� ��� ��� � ��������� ����� ������������ World Builder. ���� �� ������ ��������� ����� ����, �������� World Builder � ����� ��������� �������. �������� �������� � ����� ������.", "������");
                }
                else if (Globals.UA_Checked == true)
                {
                    MessageBox.Show("����� ���� �� ����� ���� ������������, ������� ���� � ����� ��� ���������������� World Builder. ���� �� ������ ����������� ����� ����, �������� World Builder � ����� �������� �������. �������� �������� � ����-����� �������.", "�������");
                }
                else if (Globals.BG_Checked == true)
                {
                    MessageBox.Show("Contra ��������� �� ������ �� ����� ������������, ��� ���� �� ��������� �� World Builder. ��� ������ �� ������������ Contra, ��������� World Builder � ����������� launcher-� ������.", "������");
                }
                else if (Globals.DE_Checked == true)
                {
                    MessageBox.Show("Mod dateien konnten nicht entladen werden, da sie momentan im World Builder benutzt werden. Falls du die mod dateien entladen wilst, schlie�e den World Builder und starte den Launcher erneut. Schlie�t den Launcher sowieso.", "Fehler");
                }
            }
            else
            {
                //nothing
            }
            OnApplicationExit(sender, e);
        }

        private static void renameBigToCtr()
        {
            try
            {
                List<string> bigs = new List<string>
                {
                    "!!Contra009Final_FogOff.big",
                    "!!Contra009Final_FunnyGenPics.big",
                    "!Contra009Beta2.big",
                    "!Contra009Beta2VOrig.big",
                    "!Contra009Beta2VLoc.big",
                    "!Contra009Beta2MNew.big",
                    "!Contra009Beta2EN.big",
                    "!Contra009Beta2RU.big",
                };
                foreach (string big in bigs)
                {
                    string ctr = big.Replace(".big", ".ctr");
                    if (File.Exists(big))
                    {
                        File.Move(big, ctr);
                    }
                }
                //if (File.Exists("!Contra009Beta2.big"))
                //{
                //    File.Move("!Contra009Beta2.big", "!Contra009Beta2.ctr");
                //}
                //if (File.Exists("!Contra009Beta2VOrig.big"))
                //{
                //    File.Move("!Contra009Beta2VOrig.big", "!Contra009Beta2VOrig.ctr");
                //}
                //if (File.Exists("!Contra009Beta2VLoc.big"))
                //{
                //    File.Move("!Contra009Beta2VLoc.big", "!Contra009Beta2VLoc.ctr");
                //}
                //if (File.Exists("!Contra009Beta2EN.big"))
                //{
                //    File.Move("!Contra009Beta2EN.big", "!Contra009Beta2EN.ctr");
                //}
                //if (File.Exists("!Contra009Beta2RU.big"))
                //{
                //    File.Move("!Contra009Beta2RU.big", "!Contra009Beta2RU.ctr");
                //}
                //if (File.Exists("!Contra009Beta2MNew.big"))
                //{
                //    File.Move("!Contra009Beta2MNew.big", "!Contra009Beta2MNew.ctr");
                //}
                //if (File.Exists("!!Contra009Final_FogOff.big"))
                //{
                //    File.Move("!!Contra009Final_FogOff.big", "!!Contra009Final_FogOff.ctr");
                //}
                //if (File.Exists("!!Contra009Final_FunnyGenPics.big"))
                //{
                //    File.Move("!!Contra009Final_FunnyGenPics.big", "!!Contra009Final_FunnyGenPics.ctr");
                //}
                if ((File.Exists("langdata1.dat")))
                {
                    File.Move("langdata1.dat", "langdata.dat");
                }
                if (Directory.Exists(@"Data\Scripts1"))
                {
                    Directory.Move(@"Data\Scripts1", @"Data\Scripts");
                }
                //if (File.Exists("Install_Final.bmp") && File.Exists("Install_Final_ZHC.bmp"))
                //{
                //    File.Move("Install_Final.bmp", "Install_Final_Contra.bmp");
                //    File.Move("Install_Final_ZHC.bmp", "Install_Final.bmp");
                //}
                //if (File.Exists("Install_Final_Contra.bmp") && File.Exists("Install_Final_ZHC.bmp"))
                //{
                //    File.Move("Install_Final_ZHC.bmp", "Install_Final.bmp");
                //}

                if (File.Exists("Install_Final_ZH.bmp"))
                {
                    try
                    {
                        File.SetAttributes("Install_Final.bmp", FileAttributes.Normal);
                        File.SetAttributes("Install_Final_ZH.bmp", FileAttributes.Normal);
                        File.SetAttributes("Install_Final_Contra.bmp", FileAttributes.Normal);
                        File.Copy("Install_Final_ZH.bmp", "Install_Final.bmp", true);
                    }
                    catch
                    { }
                }
            }
            catch //(Exception ex)
            {

            }
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackgroundImage = (System.Drawing.Image)(Properties.Resources._button_launch_text);
            button1.ForeColor = SystemColors.ButtonHighlight;
            button1.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackgroundImage = (System.Drawing.Image)(Properties.Resources._button_launch);
            button1.ForeColor = SystemColors.ButtonHighlight;
            button1.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            button1.BackgroundImage = (System.Drawing.Image)(Properties.Resources._button_highlight);
            button1.ForeColor = SystemColors.ButtonHighlight;
            button1.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }

        public void isWbRunning()
        {
            string wb = "worldbuilder_ctr.exe";
            Process[] wbByName = Process.GetProcessesByName(wb.Substring(0, wb.LastIndexOf('.')));
            if (wbByName.Length > 0) //if wb is already running
            {
                if (Globals.GB_Checked == true)
                {
                    MessageBox.Show("Unit voice preferences and Gentool may not load correctly since World Builder is already running. Starting Contra anyway.", "Error");
                }
                else if (Globals.RU_Checked == true)
                {
                    MessageBox.Show("��������� ���������� ������ � Gentool ����� ����������� �����������, ��� ��� World Builder ��� �������. ������ Contra � ����� ������.", "������");
                }
                else if (Globals.UA_Checked == true)
                {
                    MessageBox.Show("������ ����� �� Gentool ������ ��������������� �� ���������, ������� World Builder ��� ������. ������ Contra � ����-����� �������.", "�������");
                }
                else if (Globals.BG_Checked == true)
                {
                    MessageBox.Show("������, �� ����� ������� ��������� � Gentool ���� �� �� ������� ��������, ��� ���� World Builder � ���������. Contra �� �������� ������� ����.", "������");
                }
                else if (Globals.DE_Checked == true)
                {
                    MessageBox.Show("Einheit Sprach Pr�ferenzen und Gentool laden eventuell nicht korrekt, weil der World Builder schon l�uft. Contra wird ohnehin gestartet.", "Fehler");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) //LaunchButton
        {
            renameBigToCtr();
            try
            {
                if (File.Exists("!Contra009Beta2.ctr"))
                {
                    File.Move("!Contra009Beta2.ctr", "!Contra009Beta2.big");
                }
                if ((RadioOrigQuotes.Checked) && (File.Exists("!Contra009Beta2VOrig.ctr")))
                {
                    File.Move("!Contra009Beta2VOrig.ctr", "!Contra009Beta2VOrig.big");
                }
                if ((RadioLocQuotes.Checked) && (File.Exists("!Contra009Beta2VLoc.ctr")))
                {
                    File.Move("!Contra009Beta2VLoc.ctr", "!Contra009Beta2VLoc.big");
                }
                if ((RadioEN.Checked) && (File.Exists("!Contra009Beta2EN.ctr")))
                {
                    File.Move("!Contra009Beta2EN.ctr", "!Contra009Beta2EN.big");
                }
                if ((RadioRU.Checked) && (File.Exists("!Contra009Beta2RU.ctr")))
                {
                    File.Move("!Contra009Beta2RU.ctr", "!Contra009Beta2RU.big");
                }
                if ((MNew.Checked) && (File.Exists("!Contra009Beta2MNew.ctr")))
                {
                    File.Move("!Contra009Beta2MNew.ctr", "!Contra009Beta2MNew.big");
                }
                if ((Properties.Settings.Default.Fog == false) && (File.Exists("!!Contra009Final_FogOff.ctr")))
                {
                    File.Move("!!Contra009Final_FogOff.ctr", "!!Contra009Final_FogOff.big");
                }
                else if ((Properties.Settings.Default.Fog == true) && (File.Exists("!!Contra009Final_FogOff.big")))
                {
                    File.Move("!!Contra009Final_FogOff.big", "!!Contra009Final_FogOff.ctr");
                }
                if ((GoofyPics.Checked) && (File.Exists("!!Contra009Final_FunnyGenPics.ctr")))
                {
                    File.Move("!!Contra009Final_FunnyGenPics.ctr", "!!Contra009Final_FunnyGenPics.big");
                }
                else if ((!GoofyPics.Checked) && (File.Exists("!!Contra009Final_FunnyGenPics.big")))
                {
                    File.Move("!!Contra009Final_FunnyGenPics.big", "!!Contra009Final_FunnyGenPics.ctr");
                }
                if ((Properties.Settings.Default.LangF == false) && (File.Exists("langdata.dat")))
                {
                    File.Move("langdata.dat", "langdata1.dat");
                }
                else if ((Properties.Settings.Default.LangF == true) && (File.Exists("langdata1.dat")))
                {
                    File.Move("langdata1.dat", "langdata.dat");
                }
                if (Directory.Exists(@"Data\Scripts"))
                {
                    Directory.Move(@"Data\Scripts", @"Data\Scripts1");
                }
                //if (File.Exists("Install_Final_Contra.bmp") && File.Exists("Install_Final_ZHC.bmp") && File.Exists("Install_Final.bmp"))
                //{
                //    File.Move("Install_Final.bmp", "Install_Final_ZHC.bmp");
                //    File.Move("Install_Final_Contra.bmp", "Install_Final.bmp");
                //}
                //if (File.Exists("Install_Final_Contra.bmp") && File.Exists("Install_Final.bmp"))
                //{
                //    File.Move("Install_Final.bmp", "Install_Final_ZHC.bmp");
                //    File.Move("Install_Final_Contra.bmp", "Install_Final.bmp");
                //}
                if (File.Exists("Install_Final.bmp") && (File.Exists("Install_Final_Contra.bmp")))
                {
                    try
                    {
                        File.SetAttributes("Install_Final.bmp", FileAttributes.Normal);
                        File.SetAttributes("Install_Final_ZH.bmp", FileAttributes.Normal);
                        File.SetAttributes("Install_Final_Contra.bmp", FileAttributes.Normal);
                        File.Copy("Install_Final.bmp", "Install_Final_ZH.bmp", true);
                        File.Copy("Install_Final_Contra.bmp", "Install_Final.bmp", true);
                    }
                    catch { }
                }
                if (WinCheckBox.Checked && QSCheckBox.Checked)
                {
                    isWbRunning();
                    this.StartFileWinQS(StartGenerals);
                }
                if (QSCheckBox.Checked && WinCheckBox.Checked == false)
                {
                    isWbRunning();
                    this.StartFileQS(StartGenerals);
                }
                if (WinCheckBox.Checked && QSCheckBox.Checked == false)
                {
                    isWbRunning();
                    this.StartFileWin(StartGenerals);
                }
                if (WinCheckBox.Checked == false && QSCheckBox.Checked == false)
                {
                    isWbRunning();
                    this.StartFile(StartGenerals);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            return;
        }

        private void websitebutton_MouseEnter(object sender, EventArgs e)
        {
            websitebutton.BackgroundImage = (System.Drawing.Image)(Properties.Resources._button_website_text);
            websitebutton.ForeColor = SystemColors.ButtonHighlight;
            websitebutton.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void websitebutton_MouseLeave(object sender, EventArgs e)
        {
            websitebutton.BackgroundImage = (System.Drawing.Image)(Properties.Resources._button_website);
            websitebutton.ForeColor = SystemColors.ButtonHighlight;
            websitebutton.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void websitebutton_MouseDown(object sender, MouseEventArgs e)
        {
            websitebutton.BackgroundImage = (System.Drawing.Image)(Properties.Resources._button_highlight);
            websitebutton.ForeColor = SystemColors.ButtonHighlight;
            websitebutton.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void websitebutton_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(@"contra\website.url");
                //Process.Start("https://contra.cncguild.net/oldsite/Eng/index.php");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void button5_MouseEnter(object sender, EventArgs e)
        {
            button5.BackgroundImage = (System.Drawing.Image)(Properties.Resources._button_moddb_text);
            button5.ForeColor = SystemColors.ButtonHighlight;
            button5.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void button5_MouseLeave(object sender, EventArgs e)
        {
            button5.BackgroundImage = (System.Drawing.Image)(Properties.Resources._button_moddb);
            button5.ForeColor = SystemColors.ButtonHighlight;
            button5.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void button5_MouseDown(object sender, MouseEventArgs e)
        {
            button5.BackgroundImage = (System.Drawing.Image)(Properties.Resources._button_highlight);
            button5.ForeColor = SystemColors.ButtonHighlight;
            button5.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }

        private void button5_Click(object sender, EventArgs e) //ModDBButton
        {
            try
            {
                Process.Start(@"contra\moddb.url");
                //Process.Start("https://www.moddb.com/mods/contra");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.BackgroundImage = (System.Drawing.Image)(Properties.Resources._button_readme_text);
            button3.ForeColor = SystemColors.ButtonHighlight;
            button3.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackgroundImage = (System.Drawing.Image)(Properties.Resources._button_readme);
            button3.ForeColor = SystemColors.ButtonHighlight;
            button3.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            button3.BackgroundImage = (System.Drawing.Image)(Properties.Resources._button_highlight);
            button3.ForeColor = SystemColors.ButtonHighlight;
            button3.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }

        private void button3_Click(object sender, EventArgs e) //ReadMeButton
        {
            try
            {
                Process.Start("Readme_Contra.txt");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void button6_MouseEnter(object sender, EventArgs e)
        {
            button6.BackgroundImage = (System.Drawing.Image)(Properties.Resources._button_wb_text);
            button6.ForeColor = SystemColors.ButtonHighlight;
            button6.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void button6_MouseLeave(object sender, EventArgs e)
        {
            button6.BackgroundImage = (System.Drawing.Image)(Properties.Resources._button_wb);
            button6.ForeColor = SystemColors.ButtonHighlight;
            button6.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void button6_MouseDown(object sender, MouseEventArgs e)
        {
            button6.BackgroundImage = (System.Drawing.Image)(Properties.Resources._button_highlight);
            button6.ForeColor = SystemColors.ButtonHighlight;
            button6.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void button6_Click(object sender, EventArgs e) //WorldBuilder
        {
            if (File.Exists("!Contra009Beta2.ctr"))
            {
                File.Move("!Contra009Beta2.ctr", "!Contra009Beta2.big");
            }
            else if (File.Exists(@"..\!Contra009Beta2.ctr"))
            {
                File.Move(@"..\!Contra009Beta2.ctr", @"..\!Contra009Beta2.big");
            }
            if (File.Exists("!Contra009Beta2EN.ctr"))
            {
                File.Move("!Contra009Beta2EN.ctr", "!Contra009Beta2EN.big");
            }
            else if (File.Exists(@"..\!Contra009Beta2EN.ctr"))
            {
                File.Move(@"..\!Contra009Beta2EN.ctr", @"..\!Contra009Beta2EN.big");
            }
            if (File.Exists("!Contra009Beta2VLoc.ctr"))
            {
                File.Move("!Contra009Beta2VLoc.ctr", "!Contra009Beta2VLoc.big");
            }
            else if (File.Exists(@"..\!Contra009Beta2VLoc.ctr"))
            {
                File.Move(@"..\!Contra009Beta2VLoc.ctr", @"..\!Contra009Beta2VLoc.big");
            }
            Process wb = new Process();
            wb.StartInfo.Verb = "runas";
            try
            {
                if (File.Exists("WorldBuilder_Ctr.exe"))
                {
                    wb.StartInfo.FileName = "WorldBuilder_Ctr.exe";
                    wb.StartInfo.WorkingDirectory = Path.GetDirectoryName("WorldBuilder_Ctr.exe");
                    wb.Start();
                }
                else if (File.Exists(@"..\WorldBuilder_Ctr.exe"))
                {
                    wb.StartInfo.FileName = "WorldBuilder_Ctr.exe";
                    wb.StartInfo.WorkingDirectory = Path.GetDirectoryName(@"..\WorldBuilder_Ctr.exe");
                    wb.Start();
                }
                else if (File.Exists("WorldBuilder.exe"))
                {
                    wb.StartInfo.FileName = "WorldBuilder.exe";
                    wb.StartInfo.WorkingDirectory = Path.GetDirectoryName("WorldBuilder.exe");
                    wb.Start();
                }
                else if (File.Exists(@"..\WorldBuilder.exe"))
                {
                    wb.StartInfo.FileName = "WorldBuilder.exe";
                    wb.StartInfo.WorkingDirectory = Path.GetDirectoryName(@"..\WorldBuilder.exe");
                    wb.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            return;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            //ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
            //resources.ApplyResources(this, "$this");
            //applyResourcesEN(resources, this.Controls);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ru-RU");
            //ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
            //resources.ApplyResources(this, "$this");
            //applyResourcesRU(resources, this.Controls);
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.UpgradeRequired)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.UpgradeRequired = false;
                Properties.Settings.Default.Save();
            }
            RadioEN.Checked = Properties.Settings.Default.LangEN;
            RadioRU.Checked = Properties.Settings.Default.LangRU;
            MNew.Checked = Properties.Settings.Default.MusicNew;
            MStandard.Checked = Properties.Settings.Default.MusicStandard;
            RadioOrigQuotes.Checked = Properties.Settings.Default.VoNew;
            RadioLocQuotes.Checked = Properties.Settings.Default.VoStandard;
            QSCheckBox.Checked = Properties.Settings.Default.Quickstart;
            WinCheckBox.Checked = Properties.Settings.Default.Windowed;
            DefaultPics.Checked = Properties.Settings.Default.GenPicDef;
            GoofyPics.Checked = Properties.Settings.Default.GenPicGoo;
            radioFlag_GB.Checked = Properties.Settings.Default.Flag_GB;
            radioFlag_RU.Checked = Properties.Settings.Default.Flag_RU;
            radioFlag_UA.Checked = Properties.Settings.Default.Flag_UA;
            radioFlag_BG.Checked = Properties.Settings.Default.Flag_BG;
            radioFlag_DE.Checked = Properties.Settings.Default.Flag_DE;
            AutoScaleMode = AutoScaleMode.Dpi;

            string tincd = "tincd.exe";
            Process[] tincdsByName = Process.GetProcessesByName(tincd.Substring(0, tincd.LastIndexOf('.')));
            if (tincdsByName.Length > 0)
            {
                foreach (Process tincdByName in tincdsByName)
                {
                    tincdByName.Kill();
                    tincdByName.WaitForExit();
                    tincdByName.Dispose();
                }
                if (Globals.GB_Checked == true)
                {
                    Properties.Settings.Default.IP_Label = "ContraVPN IP: unknown";
                    labelVpnStatus.Text = "Off";
                }
                else if (Globals.RU_Checked == true)
                {
                    labelVpnStatus.Text = "����.";
                    Properties.Settings.Default.IP_Label = "ContraVPN IP: ����������";
                }
                else if (Globals.UA_Checked == true)
                {
                    labelVpnStatus.Text = "����.";
                    Properties.Settings.Default.IP_Label = "ContraVPN IP: �������";
                }
                else if (Globals.BG_Checked == true)
                {
                    labelVpnStatus.Text = "����.";
                    Properties.Settings.Default.IP_Label = "ContraVPN IP: ����������";
                }
                else if (Globals.DE_Checked == true)
                {
                    labelVpnStatus.Text = "Aus";
                    Properties.Settings.Default.IP_Label = "ContraVPN IP: unbekannt";
                }
            }
        }

        private void OnApplicationExit(object sender, EventArgs e) //AppExit
        {
            renameBigToCtr();
            Properties.Settings.Default.LangEN = RadioEN.Checked;
            Properties.Settings.Default.LangRU = RadioRU.Checked;
            Properties.Settings.Default.MusicNew = MNew.Checked;
            Properties.Settings.Default.MusicStandard = MStandard.Checked;
            Properties.Settings.Default.VoNew = RadioOrigQuotes.Checked;
            Properties.Settings.Default.VoStandard = RadioLocQuotes.Checked;
            Properties.Settings.Default.Quickstart = QSCheckBox.Checked;
            Properties.Settings.Default.Windowed = WinCheckBox.Checked;
            Properties.Settings.Default.GenPicDef = DefaultPics.Checked;
            Properties.Settings.Default.GenPicGoo = GoofyPics.Checked;
            Properties.Settings.Default.Flag_GB = radioFlag_GB.Checked;
            Properties.Settings.Default.Flag_RU = radioFlag_RU.Checked;
            Properties.Settings.Default.Flag_UA = radioFlag_UA.Checked;
            Properties.Settings.Default.Flag_BG = radioFlag_BG.Checked;
            Properties.Settings.Default.Flag_DE = radioFlag_DE.Checked;
            Properties.Settings.Default.Save();

            delTmpChunk();

            Process[] vpnprocesses = Process.GetProcessesByName("tincd");
            foreach (Process vpnprocess in vpnprocesses)
            {
                vpnprocess.Kill();
                vpnprocess.WaitForExit();
                vpnprocess.Dispose();
                //vpn_start.BackgroundImage = (System.Drawing.Image)(Properties.Resources.vpn_off);
                //labelVpnStatus.Text = "Off";
            }
            if (Globals.GB_Checked == true)
            {
                Properties.Settings.Default.IP_Label = "ContraVPN IP: unknown";
            }
            else if (Globals.RU_Checked == true)
            {
                Properties.Settings.Default.IP_Label = "ContraVPN IP: ����������";
            }
            else if (Globals.UA_Checked == true)
            {
                Properties.Settings.Default.IP_Label = "ContraVPN IP: �������";
            }
            else if (Globals.BG_Checked == true)
            {
                Properties.Settings.Default.IP_Label = "ContraVPN IP: ����������";
            }
            else if (Globals.DE_Checked == true)
            {
                Properties.Settings.Default.IP_Label = "ContraVPN IP: unbekannt";
            }

            //This renames the original file so any shortcut works and names it accordingly after the update
            if (System.IO.File.Exists(Application.StartupPath + "/Contra_Launcher(1).exe"))
            {
                System.IO.File.Move(Application.StartupPath + "/Contra_Launcher.exe", Application.StartupPath + "/Contra_Launcher(2).exe");
                System.IO.File.Move(Application.StartupPath + "/Contra_Launcher(1).exe", Application.StartupPath + "/Contra_Launcher.exe");
                System.IO.File.Delete(Application.StartupPath + "/Contra_Launcher(2).exe");
            }

            this.Close();
        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e) //MinimizeIcon
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button18_Click(object sender, EventArgs e) //ExitIcon
        {
            OnApplicationExit(sender, e);
        }

        private void buttonChat_MouseEnter(object sender, EventArgs e)
        {
            buttonChat.BackgroundImage = (System.Drawing.Image)(Properties.Resources._button_discord_text);
            buttonChat.ForeColor = SystemColors.ButtonHighlight;
            buttonChat.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void buttonChat_MouseLeave(object sender, EventArgs e)
        {
            buttonChat.BackgroundImage = (System.Drawing.Image)(Properties.Resources._button_discord);
            buttonChat.ForeColor = SystemColors.ButtonHighlight;
            buttonChat.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void buttonChat_MouseDown(object sender, MouseEventArgs e)
        {
            buttonChat.BackgroundImage = (System.Drawing.Image)(Properties.Resources._button_highlight);
            buttonChat.ForeColor = SystemColors.ButtonHighlight;
            buttonChat.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }

        private void buttonChat_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(@"contra\discord.url");
                //Process.Start("https://discordapp.com/invite/015E6KXXHmdWFXCtt");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void helpbutton_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(@"contra\mod-help.url");
                //Process.Start("https://contra.cncguild.net/oldsite/Eng/trouble.php");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void helpbutton_MouseDown_1(object sender, MouseEventArgs e)
        {
            helpbutton.BackgroundImage = (System.Drawing.Image)(Properties.Resources._button_highlight);
            helpbutton.ForeColor = SystemColors.ButtonHighlight;
            helpbutton.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void helpbutton_MouseEnter_1(object sender, EventArgs e)
        {
            helpbutton.BackgroundImage = (System.Drawing.Image)(Properties.Resources._button_help_text);
            helpbutton.ForeColor = SystemColors.ButtonHighlight;
            helpbutton.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void helpbutton_MouseLeave_1(object sender, EventArgs e)
        {
            helpbutton.BackgroundImage = (System.Drawing.Image)(Properties.Resources._button_help);
            helpbutton.ForeColor = SystemColors.ButtonHighlight;
            helpbutton.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }

        private void button6_Enter(object sender, EventArgs e)
        {
            button6.BackColor = System.Drawing.Color.Transparent;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void WinCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void voicespanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void GoofyPics_CheckedChanged(object sender, EventArgs e)
        {

        }

        //**********TINC CODE START**********

        public void vpnOff()
        {
            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    vpn_start.BackgroundImage = (System.Drawing.Image)(Properties.Resources.vpn_off);
                    refreshOnlinePlayersBtn.Hide();
                    whoIsOnline.Hide();
                    if (Globals.GB_Checked == true)
                    {
                        playersOnlineLabel.Text = "ContraVPN disabled";
                        labelVpnStatus.Text = "Off";
                        Properties.Settings.Default.IP_Label = "ContraVPN IP: unknown";
                    }
                    else if (Globals.RU_Checked == true)
                    {
                        playersOnlineLabel.Text = "ContraVPN ���������";
                        labelVpnStatus.Text = "����.";
                        Properties.Settings.Default.IP_Label = "ContraVPN IP: ����������";
                    }
                    else if (Globals.UA_Checked == true)
                    {
                        playersOnlineLabel.Text = "ContraVPN ��������";
                        labelVpnStatus.Text = "����.";
                        Properties.Settings.Default.IP_Label = "ContraVPN IP: �������";
                    }
                    else if (Globals.BG_Checked == true)
                    {
                        playersOnlineLabel.Text = "ContraVPN ��������";
                        labelVpnStatus.Text = "����.";
                        Properties.Settings.Default.IP_Label = "ContraVPN IP: ����������";
                    }
                    else if (Globals.DE_Checked == true)
                    {
                        playersOnlineLabel.Text = "ContraVPN deaktiviert";
                        labelVpnStatus.Text = "Aus";
                        Properties.Settings.Default.IP_Label = "ContraVPN IP: unbekannt";
                    }
                }));
                return;
            }
        }

        public void vpnOffNoCond()
        {
            {
                vpn_start.BackgroundImage = (System.Drawing.Image)(Properties.Resources.vpn_off);
                refreshOnlinePlayersBtn.Hide();
                whoIsOnline.Hide();
                if (Globals.GB_Checked == true)
                {
                    playersOnlineLabel.Text = "ContraVPN disabled";
                    labelVpnStatus.Text = "Off";
                    Properties.Settings.Default.IP_Label = "ContraVPN IP: unknown";
                }
                else if (Globals.RU_Checked == true)
                {
                    playersOnlineLabel.Text = "ContraVPN ���������";
                    labelVpnStatus.Text = "����.";
                    Properties.Settings.Default.IP_Label = "ContraVPN IP: ����������";
                }
                else if (Globals.UA_Checked == true)
                {
                    playersOnlineLabel.Text = "ContraVPN ��������";
                    labelVpnStatus.Text = "����.";
                    Properties.Settings.Default.IP_Label = "ContraVPN IP: �������";
                }
                else if (Globals.BG_Checked == true)
                {
                    playersOnlineLabel.Text = "ContraVPN ��������";
                    labelVpnStatus.Text = "����.";
                    Properties.Settings.Default.IP_Label = "ContraVPN IP: ����������";
                }
                else if (Globals.DE_Checked == true)
                {
                    playersOnlineLabel.Text = "ContraVPN deaktiviert";
                    labelVpnStatus.Text = "Aus";
                    Properties.Settings.Default.IP_Label = "ContraVPN IP: unbekannt";
                }
                return;
            }
        }

        public static void addFirewallExceptions()
        {
            Dictionary<string, string> exes = new Dictionary<string, string>
                        {
                            {"game.dat","udp"},
                            {"Contra_Launcher.exe","tcp"},
                            {@"contra\vpn\" + Globals.userOS + @"\tinc.exe","tcp"},
                            {@"contra\vpn\" + Globals.userOS + @"\tinc.exe","udp"},
                            {@"contra\vpn\" + Globals.userOS + @"\tincd.exe","tcp"},
                            {@"contra\vpn\" + Globals.userOS + @"\tincd.exe","udp"}
                        };
            Process netsh = new Process();
            netsh.StartInfo.FileName = "netsh.exe";
            netsh.StartInfo.UseShellExecute = false;
            netsh.StartInfo.RedirectStandardInput = true;
            netsh.StartInfo.RedirectStandardOutput = true;
            netsh.StartInfo.RedirectStandardError = true;
            netsh.StartInfo.CreateNoWindow = true;
            foreach (KeyValuePair<string,string> exe in exes)
            {
                string ExeWithoutExtension = exe.Key;
                string Protocol = exe.Value;
                int index = exe.Key.LastIndexOf(".");
                if (index > 0)
                {
                    ExeWithoutExtension = exe.Key.Substring(0, index);
                }

                netsh.StartInfo.Arguments = "advfirewall firewall show rule name=" + "\"Contra - " + ExeWithoutExtension + "\"";
                netsh.Start();
                netsh.WaitForExit();
                if (netsh.ExitCode != 0)
                {
                    netsh.StartInfo.Arguments = "advfirewall firewall add rule name=" + "\"Contra - " + ExeWithoutExtension + "\"" + " dir=in action=allow program=" + "\"" + Environment.CurrentDirectory + @"\" + exe.Key + "\"" + " protocol=" + Protocol + " enable=yes";
                    netsh.Start();
                    netsh.WaitForExit();
                }
            }
        }

        public void StartVPN()
        {
            Process tinc = new Process();
            tinc.StartInfo.Arguments = "--no-detach --config=\"" + Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Contra\\vpnconfig\\contravpn\" --debug=3 --pidfile=\"" + Environment.CurrentDirectory + "\\contra\\vpn\\tinc.pid\" --option=AddressFamily=ipv4 --option=Interface=ContraVPN";
            tinc.StartInfo.FileName = Globals.userOS + @"\tincd.exe";
            tinc.StartInfo.UseShellExecute = true;
            tinc.StartInfo.WorkingDirectory = Environment.CurrentDirectory + @"\contra\vpn";
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\contravpn\tinc.conf") && (File.Exists(@"contra\vpn\" + Globals.userOS + @"\tincd.exe")))
            {
                if (Globals.GB_Checked == true)
                {
                    playersOnlineLabel.Text = "Loading...";
                }
                else if (Globals.RU_Checked == true)
                {
                    playersOnlineLabel.Text = "��������...";
                }
                else if (Globals.UA_Checked == true)
                {
                    playersOnlineLabel.Text = "������������...";
                }
                else if (Globals.BG_Checked == true)
                {
                    playersOnlineLabel.Text = "������� ��...";
                }
                else if (Globals.DE_Checked == true)
                {
                    playersOnlineLabel.Text = "Lade...";
                }
                tinc.Start();

                //check when tinc gets turned off
                tinc.EnableRaisingEvents = true;
                tinc.Exited += (sender, e) =>
                {
                    vpnOff();
                };

                string tincd = "tincd.exe";
                Process[] tincdByName = Process.GetProcessesByName(tincd.Substring(0, tincd.LastIndexOf('.')));
                if (tincdByName.Length > 0)
                {
                    vpn_start.BackgroundImage = (System.Drawing.Image)(Properties.Resources.vpn_on);
                    refreshOnlinePlayersBtn.Show();
                    whoIsOnline.Show();
                    if (Globals.GB_Checked == true)
                    {
                        labelVpnStatus.Text = "On";
                    }
                    else if (Globals.RU_Checked == true)
                    {
                        labelVpnStatus.Text = "���.";
                    }
                    else if (Globals.UA_Checked == true)
                    {
                        labelVpnStatus.Text = "����.";
                    }
                    else if (Globals.BG_Checked == true)
                    {
                        labelVpnStatus.Text = "���.";
                    }
                    else if (Globals.DE_Checked == true)
                    {
                        labelVpnStatus.Text = "An";
                    }
                    refreshOnlinePlayersBtn.PerformClick();
                }
            }
            else if (!Directory.Exists(@"contra\vpn"))
            {
                if (Globals.GB_Checked == true)
                {
                    MessageBox.Show("Cannot start ContraVPN because \"contra\\vpn\" folder does not exist!", "Error");
                }
                else if (Globals.RU_Checked == true)
                {
                    MessageBox.Show("�� ������� ��������� ContraVPN, ������ ��� ����� \"contra\\vpn\" �� ����������!", "������");
                }
                else if (Globals.UA_Checked == true)
                {
                    MessageBox.Show("�� ������� ��������� ContraVPN, ������� ����� \"contra\\vpn\" �� ����!", "�������");
                }
                else if (Globals.BG_Checked == true)
                {
                    MessageBox.Show("ContraVPN �� ���� �� �� ��������, ������ \"contra\\vpn\" ������� �� ����������!", "������");
                }
                else if (Globals.DE_Checked == true)
                {
                    MessageBox.Show("ContraVPN kann nicht gestartet werden, weil der \"contra\\vpn\" ordner existiert nicht!", "Fehler");
                }
            }
            else if (!Directory.Exists(@"contra\vpn\" + Globals.userOS))
            {
                if (Globals.GB_Checked == true)
                {
                    MessageBox.Show("Cannot start ContraVPN because \"" + Globals.userOS + "\" folder was not found.", "Error");
                }
                else if (Globals.RU_Checked == true)
                {
                    MessageBox.Show("�� ������� ��������� ContraVPN, ������ ��� ����� \"" + Globals.userOS + "\" �� �������.", "������");
                }
                else if (Globals.UA_Checked == true)
                {
                    MessageBox.Show("�� ������� ��������� ContraVPN, ������� ����� \"" + Globals.userOS + "\" �� ��������.", "�������");
                }
                else if (Globals.BG_Checked == true)
                {
                    MessageBox.Show("ContraVPN �� ���� �� �� ��������, ������ ������� \"" + Globals.userOS + "\" �� ���� ��������.", "������");
                }
                else if (Globals.DE_Checked == true)
                {
                    MessageBox.Show("ContraVPN kann nicht gestartet werden, weil die \"" + Globals.userOS + "\" ordner nicht gefunden wurde.", "Fehler");
                }
            }
            else if (!File.Exists(@"contra\vpn\" + Globals.userOS + @"\tincd.exe"))
            {
                if (Globals.GB_Checked == true)
                {
                    MessageBox.Show("Cannot start ContraVPN because \"tincd.exe\" file was not found.", "Error");
                }
                else if (Globals.RU_Checked == true)
                {
                    MessageBox.Show("�� ������� ��������� ContraVPN, ������ ��� ���� \"tincd.exe\" �� ������.", "������");
                }
                else if (Globals.UA_Checked == true)
                {
                    MessageBox.Show("�� ������� ��������� ContraVPN, ������� ���� \"tincd.exe\" �� ��������.", "�������");
                }
                else if (Globals.BG_Checked == true)
                {
                    MessageBox.Show("ContraVPN �� ���� �� �� ��������, ������ ������ \"tincd.exe\" �� ���� �������.", "������");
                }
                else if (Globals.DE_Checked == true)
                {
                    MessageBox.Show("ContraVPN kann nicht gestartet werden, weil die \"tincd.exe\" datei nicht gefunden wurde.", "Fehler");
                }
            }
            else if (Directory.Exists(@"contra\vpn") && (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Contra\\vpnconfig\\contravpn\\tinc.conf")))
            {
                if (Globals.GB_Checked == true)
                {
                    MessageBox.Show("Cannot start ContraVPN because \"tinc.conf\" file was not found. Make sure you have entered your invitation link first (go to VPN Settings > Invite).", "Error");
                }
                else if (Globals.RU_Checked == true)
                {
                    MessageBox.Show("�� ������� ��������� ContraVPN, ������ ��� ���� \"tinc.conf\" �� ������. ���������, ��� �� ������� ������� ������ �� ����������� (��������� � ���������� VPN� > ������������).", "������");
                }
                else if (Globals.UA_Checked == true)
                {
                    MessageBox.Show("�� ������� ��������� ContraVPN, ������� ���� \"tinc.conf\" �� ��������. �������� �������������, �� �� ����� ��������� �� ���������� (�������� �� ����������� VPN > ����������).", "�������");
                }
                else if (Globals.BG_Checked == true)
                {
                    MessageBox.Show("ContraVPN �� ���� �� �� ��������, ������ ������ \"tinc.conf\" �� ���� �������. ������� ��, �� ��� ������ ����� �� �� ������ (������� ��� VPN ��������� > ������).", "������");
                }
                else if (Globals.DE_Checked == true)
                {
                    MessageBox.Show("ContraVPN kann nicht gestartet werden, weil die \"tinc.conf\" datei nicht gefunden wurde. Stelle sicher, dass du deinen invite link zuerst eingegeben hast (gehe zu VPN Einstellungen > Einladung).", "Fehler");
                }
            }
        }

        public void StartVPN_NoWindow()
        {
            Process tinc = new Process();
            tinc.StartInfo.Arguments = "--no-detach --config=\"" + Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Contra\\vpnconfig\\contravpn\" --debug=3 --pidfile=\"" + Environment.CurrentDirectory + "\\contra\\vpn\\tinc.pid\" --logfile=\"" + Environment.CurrentDirectory + "\\contra\vpn\\tinc.log\" --option=AddressFamily=ipv4 --option=Interface=ContraVPN";
            tinc.StartInfo.FileName = Environment.CurrentDirectory + @"\contra\vpn\" + Globals.userOS + @"\tincd.exe";
            tinc.StartInfo.UseShellExecute = false;
            tinc.StartInfo.CreateNoWindow = true;
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\contravpn\tinc.conf") && (File.Exists(@"contra\vpn\" + Globals.userOS + @"\tincd.exe")))
            {
                if (Globals.GB_Checked == true)
                {
                    playersOnlineLabel.Text = "Loading...";
                }
                else if (Globals.RU_Checked == true)
                {
                    playersOnlineLabel.Text = "��������...";
                }
                else if (Globals.UA_Checked == true)
                {
                    playersOnlineLabel.Text = "������������...";
                }
                else if (Globals.BG_Checked == true)
                {
                    playersOnlineLabel.Text = "������� ��...";
                }
                else if (Globals.DE_Checked == true)
                {
                    playersOnlineLabel.Text = "Lade...";
                }
                tinc.Start();

                //check when tinc gets turned off
                tinc.EnableRaisingEvents = true;
                tinc.Exited += (sender, e) =>
                {
                    vpnOff();
                };

                string tincd = "tincd.exe";
                Process[] tincdByName = Process.GetProcessesByName(tincd.Substring(0, tincd.LastIndexOf('.')));
                if (tincdByName.Length > 0)
                {
                    vpn_start.BackgroundImage = (System.Drawing.Image)(Properties.Resources.vpn_on);
                    refreshOnlinePlayersBtn.Show();
                    whoIsOnline.Show();
                    if (Globals.GB_Checked == true)
                    {
                        labelVpnStatus.Text = "On";
                    }
                    else if (Globals.RU_Checked == true)
                    {
                        labelVpnStatus.Text = "���.";
                    }
                    else if (Globals.UA_Checked == true)
                    {
                        labelVpnStatus.Text = "����.";
                    }
                    else if (Globals.BG_Checked == true)
                    {
                        labelVpnStatus.Text = "���.";
                    }
                    else if (Globals.DE_Checked == true)
                    {
                        labelVpnStatus.Text = "An";
                    }
                    refreshOnlinePlayersBtn.PerformClick();
                }
            }
            else if (!Directory.Exists(@"contra\vpn"))
            {
                if (Globals.GB_Checked == true)
                {
                    MessageBox.Show("Cannot start ContraVPN because \"contra\\vpn\" folder does not exist!", "Error");
                }
                else if (Globals.RU_Checked == true)
                {
                    MessageBox.Show("�� ������� ��������� ContraVPN, ������ ��� ����� \"contra\\vpn\" �� ����������!", "������");
                }
                else if (Globals.UA_Checked == true)
                {
                    MessageBox.Show("�� ������� ��������� ContraVPN, ������� ����� \"contra\\vpn\" �� ����!", "�������");
                }
                else if (Globals.BG_Checked == true)
                {
                    MessageBox.Show("ContraVPN �� ���� �� �� ��������, ������ \"contra\\vpn\" ������� �� ����������!", "������");
                }
                else if (Globals.DE_Checked == true)
                {
                    MessageBox.Show("ContraVPN kann nicht gestartet werden, weil der \"contra\\vpn\" ordner existiert nicht!", "Fehler");
                }
            }
            else if (!Directory.Exists(@"contra\vpn\" + Globals.userOS))
            {
                if (Globals.GB_Checked == true)
                {
                    MessageBox.Show("Cannot start ContraVPN because \"" + Globals.userOS + "\" folder was not found.", "Error");
                }
                else if (Globals.RU_Checked == true)
                {
                    MessageBox.Show("�� ������� ��������� ContraVPN, ������ ��� ����� \"" + Globals.userOS + "\" �� �������.", "������");
                }
                else if (Globals.UA_Checked == true)
                {
                    MessageBox.Show("�� ������� ��������� ContraVPN, ������� ����� \"" + Globals.userOS + "\" �� ��������.", "�������");
                }
                else if (Globals.BG_Checked == true)
                {
                    MessageBox.Show("ContraVPN �� ���� �� �� ��������, ������ ������� \"" + Globals.userOS + "\" �� ���� ��������.", "������");
                }
                else if (Globals.DE_Checked == true)
                {
                    MessageBox.Show("ContraVPN kann nicht gestartet werden, weil die \"" + Globals.userOS + "\" ordner nicht gefunden wurde.", "Fehler");
                }
            }
            else if (!File.Exists(@"contra\vpn\" + Globals.userOS + @"\tincd.exe"))
            {
                if (Globals.GB_Checked == true)
                {
                    MessageBox.Show("Cannot start ContraVPN because \"tincd.exe\" file was not found.", "Error");
                }
                else if (Globals.RU_Checked == true)
                {
                    MessageBox.Show("�� ������� ��������� ContraVPN, ������ ��� ���� \"tincd.exe\" �� ������.", "������");
                }
                else if (Globals.UA_Checked == true)
                {
                    MessageBox.Show("�� ������� ��������� ContraVPN, ������� ���� \"tincd.exe\" �� ��������.", "�������");
                }
                else if (Globals.BG_Checked == true)
                {
                    MessageBox.Show("ContraVPN �� ���� �� �� ��������, ������ ������ \"tincd.exe\" �� ���� �������.", "������");
                }
                else if (Globals.DE_Checked == true)
                {
                    MessageBox.Show("ContraVPN kann nicht gestartet werden, weil die \"tincd.exe\" datei nicht gefunden wurde.", "Fehler");
                }
            }
            else if (Directory.Exists(@"contra\vpn") && (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Contra\\vpnconfig\\contravpn\\tinc.conf")))
            {
                if (Globals.GB_Checked == true)
                {
                    MessageBox.Show("Cannot start ContraVPN because \"tinc.conf\" file was not found. Make sure you have entered your invitation link first (go to VPN Settings > Invite).", "Error");
                }
                else if (Globals.RU_Checked == true)
                {
                    MessageBox.Show("�� ������� ��������� ContraVPN, ������ ��� ���� \"tinc.conf\" �� ������. ���������, ��� �� ������� ������� ������ �� ����������� (��������� � ���������� VPN� > ������������).", "������");
                }
                else if (Globals.UA_Checked == true)
                {
                    MessageBox.Show("�� ������� ��������� ContraVPN, ������� ���� \"tinc.conf\" �� ��������. �������� �������������, �� �� ����� ��������� �� ���������� (�������� �� ����������� VPN > ����������).", "�������");
                }
                else if (Globals.BG_Checked == true)
                {
                    MessageBox.Show("ContraVPN �� ���� �� �� ��������, ������ ������ \"tinc.conf\" �� ���� �������. ������� ��, �� ��� ������ ����� �� �� ������ (������� ��� VPN ��������� > ������).", "������");
                }
                else if (Globals.DE_Checked == true)
                {
                    MessageBox.Show("ContraVPN kann nicht gestartet werden, weil die \"tinc.conf\" datei nicht gefunden wurde. Stelle sicher, dass du deinen invite link zuerst eingegeben hast (gehe zu VPN Einstellungen > Einladung).", "Fehler");
                }
            }
        }

        private void buttonVPNinvOK_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public string getCurrentCulture()
        {
            var culture = System.Globalization.CultureInfo.CurrentCulture;
            string cultureStr = culture.ToString();
            return cultureStr;
        }

        bool IsWindows8OrNewer()
        {
            var os = Environment.OSVersion;
            return os.Platform == PlatformID.Win32NT &&
                   (os.Version.Major > 6 || (os.Version.Major == 6 && os.Version.Minor >= 2));
        }

        public static class ThreadHelperClass
        {
            delegate void SetTextCallback(Form f, Control ctrl, string text);
            /// <summary>
            /// Set text property of various controls
            /// </summary>
            /// <param name="form">The calling form</param>
            /// <param name="ctrl"></param>
            /// <param name="text"></param>
            public static void SetText(Form form, Control ctrl, string text)
            {
                // InvokeRequired required compares the thread ID of the 
                // calling thread to the thread ID of the creating thread. 
                // If these threads are different, it returns true. 
                if (ctrl.InvokeRequired)
                {
                    SetTextCallback d = new SetTextCallback(SetText);
                    form.Invoke(d, new object[] { form, ctrl, text });
                }
                else
                {
                    ctrl.Text = text;
                }
            }
        }

        bool downloadTextFile = false;
        bool seekForUpdate = true;

        // This method is executed on the worker thread and makes 
        // a thread-safe call on the TextBox control. 
        private void ThreadProcSafe()
        {
            using (WebClient client = new WebClient())
            {
                if (downloadTextFile == false)
                {
                    //Check for launcher update once per launch.
                    if (seekForUpdate == true)
                    {
                        seekForUpdate = false;
                        GetUpdate();
                    }
                    downloadTextFile = true;
                }
                string txtFile = client.DownloadString("https://gist.githubusercontent.com/ThePredatorBG/65e9e36d85c5def6adf7a0a5c73fb15a/raw/gistfile1.txt");
                if (Globals.GB_Checked == true)
                {
                    string MOTDText = txtFile.Substring(txtFile.LastIndexOf("MOTD-EN: ") + 9);
                    string MOTDText2 = MOTDText.Substring(0, MOTDText.IndexOf("#"));
                    ThreadHelperClass.SetText(this, MOTD, MOTDText2);
                }
                else if (Globals.RU_Checked == true)
                {
                    string MOTDText = txtFile.Substring(txtFile.LastIndexOf("MOTD-RU: ") + 9);
                    string MOTDText2 = MOTDText.Substring(0, MOTDText.IndexOf("#"));
                    ThreadHelperClass.SetText(this, MOTD, MOTDText2);
                }
                else if (Globals.UA_Checked == true)
                {
                    string MOTDText = txtFile.Substring(txtFile.LastIndexOf("MOTD-UA: ") + 9);
                    string MOTDText2 = MOTDText.Substring(0, MOTDText.IndexOf("#"));
                    ThreadHelperClass.SetText(this, MOTD, MOTDText2);
                }
                else if (Globals.BG_Checked == true)
                {
                    byte[] bytes = Encoding.Default.GetBytes(txtFile);
                    txtFile = Encoding.UTF8.GetString(bytes);
                    string MOTDText = txtFile.Substring(txtFile.LastIndexOf("MOTD-BG: ") + 9);
                    string MOTDText2 = MOTDText.Substring(0, MOTDText.IndexOf("#"));
                    ThreadHelperClass.SetText(this, MOTD, MOTDText2);
                }
                else if (Globals.DE_Checked == true)
                {
                    string MOTDText = txtFile.Substring(txtFile.LastIndexOf("MOTD-DE: ") + 9);
                    string MOTDText2 = MOTDText.Substring(0, MOTDText.IndexOf("#"));
                    ThreadHelperClass.SetText(this, MOTD, MOTDText2);
                }
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            //Message of the Day
            //try
            //{
            //    using (WebClient client = new WebClient())
            //    {
            //        if (Globals.GB_Checked == true)
            //        {
            //            //MOTD.Text = client.DownloadString("https://gist.githubusercontent.com/ThePredatorBG/d7af1184eb6a7a90b62e8277ada4b1dd/raw/090a6dc5c6ecd0aa20db1654cce831ecf6c5828a/gistfile1.txt");
            //        }
            //        else if (Globals.RU_Checked == true)
            //        {
            //            //MOTD.Text = client.DownloadString("https://gist.githubusercontent.com/ThePredatorBG/d7af1184eb6a7a90b62e8277ada4b1dd/raw/090a6dc5c6ecd0aa20db1654cce831ecf6c5828a/gistfile1.txt");
            //        }
            //        else if (Globals.UA_Checked == true)
            //        {
            //            //MOTD.Text = client.DownloadString("https://gist.githubusercontent.com/ThePredatorBG/d7af1184eb6a7a90b62e8277ada4b1dd/raw/090a6dc5c6ecd0aa20db1654cce831ecf6c5828a/gistfile1.txt");
            //        }
            //        else if (Globals.BG_Checked == true)
            //        {
            //            //string myString = client.DownloadString("https://gist.githubusercontent.com/ThePredatorBG/cec510b72d5b232df3ed2c9e01276af5/raw/1458a1c51077ae2e8ffc927a0324d5de723d558e/gistfile1.txt");
            //            //byte[] bytes = Encoding.Default.GetBytes(myString);
            //            //myString = Encoding.UTF8.GetString(bytes);
            //            //MOTD.Text = myString;
            //        }
            //        else if (Globals.DE_Checked == true)
            //        {
            //            //MOTD.Text = client.DownloadString("https://gist.githubusercontent.com/ThePredatorBG/d7af1184eb6a7a90b62e8277ada4b1dd/raw/090a6dc5c6ecd0aa20db1654cce831ecf6c5828a/gistfile1.txt");
            //        }
            //    }
            //}
            //catch
            //{

            //}

            //Show warning if Options.ini isn't found and the user is running Windows 8 or more recent.
            if (IsWindows8OrNewer() == true)
            {
                if (!File.Exists(userDataLeafName() + "Options.ini") || (!File.Exists(myDocPath + "Options.ini")))
                {
                    if (Globals.GB_Checked == true)
                    {
                        MessageBox.Show("Options.ini not found, therefore the game will not start!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (Globals.RU_Checked == true)
                    {
                        MessageBox.Show("Options.ini not found, therefore the game will not start!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (Globals.UA_Checked == true)
                    {
                        MessageBox.Show("Options.ini not found, therefore the game will not start!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (Globals.BG_Checked == true)
                    {
                        MessageBox.Show("Options.ini not found, therefore the game will not start!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (Globals.DE_Checked == true)
                    {
                        MessageBox.Show("Options.ini not found, therefore the game will not start!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }

            if (Properties.Settings.Default.FirstRun)
            {
                //If there are older Contra config folders, this means Contra Launcher has been
                //ran before on this PC, so in this case, we skip first run welcome message.
                int directoryCount = System.IO.Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra").Length;

                //Show message on first run.
                if (getCurrentCulture() == "en-US")
                {
                    radioFlag_GB.Checked = true;
                    if (directoryCount <= 1)
                    {
                        MessageBox.Show("Welcome to Contra 009 Final! Since this is your first time running this launcher, we would like to let you know that you have a new opportunity to play Contra online via ContraVPN! We highly recommend you to join our Discord community!");
                    }
                }
                else if (getCurrentCulture() == "ru-RU")
                {
                    radioFlag_RU.Checked = true;
                    if (directoryCount <= 1)
                    {
                        MessageBox.Show("����� ���������� � Contra 009 Final! ��������� ��� ��� ������ ������ ����� ��������, �� ����� �������� ��� � ���, ��� � ��� ���� ����� ����������� ������ � Contra ������ ����� ContraVPN! �� ������������ ����������� ��� �������������� � ����� ������ Discord.");
                    }
                }
                else if (getCurrentCulture() == "uk-UA")
                {
                    radioFlag_UA.Checked = true;
                    if (directoryCount <= 1)
                    {
                        MessageBox.Show("������� ������� �� Contra 009 Final! ������� �� ��� ������ ������ ����� ��������, �� ������ ��������� ��� ��� ��, �� � ��� � ���� ��������� ��������� Contra ������ ����� ContraVPN! �� ����������� ����������� ��� ���������� �� ���� �������� Discord.");
                    }
                }
                else if (getCurrentCulture() == "bg-BG")
                {
                    radioFlag_BG.Checked = true;
                    if (directoryCount <= 1)
                    {
                        MessageBox.Show("����� ����� � Contra 009 Final! ��� ���� ���� � ������� �� ���������� �� Contra, ����� ������ �� ������, �� ����� ���� ���������� �� ������� Contra ������ ���� ContraVPN! ����� ������������ �� �� ������������ ��� ������ Discord �������! ���... �� � ��������� ����� ��! ;)");
                    }
                }
                else if (getCurrentCulture() == "de-DE")
                {
                    radioFlag_DE.Checked = true;
                    if (directoryCount <= 1)
                    {
                        MessageBox.Show("Wilkommen zu Contra 009 Final! Da du diesen launcher zum ersten mal ausf�hrst wollten wir dich wissen lassen, dass du eine neue M�glichkeit hast Contra online zu spielen �ber ContraVPN! Wir empfehlen dir unserem Discord Server beizutreten.");
                    }
                }
                else
                {
                    radioFlag_GB.Checked = true;
                    if (directoryCount <= 1)
                    {
                        MessageBox.Show("Welcome to Contra 009 Final! Since this is your first time running this launcher, we would like to let you know that you have a new opportunity to play Contra online via ContraVPN! We highly recommend you to join our Discord community!");
                    }
                }

                //Delete old Contra config folders
                System.IO.DirectoryInfo di = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Contra");

                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    if (dir.Name.Contains("vpnconfig") == true) //do not delete vpnconfig folder
                    {
                        continue;
                    }
                    dir.Delete(true);
                }
                try
                {
                    //Enable Tournament Mode (limit super weapons and super units) on first run.
                    if (Directory.Exists(userDataLeafName()))
                    {
                        string text = File.ReadAllText(userDataLeafName() + "Skirmish.ini");
                        {
                            if (text.Contains("SuperweaponRestrict = No"))
                            {
                                File.WriteAllText(userDataLeafName() + "Skirmish.ini", File.ReadAllText(userDataLeafName() + "Skirmish.ini").Replace("SuperweaponRestrict = No", "SuperweaponRestrict = Yes"));
                            }
                            else
                            {
                                //
                            }
                        }
                    }
                    else if (Directory.Exists(myDocPath))
                    {
                        string text = File.ReadAllText(myDocPath + "Skirmish.ini");
                        {
                            if (text.Contains("SuperweaponRestrict = No"))
                            {
                                File.WriteAllText(myDocPath + "Skirmish.ini", File.ReadAllText(myDocPath + "Skirmish.ini").Replace("SuperweaponRestrict = No", "SuperweaponRestrict = Yes"));
                            }
                            else
                            {
                                //
                            }
                        }
                    }
                }
                catch //(Exception ex)
                {
                    //
                }

                //Add Firewall exceptions.
                addFirewallExceptions();

                Properties.Settings.Default.FirstRun = false;
                Properties.Settings.Default.Save();
            }
        }

        private void VPNMoreButton_Click(object sender, EventArgs e)
        {
            foreach (Form VPNForm in Application.OpenForms)
            {
                if (VPNForm is VPNForm)
                {
                    VPNForm.Close();
                    new VPNForm().Show();
                    return;
                }
            }
            new VPNForm().Show();
            //            vpnIP();

            //    VPNForm VPNForm = new VPNForm();
            //    if (VPNForm == null)
            //    {
            //       VPNForm.Show();
            //   }
        }

        private void VPNMoreButton_MouseEnter(object sender, EventArgs e)
        {
            VPNMoreButton.BackgroundImage = (System.Drawing.Image)(Properties.Resources._button_config_tr);
            VPNMoreButton.ForeColor = SystemColors.ButtonHighlight;
            VPNMoreButton.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void VPNMoreButton_MouseLeave(object sender, EventArgs e)
        {
            VPNMoreButton.BackgroundImage = (System.Drawing.Image)(Properties.Resources._button_config);
            VPNMoreButton.ForeColor = SystemColors.ButtonHighlight;
            VPNMoreButton.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void VPNMoreButton_MouseDown(object sender, MouseEventArgs e)
        {
            VPNMoreButton.BackgroundImage = (System.Drawing.Image)(Properties.Resources._button_sm_highlight);
            VPNMoreButton.ForeColor = SystemColors.ButtonHighlight;
            VPNMoreButton.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }

        private void applyResources(ComponentResourceManager resources, Control.ControlCollection ctls)
        {
            foreach (Control ctl in ctls)
            {
                resources.ApplyResources(ctl, ctl.Name);
                applyResources(resources, ctl.Controls);
            }
        }

        private void radioFlag_GB_CheckedChanged(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
            resources.ApplyResources(this, "$this");
            applyResources(resources, this.Controls);
            Globals.BG_Checked = false;
            Globals.RU_Checked = false;
            Globals.UA_Checked = false;
            Globals.DE_Checked = false;
            Globals.GB_Checked = true;
            toolTip1.SetToolTip(RadioLocQuotes, "Units of all three factions will speak English.");
            toolTip1.SetToolTip(RadioOrigQuotes, "Each faction's units will speak their native language.");
            toolTip1.SetToolTip(RadioEN, "English in-game language.");
            toolTip1.SetToolTip(RadioRU, "Russian in-game language.");
            toolTip1.SetToolTip(MNew, "Use new soundtracks.");
            toolTip1.SetToolTip(MStandard, "Use standard Zero Hour soundtracks.");
            toolTip1.SetToolTip(DefaultPics, "Use default general portraits.");
            toolTip1.SetToolTip(GoofyPics, "Use funny general portraits.");
            toolTip1.SetToolTip(WinCheckBox, "Starts Contra in a window instead of full screen.");
            toolTip1.SetToolTip(QSCheckBox, "Disables intro and shellmap (game starts up faster).");
            toolTip1.SetToolTip(refreshOnlinePlayersBtn, "Refresh online players.");
            toolTip1.SetToolTip(whoIsOnline, "Show who is online.");
            toolTip1.SetToolTip(vpn_start, "Start/close ContraVPN.");
            //verLabel.Text = "Launcher version: " + Application.ProductVersion;
            versionLabel.Text = "Contra Project Team 2018 - Version 009 Final - Launcher: " + Application.ProductVersion;

            string tincd = "tincd.exe";
            Process[] tincdByName = Process.GetProcessesByName(tincd.Substring(0, tincd.LastIndexOf('.')));
            if (tincdByName.Length > 0 && wait == false) //if tinc is already running
            {
                vpn_start.BackgroundImage = (System.Drawing.Image)(Properties.Resources.vpn_on);
                refreshOnlinePlayersBtn.Show();
                whoIsOnline.Show();
                labelVpnStatus.Text = "On";
                refreshOnlinePlayersBtn.PerformClick();
            }
            else vpnIP();
            if (tincdByName.Length > 0) //if tinc is already running
            {
                labelVpnStatus.Text = "On";
                refreshOnlinePlayersBtn.PerformClick();
            }
            if (tincdByName.Length == 0) //if tinc is not running
            {
                playersOnlineLabel.Text = "ContraVPN disabled";
                labelVpnStatus.Text = "Off";
            }

            //Load MOTD
            try
            {
                {
                    System.Threading.Thread demoThread =
                       new System.Threading.Thread(new System.Threading.ThreadStart(this.ThreadProcSafe));
                    demoThread.Start();
                }
            }
            catch { }
        }

        private void radioFlag_RU_CheckedChanged(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ru-RU");
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
            resources.ApplyResources(this, "$this");
            applyResources(resources, this.Controls);
            Globals.GB_Checked = false;
            Globals.BG_Checked = false;
            Globals.UA_Checked = false;
            Globals.DE_Checked = false;
            Globals.RU_Checked = true;
            toolTip1.SetToolTip(RadioLocQuotes, "����� ���� ���� ������� ����� ������������� �� ����������.");
            toolTip1.SetToolTip(RadioOrigQuotes, "����� ������ ������� ����� ������������� �� �� ������ �����.");
            toolTip1.SetToolTip(RadioEN, "���������� ����.");
            toolTip1.SetToolTip(RadioRU, "������� ����.");
            toolTip1.SetToolTip(MNew, "�������� ����� ����������.");
            toolTip1.SetToolTip(MStandard, "�������� ����������� ���������� Zero Hour.");
            toolTip1.SetToolTip(DefaultPics, "�������� �������� ��������� �� ���������.");
            toolTip1.SetToolTip(GoofyPics, "�������� ������� �������� ���������.");
            toolTip1.SetToolTip(WinCheckBox, "������ Contra � ������ ���� ������ ��������������.");
            toolTip1.SetToolTip(QSCheckBox, "��������� ����� � ������� (���� ����������� �������).");
            toolTip1.SetToolTip(refreshOnlinePlayersBtn, "�������� ������� ������.");
            toolTip1.SetToolTip(whoIsOnline, "��������, ��� � ����.");
            toolTip1.SetToolTip(vpn_start, "�������/������� ContraVPN.");
            RadioLocQuotes.Text = "����.";
            RadioOrigQuotes.Text = "������";
            MNew.Text = "�����";
            MStandard.Text = "ZH";
            WinCheckBox.Text = "����� ����"; WinCheckBox.Left = 254;
            QSCheckBox.Text = "�����. �����"; QSCheckBox.Left = 254;
            RadioEN.Text = "����.";
            RadioRU.Text = "�������";
            DefaultPics.Text = "�� �����.";
            GoofyPics.Text = "�������";
            moreOptions.Text = "������ �����";
            versionLabel.Text = "Contra Project Team 2018 - ������ 009 ����� - Launcher: " + Application.ProductVersion;
            vpnSettingsLabel.Text = "��������� VPN";
            //verLabel.Text = "Launcher version: " + Application.ProductVersion;

            string tincd = "tincd.exe";
            Process[] tincdByName = Process.GetProcessesByName(tincd.Substring(0, tincd.LastIndexOf('.')));
            if (tincdByName.Length > 0 && wait == false) //if tinc is already running
            {
                vpn_start.BackgroundImage = (System.Drawing.Image)(Properties.Resources.vpn_on);
                refreshOnlinePlayersBtn.Show();
                whoIsOnline.Show();
                labelVpnStatus.Text = "���.";
                refreshOnlinePlayersBtn.PerformClick();
            }
            else vpnIP();
            if (tincdByName.Length > 0) //if tinc is already running
            {
                labelVpnStatus.Text = "���.";
                refreshOnlinePlayersBtn.PerformClick();
            }
            if (tincdByName.Length == 0) //if tinc is not running
            {
                playersOnlineLabel.Text = "ContraVPN ���������";
                labelVpnStatus.Text = "����."; ;
            }

            //Load MOTD
            try
            {
                {
                    System.Threading.Thread demoThread =
                       new System.Threading.Thread(new System.Threading.ThreadStart(this.ThreadProcSafe));
                    demoThread.Start();
                }
            }
            catch { }
        }

        private void radioFlag_UA_CheckedChanged(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("uk-UA");
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
            resources.ApplyResources(this, "$this");
            applyResources(resources, this.Controls);
            Globals.GB_Checked = false;
            Globals.RU_Checked = false;
            Globals.BG_Checked = false;
            Globals.DE_Checked = false;
            Globals.UA_Checked = true;
            toolTip1.SetToolTip(RadioLocQuotes, "����� ��� ����� ������� �������������� ����������.");
            toolTip1.SetToolTip(RadioOrigQuotes, "����� ����� ������� �������������� ������ ����� �����.");
            toolTip1.SetToolTip(RadioEN, "��������� ����.");
            toolTip1.SetToolTip(RadioRU, "�������� ����.");
            toolTip1.SetToolTip(MNew, "�������������� ��� ����������.");
            toolTip1.SetToolTip(MStandard, "�������������� ���������� ���������� Zero Hour.");
            toolTip1.SetToolTip(DefaultPics, "�������������� �������� �������� �� �������������.");
            toolTip1.SetToolTip(GoofyPics, "�������������� ����� �������� ��������.");
            toolTip1.SetToolTip(WinCheckBox, "������� Contra � �������� ����� ������ ��������������.");
            toolTip1.SetToolTip(QSCheckBox, "������ ����� � ������� (��� ����������� ������).");
            toolTip1.SetToolTip(refreshOnlinePlayersBtn, "������� ������� ������.");
            toolTip1.SetToolTip(whoIsOnline, "��������, ��� � �����.");
            toolTip1.SetToolTip(vpn_start, "³������/������� ContraVPN.");
            RadioLocQuotes.Text = "����.";
            RadioOrigQuotes.Text = "г���";
            MNew.Text = "����";
            MStandard.Text = "ZH";
            WinCheckBox.Text = "³������";
            QSCheckBox.Text = "��. �����";
            RadioEN.Text = "����.";
            RadioRU.Text = "���.";
            DefaultPics.Text = "�� ������.";
            GoofyPics.Text = "�����";
            moreOptions.Text = "������ �����";
            versionLabel.Text = "Contra Project Team 2018 - ����� 009 Գ��� - Launcher: " + Application.ProductVersion;
            vpnSettingsLabel.Text = "��������� VPN";
            //verLabel.Text = "Launcher version: " + Application.ProductVersion;

            string tincd = "tincd.exe";
            Process[] tincdByName = Process.GetProcessesByName(tincd.Substring(0, tincd.LastIndexOf('.')));
            if (tincdByName.Length > 0 && wait == false) //if tinc is already running
            {
                vpn_start.BackgroundImage = (System.Drawing.Image)(Properties.Resources.vpn_on);
                refreshOnlinePlayersBtn.Show();
                whoIsOnline.Show();
                labelVpnStatus.Text = "����.";
                refreshOnlinePlayersBtn.PerformClick();
            }
            else vpnIP();
            if (tincdByName.Length > 0) //if tinc is already running
            {
                labelVpnStatus.Text = "����.";
                refreshOnlinePlayersBtn.PerformClick();
            }
            if (tincdByName.Length == 0) //if tinc is not running
            {
                playersOnlineLabel.Text = "ContraVPN ��������";
                labelVpnStatus.Text = "����.";
            }

            //Load MOTD
            try
            {
                {
                    System.Threading.Thread demoThread =
                       new System.Threading.Thread(new System.Threading.ThreadStart(this.ThreadProcSafe));
                    demoThread.Start();
                }
            }
            catch { }
        }

        private void radioFlag_BG_CheckedChanged(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("bg-BG");
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
            resources.ApplyResources(this, "$this");
            applyResources(resources, this.Controls);
            Globals.GB_Checked = false;
            Globals.RU_Checked = false;
            Globals.UA_Checked = false;
            Globals.DE_Checked = false;
            Globals.BG_Checked = true;
            toolTip1.SetToolTip(RadioLocQuotes, "��������� �� ����� ������� �� ������� �� ���������.");
            toolTip1.SetToolTip(RadioOrigQuotes, "��������� �� ����� ������� �� ������� �� ������ ����� ����.");
            toolTip1.SetToolTip(RadioEN, "��������� ���� � ������.");
            toolTip1.SetToolTip(RadioRU, "����� ���� � ������.");
            toolTip1.SetToolTip(MNew, "����������� ������ ������.");
            toolTip1.SetToolTip(MStandard, "����������� ������������ ������ � Zero Hour.");
            toolTip1.SetToolTip(DefaultPics, "����������� ������������ ���������� ��������.");
            toolTip1.SetToolTip(GoofyPics, "����������� ��������� ���������� ��������.");
            toolTip1.SetToolTip(WinCheckBox, "�������� Contra � ��� �������� ������ �� ��� �����.");
            toolTip1.SetToolTip(QSCheckBox, "�������� ������� � ����������� ����� (�������). ������ �������� ��-�����.");
            toolTip1.SetToolTip(refreshOnlinePlayersBtn, "������.");
            toolTip1.SetToolTip(whoIsOnline, "������ ��� ������ �� �� �����.");
            toolTip1.SetToolTip(vpn_start, "������/������� ContraVPN.");
            RadioLocQuotes.Text = "����.";
            RadioOrigQuotes.Text = "�����";
            MNew.Text = "����";
            MStandard.Text = "ZH";
            WinCheckBox.Text = "� ��������"; WinCheckBox.Left = 267;
            QSCheckBox.Text = "���� �����"; QSCheckBox.Left = 267;
            RadioEN.Text = "����.";
            RadioRU.Text = "�����";
            DefaultPics.Text = "�� ����.";
            GoofyPics.Text = "�������";
            moreOptions.Text = "���. �����";
            versionLabel.Text = "Contra ���� 2018 - ������ 009 Final - Launcher: " + Application.ProductVersion;
            vpnSettingsLabel.Text = "VPN ���������";
            //verLabel.Text = "������ �� ��������: " + Application.ProductVersion;

            string tincd = "tincd.exe";
            Process[] tincdByName = Process.GetProcessesByName(tincd.Substring(0, tincd.LastIndexOf('.')));
            if (tincdByName.Length > 0 && wait == false) //if tinc is already running
            {
                vpn_start.BackgroundImage = (System.Drawing.Image)(Properties.Resources.vpn_on);
                refreshOnlinePlayersBtn.Show();
                whoIsOnline.Show();
                labelVpnStatus.Text = "���.";
                refreshOnlinePlayersBtn.PerformClick();
            }
            else vpnIP();
            if (tincdByName.Length > 0) //if tinc is already running
            {
                labelVpnStatus.Text = "���.";
                refreshOnlinePlayersBtn.PerformClick();
            }
            if (tincdByName.Length == 0) //if tinc is not running
            {
                playersOnlineLabel.Text = "ContraVPN ��������";
                labelVpnStatus.Text = "����.";
            }

            //Load MOTD
            try
            {
                {
                    System.Threading.Thread demoThread =
                       new System.Threading.Thread(new System.Threading.ThreadStart(this.ThreadProcSafe));
                    demoThread.Start();
                }
            }
            catch { }
        }

        private void radioFlag_DE_CheckedChanged(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de-DE");
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
            resources.ApplyResources(this, "$this");
            applyResources(resources, this.Controls);
            Globals.GB_Checked = false;
            Globals.RU_Checked = false;
            Globals.UA_Checked = false;
            Globals.BG_Checked = false;
            Globals.DE_Checked = true;
            toolTip1.SetToolTip(RadioLocQuotes, "Einheiten von allen drei Fraktionen werden Englisch sprechen.");
            toolTip1.SetToolTip(RadioOrigQuotes, "Die Einheiten jeder Fraktion sprechen ihre Muttersprache.");
            toolTip1.SetToolTip(RadioEN, "Englische in-game Sprache.");
            toolTip1.SetToolTip(RadioRU, "Russische in-game Sprache.");
            toolTip1.SetToolTip(MNew, "Verwende den neuen Soundtrack.");
            toolTip1.SetToolTip(MStandard, "Verwende den Standard Zero Hour Soundtrack.");
            toolTip1.SetToolTip(DefaultPics, "Verwende normale General Portraits.");
            toolTip1.SetToolTip(GoofyPics, "Verwende lustige General Portraits.");
            toolTip1.SetToolTip(WinCheckBox, "Startet Contra in einem Fenster anstatt im Vollbild.");
            toolTip1.SetToolTip(QSCheckBox, "Deaktiviert das Intro und die shellmap (Spiel startet schneller).");
            toolTip1.SetToolTip(refreshOnlinePlayersBtn, "Aktualisiere Spieler die online sind.");
            toolTip1.SetToolTip(whoIsOnline, "Anzeigen wer online ist.");
            toolTip1.SetToolTip(vpn_start, "Starte/Schlie�e ContraVPN.");
            voicespanel.Left = 260;
            voicespanel.Size = new Size(95, 61);
            RadioLocQuotes.Text = "Englisch"; RadioLocQuotes.Left = 0;
            RadioOrigQuotes.Text = "Einheimisch"; RadioOrigQuotes.Left = 0;
            MNew.Text = "Neu";
            MStandard.Text = "Standard";
            WinCheckBox.Text = "Fenstermodus"; WinCheckBox.Left = 260;
            QSCheckBox.Text = "Schnellstart"; QSCheckBox.Left = 260;
            RadioEN.Text = "Englisch";
            RadioRU.Text = "Russisch";
            DefaultPics.Text = "Standard";
            GoofyPics.Text = "Lustig";
            moreOptions.Text = "Einstellungen";
            versionLabel.Text = "Contra Projekt Team 2018 - Version 009 Final - Launcher: " + Application.ProductVersion;
            vpnSettingsLabel.Text = "VPN Einstellungen";
            //verLabel.Text = "Launcher version: " + Application.ProductVersion;

            string tincd = "tincd.exe";
            Process[] tincdByName = Process.GetProcessesByName(tincd.Substring(0, tincd.LastIndexOf('.')));
            if (tincdByName.Length > 0 && wait == false) //if tinc is already running
            {
                vpn_start.BackgroundImage = (System.Drawing.Image)(Properties.Resources.vpn_on);
                refreshOnlinePlayersBtn.Show();
                whoIsOnline.Show();
                labelVpnStatus.Text = "An";
                refreshOnlinePlayersBtn.PerformClick();
            }
            else vpnIP();
            if (tincdByName.Length > 0) //if tinc is already running
            {
                labelVpnStatus.Text = "An";
                refreshOnlinePlayersBtn.PerformClick();
            }
            if (tincdByName.Length == 0) //if tinc is not running
            {
                playersOnlineLabel.Text = "ContraVPN deaktiviert";
                labelVpnStatus.Text = "Aus";
            }

            //Load MOTD
            try
            {
                {
                    System.Threading.Thread demoThread =
                       new System.Threading.Thread(new System.Threading.ThreadStart(this.ThreadProcSafe));
                    demoThread.Start();
                }
            }
            catch { }
        }

        private void Resolution_Click(object sender, EventArgs e)
        {
            foreach (Form moreOptionsForm in Application.OpenForms)
            {
                if (moreOptionsForm is moreOptionsForm)
                {
                    moreOptionsForm.Close();
                    new moreOptionsForm().Show();
                    return;
                }
            }
            new moreOptionsForm().Show();
        }

        private void Resolution_MouseEnter(object sender, EventArgs e)
        {
            moreOptions.ForeColor = Color.FromArgb(255, 210, 100);
        }

        private void Resolution_MouseDown(object sender, MouseEventArgs e)
        {
            moreOptions.ForeColor = Color.FromArgb(255, 230, 160);
        }

        private void Resolution_MouseLeave(object sender, EventArgs e)
        {
            moreOptions.ForeColor = Color.FromArgb(255, 255, 255);
        }

        private void button18_MouseEnter(object sender, EventArgs e)
        {
            button18.BackgroundImage = (System.Drawing.Image)(Properties.Resources.exit11);
        }

        private void button18_MouseLeave(object sender, EventArgs e)
        {
            button18.BackgroundImage = (System.Drawing.Image)(Properties.Resources.exit1);
        }

        private void button17_MouseEnter(object sender, EventArgs e)
        {
            button17.BackgroundImage = (System.Drawing.Image)(Properties.Resources.min11);
        }

        private void button17_MouseLeave(object sender, EventArgs e)
        {
            button17.BackgroundImage = (System.Drawing.Image)(Properties.Resources.min);
        }

//        public static string playersOnlineLabel_PassFromForm1;

        private void asd()
        {
            Process onlinePlayers = new Process();
            onlinePlayers.StartInfo.Arguments = "--config=\"" + Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Contra\\vpnconfig\\contravpn\" --pidfile=\"" + Environment.CurrentDirectory + "\\contra\\vpn\\tinc.pid\"";
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
            if (s.Contains("id") == true)
            {
                int s2 = Regex.Matches(s, "id").Count;
                if (s.Contains("ctrvpntest") == true)
                {
                    s2 -= 1;
                }
                if (s.Contains("contravpn") == true)
                {
                    s2 -= 1;
                }
                //                s2 -= 1; //excluding local user

                onlinePlayers.WaitForExit();
                onlinePlayers.Close();
                vpnIP();

                if (Globals.GB_Checked == true)
                {
                    //foreach (Form onlinePlayersForm1 in Application.OpenForms)
                    //{
                    //    if (onlinePlayersForm1 is onlinePlayersForm)
                    //    {
                    //        playersOnlineLabel_PassFromForm1 = playersOnlineLabel.Text; // "Players online: " + s2.ToString();
                    //        onlinePlayersForm onlinePlayersForm = new onlinePlayersForm();
                    //        //Globals.playersOnlineLabel = "Players online: " + s2.ToString();
                    //        //playersOnlineLabel.Text = Globals.playersOnlineLabel;
                    //        playersOnlineLabel.Text = onlinePlayersForm.playersOnlineLabel_PassFromForm2;
                    //    }
                    //    else playersOnlineLabel.Text = "Players online: " + s2.ToString();
                    //}

                    Globals.playersOnlineLabel = "Players online: " + s2.ToString();
                    playersOnlineLabel.Text = Globals.playersOnlineLabel;
                }
                else if (Globals.RU_Checked == true)
                {
                    Globals.playersOnlineLabel = "������ ������: " + s2.ToString();
                    playersOnlineLabel.Text = Globals.playersOnlineLabel;
                }
                else if (Globals.UA_Checked == true)
                {
                    Globals.playersOnlineLabel = "������ � �����: " + s2.ToString();
                    playersOnlineLabel.Text = Globals.playersOnlineLabel;
                }
                else if (Globals.BG_Checked == true)
                {
                    Globals.playersOnlineLabel = "������ �� �����: " + s2.ToString();
                    playersOnlineLabel.Text = Globals.playersOnlineLabel;
                }
                else if (Globals.DE_Checked == true)
                {
                    Globals.playersOnlineLabel = "Spieler online: " + s2.ToString();
                    playersOnlineLabel.Text = Globals.playersOnlineLabel;
                }
            }
        }

        //Timer, waiting for reachable nodes to appear.
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            timer1.Interval = 1000;
            Process onlinePlayers = new Process();
            onlinePlayers.StartInfo.Arguments = "--config=\"" + Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Contra\\vpnconfig\\contravpn\" --pidfile=\"" + Environment.CurrentDirectory + "\\contra\\vpn\\tinc.pid\"";
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
            if (s.Contains("id") == true)
            {
                int s2 = Regex.Matches(s, "id").Count;
                if (s.Contains("ctrvpntest") == true)
                {
                    s2 -= 1;
                }
                if (s.Contains("contravpn") == true)
                {
                    s2 -= 1;
                }
                //                s2 -= 1; //excluding local user

                if (Globals.GB_Checked == true)
                {
                    //foreach (Form onlinePlayersForm1 in Application.OpenForms)
                    //{
                    //    if (onlinePlayersForm1 is onlinePlayersForm)
                    //    {
                    //        playersOnlineLabel_PassFromForm1 = playersOnlineLabel.Text; // "Players online: " + s2.ToString();
                    //        onlinePlayersForm onlinePlayersForm = new onlinePlayersForm();
                    //        //Globals.playersOnlineLabel = "Players online: " + s2.ToString();
                    //        //playersOnlineLabel.Text = Globals.playersOnlineLabel;
                    //        playersOnlineLabel.Text = onlinePlayersForm.playersOnlineLabel_PassFromForm2;
                    //    }
                    //    else playersOnlineLabel.Text = "Players online: " + s2.ToString();
                    //}

                    Globals.playersOnlineLabel = "Players online: " + s2.ToString();
                    playersOnlineLabel.Text = Globals.playersOnlineLabel;
                }
                else if (Globals.RU_Checked == true)
                {
                    Globals.playersOnlineLabel = "������ ������: " + s2.ToString();
                    playersOnlineLabel.Text = Globals.playersOnlineLabel;
                }
                else if (Globals.UA_Checked == true)
                {
                    Globals.playersOnlineLabel = "������ � �����: " + s2.ToString();
                    playersOnlineLabel.Text = Globals.playersOnlineLabel;
                }
                else if (Globals.BG_Checked == true)
                {
                    Globals.playersOnlineLabel = "������ �� �����: " + s2.ToString();
                    playersOnlineLabel.Text = Globals.playersOnlineLabel;
                }
                else if (Globals.DE_Checked == true)
                {
                    Globals.playersOnlineLabel = "Spieler online: " + s2.ToString();
                    playersOnlineLabel.Text = Globals.playersOnlineLabel;
                }
                onlinePlayers.WaitForExit();
                onlinePlayers.Close();
                vpnIP();
            }
        }

        private void onlinePlayersBtn_Click(object sender, EventArgs e)
        {
            string tincd = "tincd.exe";
            Process[] tincdByName = Process.GetProcessesByName(tincd.Substring(0, tincd.LastIndexOf('.')));
            if (tincdByName.Length > 0)
            {
                if (Globals.GB_Checked == true)
                {
                    playersOnlineLabel.Text = "Loading...";
                }
                else if (Globals.RU_Checked == true)
                {
                    playersOnlineLabel.Text = "��������...";
                }
                else if (Globals.UA_Checked == true)
                {
                    playersOnlineLabel.Text = "������������...";
                }
                else if (Globals.BG_Checked == true)
                {
                    playersOnlineLabel.Text = "������� ��...";
                }
                else if (Globals.DE_Checked == true)
                {
                    playersOnlineLabel.Text = "Lade...";
                }
            }
//            asd();
            timer1.Enabled = true; //Enable timer. Implementation is inside timer1_Tick()
        }

        private void onlinePlayersBtn_MouseDown(object sender, MouseEventArgs e)
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

        public static void DisplayDnsConfiguration()
        {
            bool stopDialog = false;
            string concatName = string.Empty;
            string concatDesc = string.Empty;
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in adapters)
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                concatDesc += adapter.Description;
                concatName += adapter.Name;
            }

            foreach (NetworkInterface adapter in adapters)
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                if (concatDesc.Contains("TAP-Windows Adapter V9") == true && concatName.Contains("ContraVPN") == false)
                {
                    if (adapter.Name.Contains("swr.net") == true)
                    {
                        continue;
                    }
                    if (adapter.Description.Contains("TAP-Windows Adapter V9") == true && stopDialog == false)
                    {
                        Properties.Settings.Default.adapterExists = true; //?
                        if (Globals.GB_Checked == true)
                        {
                            DialogResult dialogResult = MessageBox.Show("Use existing adapter " + adapter.Name + " for ContraVPN?", "Choose Adapter", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                Process netsh = new Process();
                                netsh.StartInfo.FileName = "netsh.exe";
                                netsh.StartInfo.UseShellExecute = false;
                                netsh.StartInfo.RedirectStandardInput = true;
                                netsh.StartInfo.RedirectStandardOutput = true;
                                netsh.StartInfo.RedirectStandardError = true;
                                netsh.StartInfo.CreateNoWindow = true;
                                netsh.StartInfo.Arguments = "interface set interface name = " + "\"" + adapter.Name + "\"" + " newname = \"contra\\vpn\"";
                                netsh.Start();

                                stopDialog = true;
                                adapterInstalled = true;
                                MessageBox.Show("All done! The new adapter is now in use by ContraVPN!");
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                                //Properties.Settings.Default.adapterExists = false;
                            }
                        }
                        else if (Globals.RU_Checked == true)
                        {
                            DialogResult dialogResult = MessageBox.Show("Use existing adapter " + adapter.Name + " for ContraVPN?", "Choose Adapter", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                Process netsh = new Process();
                                netsh.StartInfo.FileName = "netsh.exe";
                                netsh.StartInfo.UseShellExecute = false;
                                netsh.StartInfo.RedirectStandardInput = true;
                                netsh.StartInfo.RedirectStandardOutput = true;
                                netsh.StartInfo.RedirectStandardError = true;
                                netsh.StartInfo.CreateNoWindow = true;
                                netsh.StartInfo.Arguments = "interface set interface name = " + "\"" + adapter.Name + "\"" + " newname = \"contra\\vpn\"";
                                netsh.Start();

                                stopDialog = true;
                                adapterInstalled = true;
                                MessageBox.Show("��� �������! ����� ������� ������ ������������ ContraVPN!");
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                                //Properties.Settings.Default.adapterExists = false;
                            }
                        }
                        else if (Globals.UA_Checked == true)
                        {
                            DialogResult dialogResult = MessageBox.Show("Use existing adapter " + adapter.Name + " for ContraVPN?", "Choose Adapter", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                Process netsh = new Process();
                                netsh.StartInfo.FileName = "netsh.exe";
                                netsh.StartInfo.UseShellExecute = false;
                                netsh.StartInfo.RedirectStandardInput = true;
                                netsh.StartInfo.RedirectStandardOutput = true;
                                netsh.StartInfo.RedirectStandardError = true;
                                netsh.StartInfo.CreateNoWindow = true;
                                netsh.StartInfo.Arguments = "interface set interface name = " + "\"" + adapter.Name + "\"" + " newname = \"contra\\vpn\"";
                                netsh.Start();

                                stopDialog = true;
                                adapterInstalled = true;
                                MessageBox.Show("��� ��������! ����� ������� ����� ��������������� ContraVPN!");
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                                //Properties.Settings.Default.adapterExists = false;
                            }
                        }
                        else if (Globals.BG_Checked == true)
                        {
                            DialogResult dialogResult = MessageBox.Show("������� �� �� ���������� " + adapter.Name + " �� ContraVPN?", "�������� �������", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                Process netsh = new Process();
                                netsh.StartInfo.FileName = "netsh.exe";
                                netsh.StartInfo.UseShellExecute = false;
                                netsh.StartInfo.RedirectStandardInput = true;
                                netsh.StartInfo.RedirectStandardOutput = true;
                                netsh.StartInfo.RedirectStandardError = true;
                                netsh.StartInfo.CreateNoWindow = true;
                                netsh.StartInfo.Arguments = "interface set interface name = " + "\"" + adapter.Name + "\"" + " newname = \"contra\\vpn\"";
                                netsh.Start();

                                stopDialog = true;
                                adapterInstalled = true;
                                MessageBox.Show("������! ������ ������� ���� �� ������ �� ContraVPN!");
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                                //Properties.Settings.Default.adapterExists = false;
                            }
                        }
                        else if (Globals.DE_Checked == true)
                        {
                            DialogResult dialogResult = MessageBox.Show("Use existing adapter " + adapter.Name + " for ContraVPN?", "Choose Adapter", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                Process netsh = new Process();
                                netsh.StartInfo.FileName = "netsh.exe";
                                netsh.StartInfo.UseShellExecute = false;
                                netsh.StartInfo.RedirectStandardInput = true;
                                netsh.StartInfo.RedirectStandardOutput = true;
                                netsh.StartInfo.RedirectStandardError = true;
                                netsh.StartInfo.CreateNoWindow = true;
                                netsh.StartInfo.Arguments = "interface set interface name = " + "\"" + adapter.Name + "\"" + " newname = \"contra\\vpn\"";
                                netsh.Start();

                                stopDialog = true;
                                adapterInstalled = true;
                                MessageBox.Show("Alles Fertig! Der neue Adapter wird nun von ContraVPN benutzt!");
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                                //Properties.Settings.Default.adapterExists = false;
                            }
                        }
                    }
                }
                else if (concatDesc.Contains("TAP-Windows Adapter V9") == true && concatName.Contains("ContraVPN") == true)
                {
                    Properties.Settings.Default.adapterExists = true;
                }
                else
                {
                    Properties.Settings.Default.adapterExists = false;
                }
            }
            if (concatName.Contains("ContraVPN") == false)
            {
                Properties.Settings.Default.adapterExists = false;
            }
        }

        private void vpn_start_Click(object sender, EventArgs e)
        {
            string tincd1 = "tincd.exe";
            Process[] tincdByName1 = Process.GetProcessesByName(tincd1.Substring(0, tincd1.LastIndexOf('.')));
            if (tincdByName1.Length == 0)
            {
                DisplayDnsConfiguration();
            }

            //check if adapter is installed

            if (Properties.Settings.Default.adapterExists == true)
            {
                //MessageBox.Show("The adapter already exists!");
                try //check if tincd is running or not
                {
                    string tincd = "tincd.exe";
                    Process[] tincdByName = Process.GetProcessesByName(tincd.Substring(0, tincd.LastIndexOf('.')));
                    if (tincdByName.Length > 0)
                    {
                        Process[] vpnprocesses = Process.GetProcessesByName("tincd");
                        foreach (Process vpnprocess in vpnprocesses)
                        {
                            vpnprocess.Kill();
                            vpnprocess.WaitForExit();
                            vpnprocess.Dispose();

                            vpn_start.BackgroundImage = (System.Drawing.Image)(Properties.Resources.vpn_off);
                            refreshOnlinePlayersBtn.Hide();
                            whoIsOnline.Hide();
                            foreach (Form onlinePlayersForm in Application.OpenForms)
                            {
                                if (onlinePlayersForm is onlinePlayersForm)
                                {
                                    onlinePlayersForm.Close();
                                    return;
                                }
                            }
                            //if (Globals.GB_Checked == true)
                            //{
                            //    playersOnlineLabel.Text = "ContraVPN disabled";
                            //    labelVpnStatus.Text = "Off";
                            //}
                            //else if (Globals.RU_Checked == true)
                            //{
                            //    playersOnlineLabel.Text = "ContraVPN ���������";
                            //    labelVpnStatus.Text = "����.";
                            //}
                            //else if (Globals.UA_Checked == true)
                            //{
                            //    playersOnlineLabel.Text = "ContraVPN ��������";
                            //    labelVpnStatus.Text = "����.";
                            //}
                            //else if (Globals.BG_Checked == true)
                            //{
                            //    playersOnlineLabel.Text = "ContraVPN ��������";
                            //    labelVpnStatus.Text = "����.";
                            //}
                            //else if (Globals.DE_Checked == true)
                            //{
                            //    playersOnlineLabel.Text = "ContraVPN deaktiviert";
                            //    labelVpnStatus.Text = "Aus";
                            //}
                            return;
                        }
                    }
                    else
                    {
                        if (Properties.Settings.Default.ShowConsole == true && Properties.Settings.Default.adapterExists == true)
                        {
                            StartVPN();
                            //  vpnIP();
                        }
                        else if (Properties.Settings.Default.adapterExists == true)
                        {
                            StartVPN_NoWindow();
                            //   vpnIP();
                        }
                    }
                }
                catch (Exception ex)
                {
                    vpnOffNoCond();
                    MessageBox.Show(ex.Message, "Error");
                }
            }
            //else if (adapterInstalled == false || Properties.Settings.Default.adapterExists == false) //if (Properties.Settings.Default.stopDialog == false)
            else if (adapterInstalled == false) //if (Properties.Settings.Default.stopDialog == false)
            {
                try
                {
                    if (Globals.GB_Checked == true)
                    {
                        MessageBox.Show("There is no free TAP-Windows Adapter V9 installed. Starting setup...");
                    }
                    else if (Globals.RU_Checked == true)
                    {
                        MessageBox.Show("��� ���������� ������������� �������� TAP-Windows V9. ������ ���������...");
                    }
                    else if (Globals.UA_Checked == true)
                    {
                        MessageBox.Show("���� ������� ������������� TAP-Windows Adapter V9. ������ ���������...");
                    }
                    else if (Globals.BG_Checked == true)
                    {
                        MessageBox.Show("������ �������� TAP-Windows Adapter V9. ���������� �� ������������...");
                    }
                    else if (Globals.DE_Checked == true)
                    {
                        MessageBox.Show("Free TAP-Windows Adapter V9 ist nicht installiert. Starte Installation...");
                    }
                    string path = Path.GetDirectoryName(Application.ExecutablePath);
                    Process adapter = new Process();

                    adapter.StartInfo.FileName = "\"" + path + @"\contra\vpn\" + Globals.userOS + @"\devcon.exe" + "\"";
                    adapter.StartInfo.Arguments = " install " + "\"" + path + @"\contra\vpn\" + Globals.userOS + @"\OemWin2k.inf" + "\"" + " tap0901";
                    adapter.StartInfo.UseShellExecute = false;
                    adapter.StartInfo.RedirectStandardInput = true;
                    adapter.StartInfo.CreateNoWindow = true;
                    adapter.Start();
                    adapter.WaitForExit();
                    if (Globals.GB_Checked == true)
                    {
                        if (adapter.ExitCode == 0)
                        {
                            MessageBox.Show("A new TAP-Windows Adapter V9 has been installed. You may now allow it to be used by ContraVPN (selecting \"Yes\" on the first dialog message).", "Adapter Installed");
                            adapterInstalled = true;
                            DisplayDnsConfiguration();
                        }
                        else
                        {
                            MessageBox.Show("TAP-Windows Adapter V9 installation failed.", "Adapter Install Failed");
                        }
                    }
                    else if (Globals.RU_Checked == true)
                    {
                        if (adapter.ExitCode == 0)
                        {
                            MessageBox.Show("��� ���������� ����� ������� TAP-Windows V9. ������ �� ������ ��������� ��� ������������ ContraVPN (������� \"��\" � ������ ���������� ���������).", "������� ����������");
                            adapterInstalled = true;
                            DisplayDnsConfiguration();
                        }
                        else
                        {
                            MessageBox.Show("TAP-Windows Adapter V9 installation failed.", "Adapter Install Failed");
                        }
                    }
                    else if (Globals.UA_Checked == true)
                    {
                        if (adapter.ExitCode == 0)
                        {
                            MessageBox.Show("��� ������������ ����� TAP-Windows Adapter V9. ����� �� ������ ��������� ���� ��������������� ContraVPN (�������� \"���\" � ������� ���������� ����).", "������� �����������");
                            adapterInstalled = true;
                            DisplayDnsConfiguration();
                        }
                        else
                        {
                            MessageBox.Show("TAP-Windows Adapter V9 installation failed.", "Adapter Install Failed");
                        }
                    }
                    else if (Globals.BG_Checked == true)
                    {
                        if (adapter.ExitCode == 0)
                        {
                            MessageBox.Show("��� TAP-Windows Adapter V9 ���� ����������. ���� ������ ��������� ��� �� ���� ��������� �� ContraVPN (��������� \"��\" �� ������� ���������)", "��������� � ����������");
                            adapterInstalled = true;
                            DisplayDnsConfiguration();
                        }
                        else
                        {
                            MessageBox.Show("������������ �� TAP-Windows Adapter V9 �� �������.", "������");
                        }
                    }
                    else if (Globals.DE_Checked == true)
                    {
                        if (adapter.ExitCode == 0)
                        {
                            MessageBox.Show("Ein neuer TAP-Windows Adapter V9 wurde installiert. Du solltest ihm nicht erlauben von ContraVPN benutzt zu werden (W�hle \"yes\" auf dem ersten dialog Fenster)", "Adapter installiert");
                            adapterInstalled = true;
                            DisplayDnsConfiguration();
                        }
                        else
                        {
                            MessageBox.Show("TAP-Windows Adapter V9 installation failed.", "Adapter Install Failed");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private static void vpnIP()
        {
            Process ipconfig = new Process();
            ipconfig.StartInfo.FileName = "ipconfig"; //"cmd.exe";
            ipconfig.StartInfo.UseShellExecute = false;
            ipconfig.StartInfo.RedirectStandardInput = true;
            ipconfig.StartInfo.RedirectStandardOutput = true;
            ipconfig.StartInfo.CreateNoWindow = true;
            ipconfig.Start();
            //            ipconfig.StandardInput.WriteLine("ipconfig");
            ipconfig.StandardInput.Flush();
            ipconfig.StandardInput.Close();
            string s = ipconfig.StandardOutput.ReadToEnd();
            if (s.Contains("10.10.10.") == true)
            {
                //shows everything ipconfig                    MessageBox.Show(s);
                ipconfig.WaitForExit();
                ipconfig.Close();
                try
                {
                    if (File.Exists(userDataLeafName() + "Options.ini") || (File.Exists(myDocPath + "Options.ini")))
                    {
                        List<string> found = new List<string>();
                        string line;
                        using (StringReader file = new StringReader(s))
                        {
                            while ((line = file.ReadLine()) != null)
                            {
                                if (line.Contains("10.10.10."))
                                {
                                    found.Add(line);
                                    s = line;
                                    s = s.Substring(s.IndexOf(':') + 1);
                                    Properties.Settings.Default.IP_Label = "ContraVPN IP: " + s;
                                    //                                                     MessageBox.Show(s); //shows tincIP
                                }
                            }
                        }
                    }
                    if (Directory.Exists(userDataLeafName()))
                    {
                        string text = File.ReadAllText(userDataLeafName() + "Options.ini");
                        if (!text.Contains("10.10.10."))
                        {
                            File.WriteAllText(userDataLeafName() + "Options.ini", Regex.Replace(File.ReadAllText(userDataLeafName() + "Options.ini"), "\r?\nIPAddress =.*", "\r\nIPAddress =" + s + "\r\n"));
                        }
                    }
                    else if (Directory.Exists(myDocPath))
                    {
                        string text = File.ReadAllText(myDocPath + "Options.ini");
                        if (!text.Contains("10.10.10."))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", Regex.Replace(File.ReadAllText(myDocPath + "Options.ini"), "\r?\nIPAddress =.*", "\r\nIPAddress =" + s + "\r\n"));
                        }
                    }
                }
                catch
                {
                    if (!File.Exists(userDataLeafName() + "Options.ini") || (!File.Exists(myDocPath + "Options.ini")))
                    {
                        if (Globals.GB_Checked == true)
                        {
                            MessageBox.Show("Options.ini not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (Globals.RU_Checked == true)
                        {
                            MessageBox.Show("Options.ini not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (Globals.UA_Checked == true)
                        {
                            MessageBox.Show("Options.ini not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (Globals.BG_Checked == true)
                        {
                            MessageBox.Show("Options.ini not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (Globals.DE_Checked == true)
                        {
                            MessageBox.Show("Options.ini not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                if (Globals.GB_Checked == true)
                {
                    Properties.Settings.Default.IP_Label = "ContraVPN IP: unknown";
                }
                else if (Globals.RU_Checked == true)
                {
                    Properties.Settings.Default.IP_Label = "ContraVPN IP: ����������";
                }
                else if (Globals.UA_Checked == true)
                {
                    Properties.Settings.Default.IP_Label = "ContraVPN IP: �������";
                }
                else if (Globals.BG_Checked == true)
                {
                    Properties.Settings.Default.IP_Label = "ContraVPN IP: ����������";
                }
                else if (Globals.DE_Checked == true)
                {
                    Properties.Settings.Default.IP_Label = "ContraVPN IP: unbekannt";
                }
            }
        }

        private void whoIsOnline_Click(object sender, EventArgs e)
        {
            foreach (Form onlinePlayersForm in Application.OpenForms)
            {
                if (onlinePlayersForm is onlinePlayersForm)
                {
                    onlinePlayersForm.Close();

                    if (onlinePlayersForm.WindowState == FormWindowState.Normal)
                    {
                        Properties.Settings.Default.playersOnlineWindowLocation = onlinePlayersForm.Location;
                    }
                    else
                    {
                        Properties.Settings.Default.playersOnlineWindowLocation = onlinePlayersForm.RestoreBounds.Location;
                    }
                    Properties.Settings.Default.Save();

                    new onlinePlayersForm().Show();
                    return;
                }
            }
            new onlinePlayersForm().Show();

            //string tincd = "tincd.exe";
            //Process[] tincdByName = Process.GetProcessesByName(tincd.Substring(0, tincd.LastIndexOf('.')));
            //if (tincdByName.Length > 0) //if tincd is already running
            //{
            //    Process onlinePlayers = new Process();
            //    onlinePlayers.StartInfo.Arguments = "--config=\"" + Environment.CurrentDirectory + "\\contra\vpn\" --pidfile=\"" + Environment.CurrentDirectory + "\\contra\\vpn\\tinc.pid\"";
            //    onlinePlayers.StartInfo.FileName = Environment.CurrentDirectory + @"\contravpn\" + Globals.userOS + @"\tinc.exe";
            //    onlinePlayers.StartInfo.UseShellExecute = false;
            //    onlinePlayers.StartInfo.RedirectStandardInput = true;
            //    onlinePlayers.StartInfo.RedirectStandardOutput = true;
            //    onlinePlayers.StartInfo.CreateNoWindow = true;
            //    onlinePlayers.Start();
            //    onlinePlayers.StandardInput.WriteLine("dump reachable nodes");
            //    onlinePlayers.StandardInput.Flush();
            //    onlinePlayers.StandardInput.Close();
            //    string s = onlinePlayers.StandardOutput.ReadToEnd();
            //    //                MessageBox.Show(s);

            //    string s_concat = "";
            //    Regex pattern = new Regex(@"([^\s]+) id");

            //    foreach (var match in pattern.Matches(s))
            //    {
            //        string matchString = match.ToString();
            //        //             string result = rgx.Replace(matchString, "");
            //        //matchString = matchString.Replace("contravpn", "");
            //        //matchString = matchString.Replace("ctrvpntest9", "");
            //        //              matchString = Regex.Replace(matchString, pattern2, "");
            //        if (!matchString.Contains("contravpn") && (!matchString.Contains("ctrvpntest9")))
            //        {
            //            s_concat += matchString;
            //        }
            //    }
            //    //     s_concat = s_concat.Replace("  ", "\n");
            //    //    s_concat = s_concat.Replace(" ", "");
            //    //              s_concat = s_concat.Replace("  ", "\n");
            //    s_concat = s_concat.Replace("id", "\n");
            //    s_concat = s_concat.Replace("predatorbg", "PredatoR");
            //    s_concat = s_concat.Replace("ggpersun", "GG`PeRSuN^");
            //    s_concat = s_concat.Replace("armanis", "Armanis");
            //    s_concat = s_concat.Replace("maelstrom", "Maelstrom");
            //    s_concat = s_concat.Replace("wwb2", "WWB2");
            //    s_concat = s_concat.Replace("kinomoto", "Hyperion Heights");
            //    if (Globals.GB_Checked == true)
            //    {
            //        MessageBox.Show(s_concat, "Who is online?");
            //    }
            //    else if (Globals.RU_Checked == true)
            //    {
            //        MessageBox.Show(s_concat, "��� � ����?");
            //    }
            //    else if (Globals.UA_Checked == true)
            //    {
            //        MessageBox.Show(s_concat, "��� � �����?");
            //    }
            //    else if (Globals.BG_Checked == true)
            //    {
            //        MessageBox.Show(s_concat, "��� � ������?");
            //    }
            //    else if (Globals.DE_Checked == true)
            //    {
            //        MessageBox.Show(s_concat, "Wer ist online?");
            //    }

            //    if (s.Contains("id") == true)
            //    {
            //        int s2 = Regex.Matches(s, "id").Count;
            //        if (s.Contains("ctrvpntest") == true)
            //        {
            //            s2 -= 1;
            //        }
            //        if (s.Contains("contravpn") == true)
            //        {
            //            s2 -= 1;
            //        }
            //        if (Globals.GB_Checked == true)
            //        {
            //            playersOnlineLabel.Text = "Players online: " + s2.ToString();
            //        }
            //        else if (Globals.RU_Checked == true)
            //        {
            //            playersOnlineLabel.Text = "������ ������: " + s2.ToString();
            //        }
            //        else if (Globals.UA_Checked == true)
            //        {
            //            playersOnlineLabel.Text = "������ � �����: " + s2.ToString();
            //        }
            //        else if (Globals.BG_Checked == true)
            //        {
            //            playersOnlineLabel.Text = "������ �� �����: " + s2.ToString();
            //        }
            //        else if (Globals.DE_Checked == true)
            //        {
            //            playersOnlineLabel.Text = "Spieler online: " + s2.ToString();
            //        }
            //        onlinePlayers.WaitForExit();
            //        onlinePlayers.Close();
            //    }
            //}


        }

        private void whoIsOnline_MouseDown(object sender, MouseEventArgs e)
        {
            whoIsOnline.BackgroundImage = (System.Drawing.Image)(Properties.Resources.ppl);
            whoIsOnline.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }

        private void whoIsOnline_MouseEnter(object sender, EventArgs e)
        {
            whoIsOnline.BackgroundImage = (System.Drawing.Image)(Properties.Resources.ppl_tr);
            whoIsOnline.FlatAppearance.MouseOverBackColor = Color.Transparent;
            whoIsOnline.FlatAppearance.MouseDownBackColor = Color.Transparent;
            whoIsOnline.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }

        private void whoIsOnline_MouseLeave(object sender, EventArgs e)
        {
            whoIsOnline.BackgroundImage = (System.Drawing.Image)(Properties.Resources.ppl);
            whoIsOnline.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }

        private void radioFlag_GB_MouseEnter(object sender, EventArgs e)
        {
            radioFlag_GB.BackgroundImage = (System.Drawing.Image)(Properties.Resources.flag_gb_tr);
        }
        private void radioFlag_GB_MouseLeave(object sender, EventArgs e)
        {
            radioFlag_GB.BackgroundImage = (System.Drawing.Image)(Properties.Resources.flag_gb);
        }

        private void radioFlag_RU_MouseEnter(object sender, EventArgs e)
        {
            radioFlag_RU.BackgroundImage = (System.Drawing.Image)(Properties.Resources.flag_ru_tr);
        }
        private void radioFlag_RU_MouseLeave(object sender, EventArgs e)
        {
            radioFlag_RU.BackgroundImage = (System.Drawing.Image)(Properties.Resources.flag_ru);
        }

        private void radioFlag_UA_MouseEnter(object sender, EventArgs e)
        {
            radioFlag_UA.BackgroundImage = (System.Drawing.Image)(Properties.Resources.flag_ua_tr);
        }
        private void radioFlag_UA_MouseLeave(object sender, EventArgs e)
        {
            radioFlag_UA.BackgroundImage = (System.Drawing.Image)(Properties.Resources.flag_ua);
        }

        private void radioFlag_BG_MouseEnter(object sender, EventArgs e)
        {
            radioFlag_BG.BackgroundImage = (System.Drawing.Image)(Properties.Resources.flag_bg_tr);
        }
        private void radioFlag_BG_MouseLeave(object sender, EventArgs e)
        {
            radioFlag_BG.BackgroundImage = (System.Drawing.Image)(Properties.Resources.flag_bg);
        }

        private void radioFlag_DE_MouseEnter(object sender, EventArgs e)
        {
            radioFlag_DE.BackgroundImage = (System.Drawing.Image)(Properties.Resources.flag_de_tr);
        }

        private void radioFlag_DE_MouseLeave(object sender, EventArgs e)
        {
            radioFlag_DE.BackgroundImage = (System.Drawing.Image)(Properties.Resources.flag_de);
        }

        private void vpn_start_MouseDown(object sender, MouseEventArgs e)
        {
            vpn_start.BackgroundImage = (System.Drawing.Image)(Properties.Resources._button_sm_highlight);
            vpn_start.ForeColor = SystemColors.ButtonHighlight;
            vpn_start.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);

            string tincd = "tincd.exe";
            Process[] tincdByName = Process.GetProcessesByName(tincd.Substring(0, tincd.LastIndexOf('.')));
            if (tincdByName.Length == 0)
            {
                timer1.Interval = 5000;
            }
        }

        private void vpn_start_MouseEnter(object sender, EventArgs e)
        {
            string tincd = "tincd.exe";
            Process[] tincdByName = Process.GetProcessesByName(tincd.Substring(0, tincd.LastIndexOf('.')));
            if (tincdByName.Length > 0)
            {
                vpn_start.BackgroundImage = (System.Drawing.Image)(Properties.Resources.vpn_on_over);
                vpn_start.ForeColor = SystemColors.ButtonHighlight;
                vpn_start.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            }
            else
            {
                vpn_start.BackgroundImage = (System.Drawing.Image)(Properties.Resources.vpn_off_over);
                vpn_start.ForeColor = SystemColors.ButtonHighlight;
                vpn_start.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            }
        }

        private void vpn_start_MouseLeave(object sender, EventArgs e)
        {
            string tincd = "tincd.exe";
            Process[] tincdByName = Process.GetProcessesByName(tincd.Substring(0, tincd.LastIndexOf('.')));
            if (tincdByName.Length > 0)
            {
                vpn_start.BackgroundImage = (System.Drawing.Image)(Properties.Resources.vpn_on);
                vpn_start.ForeColor = SystemColors.ButtonHighlight;
                vpn_start.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            }
            else
            {
                vpn_start.BackgroundImage = (System.Drawing.Image)(Properties.Resources.vpn_off);
                vpn_start.ForeColor = SystemColors.ButtonHighlight;
                vpn_start.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            }
        }
    }
}