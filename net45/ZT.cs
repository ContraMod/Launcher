using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Contra
{
    class ZT
    {
        [DllImport("kernel32.dll")]
        static extern bool CreateSymbolicLink(
        string lpSymlinkFileName, string lpTargetFileName, SymbolicLink dwFlags);
        enum SymbolicLink
        {
            File = 0,
            Directory = 1
        }
        string symbolicLink = contravpnPath + "zt-config";
        string linksTo = ztPath;

        static string contravpnPath = Environment.CurrentDirectory + @"\contra\vpn\";
        string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt-temp\";
        static string ztPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt\";

        static bool updateZTVersion = false;

        //WebClient ztDL = new WebClient();
        //string ZTURL = "https://download.zerotier.com/dist/ZeroTier%20One.msi";
        static readonly string ZTFileName = "ZeroTier One.msi";

        public async void CheckZTInstall(string ZTURL)
        {
            Globals.ZTReady = 0;

            try
            {
                // Download ZT if user doesn't have it
                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt\tap-windows") ||
                    !File.Exists(contravpnPath + "zt-x64.exe") || !File.Exists(contravpnPath + "zt-x86.exe") ||
                    updateZTVersion == true)
                {
                    if (Globals.GB_Checked == true)
                    {
                        MessageBox.Show("ContraVPN (ZeroTier One) needs to be downloaded. Starting download...", "ContraVPN download");
                    }
                    else if (Globals.RU_Checked == true)
                    {
                        MessageBox.Show("ContraVPN (ZeroTier One) должен быть загружен. Начало загрузки...", "ContraVPN должен быть загружен");
                    }
                    else if (Globals.UA_Checked == true)
                    {
                        MessageBox.Show("ContraVPN (ZeroTier One) потрібно завантажити. Початок завантаження...", "ContraVPN потрібно завантажити");
                    }
                    else if (Globals.BG_Checked == true)
                    {
                        MessageBox.Show("ContraVPN (ZeroTier One) трябва да бъде изтеглен. Стартирането на изтеглянето...", "Изтегляне на ContraVPN");
                    }
                    else if (Globals.DE_Checked == true)
                    {
                        MessageBox.Show("ContraVPN (ZeroTier One) muss heruntergeladen werden. Download wird gestartet...", "ContraVPN herunterladen");
                    }

                    await Form1.DownloadFileSimple(ZTURL, $"{Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\{ZTFileName}", TimeSpan.FromMinutes(10));
                    CheckZTInstallSteps();
                    //await (new Form1()).DownloadFile(ZTURL, $"{Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\{ZTFileName}", TimeSpan.FromMinutes(5), Form1.httpCancellationToken.Token);

                    //ztDL.DownloadFileCompleted += new AsyncCompletedEventHandler(ztDL_DownloadCompleted);
                    //
                    //CheckIfFileIsAvailable(ZTURL);
                    //ztDL.OpenRead(ZTURL);
                    //ztDL.DownloadFileAsync(new Uri(ZTURL), Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\" + ZTFileName);
                }
                else
                {
                    CheckZTInstallSteps();
                    CheckZTVersion();
                }
            }
            catch (Exception ex) {
                if (File.Exists(ZTFileName)) File.Delete(ZTFileName);
                MessageBox.Show(ex.ToString());
            }
        }

        public void CheckIfFileIsAvailable(string ZTURL)
        {
            var url = ZTURL;
            HttpWebResponse response = null;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException)
            {
                /* A WebException will be thrown if the status of the response is not `200 OK` */
                if (Globals.GB_Checked == true)
                {
                    MessageBox.Show("The file is currently unavailable. Try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (Globals.RU_Checked == true)
                {
                    MessageBox.Show("Файл в данный момент недоступен. Попробуйте позже.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (Globals.UA_Checked == true)
                {
                    MessageBox.Show("Файл наразі недоступний. Повторіть спробу пізніше.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (Globals.BG_Checked == true)
                {
                    MessageBox.Show("Понастоящем файлът не е налице. Опитайте отново по-късно.", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (Globals.DE_Checked == true)
                {
                    MessageBox.Show("Die Datei ist derzeit nicht verfügbar. Versuchen Sie es später noch einmal.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            finally
            {
                // Don't forget to close your response.
                if (response != null)
                {
                    response.Close();
                }
            }
        }

        //void ztDL_DownloadCompleted(object sender, AsyncCompletedEventArgs e)
        //{
        //    //     PatchDLPanel.Hide();

        //    if (e.Cancelled)
        //    {
        //        // delete the partially-downloaded file
        //        File.Delete(ZTFileName);
        //        return;
        //    }

        //    // display completion status.
        //    if (e.Error != null)
        //    {
        //        MessageBox.Show(e.Error.Message);
        //        return;
        //    }

        //    //Show a message when the patch download has completed
        //    //if (Globals.GB_Checked == true)
        //    //{
        //    //    MessageBox.Show("A new patch has been downloaded!\n\nThe application will now restart!", "Update Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    //}
        //    //else if (Globals.RU_Checked == true)
        //    //{
        //    //    MessageBox.Show("Новый патч был загружен!\n\nПриложение будет перезагружено!", "Обновление завершено", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    //}
        //    //else if (Globals.UA_Checked == true)
        //    //{
        //    //    MessageBox.Show("Новий виправлення завантажено!\n\nПрограма буде перезавантажена!", "Оновлення завершено", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    //}
        //    //else if (Globals.BG_Checked == true)
        //    //{
        //    //    MessageBox.Show("Нов пач беше изтеглен!\n\nСега ще се рестартира!", "Обновяването е завършено", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    //}
        //    //else if (Globals.DE_Checked == true)
        //    //{
        //    //    MessageBox.Show("Ein neuer Patch wurde heruntergeladen!\n\nDas Programm wird sich jetzt neu starten!", "Aktualisierung abgeschlossen", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    //}

        //    CheckZTInstallSteps();
        //}

        private void DisplayInstallMessage()
        {
            if (Globals.GB_Checked == true)
            {
                MessageBox.Show("ContraVPN is not yet installed. Starting installation...", "Installing ContraVPN");
            }
            else if (Globals.RU_Checked == true)
            {
                MessageBox.Show("ContraVPN еще не установлен. Начинается установка...", "Установка ContraVPN");
            }
            else if (Globals.UA_Checked == true)
            {
                MessageBox.Show("ContraVPN ще не встановлено. Початок установки...", "Встановлення ContraVPN");
            }
            else if (Globals.BG_Checked == true)
            {
                MessageBox.Show("ContraVPN не е инсталиран. Стартиране на инсталацията...", "Инсталиране на ContraVPN");
            }
            else if (Globals.DE_Checked == true)
            {
                MessageBox.Show("ContraVPN ist noch nicht installiert. Installation wird gestartet...", "ContraVPN installieren");
            }
        }

        public void CheckZTVersion()
        {
            Process ztExe = new Process();
            ztExe.StartInfo.WorkingDirectory = contravpnPath;
            ztExe.StartInfo.FileName = @"C:\windows\system32\windowspowershell\v1.0\powershell.exe";
            ztExe.StartInfo.Arguments = "./zt-cli -v";
            ztExe.StartInfo.UseShellExecute = false;
            ztExe.StartInfo.CreateNoWindow = true;
            ztExe.StartInfo.RedirectStandardOutput = true;
            ztExe.Start();
            string ztVersionLocal = ztExe.StandardOutput.ReadToEnd().Trim();
            //MessageBox.Show(ztVersionLocal);

            try
            {
                WebClient wc = new WebClient();
                string versionTxt = wc.DownloadString("https://raw.githubusercontent.com/ContraMod/Launcher/master/Versions.txt");

                //Get zt version
                string ztVersionLatest = versionTxt.Substring(versionTxt.LastIndexOf("ZeroTier: ") + 10);
                ztVersionLatest = ztVersionLatest.Substring(0, ztVersionLatest.IndexOf("$")).Trim();
                //MessageBox.Show(ztVersionLatest);

                if (ztVersionLocal != ztVersionLatest)
                {
                    updateZTVersion = true;

                    if (Globals.GB_Checked == true)
                    {
                        MessageBox.Show("A new ZeroTier One version is available.", "VPN update");
                    }
                    else if (Globals.RU_Checked == true)
                    {
                        MessageBox.Show("Доступна новая версия ZeroTier One.", "Обновление VPN");
                    }
                    else if (Globals.UA_Checked == true)
                    {
                        MessageBox.Show("Доступна нова версія ZeroTier One.", "Оновлення VPN");
                    }
                    else if (Globals.BG_Checked == true)
                    {
                        MessageBox.Show("Достъпна е нова версия на ZeroTier One.", "VPN обновление");
                    }
                    else if (Globals.DE_Checked == true)
                    {
                        MessageBox.Show("Eine neue ZeroTier One-Version ist verfügbar.", "VPN-Update");
                    }

                    CheckZTInstall("https://download.zerotier.com/dist/ZeroTier%20One.msi");
                }

                //int ztVersionLatestInt = int.Parse(ztVersionLatest);
            }
            catch
            {

            }
        }

        public void CheckZTInstallSteps()
        {
            bool installMessage = false;

            // Files from package missing
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt\tap-windows") ||
                Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt\tap-windows", "*", SearchOption.AllDirectories).Length == 0 ||
                !File.Exists(contravpnPath + "zt-x64.exe") || !File.Exists(contravpnPath + "zt-x86.exe"))
            {
                if (installMessage == false)
                {
                    //MessageBox.Show("here");
                    DisplayInstallMessage();
                    installMessage = true;
                }
                InstallZTPackage();
            }
            else
            {
                Globals.ZTReady += 1;
            }

            // ZT Driver missing
            string infName = null;
            System.Management.ManagementObjectSearcher objSearcher = new System.Management.ManagementObjectSearcher("Select * from Win32_PnPSignedDriver Where DeviceName = 'ZeroTier One Virtual Port'");
            System.Management.ManagementObjectCollection objCollection = objSearcher.Get();
            foreach (System.Management.ManagementObject obj in objCollection)
            {
                infName = String.Format("{0}", obj["InfName"]);
            }
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            cmd.StandardInput.WriteLine("pnputil -e");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            string Output = cmd.StandardOutput.ReadToEnd();

            //MessageBox.Show(infName);

            //if (!string.IsNullOrWhiteSpace(infName)) //if (Output.Contains(infName))
            if (!string.IsNullOrWhiteSpace(infName) && Output.Contains(infName))
            {
                //cmd.CloseMainWindow();
                //cmd.Close();

                Globals.ZTReady += 1;
            }
            else
            {
                //cmd.CloseMainWindow();
                //cmd.Close();

                if (installMessage == false)
                {
                    DisplayInstallMessage();
                    installMessage = true;
                }
                InstallZTDriver();
            }

            // ZT Files missing in LocalAppData
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt\moons.d\0000008cc55dfcea.moon") ||
                !File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt\local.conf"))
            {
                if (installMessage == false)
                {
                    DisplayInstallMessage();
                    installMessage = true;
                }
                MoveZTFiles();
            }
            else
            {
                Globals.ZTReady += 1;
            }

            // ZT Network not joined
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt\networks.d") ||
                Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt\networks.d").Length < 2)
            {
                if (installMessage == false)
                {
                    DisplayInstallMessage();
                    installMessage = true;
                }
                JoinZTNetwork();
            }
            else
            {
                Globals.ZTReady += 1;
            }
        }
        // End of step checks.

        public async void InstallZTPackage()
        {
            string tempPathTap = tempPath + @"CommonAppDataFolder\ZeroTier\One\tap-windows\";
            string targetPathTap = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt\tap-windows\";

            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt");
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt\tap-windows");

            if (!Directory.Exists((Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt-temp")))
            {
                Process cmd = new Process();
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.Start();

                cmd.StandardInput.WriteLine("msiexec /a \"ZeroTier One.msi\" /qb TARGETDIR=\"%LOCALAPPDATA%\\Contra\\vpnconfig\\zt-temp\"");
                cmd.StandardInput.Flush();
                cmd.StandardInput.Close();
                cmd.WaitForExit();
            }

            // Get files from temp folder and delete it.
            int i = 0;
            while (!File.Exists(tempPath + @"CommonAppDataFolder\ZeroTier\One\zerotier-one_x64.exe") && i < 30 ||
                !File.Exists(tempPath + @"CommonAppDataFolder\ZeroTier\One\zerotier-one_x86.exe") && i < 30 ||
                !File.Exists(tempPath + @"CommonAppDataFolder\ZeroTier\One\tap-windows\x64\zttap300.cat") && i < 30 ||
                !File.Exists(tempPath + @"CommonAppDataFolder\ZeroTier\One\tap-windows\x64\zttap300.inf") && i < 30 ||
                !File.Exists(tempPath + @"CommonAppDataFolder\ZeroTier\One\tap-windows\x64\zttap300.sys") && i < 30 ||
                !File.Exists(tempPath + @"CommonAppDataFolder\ZeroTier\One\tap-windows\x86\zttap300.cat") && i < 30 ||
                !File.Exists(tempPath + @"CommonAppDataFolder\ZeroTier\One\tap-windows\x86\zttap300.inf") && i < 30 ||
                !File.Exists(tempPath + @"CommonAppDataFolder\ZeroTier\One\tap-windows\x86\zttap300.sys") && i < 30)
            {
                await Task.Delay(1000);
                //System.Threading.Thread.Sleep(1000);
                i++;
            }
            try
            {
                File.Copy(tempPath + @"CommonAppDataFolder\ZeroTier\One\zerotier-one_x64.exe", contravpnPath + "zt-x64.exe", true);
                File.Copy(tempPath + @"CommonAppDataFolder\ZeroTier\One\zerotier-one_x86.exe", contravpnPath + "zt-x86.exe", true);

                //Directory.Move(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt-temp\CommonAppDataFolder\ZeroTier\One\tap-windows", @"\Contra\vpnconfig\zt\tap-windows");
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt\tap-windows");

                foreach (string dir in Directory.GetDirectories(tempPath + @"CommonAppDataFolder\ZeroTier\One\tap-windows", "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(Path.Combine(targetPathTap, dir.Substring(tempPathTap.Length)));
                }
                foreach (string file_name in Directory.GetFiles(tempPath + @"CommonAppDataFolder\ZeroTier\One\tap-windows", "*", SearchOption.AllDirectories))
                {
                    File.Copy(file_name, Path.Combine(targetPathTap, file_name.Substring(tempPathTap.Length)), true);
                }

                CreateSymbolicLink(symbolicLink, linksTo, SymbolicLink.Directory);

                Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt-temp", true);

                Globals.ZTReady += 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void InstallZTDriver()
        {
            //MessageBox.Show("InstallZTDriver()");
            try
            {
                Process cmd = new Process();
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.Start();

                cmd.StandardInput.WriteLine("pnputil -i -a \"" + Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt\tap-windows\x" + Globals.userOS + "\\zttap300.inf\"");
                //cmd.StandardInput.WriteLine("pnputil /i /a \"$PWD\\config\\tap-windows\\x" + Globals.userOS + "\\zttap300.inf\"");
                cmd.StandardInput.Flush();
                cmd.StandardInput.Close();
                cmd.WaitForExit();
                Globals.ZTReady += 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void MoveZTFiles()
        {
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt\moons.d");
            try
            {
                File.Copy(contravpnPath + "zt-local.ctr", ztPath + "local.conf", true);
                File.Copy(contravpnPath + "zt-moon.ctr", ztPath + @"moons.d\0000008cc55dfcea.moon", true);
                Globals.ZTReady += 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void JoinZTNetwork()
        {
            //MessageBox.Show("here");
            //Process[] vpnprocesses = Process.GetProcessesByName("zt-x" + Globals.userOS);

            Process ztDaemon = new Process();
            ztDaemon.StartInfo.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig";
            ztDaemon.StartInfo.FileName = contravpnPath + "zt-x" + Globals.userOS;
            ztDaemon.StartInfo.Arguments = "-C \"zt\"";
            ztDaemon.StartInfo.UseShellExecute = false;
            //ztDaemon.StartInfo.CreateNoWindow = true;
            //ztDaemon.StartInfo.RedirectStandardOutput = true;
            ztDaemon.Start();
            //string Outputa = ztDaemon.StandardOutput.ReadToEnd();
            //MessageBox.Show(Outputa);
            System.Threading.Thread.Sleep(5000);

            Process ztJoinNetwork = new Process();
            ztJoinNetwork.StartInfo.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig";
            ztJoinNetwork.StartInfo.FileName = contravpnPath + "zt-x" + Globals.userOS;
            ztJoinNetwork.StartInfo.Arguments = "-q -D\"zt\" join 8cc55dfcea100100";
            //ztJoinNetwork.StartInfo.RedirectStandardInput = true;
            ztJoinNetwork.StartInfo.RedirectStandardOutput = true;
            //ztJoinNetwork.StartInfo.RedirectStandardError = true;

            ztJoinNetwork.StartInfo.UseShellExecute = false;
            ztJoinNetwork.StartInfo.CreateNoWindow = true;
            ztJoinNetwork.Start();
            string Output = ztJoinNetwork.StandardOutput.ReadToEnd();
            //MessageBox.Show(Output);
            if (Output.Contains("OK"))
            {
                Globals.ZTReady += 1;
                ztDaemon.CloseMainWindow();
                ztDaemon.Close();
                //ztDaemon.Kill();
                //ztJoinNetwork.Kill();
                if (Globals.GB_Checked == true)
                {
                    MessageBox.Show("VPN successfully installed!");
                }
                else if (Globals.RU_Checked == true)
                {
                    MessageBox.Show("VPN успешно установлен!");
                }
                else if (Globals.UA_Checked == true)
                {
                    MessageBox.Show("VPN успішно встановлено!");
                }
                else if (Globals.BG_Checked == true)
                {
                    MessageBox.Show("VPN успешно инсталиран!");
                }
                else if (Globals.DE_Checked == true)
                {
                    MessageBox.Show("VPN erfolgreich installiert!");
                }
            }
            else
            {
                ztDaemon.CloseMainWindow();
                ztDaemon.Close();
                if (Globals.GB_Checked == true)
                {
                    MessageBox.Show("An error occurred while attempting to join the ZT network.\n" + Output, "Network join failed.");
                }
                else if (Globals.RU_Checked == true)
                {
                    MessageBox.Show("Произошла ошибка при попытке присоединиться к сети ZT.\n" + Output, "Не удалось подключиться к сети.");
                }
                else if (Globals.UA_Checked == true)
                {
                    MessageBox.Show("Під час спроби приєднатися до мережі ZT сталася помилка.\n" + Output, "Помилка приєднання до мережі.");
                }
                else if (Globals.BG_Checked == true)
                {
                    MessageBox.Show("Възникна грешка в опита да се присъедините към ZT мрежата.\n" + Output, "Присъединяването към мрежата се провали.");
                }
                else if (Globals.DE_Checked == true)
                {
                    MessageBox.Show("Beim Versuch, dem ZT-Netzwerk beizutreten, ist ein Fehler aufgetreten.\n" + Output, "Netzwerkverbindung fehlgeschlagen.");
                }
            }
        }

        public void LeaveZTNetwork()
        {
            Process[] vpnprocesses = Process.GetProcessesByName("zt-x" + Globals.userOS);

            Process ztDaemon = new Process();
            ztDaemon.StartInfo.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig";
            ztDaemon.StartInfo.FileName = contravpnPath + "zt-x" + Globals.userOS;
            ztDaemon.StartInfo.Arguments = "-C \"zt\"";
            ztDaemon.StartInfo.UseShellExecute = false;
            //ztDaemon.StartInfo.CreateNoWindow = true;

            //ztDaemon.StartInfo.RedirectStandardInput = true;

            ztDaemon.Start();
            System.Threading.Thread.Sleep(5000);

            Process ztLeaveNetwork = new Process();
            ztLeaveNetwork.StartInfo.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig";
            ztLeaveNetwork.StartInfo.FileName = contravpnPath + "zt-x" + Globals.userOS;
            ztLeaveNetwork.StartInfo.Arguments = "-q -D\"zt\" leave 8cc55dfcea100100";
            ztLeaveNetwork.StartInfo.RedirectStandardOutput = true;
            ztLeaveNetwork.StartInfo.UseShellExecute = false;
            ztLeaveNetwork.StartInfo.CreateNoWindow = true;

            //ztLeaveNetwork.StartInfo.RedirectStandardInput = true;

            ztLeaveNetwork.Start();

            string Output = ztLeaveNetwork.StandardOutput.ReadToEnd();
            //MessageBox.Show(Output);
            if (Output.Contains("OK"))
            {
                try
                {
                    ztDaemon.CloseMainWindow();
                    ztDaemon.Close();
                    //ztLeaveNetwork.CloseMainWindow();
                    //ztLeaveNetwork.Close();
                    //ztDaemon.Kill();
                    //ztLeaveNetwork.Kill();
                }
                catch { }
                //ztDaemon.StandardInput.WriteLine("exit");
                //ztLeaveNetwork.StandardInput.WriteLine("exit");
            }
            else if (Output.Contains("404") || Output.Contains("failed"))
            {
                try
                {
                    ztDaemon.CloseMainWindow();
                    ztDaemon.Close();
                    //ztLeaveNetwork.CloseMainWindow();
                    //ztLeaveNetwork.Close();
                    //ztDaemon.Kill();
                    //ztLeaveNetwork.Kill();
                }
                catch { }
                //ztDaemon.StandardInput.WriteLine("exit");
                //ztLeaveNetwork.StandardInput.WriteLine("exit");
                if (Globals.GB_Checked == true)
                {
                    MessageBox.Show("Attempt to leave ZT network failed. Perhaps you've already left the network.\n\nContinuing with ZT driver uninstallation process...", "Network leave failed.");
                }
                else if (Globals.RU_Checked == true)
                {
                    MessageBox.Show("Попытка покинуть сеть ZT не удалась. Возможно, вы уже вышли из сети.\n\nПродолжаем процесс удаления драйвера ZT...", "Не удалось выйти из сети.");
                }
                else if (Globals.UA_Checked == true)
                {
                    MessageBox.Show("Не вдалося спробувати залишити мережу ZT. Можливо, ви вже вийшли з мережі.\n\nПродовження процесу видалення драйвера ZT...", "Не вдалося вийти з мережі.");
                }
                else if (Globals.BG_Checked == true)
                {
                    MessageBox.Show("Опитът да напуснете ZT мрежата се провали. Вероятно вече сте я напуснали.\n\nПродължаваме с деинсталацията на ZT драйвера...", "Провал в опита да излезете от мрежата.");
                }
                else if (Globals.DE_Checked == true)
                {
                    MessageBox.Show("Der Versuch, das ZT-Netzwerk zu verlassen, ist fehlgeschlagen. Möglicherweise haben Sie das Netzwerk bereits verlassen.\n\nWeiter mit der Deinstallation des ZT-Treibers...", "Netzwerk konnte nicht verlassen werden.");
                }
            }
        }

        public void UninstallZTDriver()
        {
            string infName = null;
            //System.Management.ManagementObjectSearcher objSearcher = new System.Management.ManagementObjectSearcher("Select * from Win32_PnPSignedDriver Where DeviceName = 'ZeroTier One Virtual Port'");
            //System.Management.ManagementObjectCollection objCollection = objSearcher.Get();
            //foreach (System.Management.ManagementObject obj in objCollection)
            //{
            //    infName = String.Format("{0}", obj["InfName"]);
            //    //MessageBox.Show(infName);
            //}
            //MessageBox.Show(infName);

            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            //cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            cmd.StandardInput.WriteLine("pnputil -e");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            string Output = cmd.StandardOutput.ReadToEnd();

            //MessageBox.Show(Output);
            try
            {
                using (StringReader reader = new StringReader(Output))
                {
                    string line = string.Empty;
                    do
                    {
                        line = reader.ReadLine();
                        if (line.Contains("ZeroTier Networks LLC"))
                        {
                            infName = infName.Substring(infName.IndexOf(':') + 1);
                            infName = Regex.Replace(infName, @"\s+", "");
                            break;
                        }
                        infName = line;
                    } while (line != null);
                }
            }
            catch { }

            if (!string.IsNullOrWhiteSpace(infName)) //if (Output.Contains(infName))
            {
                //cmd.CloseMainWindow();
                //cmd.Close();

                Process cmd2 = new Process();
                cmd2.StartInfo.FileName = "cmd.exe";
                cmd2.StartInfo.RedirectStandardInput = true;
                cmd2.StartInfo.RedirectStandardOutput = true;
                //cmd2.StartInfo.CreateNoWindow = true;
                cmd2.StartInfo.UseShellExecute = false;
                cmd2.Start();
                //MessageBox.Show(infName);
                cmd2.StandardInput.WriteLine("pnputil -f -d " + infName);
                cmd2.StandardInput.Flush();
                cmd2.StandardInput.Close();

                string Output2 = cmd2.StandardOutput.ReadToEnd();
                //MessageBox.Show(Output2);

                if (Output2.Contains("Driver package deleted successfully."))
                {
                    try
                    {
                        cmd.CloseMainWindow();
                        cmd.Close();
                        cmd2.CloseMainWindow();
                        cmd2.Close();
                    }
                    catch { }
                    //MessageBox.Show("uninst driver succc");
                    Globals.ZTDriverUninstallSuccessful = true;
                }
            }
        }
    }
}
