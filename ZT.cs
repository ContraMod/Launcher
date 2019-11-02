﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
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
                ztDL.DownloadFileCompleted += new AsyncCompletedEventHandler(ztDL_DownloadCompleted);
                //         ztDL.DownloadProgressChanged += ztDL_DownloadProgressChanged;

                //Download ZT
                if (!File.Exists(ZTFileName)) //If user doesn't have ZT
                {
                    CheckIfFileIsAvailable(ZTURL);
                    //             currentFile = ZTFileName;
                    ztDL.OpenRead(ZTURL);
                    //              bytes_total = Convert.ToInt64(ztDL.ResponseHeaders["Content-Length"]);

                    ztDL.DownloadFileAsync(new Uri(ZTURL), Application.StartupPath + @"\" + ZTFileName);
                }
                //         PatchDLPanel.Show();

                //  while (wc.IsBusy) { }

                CheckZTInstallSteps();
            }
            catch (Exception ex) { Console.Error.WriteLine(ex); }
        }

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
            if (Globals.GB_Checked == true)
            {
                MessageBox.Show("A new patch has been downloaded!\n\nThe application will now restart!", "Update Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (Globals.RU_Checked == true)
            {
                MessageBox.Show("Новый патч был загружен!\n\nПриложение будет перезагружено!", "Обновление завершено", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (Globals.UA_Checked == true)
            {
                MessageBox.Show("Новий виправлення завантажено!\n\nПрограма буде перезавантажена!", "Оновлення завершено", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (Globals.BG_Checked == true)
            {
                MessageBox.Show("Нов пач беше изтеглен!\n\nСега ще се рестартира!", "Обновяването е завършено", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (Globals.DE_Checked == true)
            {
                MessageBox.Show("Ein neuer Patch wurde heruntergeladen!\n\nDas Programm wird sich jetzt neu starten!", "Aktualisierung abgeschlossen", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //          Application.Restart();


            CheckZTInstallSteps();
        }

        public void CheckZTInstallSteps()
        {
            //Package downloaded but not installed
            if (File.Exists(ZTFileName) && (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt")))
            {
                InstallZTPackage();
            }
            else
            {
                Globals.ZTReady += 1;
            }

            //ZT Driver missing
            System.Management.SelectQuery query = new System.Management.SelectQuery("Win32_SystemDriver");
            query.Condition = "Name = 'zttap300'"; //"DisplayName = 'Zerotier One Virtual Port'";
            System.Management.ManagementObjectSearcher searcher = new System.Management.ManagementObjectSearcher(query);
            var drivers = searcher.Get();

            if (drivers.Count == 0)
            {
                InstallZTDriver();
            }
            else
            {
                Globals.ZTReady += 1;
            }

            //ZT Files missing in LocalAppData
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt\CommonAppDataFolder\ZeroTier\One\config"))
            {
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

            cmd.StandardInput.WriteLine("pnputil /i /a \"$PWD\\config\\tap-windows\\x" + Globals.userOS + "\\zttap300.inf\"");
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
                Globals.ZTReady += 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void JoinZTNetwork()
        {
            Process[] vpnprocesses = Process.GetProcessesByName("zerotier-one_x" + Globals.userOS);

            Process ztDaemon = new Process();
            ztDaemon.StartInfo.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt\CommonAppDataFolder\ZeroTier\One";
            ztDaemon.StartInfo.FileName = ztDaemon.StartInfo.WorkingDirectory + @"\zerotier-one_x" + Globals.userOS;
            ztDaemon.StartInfo.Arguments = "-C \"config\"";
            ztDaemon.StartInfo.UseShellExecute = false;
            ztDaemon.StartInfo.CreateNoWindow = true;
            ztDaemon.Start();

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
                ztDaemon.Kill();
                ztJoinNetwork.Kill();
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
            catch (WebException ex)
            {
                /* A WebException will be thrown if the status of the response is not `200 OK` */
                if (Globals.GB_Checked == true)
                {
                    MessageBox.Show("The file is currently unavailable. Try again later or download it from: www.moddb.com/mods/contra/downloads", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (Globals.RU_Checked == true)
                {
                    MessageBox.Show("Файл в данный момент недоступен. Попробуйте позже или загрузите его с: www.moddb.com/mods/contra/downloads", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (Globals.UA_Checked == true)
                {
                    MessageBox.Show("Файл наразі недоступний. Повторіть спробу пізніше або завантажте його з: www.moddb.com/mods/contra/downloads", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (Globals.BG_Checked == true)
                {
                    MessageBox.Show("Понастоящем файлът не е налице. Опитайте отново по-късно или го изтеглете от: www.moddb.com/mods/contra/downloads", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (Globals.DE_Checked == true)
                {
                    MessageBox.Show("Die Datei ist derzeit nicht verfügbar. Versuchen Sie es später noch einmal oder laden Sie es von folgender Adresse herunter: www.moddb.com/mods/contra/downloads", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Information);
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