using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Contra
{
    class ZT
    {
        WebClient ztDL = new WebClient();
        //string ZTURL = "https://download.zerotier.com/dist/ZeroTier%20One.msi";
        string ZTFileName = "ZeroTier One.msi";

        public void CheckZTInstall(string ZTURL)
        {
            Globals.ZTReady = 0;

            try
            {
                //Download ZT
                if (!File.Exists(ZTFileName)) //If user doesn't have ZT
                {
                    ztDL.DownloadFileCompleted += new AsyncCompletedEventHandler(ztDL_DownloadCompleted);
                    //         ztDL.DownloadProgressChanged += ztDL_DownloadProgressChanged;

                    CheckIfFileIsAvailable(ZTURL);
                    //             currentFile = ZTFileName;
                    ztDL.OpenRead(ZTURL);
                    //              bytes_total = Convert.ToInt64(ztDL.ResponseHeaders["Content-Length"]);

                    ztDL.DownloadFileAsync(new Uri(ZTURL), Application.StartupPath + @"\" + ZTFileName);

                    //         PatchDLPanel.Show();

                    //  while (wc.IsBusy) { }
                }

                CheckZTInstallSteps();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

            //        if (ztExeByName.Length != 0)
            //{

        void ztDL_DownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
       //     PatchDLPanel.Hide();
            //TO-DO: update version string? Currently handled by forced launcher restart

            if (e.Cancelled)
            {
                // delete the partially-downloaded file
                File.Delete(ZTFileName);
                return;
            }

            // display completion status.
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                return;
            }

            ////Extract zip
            //string extractPath = Application.StartupPath;
            //string zipPath = Application.StartupPath + @"\" + ZTFileName;

            //try //To prevent crash
            //{
            //    System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, extractPath);
            //}
            //catch { }
            //File.Delete(ZTFileName);



            //Show a message when the patch download has completed
            //if (Globals.GB_Checked == true)
            //{
            //    MessageBox.Show("A new patch has been downloaded!\n\nThe application will now restart!", "Update Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else if (Globals.RU_Checked == true)
            //{
            //    MessageBox.Show("Новый патч был загружен!\n\nПриложение будет перезагружено!", "Обновление завершено", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else if (Globals.UA_Checked == true)
            //{
            //    MessageBox.Show("Новий виправлення завантажено!\n\nПрограма буде перезавантажена!", "Оновлення завершено", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else if (Globals.BG_Checked == true)
            //{
            //    MessageBox.Show("Нов пач беше изтеглен!\n\nСега ще се рестартира!", "Обновяването е завършено", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else if (Globals.DE_Checked == true)
            //{
            //    MessageBox.Show("Ein neuer Patch wurde heruntergeladen!\n\nDas Programm wird sich jetzt neu starten!", "Aktualisierung abgeschlossen", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}



            //          Application.Restart();


            CheckZTInstallSteps();
        }

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

        public void CheckZTInstallSteps()
        {
            bool installMessage = false;

            //Package downloaded but not installed
            if (File.Exists(ZTFileName) && (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt")))
            {
                if (installMessage == false)
                {
                    DisplayInstallMessage();
                    installMessage = true;
                }
                InstallZTPackage();
            }
            else
            {
                Globals.ZTReady += 1;
            }

            //ZT Driver missing

            //bool found = false;
            //System.Management.ManagementObjectSearcher objSearcher = new System.Management.ManagementObjectSearcher("Select * from Win32_PnPSignedDriver Where DeviceName = 'Zerotier One Virtual Port'");
            //System.Management.ManagementObjectCollection objCollection = objSearcher.Get();
            //foreach (System.Management.ManagementObject obj in objCollection)
            //{
            //    found = true;
            //}

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

            if (!string.IsNullOrWhiteSpace(infName)) //if (Output.Contains(infName))
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

            //ZT Files missing in LocalAppData
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt\CommonAppDataFolder\ZeroTier\One\config\moons.d") ||
                !File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt\CommonAppDataFolder\ZeroTier\One\config\local.conf"))
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

            //ZT Network not joined
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt\CommonAppDataFolder\ZeroTier\One\config\networks.d") ||
                Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt\CommonAppDataFolder\ZeroTier\One\config\networks.d").Length < 2)
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

        public void InstallZTPackage()
        {
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt");

            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine("msiexec /a \"ZeroTier One.msi\" /qb TARGETDIR=\"%LOCALAPPDATA%\\Contra\\vpnconfig\\zt\"");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Globals.ZTReady += 1;
        }

        public void InstallZTDriver()
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine("pnputil -i -a \"" + Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt\CommonAppDataFolder\ZeroTier\One\config\tap-windows\x" + Globals.userOS + "\\zttap300.inf\"");
            //cmd.StandardInput.WriteLine("pnputil /i /a \"$PWD\\config\\tap-windows\\x" + Globals.userOS + "\\zttap300.inf\"");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Globals.ZTReady += 1;
        }

        public void MoveZTFiles()
        {
            string sourcePath = Environment.CurrentDirectory + @"\contra\vpn\";
            string targetPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt\CommonAppDataFolder\ZeroTier\One\";
            string configSourcePath = Environment.CurrentDirectory + @"\contra\vpn\config\";
            string configTargetPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt\CommonAppDataFolder\ZeroTier\One\config";

            try
            {
                File.Copy(sourcePath + "zerotier-one_x64.exe", targetPath + "zerotier-one_x64.exe", true);
                File.Copy(sourcePath + "zerotier-one_x86.exe", targetPath + "zerotier-one_x86.exe", true);
                File.Copy(sourcePath + "zt-cli.cmd", targetPath + "zt-cli.cmd", true);
                File.Copy(sourcePath + "zt-daemon.cmd", targetPath + "zt-daemon.cmd", true);
                File.Copy(sourcePath + "zt-join-network.cmd", targetPath + "zt-join-network.cmd", true);
                File.Copy(sourcePath + "zt-leave-network.cmd", targetPath + "zt-leave-network.cmd", true);
                File.Copy(sourcePath + "zt-peers.cmd", targetPath + "zt-peers.cmd", true);
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt\CommonAppDataFolder\ZeroTier\One\config");
                foreach (string dir in Directory.GetDirectories(configSourcePath, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(Path.Combine(configTargetPath, dir.Substring(configSourcePath.Length)));
                }
                foreach (string file_name in Directory.GetFiles(configSourcePath, "*", SearchOption.AllDirectories))
                {
                    File.Copy(file_name, Path.Combine(configTargetPath, file_name.Substring(configSourcePath.Length)));
                }
            }
            catch { }
            Globals.ZTReady += 1;
        }

        public void JoinZTNetwork()
        {
            //MessageBox.Show("here");
            Process[] vpnprocesses = Process.GetProcessesByName("zerotier-one_x" + Globals.userOS);

            Process ztDaemon = new Process();
            ztDaemon.StartInfo.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt\CommonAppDataFolder\ZeroTier\One";
            ztDaemon.StartInfo.FileName = ztDaemon.StartInfo.WorkingDirectory + @"\zerotier-one_x" + Globals.userOS;
            ztDaemon.StartInfo.Arguments = "-C \"config\"";
            ztDaemon.StartInfo.UseShellExecute = false;
            //ztDaemon.StartInfo.CreateNoWindow = true;
            ztDaemon.Start();
            System.Threading.Thread.Sleep(3000);

            Process ztJoinNetwork = new Process();
            ztJoinNetwork.StartInfo.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt\CommonAppDataFolder\ZeroTier\One";
            ztJoinNetwork.StartInfo.FileName = ztJoinNetwork.StartInfo.WorkingDirectory + @"\zerotier-one_x" + Globals.userOS;
            ztJoinNetwork.StartInfo.Arguments = "-q -D\"config\" join 8cc55dfcea100100";
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
            }
            else
            {
                ztDaemon.CloseMainWindow();
                ztDaemon.Close();
                if (Globals.GB_Checked == true)
                {
                    MessageBox.Show("An error occurred while attempting to join the ZT network.", "Network join failed.");
                }
                else if (Globals.RU_Checked == true)
                {
                    MessageBox.Show("Произошла ошибка при попытке присоединиться к сети ZT.", "Не удалось подключиться к сети.");
                }
                else if (Globals.UA_Checked == true)
                {
                    MessageBox.Show("Під час спроби приєднатися до мережі ZT сталася помилка.", "Помилка приєднання до мережі.");
                }
                else if (Globals.BG_Checked == true)
                {
                    MessageBox.Show("Възникна грешка в опита да се присъедините към ZT мрежата.", "Присъединяването към мрежата се провали.");
                }
                else if (Globals.DE_Checked == true)
                {
                    MessageBox.Show("Beim Versuch, dem ZT-Netzwerk beizutreten, ist ein Fehler aufgetreten.", "Netzwerkverbindung fehlgeschlagen.");
                }
            }
        }

        public void LeaveZTNetwork()
        {
            Process[] vpnprocesses = Process.GetProcessesByName("zerotier-one_x" + Globals.userOS);

            Process ztDaemon = new Process();
            ztDaemon.StartInfo.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt\CommonAppDataFolder\ZeroTier\One";
            ztDaemon.StartInfo.FileName = ztDaemon.StartInfo.WorkingDirectory + @"\zerotier-one_x" + Globals.userOS;
            ztDaemon.StartInfo.Arguments = "-C \"config\"";
            ztDaemon.StartInfo.UseShellExecute = false;
            //ztDaemon.StartInfo.CreateNoWindow = true;

            //ztDaemon.StartInfo.RedirectStandardInput = true;

            ztDaemon.Start();
            System.Threading.Thread.Sleep(3000);

            Process ztLeaveNetwork = new Process();
            ztLeaveNetwork.StartInfo.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt\CommonAppDataFolder\ZeroTier\One";
            ztLeaveNetwork.StartInfo.FileName = ztLeaveNetwork.StartInfo.WorkingDirectory + @"\zerotier-one_x" + Globals.userOS;
            ztLeaveNetwork.StartInfo.Arguments = "-q -D\"config\" leave 8cc55dfcea100100";
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
            Globals.TriedToLeaveNetwork = true;
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
            cmd.StartInfo.CreateNoWindow = true;
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
                cmd2.StartInfo.CreateNoWindow = true;
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
                    //MessageBox.Show("uninst driver succc");
                    Globals.ZTDriverUninstallSuccessful = true;
                }
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
    }
}
