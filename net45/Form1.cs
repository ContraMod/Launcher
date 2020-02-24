using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Net;
using System.Runtime.InteropServices;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;

namespace Contra
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            InitializeComponent();
            Application.ApplicationExit += new EventHandler(OnApplicationExit);
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
            RadioFlag_GB.TabStop = false;
            RadioFlag_RU.TabStop = false;
            RadioFlag_UA.TabStop = false;
            RadioFlag_BG.TabStop = false;
            RadioFlag_DE.TabStop = false;
            vpn_start.TabStop = false;
            ZTConsoleBtn.TabStop = false;
            ZTConfigBtn.TabStop = false;
            ZTNukeBtn.TabStop = false;
            DonateBtn.TabStop = false;
            vpn_start.FlatAppearance.MouseOverBackColor = Color.Transparent;
            vpn_start.FlatAppearance.MouseDownBackColor = Color.Transparent;
            vpn_start.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            DonateBtn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            DonateBtn.FlatAppearance.MouseDownBackColor = Color.Transparent;

            //Determine OS bitness
            if (IntPtr.Size == 8)
            {
                Globals.userOS = "64";
            }
            else
            {
                Globals.userOS = "32";
            }
            
            IP_LabelReset();
            DelTmpChunk();
        }

        private void IP_LabelReset()
        {
            try
            {

                IP_Label.Text = new Dictionary<string, bool>()
                {
                    { "ContraVPN: Off", Globals.GB_Checked },
                    { "ContraVPN: Выкл", Globals.RU_Checked },
                    { "ContraVPN: Вимк", Globals.UA_Checked },
                    { "ContraVPN: Изкл", Globals.BG_Checked },
                    { "ContraVPN: Aus", Globals.DE_Checked },
                }.Single(l => l.Value).Key;
            }
            catch { IP_Label.Text = "ContraVPN: Off"; };
        }

        string currentFileLabel;
        //string currentFile;

        string newVersion, genToolFileName = "";

        //int modVersionLocalInt;
        //bool patch1Found, patch2Found;
        bool applyNewLauncher = false;

        bool disableVPNOnBtn = false;
        string ip;

        static string launcherExecutingPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        readonly string contravpnPath = Environment.CurrentDirectory + @"\contra\vpn\";

        [DllImport("version.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetFileVersionInfoSize(string lptstrFilename, out int lpdwHandle);
        [DllImport("version.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GetFileVersionInfo(string lptstrFilename, int dwHandle, int dwLen, byte[] lpData);
        [DllImport("version.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool VerQueryValue(byte[] pBlock, string lpSubBlock, out IntPtr lplpBuffer, out int puLen);

        [DllImport("ntdll.dll", CharSet = CharSet.Ansi)]
        public static extern string wine_get_version();

        public static bool IsRunningOnWine()
        {
            try
            {
                string wineVer = wine_get_version();
                MessageBox.Show($"We've detected you're running launcher on Wine {wineVer}\nVPN functionality disabled. Instead of using the bundled ZT for Windows, install ZeroTier One from your distro's package repositories.\n\nThen you can start the ZT service and run\n\nsudo zerotier-cli join 8cc55dfcea100100\n\nto connect to the ContraVPN network.\n\nPlease visit #support channel on our discord if you need help with this.");
                return true;
            }
            catch { return false; }
        }

        public static readonly CancellationTokenSource httpCancellationToken = new CancellationTokenSource();

        public async void GetLauncherUpdate(string motd, string launcher_url)
        {
            string launcher_ver = motd.Substring(motd.LastIndexOf("Launcher: ") + 10);
            newVersion = launcher_ver.Substring(0, launcher_ver.IndexOf("$"));
            string zip_url = launcher_url + launcher_ver.Substring(0, launcher_ver.IndexOf("$")) + @"/Contra_Launcher.zip";
            string zip_path = zip_url.Split('/').Last();

            // If there is a new launcher version, call the DownloadUpdate method
            if (newVersion != Application.ProductVersion)
            {
                try
                {
                    var updatePendingText = new Dictionary<Tuple<string, string>, bool>
                    {
                        { Tuple.Create($"Contra Launcher version {newVersion} is available! Click OK to update and restart!", "Update Available"), Globals.GB_Checked},
                        { Tuple.Create($"Версия Contra Launcher {newVersion} доступна! Нажмите «ОК», чтобы обновить и перезапустить!", "Доступно обновление"), Globals.RU_Checked},
                        { Tuple.Create($"Версія Contra Launcher {newVersion} доступна! Натисніть кнопку ОК, щоб оновити та перезапустити!", "Доступне оновлення"), Globals.UA_Checked},
                        { Tuple.Create($"Contra Launcher версия {newVersion} е достъпна! Щракнете OK, за да обновите и рестартирате!", "Достъпна е актуализация"), Globals.BG_Checked},
                        { Tuple.Create($"Contra Launcher version {newVersion} ist verfьgbar! Klicke OK zum aktualisieren und neu starten!", "Aktualisierung verfьgbar"), Globals.DE_Checked},
                    }.Single(l => l.Value).Key;
                    MessageBox.Show(new Form { TopMost = true }, updatePendingText.Item1, updatePendingText.Item2, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    await DownloadFile(zip_url, zip_path, TimeSpan.FromMinutes(5), httpCancellationToken.Token);

                    using (ZipArchive archive = await Task.Run(() => ZipFile.OpenRead(zip_path)))
                    {
                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            if (entry.Name == "Contra_Launcher.exe") continue;
                            await Task.Run(() => entry.ExtractToFile(entry.Name, true));
                        }
                    }

                    File.Delete(zip_path);
                    applyNewLauncher = true;

                    // Show a message when the launcher download has completed
                    var updateDoneText = new Dictionary<Tuple<string, string>, bool>
                    {
                        { Tuple.Create("Your application is now up-to-date!\n\nThe application will now restart!", "Update Complete"), Globals.GB_Checked},
                        { Tuple.Create("Ваше приложение теперь обновлено!\n\nПриложение будет перезагружено!", "Обновление завершено"), Globals.RU_Checked},
                        { Tuple.Create("Ваша готова до оновлення!\n\nПрограма буде перезавантажена!", "Оновлення завершено"), Globals.UA_Checked},
                        { Tuple.Create("Приложението е вече обновено!\n\nСега ще се рестартира!", "Обновяването е завършено"), Globals.BG_Checked},
                        { Tuple.Create("Ihr Programm ist jetzt auf dem neuesten Stand!\n\nDas Programm wird sich jetzt neu starten!", "Aktualisierung abgeschlossen"), Globals.DE_Checked},
                    }.Single(l => l.Value).Key;
                    MessageBox.Show(new Form { TopMost = true }, updateDoneText.Item1, updateDoneText.Item2, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                }
                catch (OperationCanceledException)
                {
                    applyNewLauncher = false;
                    File.Delete(zip_path); // Clean-up partial download
                    PatchDLPanel.Hide();
                }
                catch (Exception ex)
                {
                    applyNewLauncher = false;
                    File.Delete(zip_path); // Clean-up partial download
                    PatchDLPanel.Hide();
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        //public void GetModUpdate(string motd, string patch_url)
        //{
        //    try
        //    {
        //        WebClient wc = new WebClient();

        //        // Get mod version
        //        string modVersionActual = motd.Substring(motd.LastIndexOf("Mod: ") + 5);
        //        modVersionActual = modVersionActual.Substring(0, modVersionActual.IndexOf("$"));

        //        int modVersionActualInt = int.Parse(modVersionActual);

        //        // Get mod version text
        //        string modVersionText = motd.Substring(motd.LastIndexOf("Mod Text: ") + 10); //The latest patch name
        //        modVersionText = modVersionText.Substring(0, modVersionText.IndexOf("$"));

        //        // Determine current mod version
        //        if (File.Exists("!Contra009Final.big") || File.Exists("!Contra009Final.ctr"))
        //        {
        //            if (File.Exists("!!!Contra009Final_Patch2.big") || File.Exists("!!!Contra009Final_Patch2.ctr") && File.Exists("!!!Contra009Final_Patch2_GameData.big") || File.Exists("!!!Contra009Final_Patch2_GameData.ctr"))
        //            {
        //                patch2Found = true;
        //            }
        //            if (File.Exists("!!Contra009Final_Patch1.big") || File.Exists("!!Contra009Final_Patch1.ctr"))
        //            {
        //                patch1Found = true;
        //            }
        //            if (patch1Found == true && patch2Found == true)
        //            {
        //                modVersionLocalInt = 2;
        //            }
        //        }

        //        // Download new mod version if local one is outdated and launcher is up to date
        //        if ((modVersionLocalInt < modVersionActualInt) && (newVersion == Application.ProductVersion) && (File.Exists("!Contra009Final.big") || File.Exists("!Contra009Final.ctr")))
        //        {
        //            if (patch1Found == false)
        //            {
        //                var patch1Text = new Dictionary<Tuple<string, string>, bool>
        //                {
        //                    { Tuple.Create("Contra is not up to date. An old patch is missing and needs to be downloaded! Click OK to update!", "Update Available"), Globals.GB_Checked},
        //                    { Tuple.Create("Contra должна быть обновлена. Старый патч отсутствует и должен быть загружен! Нажмите «ОК», чтобы обновить!", "Доступно обновление"), Globals.RU_Checked},
        //                    { Tuple.Create("Contra повинна бути оновлена. Старий патч відсутній і його потрібно завантажити! Натисніть кнопку ОК, щоб оновити!", "Доступне оновлення"), Globals.UA_Checked},
        //                    { Tuple.Create("Contra трябва да бъде обновена. Стар пач липсва и трябва да бъде изтеглен! Щракнете OK, за да обновите!", "Достъпна е актуализация"), Globals.BG_Checked},
        //                    { Tuple.Create("Contra muss aktualisiert werden. Ein alter Patch fehlt und muss heruntergeladen werden! Klicke OK zum aktualisieren!", "Aktualisierung verfьgbar"), Globals.DE_Checked},
        //                }.Single(l => l.Value).Key;
        //                MessageBox.Show(new Form { TopMost = true }, patch1Text.Item1, patch1Text.Item2, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //            else if (patch2Found == false)
        //            {
        //                var patch2Text = new Dictionary<Tuple<string, string>, bool>
        //                {
        //                    { Tuple.Create("Contra is not up to date. The latest version which will be downloaded now is " + modVersionText + "! Click OK to update!", "Update Available"), Globals.GB_Checked},
        //                    { Tuple.Create("Contra должна быть обновлена. Последняя версия " + modVersionText + "! Нажмите «ОК», чтобы обновить!", "Доступно обновление"), Globals.RU_Checked},
        //                    { Tuple.Create("Contra повинна бути оновлена. Остання версія " + modVersionText + "! Натисніть кнопку ОК, щоб оновити!", "Доступне оновлення"), Globals.UA_Checked},
        //                    { Tuple.Create("Contra трябва да бъде обновена. Последната версия е " + modVersionText + "! Щракнете OK, за да обновите!", "Достъпна е актуализация"), Globals.BG_Checked},
        //                    { Tuple.Create("Contra muss aktualisiert werden. Die neueste Version ist " + modVersionText + "! Klicke OK zum aktualisieren!", "Aktualisierung verfьgbar"), Globals.DE_Checked},
        //                }.Single(l => l.Value).Key;
        //                MessageBox.Show(new Form { TopMost = true }, patch2Text.Item1, patch2Text.Item2, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //            DownloadModUpdate(patch_url);
        //        }
        //    }
        //    catch { }
        //}

        //static String BytesToString(long byteCount)
        //{
        //    string[] suf = { " B", " KB", " MB", " GB", " TB", " PB", " EB" }; //Longs run out around EB
        //    if (byteCount == 0)
        //        return "0" + suf[0];
        //    long bytes = Math.Abs(byteCount);
        //    int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
        //    double num = Math.Round(bytes / Math.Pow(1024, place), 1);
        //    return (Math.Sign(byteCount) * num).ToString() + suf[place];
        //}

        //WebClient wcMod = new WebClient();
        //Int64 bytes_total;
        //string patchFileName;

        //public void CheckIfFileIsAvailable(string file_url)
        //{
        //    HttpWebResponse response = null;
        //    var request = (HttpWebRequest)WebRequest.Create(file_url);
        //    request.Method = "GET";

        //    try
        //    {
        //        response = (HttpWebResponse)request.GetResponse();
        //    }
        //    catch (WebException)
        //    {
        //        /* A WebException will be thrown if the status of the response is not `200 OK` */
        //        var unavailableLangText = new Dictionary<Tuple<string, string>, bool>
        //        {
        //            { Tuple.Create("The file is currently unavailable, please try again later.", "Error"), Globals.GB_Checked},
        //            { Tuple.Create("Файл в данный момент недоступен, Повторите связаться позже.", "Ошибка"), Globals.RU_Checked},
        //            { Tuple.Create("Файл наразі недоступний, будь-ласка спробуйте пізніше.", "Помилка"), Globals.UA_Checked},
        //            { Tuple.Create("Понастоящем файлът не е налице, будь-ласка спробуйте пізніше.", "Грешка"), Globals.BG_Checked},
        //            { Tuple.Create("Die Datei ist derzeit nicht verfügbar, bitte versuchen Sie es später noch einmal.", "Fehler"), Globals.DE_Checked},
        //        }.Single(l => l.Value).Key;
        //        MessageBox.Show(new Form { TopMost = true }, unavailableLangText.Item1, unavailableLangText.Item2, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        // Don't forget to close your response.
        //        if (response != null) response.Close();
        //    }
        //}

        public static readonly HttpClient httpclient = new HttpClient();

        public static Tuple<double, string> ByteToSizeType(long value)
        {
            if (value == 0L) return Tuple.Create(0D, "Bytes"); // zero is plural
            IReadOnlyDictionary<long, string> thresholds = new Dictionary<long, string>()
                {
                    { 1, "Byte" },
                    { 2, "Bytes" },
                    { 1024, "KiB" },
                    { 1048576, "MiB" },
                    { 1073741824, "GiB" },
                    { 1099511627776, "TiB" },
                    { 1125899906842620, "PiB" },
                    { 1152921504606850000, "EiB" },
                };
            for (int t = thresholds.Count - 1; t > 0; t--)
            {
                if (value >= thresholds.ElementAt(t).Key) return Tuple.Create(Math.Round((double)value / thresholds.ElementAt(t).Key, 2), thresholds.ElementAt(t).Value);
            }
            // handle negative values if given
            var reValue = ByteToSizeType(-value);
            return Tuple.Create(-reValue.Item1, reValue.Item2);
        }

        public async Task DownloadFile(string url, string outPath, TimeSpan timeout, CancellationToken cancellationToken = default)
        {
            httpclient.Timeout = timeout;
            var response = httpclient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead).Result;
            response.EnsureSuccessStatusCode();

            var contentLength = response.Content.Headers.ContentLength.GetValueOrDefault();
            var totalToDownload = ByteToSizeType(contentLength);
            var downloadSize = totalToDownload.Item1;
            var downloadUnit = totalToDownload.Item2;
            PatchDLPanel.Show();

            using (Stream contentStream = await response.Content.ReadAsStreamAsync(), fileStream = new FileStream(outPath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
            {
                var data = new byte[8192];
                long totalBytesRead = 0L, readCount = 0L;
                bool bytesRemaining = true;

                do
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    var bytesRead = await contentStream.ReadAsync(data, 0, data.Length, cancellationToken);

                    if (bytesRead == 0) bytesRemaining = false;
                    else
                    {
                        await fileStream.WriteAsync(data, 0, bytesRead);
                        totalBytesRead += bytesRead;
                        readCount += 1;
                        if (readCount % 100 == 0)
                        {
                            PatchDLProgressBar.Value = Convert.ToInt32((double)totalBytesRead / contentLength * 100);
                            DLPercentLabel.Text = $"{(double)totalBytesRead / contentLength * 100:F2}%";
                            ModDLCurrentFileLabel.Text = currentFileLabel + outPath;
                            ModDLFileSizeLabel.Text = $"{ByteToSizeType(totalBytesRead).Item1} / {downloadSize} {downloadUnit}";
                            //ModDLFileSizeLabel.Text = $"{BytesToSize(e.BytesReceived, SizeUnits.MiB)} MiB / {BytesToSize(e.TotalBytesToReceive, SizeUnits.MiB)} MiB";
                        }
                    }
                }
                while (bytesRemaining);
            }
            PatchDLPanel.Hide();
        }

        public static async Task DownloadFileSimple(string url, string outPath, TimeSpan timeout)
        {
            httpclient.Timeout = timeout;
            var response = httpclient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead).Result;
            response.EnsureSuccessStatusCode();

            using (var contentStream = new FileStream(outPath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
            {
                await response.Content.CopyToAsync(contentStream);
            }
        }

        //Old WebClient based implementation -- can't cancel download in progress
        //readonly WebClient wcDL = new WebClient();
        //public async Task DownloadFile(string url, string output, TimeSpan timeout)
        //{
        //    PatchDLPanel.Show();
        //    DateTime? lastReceived = null;
        //    wcDL.DownloadProgressChanged += (o, e) =>
        //    {
        //        lastReceived = DateTime.Now;
        //        PatchDLProgressBar.Value = e.ProgressPercentage;
        //        DLPercentLabel.Text = e.ProgressPercentage.ToString() + "%";
        //        ModDLCurrentFileLabel.Text = currentFileLabel + output;
        //        ModDLFileSizeLabel.Text = $"{BytesToSize(e.BytesReceived, SizeUnits.MiB)} MiB / {BytesToSize(e.TotalBytesToReceive, SizeUnits.MiB)} MiB";
        //    };

        //    var download = wcDL.DownloadFileTaskAsync(url, output);

        //    // await until download is completed or timeout expires
        //    while (lastReceived == null || DateTime.Now - lastReceived < timeout)
        //    {
        //        await Task.WhenAny(Task.Delay(1000), download); // 1 second wait vs download task
        //        if (download.IsCompleted) break;
        //    }

        //    PatchDLPanel.Hide();
        //    if (download.IsCanceled) throw new TaskCanceledException("File cancelled by user.");

        //    var exception = download.Exception;
        //    bool timed_out = !download.IsCompleted && exception == null;

        //    // download did not complete, nor did it fail, most likely user's connection dropped, let's cancel it
        //    if (timed_out) wcDL.CancelAsync();

        //    if (timed_out || exception != null)
        //    {
        //        // delete partially downloaded file if any (CancelAsync() is not immediate so multiple tries might be needed)
        //        int fails = 0;
        //        while (true)
        //        {
        //            try
        //            {
        //                File.Delete(output);
        //                break;
        //            }
        //            catch
        //            {
        //                fails++;
        //                if (fails >= 10) break;

        //                await Task.Delay(1000);
        //            }
        //        }
        //    }

        //    if (exception != null) throw new Exception("Failed to download file", exception);
        //    if (timed_out) throw new Exception($"Failed to download file (timeout reached: {timeout})");
        //}

        // There should be only one instance of HttpClient for more efficient socket usage, no need to wrap it in a 'using' or dispose it.

        //public void DownloadModUpdate(string patch_url)
        //{
        //    void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        //    {
        //        // In case you don't have a progressBar Log the value instead 
        //        // Console.WriteLine(e.ProgressPercentage);
        //        PatchDLProgressBar.Value = e.ProgressPercentage;
        //        DLPercentLabel.Text = e.ProgressPercentage.ToString() + "%";
        //        ModDLCurrentFileLabel.Text = currentFileLabel + currentFile;
        //        FileInfo f = new FileInfo(currentFile);
        //        long s1 = f.Length;
        //        ModDLFileSizeLabel.Text = BytesToString(s1) + " / " + BytesToString(bytes_total);
        //    }

        //    void wc_DownloadPatchCompleted(object sender, AsyncCompletedEventArgs e)
        //    {
        //        PatchDLPanel.Hide();
        //        //TO-DO: update version string? Currently handled by forced launcher restart

        //        if (e.Cancelled)
        //        {
        //            // delete the partially-downloaded file
        //            File.Delete(patchFileName);
        //            return;
        //        }

        //        // display completion status.
        //        if (e.Error != null)
        //        {
        //            MessageBox.Show(e.Error.Message);
        //            return;
        //        }

        //        // Extract patch zip
        //        string zipPath = launcherExecutingPath + @"\" + patchFileName;
        //        //if (patchFileName == "Contra009FinalPatch2.zip") //If the current patch installed is patch 2
        //        //{
        //        //    try
        //        //    {
        //        //        Directory.Delete("contra"); //Delete old contra folder containing tinc vpn scripts
        //        //    }
        //        //    catch { }
        //        //}
        //        try //To prevent crash
        //        {
        //            ZipFile.ExtractToDirectory(zipPath, launcherExecutingPath);
        //        }
        //        catch { }
        //        File.Delete(patchFileName);

        //        // Show a message when the patch download has completed
        //        var updateLangText = new Dictionary<Tuple<string, string>, bool>
        //    {
        //        { Tuple.Create("A new patch has been downloaded!\n\nThe application will now restart!", "Update Complete"), Globals.GB_Checked},
        //        { Tuple.Create("Новый патч был загружен!\n\nПриложение будет перезагружено!", "Обновление завершено"), Globals.RU_Checked},
        //        { Tuple.Create("Новий виправлення завантажено!\n\nПрограма буде перезавантажена!", "Оновлення завершено"), Globals.UA_Checked},
        //        { Tuple.Create("Нов пач беше изтеглен!\n\nСега ще се рестартира!", "Обновяването е завършено"), Globals.BG_Checked},
        //        { Tuple.Create("Ein neuer Patch wurde heruntergeladen!\n\nDas Programm wird sich jetzt neu starten!", "Aktualisierung abgeschlossen"), Globals.DE_Checked},
        //    }.Single(l => l.Value).Key;
        //        MessageBox.Show(new Form { TopMost = true }, updateLangText.Item1, updateLangText.Item2, MessageBoxButtons.OK, MessageBoxIcon.Information);

        //        System.Diagnostics.Process.Start(launcherExecutingPath + "\\Contra_Launcher.exe");
        //        this.Close();
        //    }
        //    try
        //    {
        //        wcMod.DownloadFileCompleted += new AsyncCompletedEventHandler(wc_DownloadPatchCompleted);
        //        wcMod.DownloadProgressChanged += wc_DownloadProgressChanged;

        //        // Download one patch at a time
        //        if (modVersionLocalInt != 2) //If user doesn't have the latest patch
        //        {
        //            if (patch1Found == false)
        //            {
        //                patchFileName = "Contra009FinalPatch1.zip";
        //            }
        //            else if (patch2Found == false)
        //            {
        //                patchFileName = "Contra009FinalPatch2.zip";
        //            }
        //            CheckIfFileIsAvailable(patch_url + patchFileName);
        //            currentFile = patchFileName;
        //            wcMod.OpenRead(patch_url + patchFileName);
        //            bytes_total = Convert.ToInt64(wcMod.ResponseHeaders["Content-Length"]);

        //            wcMod.DownloadFileAsync(new Uri(patch_url + patchFileName), Path.Combine(launcherExecutingPath, patchFileName));
        //        }
        //        PatchDLPanel.Show();

        //    }
        //    catch {}
        //}

        public static string UserDataLeafName()
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

        public static bool wait = true;

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

        //**********MINIMIZE FORM CODE START**********
        const int WS_MINIMIZEBOX = 0x20000;
        const int CS_DBLCLKS = 0x8;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= WS_MINIMIZEBOX;
                cp.ClassStyle |= CS_DBLCLKS;
                return cp;
            }
        }
        //**********MINIMIZE FORM CODE END**********

        private void CheckInstallDir()
        {
            if (Globals.GB_Checked == true)
            {
                MessageBox.Show("You have installed Contra in the wrong folder. Install it in the Zero Hour folder which contains the \"generals.exe\". It's very often the parent folder.", "Error");
            }
            else if (Globals.RU_Checked == true)
            {
                MessageBox.Show("Вы установили Contra в неправильную папку. Установите его в папку Zero Hour, которая содержит файл \"generals.exe\". Это очень часто предыдущая папка.", "Ошибка");
            }
            else if (Globals.UA_Checked == true)
            {
                MessageBox.Show("Ви встановили Contra у неправильній папці. Встановіть це в папку Zero Hour, яка містить \"generals.exe\". Це дуже часто попередня папка.", "Помилка");
            }
            else if (Globals.BG_Checked == true)
            {
                MessageBox.Show("Инсталирали сте Contra в грешната папка. Инсталирайте в Zero Hour папката, която съдържа \"generals.exe\". Обикновено това е предишната папка.", "Грешка");
            }
            else if (Globals.DE_Checked == true)
            {
                MessageBox.Show("Du hast Contra im falschen ordner installiert. Installiere es in dem Zero Hour ordner in dem die \"generals.exe\" ist. Es ist sehr oft der übergeordnete Ordner.", "Fehler");
            }
        }

        private void DelTmpChunk()
        {
            if (File.Exists(UserDataLeafName() + "_tmpChunk.dat"))
            {
                File.Delete(UserDataLeafName() + "_tmpChunk.dat");
            }
            else if (File.Exists(myDocPath + "_tmpChunk.dat"))
            {
                File.Delete(myDocPath + "_tmpChunk.dat");
            }
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.BackgroundImage = Properties.Resources._button_exit_text;
            button2.ForeColor = SystemColors.ButtonHighlight;
            button2.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackgroundImage = Properties.Resources._button_exit;
            button2.ForeColor = SystemColors.ButtonHighlight;
            button2.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            button2.BackgroundImage = Properties.Resources._button_highlight;
            button2.ForeColor = SystemColors.ButtonHighlight;
            button2.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }

        private void button2_Click(object sender, EventArgs e) // ExitButton
        {
            Process[] wbByName = Process.GetProcessesByName("worldbuilder_ctr");
            if (wbByName.Length > 0) //if wb is already running
            {
                if (Globals.GB_Checked == true)
                {
                    MessageBox.Show("Mod files could not be unloaded since they are currently in use by World Builder. If you want to unload mod files, close World Builder and run the launcher again. Closing the launcher anyway.", "Error");
                }
                else if (Globals.RU_Checked == true)
                {
                    MessageBox.Show("Файлы мода не могут быть выгружены, так как они в настоящее время используются World Builder. Если вы хотите выгрузить файлы мода, закройте World Builder и снова запустите лаунчер. Закрытие лаунчера в любом случае.", "Ошибка");
                }
                else if (Globals.UA_Checked == true)
                {
                    MessageBox.Show("Файли моду не могли бути розвантажені, оскільки вони в даний час використовуються World Builder. Якщо ви хочете завантажити файли моду, закрийте World Builder і знову запустіть лаунчер. Закриття лаунчера в будь-якому випадку.", "Помилка");
                }
                else if (Globals.BG_Checked == true)
                {
                    MessageBox.Show("Contra файловете не можаха да бъдат деактивирани, тъй като се използват от World Builder. Ако искате да деактивирате Contra, затворете World Builder и стартирайте launcher-а отново.", "Грешка");
                }
                else if (Globals.DE_Checked == true)
                {
                    MessageBox.Show("Mod dateien konnten nicht entladen werden, da sie momentan im World Builder benutzt werden. Falls du die mod dateien entladen wilst, schlieЯe den World Builder und starte den Launcher erneut. SchlieЯt den Launcher sowieso.", "Fehler");
                }
            }
            OnApplicationExit(sender, e);
        }

        private static void DeleteDuplicateFiles()
        {
            if (File.Exists("!!!Contra009Final_Patch2_GameData.ctr") && File.Exists("!!!Contra009Final_Patch2_GameData.big"))
            {
                File.Delete("!!!Contra009Final_Patch2_GameData.big");
            }
            if (File.Exists("!!!Contra009Final_Patch2.ctr") && File.Exists("!!!Contra009Final_Patch2.big"))
            {
                File.Delete("!!!Contra009Final_Patch2.big");
            }
            if (File.Exists("!!!Contra009Final_Patch2_RU.ctr") && File.Exists("!!!Contra009Final_Patch2_RU.big"))
            {
                File.Delete("!!!Contra009Final_Patch2_RU.big");
            }
            if (File.Exists("!!!Contra009Final_Patch2_EN.ctr") && File.Exists("!!!Contra009Final_Patch2_EN.big"))
            {
                File.Delete("!!!Contra009Final_Patch2_EN.big");
            }
            if (File.Exists("!!!Contra009Final_Patch2_NatVO.ctr") && File.Exists("!!!Contra009Final_Patch2_NatVO.big"))
            {
                File.Delete("!!!Contra009Final_Patch2_NatVO.big");
            }
            if (File.Exists("!!!Contra009Final_Patch2_EngVO.ctr") && File.Exists("!!!Contra009Final_Patch2_EngVO.big"))
            {
                File.Delete("!!!Contra009Final_Patch2_EngVO.big");
            }
            if (File.Exists("!!Contra009Final_Patch1.ctr") && File.Exists("!!Contra009Final_Patch1.big"))
            {
                File.Delete("!!Contra009Final_Patch1.big");
            }
            if (File.Exists("!!Contra009Final_Patch1_RU.ctr") && File.Exists("!!Contra009Final_Patch1_RU.big"))
            {
                File.Delete("!!Contra009Final_Patch1_RU.big");
            }
            if (File.Exists("!!Contra009Final_Patch1_EN.ctr") && File.Exists("!!Contra009Final_Patch1_EN.big"))
            {
                File.Delete("!!Contra009Final_Patch1_EN.big");
            }
            if (File.Exists("!!Contra009Final_Patch1_EngVO.ctr") && File.Exists("!!Contra009Final_Patch1_EngVO.big"))
            {
                File.Delete("!!Contra009Final_Patch1_EngVO.big");
            }
            if (File.Exists("!!Contra009Final_FogOff.ctr") && File.Exists("!!Contra009Final_FogOff.big"))
            {
                File.Delete("!!Contra009Final_FogOff.big");
            }
            if (File.Exists("!!Contra009Final_WaterEffectsOff.ctr") && File.Exists("!!Contra009Final_WaterEffectsOff.big"))
            {
                File.Delete("!!Contra009Final_WaterEffectsOff.big");
            }
            if (File.Exists("!!Contra009Final_FunnyGenPics.ctr") && File.Exists("!!Contra009Final_FunnyGenPics.big"))
            {
                File.Delete("!!Contra009Final_FunnyGenPics.big");
            }
            if (File.Exists("!Contra009Final.ctr") && File.Exists("!Contra009Final.big"))
            {
                File.Delete("!Contra009Final.big");
            }
            if (File.Exists("!Contra009Final_NatVO.ctr") && File.Exists("!Contra009Final_NatVO.big"))
            {
                File.Delete("!Contra009Final_NatVO.big");
            }
            if (File.Exists("!Contra009Final_EngVO.ctr") && File.Exists("!Contra009Final_EngVO.big"))
            {
                File.Delete("!Contra009Final_EngVO.big");
            }
            if (File.Exists("!Contra009Final_NewMusic.ctr") && File.Exists("!Contra009Final_NewMusic.big"))
            {
                File.Delete("!Contra009Final_NewMusic.big");
            }
            if (File.Exists("!Contra009Final_EN.ctr") && File.Exists("!Contra009Final_EN.big"))
            {
                File.Delete("!Contra009Final_EN.big");
            }
            if (File.Exists("!Contra009Final_RU.ctr") && File.Exists("!Contra009Final_RU.big"))
            {
                File.Delete("!Contra009Final_RU.big");
            }
            if (File.Exists("langdata.dat") && File.Exists("langdata1.dat"))
            {
                File.Delete("langdata1.dat");
            }
            if (File.Exists("dbghelp.dll") && File.Exists("dbghelp.ctr"))
            {
                File.Delete("dbghelp.ctr");
            }
        }

        private static void RenameBigToCtr()
        {
            try
            {
                List<string> bigs = new List<string>
                {
                    "!!!Contra009Final_Patch2_GameData.big",
                    "!!!Contra009Final_Patch2.big",
                    "!!!Contra009Final_Patch2_RU.big",
                    "!!!Contra009Final_Patch2_EN.big",
                    "!!!Contra009Final_Patch2_NatVO.big",
                    "!!!Contra009Final_Patch2_EngVO.big",
                    "!!Contra009Final_Patch1.big",
                    "!!Contra009Final_Patch1_RU.big",
                    "!!Contra009Final_Patch1_EN.big",
                    "!!Contra009Final_Patch1_EngVO.big",
                    "!!Contra009Final_FogOff.big",
                    "!!Contra009Final_WaterEffectsOff.big",
                    "!!Contra009Final_FunnyGenPics.big",
                    "!Contra009Final.big",
                    "!Contra009Final_NatVO.big",
                    "!Contra009Final_EngVO.big",
                    "!Contra009Final_NewMusic.big",
                    "!Contra009Final_EN.big",
                    "!Contra009Final_RU.big",
                };
                foreach (string big in bigs)
                {
                    string ctr = big.Replace(".big", ".ctr");
                    if (File.Exists(big))
                    {
                        File.Move(big, ctr);
                    }
                }
                if (File.Exists("langdata1.dat"))
                {
                    File.Move("langdata1.dat", "langdata.dat");
                }
                if (Directory.Exists(@"Data\Scripts1"))
                {
                    Directory.Move(@"Data\Scripts1", @"Data\Scripts");
                }

                File.Move("dbghelp.ctr", "dbghelp.dll");

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

                if (File.Exists("generals_zh.exe"))
                {
                    try
                    {
                        File.SetAttributes("generals.exe", FileAttributes.Normal);
                        File.SetAttributes("generals_zh.exe", FileAttributes.Normal);
                        File.SetAttributes("generals.ctr", FileAttributes.Normal);
                        File.Copy("generals_zh.exe", "generals.exe", true);
                    }
                    catch
                    { }
                }
            }
            catch {}
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackgroundImage = Properties.Resources._button_launch_text;
            button1.ForeColor = SystemColors.ButtonHighlight;
            button1.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackgroundImage = Properties.Resources._button_launch;
            button1.ForeColor = SystemColors.ButtonHighlight;
            button1.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            button1.BackgroundImage = Properties.Resources._button_highlight;
            button1.ForeColor = SystemColors.ButtonHighlight;
            button1.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }

        public void IsWbRunning()
        {
            Process[] wbByName = Process.GetProcessesByName("worldbuilder_ctr");
            if (wbByName.Length > 0) //if wb is already running
            {
                if (Globals.GB_Checked == true)
                {
                    MessageBox.Show("Unit voice preferences may not load correctly since World Builder is already running. Starting Contra anyway.", "Error");
                }
                else if (Globals.RU_Checked == true)
                {
                    MessageBox.Show("Настройки голосового канала могут загружаться неправильно, так как World Builder уже запущен. Запуск Contra в любом случае.", "Ошибка");
                }
                else if (Globals.UA_Checked == true)
                {
                    MessageBox.Show("Голоси юнітів можуть завантажуватися не правильно, оскільки World Builder вже працює. Запуск Contra в будь-якому випадку.", "Помилка");
                }
                else if (Globals.BG_Checked == true)
                {
                    MessageBox.Show("Езикът, на който говорят единиците може да не заредят правилно, тъй като World Builder е стартиран. Contra ще стартира въпреки това.", "Грешка");
                }
                else if (Globals.DE_Checked == true)
                {
                    MessageBox.Show("Einheit Sprach Prдferenzen laden eventuell nicht korrekt, weil der World Builder schon lдuft. Contra wird ohnehin gestartet.", "Fehler");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) // LaunchButton
        {
            DeleteDuplicateFiles();
            RenameBigToCtr();
            try
            {
                //if (File.Exists("!Contra009Final.ctr"))
                //{
                //    File.Move("!Contra009Final.ctr", "!Contra009Final.big");
                //}
                //if (File.Exists("!!Contra009Final_Patch1.ctr"))
                //{
                //    File.Move("!!Contra009Final_Patch1.ctr", "!!Contra009Final_Patch1.big");
                //}
                //if (File.Exists("!!!Contra009Final_Patch2.ctr"))
                //{
                //    File.Move("!!!Contra009Final_Patch2.ctr", "!!!Contra009Final_Patch2.big");
                //}
                //if (File.Exists("!!!Contra009Final_Patch2_GameData.ctr"))
                //{
                //    File.Move("!!!Contra009Final_Patch2_GameData.ctr", "!!!Contra009Final_Patch2_GameData.big");
                //}
                try
                {
                    File.Move("!Contra009Final.ctr", "!Contra009Final.big");
                    File.Move("!!Contra009Final_Patch1.ctr", "!!Contra009Final_Patch1.big");
                    File.Move("!!!Contra009Final_Patch2.ctr", "!!!Contra009Final_Patch2.big");
                    File.Move("!!!Contra009Final_Patch2_GameData.ctr", "!!!Contra009Final_Patch2_GameData.big");

                    File.Move("dbghelp.dll", "dbghelp.ctr");
                }
                catch { }
                if ((RadioOrigQuotes.Checked) && (File.Exists("!Contra009Final_NatVO.ctr")))
                {
                    File.Move("!Contra009Final_NatVO.ctr", "!Contra009Final_NatVO.big");
                }
                if ((RadioLocQuotes.Checked) && (File.Exists("!Contra009Final_EngVO.ctr")))
                {
                    File.Move("!Contra009Final_EngVO.ctr", "!Contra009Final_EngVO.big");
                }
                if ((RadioLocQuotes.Checked) && (File.Exists("!!Contra009Final_Patch1_EngVO.ctr")))
                {
                    File.Move("!!Contra009Final_Patch1_EngVO.ctr", "!!Contra009Final_Patch1_EngVO.big");
                }
                if ((RadioOrigQuotes.Checked) && (File.Exists("!!!Contra009Final_Patch2_NatVO.ctr")))
                {
                    File.Move("!!!Contra009Final_Patch2_NatVO.ctr", "!!!Contra009Final_Patch2_NatVO.big");
                }
                if ((RadioLocQuotes.Checked) && (File.Exists("!!!Contra009Final_Patch2_EngVO.ctr")))
                {
                    File.Move("!!!Contra009Final_Patch2_EngVO.ctr", "!!!Contra009Final_Patch2_EngVO.big");
                }
                if ((RadioEN.Checked) && (File.Exists("!Contra009Final_EN.ctr")))
                {
                    File.Move("!Contra009Final_EN.ctr", "!Contra009Final_EN.big");
                }
                if ((RadioEN.Checked) && (File.Exists("!!Contra009Final_Patch1_EN.ctr")))
                {
                    File.Move("!!Contra009Final_Patch1_EN.ctr", "!!Contra009Final_Patch1_EN.big");
                }
                if ((RadioEN.Checked) && (File.Exists("!!!Contra009Final_Patch2_EN.ctr")))
                {
                    File.Move("!!!Contra009Final_Patch2_EN.ctr", "!!!Contra009Final_Patch2_EN.big");
                }
                if ((RadioRU.Checked) && (File.Exists("!Contra009Final_RU.ctr")))
                {
                    File.Move("!Contra009Final_RU.ctr", "!Contra009Final_RU.big");
                }
                if ((RadioRU.Checked) && (File.Exists("!!Contra009Final_Patch1_RU.ctr")))
                {
                    File.Move("!!Contra009Final_Patch1_RU.ctr", "!!Contra009Final_Patch1_RU.big");
                }
                if ((RadioRU.Checked) && (File.Exists("!!!Contra009Final_Patch2_RU.ctr")))
                {
                    File.Move("!!!Contra009Final_Patch2_RU.ctr", "!!!Contra009Final_Patch2_RU.big");
                }
                if ((MNew.Checked) && (File.Exists("!Contra009Final_NewMusic.ctr")))
                {
                    File.Move("!Contra009Final_NewMusic.ctr", "!Contra009Final_NewMusic.big");
                }
                if ((Properties.Settings.Default.Fog == false) && (File.Exists("!!Contra009Final_FogOff.ctr")))
                {
                    File.Move("!!Contra009Final_FogOff.ctr", "!!Contra009Final_FogOff.big");
                }
                else if ((Properties.Settings.Default.Fog == true) && (File.Exists("!!Contra009Final_FogOff.big")))
                {
                    File.Move("!!Contra009Final_FogOff.big", "!!Contra009Final_FogOff.ctr");
                }
                if ((Properties.Settings.Default.WaterEffects == false) && (File.Exists("!!Contra009Final_WaterEffectsOff.ctr")))
                {
                    File.Move("!!Contra009Final_WaterEffectsOff.ctr", "!!Contra009Final_WaterEffectsOff.big");
                }
                else if ((Properties.Settings.Default.WaterEffects == true) && (File.Exists("!!Contra009Final_WaterEffectsOff.big")))
                {
                    File.Move("!!Contra009Final_WaterEffectsOff.big", "!!Contra009Final_WaterEffectsOff.ctr");
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
                    int scripts = Directory.GetFiles(@"Data\Scripts").Length;
                    if (scripts == 0)
                    {
                        Directory.Delete(@"Data\Scripts");
                    }
                }
                if (Directory.Exists(@"Data\Scripts1"))
                {
                    int scripts1 = Directory.GetFiles(@"Data\Scripts1").Length;
                    if (scripts1 == 0)
                    {
                        Directory.Delete(@"Data\Scripts1");
                    }
                }
                if (Directory.Exists(@"Data\Scripts"))
                {
                    Directory.Move(@"Data\Scripts", @"Data\Scripts1");
                }
                if (File.Exists("Install_Final.bmp") && (File.Exists("Install_Final_Contra.bmp")))
                {
                    try
                    {
                        File.SetAttributes("Install_Final.bmp", FileAttributes.Normal);
                        if (File.Exists("Install_Final_ZH"))
                        {
                            File.SetAttributes("Install_Final_ZH.bmp", FileAttributes.Normal);
                        }
                        File.SetAttributes("Install_Final_Contra.bmp", FileAttributes.Normal);
                        File.Copy("Install_Final.bmp", "Install_Final_ZH.bmp", true);
                        File.Copy("Install_Final_Contra.bmp", "Install_Final.bmp", true);
                    }
                    catch
                    { }
                }

                // Disable cyrillic letters, enable German umlauts.
                if (File.Exists("GermanZH.big") && File.Exists("GenArial.ttf"))
                {
                    File.Move("GenArial.ttf", "GenArial_.ttf");
                }

                // Check for generals.ctr
                string message = null;
                string title = null;
                if (!File.Exists("generals.ctr") || CalculateMD5("generals.ctr") != "ee7d5e6c2d7fb66f5c27131f33da5fd3")
                {
                    if (Globals.GB_Checked == true)
                    {
                        message = "\"generals.ctr\" not found or checksum mismatch! Please, extract it from the \"Contra009FinalPatch2\" archive if you want camera height setting to work and be able to play online with ContraVPN.\n\nWould you like to start the game anyway?";
                        title = "Warning";
                    }
                    else if (Globals.RU_Checked == true)
                    {
                        message = "\"generals.ctr\" не найден или несоответствие контрольной суммы! Извлеките его из архива \"Contra009FinalPatch2\", если вы хотите, чтобы настройка высоты камеры работала и была возможность играть онлайн с ContraVPN.\n\nХотели бы вы начать игру?";
                        title = "Предупреждение";
                    }
                    else if (Globals.UA_Checked == true)
                    {
                        message = "\"generals.ctr\" не знайден або невідповідність контрольної суми! Будь ласка, витягніть його з архіву \"Contra009FinalPatch2\", якщо ви хочете, щоб налаштування висоти камери працювало та матимете змогу грати в режимі он-лайн з ContraVPN.\n\nВи хочете все-таки почати гру?";
                        title = "Попередження";
                    }
                    else if (Globals.BG_Checked == true)
                    {
                        message = "\"generals.ctr\" не е намерен или има несъответствие в контролната сума! Моля, извлечете го от архива \"Contra009FinalPatch2\", ако искате настройката на височината на камерата да работи и да можете да играете онлайн с ContraVPN.\n\nЖелаете ли да стартирате играта въпреки това?";
                        title = "Предупреждение";
                    }
                    else if (Globals.DE_Checked == true)
                    {
                        message = "\"generals.ctr\" nicht gefunden oder Prüfsummeninkongruenz! Bitte extrahieren Sie es aus dem Archiv \"Contra009FinalPatch2\", wenn die Einstellung der Kamerahöhe funktionieren soll und Sie mit ContraVPN online spielen können.\n\nMöchten Sie das Spiel trotzdem starten?";
                        title = "Warnung";
                    }
                    DialogResult dialogResult = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        StartGenerals();
                    }
                    else
                    { }
                }
                else
                {
                    StartGenerals();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            return;
        }

        public void StartGenerals()
        {
            // Rename generals.exes
            if (File.Exists("generals.exe") && (File.Exists("generals.ctr")))
            {
                try
                {
                    File.SetAttributes("generals.exe", FileAttributes.Normal);
                    if (File.Exists("generals_zh.exe"))
                    {
                        File.SetAttributes("generals_zh.exe", FileAttributes.Normal);
                    }
                    File.SetAttributes("generals.ctr", FileAttributes.Normal);
                    File.Copy("generals.exe", "generals_zh.exe", true);
                    File.Copy("generals.ctr", "generals.exe", true);
                }
                catch
                { }
            }

            if (File.Exists("generals.exe"))
            {
                IsWbRunning();
                Process generals = new Process();
                generals.StartInfo.FileName = "generals.exe";

                if (WinCheckBox.Checked == false && QSCheckBox.Checked == false)
                {
                    //no start arguments
                }
                else if (QSCheckBox.Checked && WinCheckBox.Checked == false)
                {
                    generals.StartInfo.Arguments = "-quickstart -nologo";
                }
                else if (WinCheckBox.Checked && QSCheckBox.Checked)
                {
                    generals.StartInfo.Arguments = "-win -quickstart -nologo";
                }
                else //if (WinCheckBox.Checked && QSCheckBox.Checked == false)
                {
                    generals.StartInfo.Arguments = "-win";
                }

                generals.EnableRaisingEvents = true;
                generals.Exited += (sender1, e1) =>
                {
                    WindowState = FormWindowState.Normal;
                };
                generals.StartInfo.WorkingDirectory = Path.GetDirectoryName("generals.exe");
                WindowState = FormWindowState.Minimized;
                generals.Start();
            }
            else
            {
                CheckInstallDir();
            }
        }

        private void websitebutton_MouseEnter(object sender, EventArgs e)
        {
            websitebutton.BackgroundImage = Properties.Resources._button_website_text;
            websitebutton.ForeColor = SystemColors.ButtonHighlight;
            websitebutton.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void websitebutton_MouseLeave(object sender, EventArgs e)
        {
            websitebutton.BackgroundImage = Properties.Resources._button_website;
            websitebutton.ForeColor = SystemColors.ButtonHighlight;
            websitebutton.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void websitebutton_MouseDown(object sender, MouseEventArgs e)
        {
            websitebutton.BackgroundImage = Properties.Resources._button_highlight;
            websitebutton.ForeColor = SystemColors.ButtonHighlight;
            websitebutton.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        internal static bool Url_open(string url)
        {
            try
            {
                Process.Start(url);
                return true;
            }
            catch
            {
                try
                {
                    Process.Start("IExplore.exe", url);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not use your default browser to open URL:\n" + url + "\n\n" + ex.Message, "Opening link failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
        }
        private void websitebutton_Click(object sender, EventArgs e)
        {
            Url_open("https://contra.cncguild.net/oldsite/Eng/index.php");
        }
        private void button5_MouseEnter(object sender, EventArgs e)
        {
            button5.BackgroundImage = Properties.Resources._button_moddb_text;
            button5.ForeColor = SystemColors.ButtonHighlight;
            button5.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void button5_MouseLeave(object sender, EventArgs e)
        {
            button5.BackgroundImage = Properties.Resources._button_moddb;
            button5.ForeColor = SystemColors.ButtonHighlight;
            button5.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void button5_MouseDown(object sender, MouseEventArgs e)
        {
            button5.BackgroundImage = Properties.Resources._button_highlight;
            button5.ForeColor = SystemColors.ButtonHighlight;
            button5.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }

        private void button5_Click(object sender, EventArgs e) //ModDBButton
        {
            Url_open("https://www.moddb.com/mods/contra");
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.BackgroundImage = Properties.Resources._button_readme_text;
            button3.ForeColor = SystemColors.ButtonHighlight;
            button3.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackgroundImage = Properties.Resources._button_readme;
            button3.ForeColor = SystemColors.ButtonHighlight;
            button3.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            button3.BackgroundImage = Properties.Resources._button_highlight;
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
            button6.BackgroundImage = Properties.Resources._button_wb_text;
            button6.ForeColor = SystemColors.ButtonHighlight;
            button6.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void button6_MouseLeave(object sender, EventArgs e)
        {
            button6.BackgroundImage = Properties.Resources._button_wb;
            button6.ForeColor = SystemColors.ButtonHighlight;
            button6.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void button6_MouseDown(object sender, MouseEventArgs e)
        {
            button6.BackgroundImage = Properties.Resources._button_highlight;
            button6.ForeColor = SystemColors.ButtonHighlight;
            button6.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void button6_Click(object sender, EventArgs e) //WorldBuilder
        {
            DeleteDuplicateFiles();

            //if (File.Exists("!Contra009Final.ctr"))
            //{
            //    File.Move("!Contra009Final.ctr", "!Contra009Final.big");
            //}
            //if (File.Exists("!!Contra009Final_Patch1.ctr"))
            //{
            //    File.Move("!!Contra009Final_Patch1.ctr", "!!Contra009Final_Patch1.big");
            //}
            //if (File.Exists("!!!Contra009Final_Patch2.ctr"))
            //{
            //    File.Move("!!!Contra009Final_Patch2.ctr", "!!!Contra009Final_Patch2.big");
            //}
            //if (File.Exists("!Contra009Final_EN.ctr"))
            //{
            //    File.Move("!Contra009Final_EN.ctr", "!Contra009Final_EN.big");
            //}
            //if (File.Exists("!!Contra009Final_Patch1_EN.ctr"))
            //{
            //    File.Move("!!Contra009Final_Patch1_EN.ctr", "!!Contra009Final_Patch1_EN.big");
            //}
            //if (File.Exists("!!!Contra009Final_Patch2_EN.ctr"))
            //{
            //    File.Move("!!!Contra009Final_Patch2_EN.ctr", "!!!Contra009Final_Patch2_EN.big");
            //}
            //if (File.Exists("!Contra009Final_EngVO.ctr"))
            //{
            //    File.Move("!Contra009Final_EngVO.ctr", "!Contra009Final_EngVO.big");
            //}
            //if (File.Exists("!!Contra009Final_Patch1_EngVO.ctr"))
            //{
            //    File.Move("!!Contra009Final_Patch1_EngVO.ctr", "!!Contra009Final_Patch1_EngVO.big");
            //}
            //if (File.Exists("!!!Contra009Final_Patch2_EngVO.ctr"))
            //{
            //    File.Move("!!!Contra009Final_Patch2_EngVO.ctr", "!!!Contra009Final_Patch2_EngVO.big");
            //}
            try
            {
                File.Move("!Contra009Final.ctr", "!Contra009Final.big");
                File.Move("!!Contra009Final_Patch1.ctr", "!!Contra009Final_Patch1.big");
                File.Move("!!!Contra009Final_Patch2.ctr", "!!!Contra009Final_Patch2.big");
                File.Move("!Contra009Final_EN.ctr", "!Contra009Final_EN.big");
                File.Move("!!Contra009Final_Patch1_EN.ctr", "!!Contra009Final_Patch1_EN.big");
                File.Move("!!!Contra009Final_Patch2_EN.ctr", "!!!Contra009Final_Patch2_EN.big");
                File.Move("!Contra009Final_EngVO.ctr", "!Contra009Final_EngVO.big");
                File.Move("!!Contra009Final_Patch1_EngVO.ctr", "!!Contra009Final_Patch1_EngVO.big");
                File.Move("!!!Contra009Final_Patch2_EngVO.ctr", "!!!Contra009Final_Patch2_EngVO.big");
                if ((Properties.Settings.Default.WaterEffects == false) && (File.Exists("!!Contra009Final_WaterEffectsOff.ctr")))
                {
                    File.Move("!!Contra009Final_WaterEffectsOff.ctr", "!!Contra009Final_WaterEffectsOff.big");
                }
                else
                {
                    File.Move("!!Contra009Final_WaterEffectsOff.big", "!!Contra009Final_WaterEffectsOff.ctr");
                }
            }
            catch { }
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
                else if (File.Exists("WorldBuilder.exe"))
                {
                    wb.StartInfo.FileName = "WorldBuilder.exe";
                    wb.StartInfo.WorkingDirectory = Path.GetDirectoryName("WorldBuilder.exe");
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
            RadioFlag_GB.Checked = Properties.Settings.Default.Flag_GB;
            RadioFlag_RU.Checked = Properties.Settings.Default.Flag_RU;
            RadioFlag_UA.Checked = Properties.Settings.Default.Flag_UA;
            RadioFlag_BG.Checked = Properties.Settings.Default.Flag_BG;
            RadioFlag_DE.Checked = Properties.Settings.Default.Flag_DE;
            AutoScaleMode = AutoScaleMode.Dpi;

            Process[] ztExesByName = Process.GetProcessesByName("zt-x" + Globals.userOS);
            if (ztExesByName.Length > 0)
            {
                try
                {
                    foreach (Process ztExeByName in ztExesByName)
                    {
                        ztExeByName.Kill();
                        ztExeByName.WaitForExit();
                        ztExeByName.Dispose();
                    }
                }
                catch //(Exception ex)
                {
                    //MessageBox.Show(ex.ToString());
                    //string a = ex.ToString();
                }

                IP_LabelReset();
            }
        }

        private void OnApplicationExit(object sender, EventArgs e) //AppExit
        {
            DeleteDuplicateFiles();
            RenameBigToCtr();
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
            Properties.Settings.Default.Flag_GB = RadioFlag_GB.Checked;
            Properties.Settings.Default.Flag_RU = RadioFlag_RU.Checked;
            Properties.Settings.Default.Flag_UA = RadioFlag_UA.Checked;
            Properties.Settings.Default.Flag_BG = RadioFlag_BG.Checked;
            Properties.Settings.Default.Flag_DE = RadioFlag_DE.Checked;
            Properties.Settings.Default.Save();

            DelTmpChunk();

            Process[] vpnprocesses = Process.GetProcessesByName("zt-x" + Globals.userOS);
            try
            {
                foreach (Process vpnprocess in vpnprocesses)
                {
                    vpnprocess.Kill();
                    //vpnprocess.WaitForExit();
                    //vpnprocess.Dispose();

                    //vpn_start.BackgroundImage = (System.Drawing.Image)(Properties.Resources.vpn_off);
                    //labelVpnStatus.Text = "Off";
                }
            }
            catch {}
            //catch (Exception ex) { MessageBox.Show(ex.ToString()); }

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
            WindowState = FormWindowState.Minimized;
        }

        private void button18_Click(object sender, EventArgs e) //ExitIcon
        {
            OnApplicationExit(sender, e);
        }

        private void buttonChat_MouseEnter(object sender, EventArgs e)
        {
            buttonChat.BackgroundImage = Properties.Resources._button_discord_text;
            buttonChat.ForeColor = SystemColors.ButtonHighlight;
            buttonChat.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void buttonChat_MouseLeave(object sender, EventArgs e)
        {
            buttonChat.BackgroundImage = Properties.Resources._button_discord;
            buttonChat.ForeColor = SystemColors.ButtonHighlight;
            buttonChat.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void buttonChat_MouseDown(object sender, MouseEventArgs e)
        {
            buttonChat.BackgroundImage = Properties.Resources._button_highlight;
            buttonChat.ForeColor = SystemColors.ButtonHighlight;
            buttonChat.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }

        private void buttonChat_Click(object sender, EventArgs e)
        {
            Url_open("https://discordapp.com/invite/015E6KXXHmdWFXCtt");
        }

        private void helpbutton_Click(object sender, EventArgs e)
        {
            Url_open("https://contra.cncguild.net/oldsite/Eng/trouble.php");
        }

        private void helpbutton_MouseDown_1(object sender, MouseEventArgs e)
        {
            helpbutton.BackgroundImage = Properties.Resources._button_highlight;
            helpbutton.ForeColor = SystemColors.ButtonHighlight;
            helpbutton.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void helpbutton_MouseEnter_1(object sender, EventArgs e)
        {
            helpbutton.BackgroundImage = Properties.Resources._button_help_text;
            helpbutton.ForeColor = SystemColors.ButtonHighlight;
            helpbutton.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void helpbutton_MouseLeave_1(object sender, EventArgs e)
        {
            helpbutton.BackgroundImage = Properties.Resources._button_help;
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

        //**********VPN CODE START**********

        public void VpnOff()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate
                {
                    vpn_start.BackgroundImage = Properties.Resources.vpn_off;
                    Properties.Settings.Default.IP_Label = new Dictionary<string, bool>()
                    {
                        { "ContraVPN: Off", Globals.GB_Checked },
                        { "ContraVPN: Выкл", Globals.RU_Checked },
                        { "ContraVPN: Вимк", Globals.UA_Checked },
                        { "ContraVPN: Изкл", Globals.BG_Checked },
                        { "ContraVPN: Aus", Globals.DE_Checked },
                    }.Single(l => l.Value).Key;
                }));
                return;
            }
        }

        public static void SetFirewallExcemption(string exePath)
        {
            // Full path in rule name is ugly, let's only show filename instead
            string ExeWithoutPath = exePath;
            int idx = exePath.LastIndexOf(@"\");
            if (idx != -1) ExeWithoutPath = exePath.Substring(idx + 1);

            // Check if rule with same name exists
            var netsh = new Process();
            netsh.StartInfo.FileName = "netsh.exe";
            netsh.StartInfo.CreateNoWindow = true;
            netsh.StartInfo.UseShellExecute = false;
            netsh.StartInfo.Arguments = $"advfirewall firewall show rule name=\"Contra - \"{ExeWithoutPath}\"";
            netsh.Start();
            netsh.WaitForExit();

            // Add new firewall excemption rule if missing
            if (netsh.ExitCode != 0)
            {
                netsh.StartInfo.Arguments = $"advfirewall firewall add rule name=\"Contra - {ExeWithoutPath}\" dir=in action=allow program=\"{Environment.CurrentDirectory}\\{exePath}\" protocol=tcp profile=private,public edge=yes enable=yes";
                netsh.Start();

                Process netsh2 = new Process();
                netsh2.StartInfo = netsh.StartInfo;
                netsh2.StartInfo.Arguments = $"advfirewall firewall add rule name=\"Contra - {exePath}\" dir=in action=allow program=\"{Environment.CurrentDirectory}\\{exePath}\" protocol=udp profile=private,public edge=yes enable=yes";
                netsh2.Start();

                netsh.WaitForExit();
                netsh2.WaitForExit();
            }
        }

        public static void CheckFirewallExceptions()
        {
            // All executables which need listening ports open
            ReadOnlyCollection<string> exes = Array.AsReadOnly(new[] {
                "game.dat",
                "generals.exe",
            });

            // Check if all files exist first before attempting to add any rules
            bool allFilesExist = exes.All(file => File.Exists(Environment.CurrentDirectory + @"\" + file));
            if (allFilesExist) foreach (string exe in exes) SetFirewallExcemption(exe);
        }

        public void StartZT()
        {
            try
            {
                Process ztDaemon = new Process();
                ztDaemon.StartInfo.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig";
                ztDaemon.StartInfo.FileName = contravpnPath + "zt-x" + Globals.userOS;
                ztDaemon.StartInfo.Arguments = "-C \"zt\"";
                ztDaemon.StartInfo.UseShellExecute = false;
                ztDaemon.StartInfo.CreateNoWindow = true;

                ztDaemon.Start();
                ZT_IP();
                refreshVpnIpTimer.Enabled = true;

                //check when zt daemon gets turned off
                ztDaemon.EnableRaisingEvents = true;
                ztDaemon.Exited += (sender, e) =>
                {
                    VpnOff();
                };

                disableVPNBtnChangeTimer.Enabled = true;
                vpn_start.BackgroundImage = Properties.Resources.vpn_on;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void buttonVPNinvOK_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public string GetCurrentCulture()
        {
            var culture = System.Globalization.CultureInfo.CurrentCulture;
            string cultureStr = culture.ToString();
            return cultureStr;
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
        private void ThreadProcSafeMOTD(string motd)
        {
            try
            {
                {
                    if (downloadTextFile == false)
                    {
                        //Check for launcher update once per launch.
                        if (seekForUpdate == true)
                        {
                            seekForUpdate = false;
                            //GetLauncherUpdate(motd, launcher_url);
                            //GetModUpdate(motd, patch_url);
                        }
                        downloadTextFile = true;
                    }
                    void SetMOTD(string prefix)
                    {
                        string MOTDText = motd.Substring(motd.LastIndexOf(prefix) + 9);
                        string MOTDText2 = MOTDText.Substring(0, MOTDText.IndexOf("$"));
                        ThreadHelperClass.SetText(this, MOTD, MOTDText2);
                    }

                    var motd_lang = new Dictionary<string, bool>
                    {
                        {"MOTD-EN: ", Globals.GB_Checked},
                        {"MOTD-RU: ", Globals.RU_Checked},
                        {"MOTD-UA: ", Globals.UA_Checked},
                        {"MOTD-BG: ", Globals.BG_Checked},
                        {"MOTD-DE: ", Globals.DE_Checked},
                    };
                    SetMOTD(motd_lang.Single(l => l.Value).Key);
                }
            }
            catch {}
        }

        void gtwc_DownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            //Extract zip
            string zipPath = launcherExecutingPath + @"\" + genToolFileName;

            try //To prevent crash
            {
                using (ZipArchive archive = ZipFile.OpenRead(zipPath))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries.Where(a => a.FullName.Contains("d3d8.dll")))
                    {
                        entry.ExtractToFile(Path.Combine(launcherExecutingPath, entry.FullName), true);
                    }
                }
            }
            catch {}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            try
            {
                File.Delete(genToolFileName);
            }
            catch {}

            //Show a message when the patch download has completed
            var gtLangText = new Dictionary<Tuple<string, string>, bool>
                {
                    { Tuple.Create("A new version of Gentool has been downloaded!", "Gentool update Complete"), Globals.GB_Checked},
                    { Tuple.Create("Новая версия GenTool был загружен!", "Gentool обновление завершено"), Globals.RU_Checked},
                    { Tuple.Create("Новий GenTool завантажено!", "Оновлення GenTool завершено"), Globals.UA_Checked},
                    { Tuple.Create("Нова версия на GenTool беше изтегленa!", "Обновяването на GenTool е завършено"), Globals.BG_Checked},
                    { Tuple.Create("Ein neuer GenTool wurde heruntergeladen!", "Aktualisierung GenTool abgeschlossen"), Globals.DE_Checked},
                }.Single(l => l.Value).Key;
            MessageBox.Show(new Form { TopMost = true }, gtLangText.Item1, gtLangText.Item2, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void DownloadGentool(string url)
        {
            try
            {
                WebClient gtwc = new WebClient();
                gtwc.DownloadFileCompleted += new AsyncCompletedEventHandler(gtwc_DownloadCompleted);

                //CheckIfFileIsAvailable(url);
                //gtwc.OpenRead(url + genToolFileName);
                //bytes_total = Convert.ToInt64(gtwc.ResponseHeaders["Content-Length"]);

                gtwc.DownloadFileAsync(new Uri(url), launcherExecutingPath + @"\" + genToolFileName);
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        public static Tuple<int, int> getScreenResolution() => Tuple.Create(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        readonly int ScreenResolutionX = getScreenResolution().Item1;
        readonly int ScreenResolutionY = getScreenResolution().Item2;

        public void CreateOptionsINI()
        {
            try
            {
                using (FileStream fs = File.Create(UserDataLeafName() + @"\Options.ini"))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes("IdealStaticGameLOD = High" + Environment.NewLine + "Resolution = " + ScreenResolutionX + " " + ScreenResolutionY);
                    fs.Write(info, 0, info.Length);
                }
                using (FileStream fs = File.Create(myDocPath + @"\Options.ini"))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes("IdealStaticGameLOD = High" + Environment.NewLine + "Resolution = " + ScreenResolutionX + " " + ScreenResolutionY);
                    fs.Write(info, 0, info.Length);
                }
            }

            catch { }
        }

        static string CalculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        public string AspectRatio(int x, int y)
        {
            double value = (double)x / y;
            if (value > 1.7)
                return "16:9";
            else
                return "4:3";
        }

        private void ChangeCamHeight()
        {
            if (File.Exists("!!!Contra009Final_Patch2_GameData.big"))
            {
                Encoding encoding = Encoding.GetEncoding("windows-1252");
                var regex = Regex.Replace(File.ReadAllText("!!!Contra009Final_Patch2_GameData.big"), "  MaxCameraHeight = .*\r?\n", "  MaxCameraHeight = 282.0 ;350.0\r\n");
                string read = File.ReadAllText("!!!Contra009Final_Patch2_GameData.big", encoding);
                File.WriteAllText("!!!Contra009Final_Patch2_GameData.big", regex, encoding);
            }
            else
            {
                if (Globals.GB_Checked == true)
                {
                    MessageBox.Show("\"!!!Contra009Final_Patch2_GameData.big\" file not found!", "Error");
                }
                else if (Globals.RU_Checked == true)
                {
                    MessageBox.Show("Файл \"!!!Contra009Final_Patch2_GameData.big\" не найден!", "Ошибка");
                }
                else if (Globals.UA_Checked == true)
                {
                    MessageBox.Show("Файл \"!!!Contra009Final_Patch2_GameData.big\" не знайдений!", "Помилка");
                }
                else if (Globals.BG_Checked == true)
                {
                    MessageBox.Show("Файлът \"!!!Contra009Final_Patch2_GameData.big\" не беше намерен!", "Грешка");
                }
                else if (Globals.DE_Checked == true)
                {
                    MessageBox.Show("\"!!!Contra009Final_Patch2_GameData.big\" nicht gefunden!", "Fehler");
                }
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            string gtHash = null;
            try
            {
                gtHash = CalculateMD5("d3d8.dll");
            }
            catch {}

            if (isGentoolInstalled("d3d8.dll") && isGentoolOutdated("d3d8.dll", 79) || (gtHash == "70c28745f6e9a9a59cfa1be00df6836a" || gtHash == "13a13584d97922de92443631931d46c3"))
            {
                //try
                //{
                //    {
                //        System.Threading.Thread demoThread =
                //           new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProcSafeGentool));
                //        demoThread.Start();
                //    }
                //}
                //catch (Exception ex) { MessageBox.Show(ex.ToString()); }

                try
                {
                    using (WebClient client = new WebClient())
                    {
                        genToolFileName = client.DownloadString("http://www.gentool.net/download/patch");
                        genToolFileName = genToolFileName.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)[1];

                        //MessageBox.Show(genToolFileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return;
                }

                string gtURL = "http://www.gentool.net/download/" + genToolFileName;
                DownloadGentool(gtURL);
            }

            // Cleanup old Launcher file after update
            if (File.Exists(launcherExecutingPath + @"\Contra_Launcher_ToDelete.exe"))
            {
                File.SetAttributes("Contra_Launcher_ToDelete.exe", FileAttributes.Normal);
                File.Delete(launcherExecutingPath + @"\Contra_Launcher_ToDelete.exe");
            }

            // Generate Options.ini if missing.
            if (!File.Exists(UserDataLeafName() + "Options.ini") && (!File.Exists(myDocPath + "Options.ini")))
            {
                //if (Globals.GB_Checked == true)
                //{
                //    MessageBox.Show("Options.ini not found, therefore the game will not start! You have to run Zero Hour once for it to generate the file.\nIf that fails, you will have to create the file manually. Click the \"Help\" button in launcher to be brought to a page with instructions.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
                //else if (Globals.RU_Checked == true)
                //{
                //    MessageBox.Show("Файл \"Options.ini\" не найден, поэтому игра не запустится! Вам нужно запустить Zero Hour один раз, чтобы он сгенерировал файл.\nЕсли не получится, вам придется создать файл вручную. Нажмите кнопку «Help» в панели лаунчера, чтобы перейти на страницу с инструкциями.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
                //else if (Globals.UA_Checked == true)
                //{
                //    MessageBox.Show("Файл Options.ini не знайдений, отже гра не розпочнеться! Вам потрібно запустити Zero Hour один раз, щоб створити файл.\nЯкщо це не вдасться, вам доведеться створити файл вручну. Натисніть кнопку \"Help\" в панелі запуску, щоб перейти на сторінку з інструкціями.", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
                //else if (Globals.BG_Checked == true)
                //{
                //    MessageBox.Show("Options.ini не беше намерен, следователно играта няма да се стартира! Трябва да стартирате Zero Hour веднъж, за да генерира файла.\nАко това се провали, трябва да създадете файла ръчно. Щракнете на \"Help\" бутона в launcher-а, за да отидете на страница с инструкции.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
                //else if (Globals.DE_Checked == true)
                //{
                //    MessageBox.Show("Options.ini nicht gefunden, daher startet das Spiel nicht! Sie müssen Zero Hour einmal ausführen, damit die Datei generiert wird.\nWenn dies fehlschlägt, müssen Sie die Datei manuell erstellen. Klicken Sie im Launcher auf die Schaltfläche \"Help\", um zu einer Seite mit Anweisungen zu gelangen.", "Warnung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
                CreateOptionsINI();
            }

            if (Properties.Settings.Default.FirstRun)
            {
                // Enable GameData
                if (File.Exists("!!!Contra009Final_Patch2_GameData.ctr"))
                {
                    File.Move("!!!Contra009Final_Patch2_GameData.ctr", "!!!Contra009Final_Patch2_GameData.big");
                }
                // Set default cam height
                try
                {
                    if (AspectRatio(ScreenResolutionX, ScreenResolutionY) == "16:9" && isGentoolInstalled("d3d8.dll"))
                    {
                        ChangeCamHeight();
                    }
                }
                catch { }

                // Delete tinc vpn files
                try
                {
                    Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\contravpn", true);
                    Directory.Delete(@"contra\vpn\32");
                    Directory.Delete(@"contra\vpn\64");
                    File.Delete(@"contra\vpn\tinc-adapters.cmd");
                    File.Delete(@"contra\vpn\tinc-add-tap-adapter.cmd");
                    File.Delete(@"contra\vpn\tinc-add-tap-adapter.ps1");
                    File.Delete(@"contra\vpn\tinc-config.cmd");
                    File.Delete(@"contra\vpn\tinc-console.cmd");
                    File.Delete(@"contra\vpn\tinc-license.txt");
                    File.Delete(@"contra\vpn\tinc-remove-tap-adapters.cmd");
                    File.Delete(@"contra\vpn\tinc-remove-tap-adapters.ps1");
                    File.Delete(@"contra\vpn\tinc-sources.url");
                    File.Delete(@"contra\vpn\tinc-start-daemon.cmd");
                    File.Delete(@"contra\vpn\tinc-up.cmd");
                }
                catch { }

                // Create vpnconfig folder.
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig");

                // If there are older Contra config folders, this means Contra Launcher has been
                // ran before on this PC, so in this case, we skip first run welcome message.
                int directoryCount = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra").Length;

                // Zero Hour has a 'DeleteFile("Data\INI\INIZH.big");' line in GameEngine::init with no condition whatsoever (will always try to delete it if exists)
                // an identical copy of this file exists in root ZH folder so we can safely delete it before ZH runs to prevent unwanted crashes
                try
                {
                    File.Delete(@"Data\INI\INIZH.big");
                }
                catch { }

                // Show message on first run.
                if (GetCurrentCulture() == "en-US")
                {
                    RadioFlag_GB.Checked = true;
                    if (directoryCount <= 2)
                    {
                        MessageBox.Show("Welcome to Contra 009 Final! Since this is your first time running this launcher, we would like to let you know that you have a new opportunity to play Contra online via ContraVPN! We highly recommend you to join our Discord community!");
                    }
                }
                else if (GetCurrentCulture() == "ru-RU")
                {
                    RadioFlag_RU.Checked = true;
                    if (directoryCount <= 2)
                    {
                        MessageBox.Show("Добро пожаловать в Contra 009 Final! Поскольку это Ваш первый запуск этого лаунчера, мы хотим сообщить Вам о том, что у Вас есть новая возможность играть в Contra онлайн через ContraVPN! Мы настоятельно рекомендуем Вам присоедениться к нашей группе Discord.");
                    }
                }
                else if (GetCurrentCulture() == "uk-UA")
                {
                    RadioFlag_UA.Checked = true;
                    if (directoryCount <= 2)
                    {
                        MessageBox.Show("Ласкаво просимо до Contra 009 Final! Оскільки це Ваш перший запуск цього лаунчера, ми хочемо повідомити Вас про те, що у Вас є нова можливість відтворити Contra онлайн через ContraVPN! Ми максимально рекомендуємо Вам приєднатися до нашої спільноти Discord.");
                    }
                }
                else if (GetCurrentCulture() == "bg-BG")
                {
                    RadioFlag_BG.Checked = true;
                    if (directoryCount <= 2)
                    {
                        MessageBox.Show("Добре дошли в Contra 009 Final! Тъй като това е първото Ви стартиране на Contra, бихме искали да знаете, че имате нова възможност да играете Contra онлайн чрез ContraVPN! Силно препоръчваме да се присъедините към нашата Discord общност!");
                    }
                }
                else if (GetCurrentCulture() == "de-DE")
                {
                    RadioFlag_DE.Checked = true;
                    if (directoryCount <= 2)
                    {
                        MessageBox.Show("Wilkommen zu Contra 009 Final! Da du diesen launcher zum ersten mal ausfьhrst wollten wir dich wissen lassen, dass du eine neue Mцglichkeit hast Contra online zu spielen ьber ContraVPN! Wir empfehlen dir unserem Discord Server beizutreten.");
                    }
                }
                else
                {
                    RadioFlag_GB.Checked = true;
                    if (directoryCount <= 1)
                    {
                        MessageBox.Show("Welcome to Contra 009 Final! Since this is your first time running this launcher, we would like to let you know that you have a new opportunity to play Contra online via ContraVPN! We highly recommend you to join our Discord community!");
                    }
                }

                // Show tooltip on Options
                Point pt = new Point(0, 0);
                pt.Offset(moreOptions.Width - 30, moreOptions.Height - 55);
                if (Globals.GB_Checked == true)
                {
                    optionsToolTip.Show("You can customize many options including camera height here.", moreOptions, pt, 10000);
                }
                else if (Globals.RU_Checked == true)
                {
                    optionsToolTip.Show("Здесь вы можете настроить множество параметров, включая высоту камеры.", moreOptions, pt, 10000);
                }
                else if (Globals.UA_Checked == true)
                {
                    optionsToolTip.Show("Тут можна налаштувати багато варіантів, включаючи висоту камери.", moreOptions, pt, 10000);
                }
                else if (Globals.BG_Checked == true)
                {
                    optionsToolTip.Show("Тук можете да персонализирате много опции, включително височина на камерата.", moreOptions, pt, 10000);
                }
                else if (Globals.DE_Checked == true)
                {
                    optionsToolTip.Show("Sie können hier viele Optionen anpassen, einschließlich der Kamerahöhe.", moreOptions, pt, 10000);
                }

                // Delete old Contra config folders
                DirectoryInfo di = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Contra");

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
                    // Enable Tournament Mode (limit super weapons and super units) on first run.
                    if (Directory.Exists(UserDataLeafName()))
                    {
                        string text = File.ReadAllText(UserDataLeafName() + "Skirmish.ini");
                        {
                            if (text.Contains("SuperweaponRestrict = No"))
                            {
                                File.WriteAllText(UserDataLeafName() + "Skirmish.ini", File.ReadAllText(UserDataLeafName() + "Skirmish.ini").Replace("SuperweaponRestrict = No", "SuperweaponRestrict = Yes"));
                            }
                            else if (text.Contains("SuperweaponRestrict = no"))
                            {
                                File.WriteAllText(UserDataLeafName() + "Skirmish.ini", File.ReadAllText(UserDataLeafName() + "Skirmish.ini").Replace("SuperweaponRestrict = no", "SuperweaponRestrict = Yes"));
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
                            else if (text.Contains("SuperweaponRestrict = no"))
                            {
                                File.WriteAllText(myDocPath + "Skirmish.ini", File.ReadAllText(myDocPath + "Skirmish.ini").Replace("SuperweaponRestrict = no", "SuperweaponRestrict = Yes"));
                            }
                        }
                    }
                }
                catch //(Exception ex)
                {
                    //
                }

                // Add Firewall exceptions.
                CheckFirewallExceptions();

                Properties.Settings.Default.FirstRun = false;
                Properties.Settings.Default.Save();
            }

            // Show warning if the base mod isn't found.
            if (!File.Exists("!Contra009Final.ctr") && !File.Exists("!Contra009Final.big"))
            {
                if (Globals.GB_Checked == true)
                {
                    MessageBox.Show("\"!Contra009Final.ctr\" is missing!\nPlease, install 009 Final first, or the mod will not start!\nIt is common for people to install the patch, but not the base mod.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (Globals.RU_Checked == true)
                {
                    MessageBox.Show("\"!Contra009Final.ctr\" отсутствует!\nПожалуйста, сначала установите 009 Final, или мод не запустится!\nЛюди обычно устанавливают патч, но не базовый мод.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (Globals.UA_Checked == true)
                {
                    MessageBox.Show("\"!Contra009Final.ctr\" відсутня!\nБудь ласка, спочатку встановіть 009 Final, або мод не запуститься!\nЛюди звичайно встановлюють патч, але не базовий мод.", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (Globals.BG_Checked == true)
                {
                    MessageBox.Show("\"!Contra009Final.ctr\" не беше намерен!\nМоля, първо инсталирайте 009 Final, или модът няма да стартира!\nЧесто хората инсталират само последния пач, но не и базовия мод.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (Globals.DE_Checked == true)
                {
                    MessageBox.Show("\"!Contra009Final.ctr\" wird vermisst!\nBitte installieren Sie zuerst 009 Final, oder der mod startet nicht!\nEs ist üblich, dass Leute den Patch installieren, nicht aber den Basemod.", "Warnung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            // Show warning if there are .ini files in Data\INI or its subfolders.
            try
            {
                if (Directory.GetFiles(Environment.CurrentDirectory + @"\Data\INI", "*.ini", SearchOption.AllDirectories).Length == 0)
                {
                    // no .ini files
                }
                else
                {
                    if (Globals.GB_Checked == true)
                    {
                        MessageBox.Show("Found .ini files in the Data\\INI directory. They may corrupt the mod or cause mismatch online.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (Globals.RU_Checked == true)
                    {
                        MessageBox.Show("Найдены файлы .ini в каталоге Data\\INI. Они могут повредить мод или вызвать несоответствие в сети.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (Globals.UA_Checked == true)
                    {
                        MessageBox.Show("Знайдено файли .ini в каталозі Data\\INI. Вони можуть пошкодити мод або призвести до невідповідності в Інтернеті.", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (Globals.BG_Checked == true)
                    {
                        MessageBox.Show("Намерени .ini файлове в директорията Data\\INI. Те могат да повредят мода или да причинят несъответствие онлайн.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (Globals.DE_Checked == true)
                    {
                        MessageBox.Show("Es wurden INI-Dateien im Verzeichnis \"Data\\INI\" gefunden. Sie können den Mod beschädigen oder online zu Unstimmigkeiten führen.", "Warnung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch { }
        }

        private void applyResources(ComponentResourceManager resources, Control.ControlCollection ctls)
        {
            foreach (Control ctl in ctls)
            {
                resources.ApplyResources(ctl, ctl.Name);
                applyResources(resources, ctl.Controls);
            }
        }

        private void RadioFlag_GB_CheckedChanged(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
            resources.ApplyResources(this, "$this");
            applyResources(resources, Controls);
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
            toolTip1.SetToolTip(vpn_start, "Start/close ContraVPN.");
            toolTip1.SetToolTip(ZTConfigBtn, "Open the ContraVPN config file.");
            toolTip1.SetToolTip(ZTConsoleBtn, "Open the ContraVPN console window.");
            toolTip1.SetToolTip(ZTNukeBtn, "Uninstall ContraVPN.");
            toolTip1.SetToolTip(DonateBtn, "Make a donation.");
            currentFileLabel = "File: ";
            ModDLLabel.Text = "Download progress: ";
            string verString, yearString = "";
            if (File.Exists("!!!Contra009Final_Patch2.big") || File.Exists("!!!Contra009Final_Patch2.ctr") && (File.Exists("!!Contra009Final_Patch1.big") || File.Exists("!!Contra009Final_Patch1.ctr")) && (File.Exists("!Contra009Final.big") || File.Exists("!Contra009Final.ctr")))
            {
                verString = "009 Final Patch 2";
                yearString = "2019";
            }
            else if (File.Exists("!!Contra009Final_Patch1.big") || File.Exists("!!Contra009Final_Patch1.ctr") && (File.Exists("!Contra009Final.big") || File.Exists("!Contra009Final.ctr")))
            {
                verString = "009 Final Patch 1";
                yearString = "2019";
            }
            else if (File.Exists("!Contra009Final.big") || File.Exists("!Contra009Final.ctr"))
            {
                verString = "009 Final";
                yearString = "2018";
            }
            else
            {
                verString = "???";
                yearString = "2018";
            }
            versionLabel.Text = "Contra Project Team " + yearString + " - Version " + verString + " - Launcher: " + Application.ProductVersion;

            Process[] ztExeByName = Process.GetProcessesByName("zt-x" + Globals.userOS);
            if (ztExeByName.Length > 0 && wait == false) //if zt is already running
            {
                vpn_start.BackgroundImage = Properties.Resources.vpn_on;
                IP_Label.Text = "IP: " + ip;
            }
            if (ztExeByName.Length > 0) //if zt is already running
            {
                IP_Label.Text = "IP: " + ip;
            }
            if (ztExeByName.Length == 0) //if zt is not running
            {
                IP_Label.Text = "ContraVPN: Off";
            }

            // Temporary hack so update runs on main thread, motd should be rewritten to be async if possible
            try
            {
                string motd = (new WebClient { Encoding = Encoding.UTF8 }).DownloadString("https://raw.githubusercontent.com/ContraMod/Launcher/master/Versions.txt");
                string launcher_url = "https://github.com/ContraMod/Launcher/releases/download/";
                GetLauncherUpdate(motd, launcher_url);

                //Load MOTD
                new Thread(() => ThreadProcSafeMOTD(motd)) { IsBackground = true }.Start();
            }
            catch {}
        }

        private void RadioFlag_RU_CheckedChanged(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ru-RU");
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
            resources.ApplyResources(this, "$this");
            applyResources(resources, Controls);
            Globals.GB_Checked = false;
            Globals.BG_Checked = false;
            Globals.UA_Checked = false;
            Globals.DE_Checked = false;
            Globals.RU_Checked = true;
            toolTip1.SetToolTip(RadioLocQuotes, "Юниты всех трех фракций будут разговаривать на английском.");
            toolTip1.SetToolTip(RadioOrigQuotes, "Юниты каждой фракции будут разговаривать на их родном языке.");
            toolTip1.SetToolTip(RadioEN, "Английский язык.");
            toolTip1.SetToolTip(RadioRU, "Русский язык.");
            toolTip1.SetToolTip(MNew, "Включить новые саундтреки.");
            toolTip1.SetToolTip(MStandard, "Включить стандартные саундтреки Zero Hour.");
            toolTip1.SetToolTip(DefaultPics, "Включить портреты Генералов по умолчанию.");
            toolTip1.SetToolTip(GoofyPics, "Включить смешные портреты Генералов.");
            toolTip1.SetToolTip(WinCheckBox, "Запуск Contra в режиме окна вместо полноэкранного.");
            toolTip1.SetToolTip(QSCheckBox, "Отключает интро и шелмапу (игра запускается быстрее).");
            toolTip1.SetToolTip(vpn_start, "Открыть/Закрыть ContraVPN.");
            toolTip1.SetToolTip(ZTConfigBtn, "Открыть файл конфигурации ContraVPN.");
            toolTip1.SetToolTip(ZTConsoleBtn, "Открыть консоль ContraVPN.");
            toolTip1.SetToolTip(ZTNukeBtn, "Удалить ContraVPN.");
            toolTip1.SetToolTip(DonateBtn, "Дарить команду проекта.");
            RadioLocQuotes.Text = "Англ.";
            RadioOrigQuotes.Text = "Родные";
            MNew.Text = "Новая";
            MStandard.Text = "ZH";
            WinCheckBox.Text = "Режим окна"; WinCheckBox.Left = 254;
            QSCheckBox.Text = "Быстр. старт"; QSCheckBox.Left = 254;
            RadioEN.Text = "Англ.";
            RadioRU.Text = "Русский";
            DefaultPics.Text = "По умолч.";
            GoofyPics.Text = "Смешные";
            moreOptions.Text = "Больше опций";
            currentFileLabel = "Файл: ";
            ModDLLabel.Text = "Прогресс загрузки: ";
            string verString, yearString = "";
            if (File.Exists("!!!Contra009Final_Patch2.big") || File.Exists("!!!Contra009Final_Patch2.ctr"))
            {
                verString = " Патч 2";
                yearString = "2019";
            }
            else if (File.Exists("!!Contra009Final_Patch1.big") || File.Exists("!!Contra009Final_Patch1.ctr"))
            {
                verString = " Патч 1";
                yearString = "2019";
            }
            else
            {
                verString = "???";
                yearString = "2018";
            }
            versionLabel.Text = "Contra Project Team " + yearString + " - Версия 009 Финал" + verString + " - Launcher: " + Application.ProductVersion;

            Process[] ztExeByName = Process.GetProcessesByName("zt-x" + Globals.userOS);
            if (ztExeByName.Length > 0 && wait == false) //if zt is already running
            {
                vpn_start.BackgroundImage = Properties.Resources.vpn_on;
                IP_Label.Text = "IP: " + ip;
            }
            if (ztExeByName.Length > 0) //if zt is already running
            {
                IP_Label.Text = "IP: " + ip;
            }
            if (ztExeByName.Length == 0) //if zt is not running
            {
                IP_Label.Text = "ContraVPN: Выкл.";
            }

            // Temporary hack so update runs on main thread, motd should be rewritten to be async if possible
            try
            {
                string motd = (new WebClient { Encoding = Encoding.UTF8 }).DownloadString("https://raw.githubusercontent.com/ContraMod/Launcher/master/Versions.txt");
                string launcher_url = "https://github.com/ContraMod/Launcher/releases/download/";
                GetLauncherUpdate(motd, launcher_url);

                //Load MOTD
                new Thread(() => ThreadProcSafeMOTD(motd)) { IsBackground = true }.Start();
            }
            catch {}
        }

        private void RadioFlag_UA_CheckedChanged(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("uk-UA");
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
            resources.ApplyResources(this, "$this");
            applyResources(resources, Controls);
            Globals.GB_Checked = false;
            Globals.RU_Checked = false;
            Globals.BG_Checked = false;
            Globals.DE_Checked = false;
            Globals.UA_Checked = true;
            toolTip1.SetToolTip(RadioLocQuotes, "Юніти всіх трьох фракцій розмовлятимуть англійською.");
            toolTip1.SetToolTip(RadioOrigQuotes, "Юніти кожної фракції розмовлятимуть їхньою рідною мовою.");
            toolTip1.SetToolTip(RadioEN, "Англійська мова.");
            toolTip1.SetToolTip(RadioRU, "Російська мова.");
            toolTip1.SetToolTip(MNew, "Використовуйте нові саундтреки.");
            toolTip1.SetToolTip(MStandard, "Використовуйте стандартні саундтреки Zero Hour.");
            toolTip1.SetToolTip(DefaultPics, "Використовуйте портрети Генералів за замовчуванням.");
            toolTip1.SetToolTip(GoofyPics, "Використовуйте смішні портрети Генералів.");
            toolTip1.SetToolTip(WinCheckBox, "Запускає Contra у віконному режимі замість повноекранного.");
            toolTip1.SetToolTip(QSCheckBox, "Вимикає інтро і шелмапу (гра запускається швидше).");
            toolTip1.SetToolTip(vpn_start, "Відкрити/закрити ContraVPN.");
            toolTip1.SetToolTip(ZTConfigBtn, "Відкрити файл конфігурації ContraVPN.");
            toolTip1.SetToolTip(ZTConsoleBtn, "Відкрити консоль ContraVPN.");
            toolTip1.SetToolTip(ZTNukeBtn, "Видалити ContraVPN.");
            toolTip1.SetToolTip(DonateBtn, "Дарить команду проекту.");
            RadioLocQuotes.Text = "Англ.";
            RadioOrigQuotes.Text = "Рідні";
            MNew.Text = "Нова";
            MStandard.Text = "ZH";
            WinCheckBox.Text = "Віконний";
            QSCheckBox.Text = "Шв. старт";
            RadioEN.Text = "Англ.";
            RadioRU.Text = "Рос.";
            DefaultPics.Text = "За замовч.";
            GoofyPics.Text = "Смішні";
            moreOptions.Text = "Більше опцій";
            currentFileLabel = "Файл: ";
            ModDLLabel.Text = "Прогрес завантаження: ";
            string verString, yearString = "";
            if (File.Exists("!!!Contra009Final_Patch2.big") || File.Exists("!!!Contra009Final_Patch2.ctr"))
            {
                verString = " Патч 2";
                yearString = "2019";
            }
            else if (File.Exists("!!Contra009Final_Patch1.big") || File.Exists("!!Contra009Final_Patch1.ctr"))
            {
                verString = " Патч 1";
                yearString = "2019";
            }
            else
            {
                verString = "???";
                yearString = "2018";
            }
            versionLabel.Text = "Contra Project Team " + yearString + " - Версія 009 Фінал" + verString + " - Launcher: " + Application.ProductVersion;

            Process[] ztExeByName = Process.GetProcessesByName("zt-x" + Globals.userOS);
            if (ztExeByName.Length > 0 && wait == false) //if zt is already running
            {
                vpn_start.BackgroundImage = Properties.Resources.vpn_on;
                IP_Label.Text = "IP: " + ip;
            }
            if (ztExeByName.Length > 0) //if zt is already running
            {
                IP_Label.Text = "IP: " + ip;
            }
            if (ztExeByName.Length == 0) //if zt is not running
            {
                IP_Label.Text = "ContraVPN: Вимк.";
            }

            // Temporary hack so update runs on main thread, motd should be rewritten to be async if possible
            try
            {
                string motd = (new WebClient { Encoding = Encoding.UTF8 }).DownloadString("https://raw.githubusercontent.com/ContraMod/Launcher/master/Versions.txt");
                string launcher_url = "https://github.com/ContraMod/Launcher/releases/download/";
                GetLauncherUpdate(motd, launcher_url);

                //Load MOTD
                new Thread(() => ThreadProcSafeMOTD(motd)) { IsBackground = true }.Start();
            }
            catch {}
        }

        private void RadioFlag_BG_CheckedChanged(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("bg-BG");
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
            resources.ApplyResources(this, "$this");
            applyResources(resources, Controls);
            Globals.GB_Checked = false;
            Globals.RU_Checked = false;
            Globals.UA_Checked = false;
            Globals.DE_Checked = false;
            Globals.BG_Checked = true;
            toolTip1.SetToolTip(RadioLocQuotes, "Единиците на трите фракции ще говорят на английски.");
            toolTip1.SetToolTip(RadioOrigQuotes, "Единиците на трите фракции ще говорят на техния роден език.");
            toolTip1.SetToolTip(RadioEN, "Английски език в играта.");
            toolTip1.SetToolTip(RadioRU, "Руски език в играта.");
            toolTip1.SetToolTip(MNew, "Използвайте новата музика.");
            toolTip1.SetToolTip(MStandard, "Използвайте стандартната музика в Zero Hour.");
            toolTip1.SetToolTip(DefaultPics, "Използвайте оригиналните генералски портрети.");
            toolTip1.SetToolTip(GoofyPics, "Използвайте забавните генералски портрети.");
            toolTip1.SetToolTip(WinCheckBox, "Стартира Contra в нов прозорец вместо на цял екран.");
            toolTip1.SetToolTip(QSCheckBox, "Изключва интрото и анимираната карта (шелмапа). Играта стартира по-бързо.");
            toolTip1.SetToolTip(vpn_start, "Включете/изключете ContraVPN.");
            toolTip1.SetToolTip(ZTConfigBtn, "Отворете конфигурационния файл на ContraVPN.");
            toolTip1.SetToolTip(ZTConsoleBtn, "Отворете конзолния прозорец на ContraVPN.");
            toolTip1.SetToolTip(ZTNukeBtn, "Деинсталирайте ContraVPN.");
            toolTip1.SetToolTip(DonateBtn, "Направете дарение.");
            RadioLocQuotes.Text = "Англ.";
            RadioOrigQuotes.Text = "Родни";
            MNew.Text = "Нова";
            MStandard.Text = "ZH";
            WinCheckBox.Text = "В прозорец"; WinCheckBox.Left = 267;
            QSCheckBox.Text = "Бърз старт"; QSCheckBox.Left = 267;
            RadioEN.Text = "Англ.";
            RadioRU.Text = "Руски";
            DefaultPics.Text = "По подр.";
            GoofyPics.Text = "Забавни";
            moreOptions.Text = "Доп. Опции";
            currentFileLabel = "Файл: ";
            ModDLLabel.Text = "Прогрес на изтегляне: ";
            string verString, yearString = "";
            if (File.Exists("!!!Contra009Final_Patch2.big") || File.Exists("!!!Contra009Final_Patch2.ctr"))
            {
                verString = " Пач 2";
                yearString = "2019";
            }
            else if (File.Exists("!!Contra009Final_Patch1.big") || File.Exists("!!Contra009Final_Patch1.ctr"))
            {
                verString = " Пач 1";
                yearString = "2019";
            }
            else
            {
                verString = "???";
                yearString = "2018";
            }
            versionLabel.Text = "Contra Екип " + yearString + " - Версия 009 Final" + verString + " - Launcher: " + Application.ProductVersion;

            Process[] ztExeByName = Process.GetProcessesByName("zt-x" + Globals.userOS);
            if (ztExeByName.Length > 0 && wait == false) //if zt is already running
            {
                vpn_start.BackgroundImage = Properties.Resources.vpn_on;
                IP_Label.Text = "IP: " + ip;
            }
            if (ztExeByName.Length > 0) //if zt is already running
            {
                IP_Label.Text = "IP: " + ip;
            }
            if (ztExeByName.Length == 0) //if zt is not running
            {
                IP_Label.Text = "ContraVPN: Изкл.";
            }

            // Temporary hack so update runs on main thread, motd should be rewritten to be async if possible
            try
            {
                string motd = (new WebClient { Encoding = Encoding.UTF8 }).DownloadString("https://raw.githubusercontent.com/ContraMod/Launcher/master/Versions.txt");
                string launcher_url = "https://github.com/ContraMod/Launcher/releases/download/";
                GetLauncherUpdate(motd, launcher_url);

                //Load MOTD
                new Thread(() => ThreadProcSafeMOTD(motd)) { IsBackground = true }.Start();
            }
            catch {}
        }

        private void RadioFlag_DE_CheckedChanged(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de-DE");
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
            resources.ApplyResources(this, "$this");
            applyResources(resources, Controls);
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
            toolTip1.SetToolTip(vpn_start, "Starte/SchlieЯe ContraVPN.");
            toolTip1.SetToolTip(ZTConfigBtn, "Öffnen Sie die ContraVPN-Konfigurationsdatei.");
            toolTip1.SetToolTip(ZTConsoleBtn, "Öffnen Sie die ContraVPN-Konsole.");
            toolTip1.SetToolTip(ZTNukeBtn, "Deinstallieren Sie ContraVPN.");
            toolTip1.SetToolTip(DonateBtn, "Spende an das Contra-Team.");
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
            currentFileLabel = "Datei: ";
            ModDLLabel.Text = "Downloadfortschritt: ";
            string verString, yearString = "";
            if (File.Exists("!!!Contra009Final_Patch2.big") || File.Exists("!!!Contra009Final_Patch2.ctr"))
            {
                verString = " Patch 2";
                yearString = "2019";
            }
            else if (File.Exists("!!Contra009Final_Patch1.big") || File.Exists("!!Contra009Final_Patch1.ctr"))
            {
                verString = " Patch 1";
                yearString = "2019";
            }
            else
            {
                verString = "???";
                yearString = "2018";
            }
            versionLabel.Text = "Contra Projekt Team " + yearString + " - Version 009 Final" + verString + " - Launcher: " + Application.ProductVersion;

            Process[] ztExeByName = Process.GetProcessesByName("zt-x" + Globals.userOS);
            if (ztExeByName.Length > 0 && wait == false) //if zt is already running
            {
                vpn_start.BackgroundImage = Properties.Resources.vpn_on;
                IP_Label.Text = "IP: " + ip;
            }
            if (ztExeByName.Length > 0) //if zt is already running
            {
                IP_Label.Text = "IP: " + ip;
            }
            if (ztExeByName.Length == 0) //if zt is not running
            {
                IP_Label.Text = "ContraVPN: Aus";
            }

            // Temporary hack so update runs on main thread, motd should be rewritten to be async if possible
            try
            {
                string motd = (new WebClient { Encoding = Encoding.UTF8 }).DownloadString("https://raw.githubusercontent.com/ContraMod/Launcher/master/Versions.txt");
                string launcher_url = "https://github.com/ContraMod/Launcher/releases/download/";
                GetLauncherUpdate(motd, launcher_url);

                //Load MOTD
                new Thread(() => ThreadProcSafeMOTD(motd)) { IsBackground = true }.Start();
            }
            catch {}
        }

        private void Resolution_Click(object sender, EventArgs e) //Opens More Options form
        {
            //Delete duplicate GameData if such exists
            if (File.Exists("!!!Contra009Final_Patch2_GameData.ctr") && File.Exists("!!!Contra009Final_Patch2_GameData.big"))
            {
                File.Delete("!!!Contra009Final_Patch2_GameData.big");
            }
            //Enable GameData so that we can show current camera height in Options
            if (File.Exists("!!!Contra009Final_Patch2_GameData.ctr"))
            {
                File.Move("!!!Contra009Final_Patch2_GameData.ctr", "!!!Contra009Final_Patch2_GameData.big");
            }

            if (File.Exists(UserDataLeafName() + "Options.ini") || (File.Exists(myDocPath + "Options.ini")))
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
            else
            {
                if (Globals.GB_Checked == true)
                {
                    MessageBox.Show("Options.ini not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (Globals.RU_Checked == true)
                {
                    MessageBox.Show("Файл \"Options.ini\" не найден!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (Globals.UA_Checked == true)
                {
                    MessageBox.Show("Файл Options.ini не знайдений!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (Globals.BG_Checked == true)
                {
                    MessageBox.Show("Options.ini не беше намерен!", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (Globals.DE_Checked == true)
                {
                    MessageBox.Show("Options.ini nicht gefunden!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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
            button18.BackgroundImage = Properties.Resources.exit11;
        }

        private void button18_MouseLeave(object sender, EventArgs e)
        {
            button18.BackgroundImage = Properties.Resources.exit1;
        }

        private void button17_MouseEnter(object sender, EventArgs e)
        {
            button17.BackgroundImage = Properties.Resources.min11;
        }

        private void button17_MouseLeave(object sender, EventArgs e)
        {
            button17.BackgroundImage = Properties.Resources.min;
        }

        int ztTimer = 0;
        private void RefreshVpnIpTimer_Tick(object sender, EventArgs e) //refresh VPN IP five times
        {
            ZT_IP();
            ztTimer++;
            if (ztTimer == 3)
            {
                refreshVpnIpTimer.Interval = 5000;
            }
            if (ztTimer == 4)
            {
                refreshVpnIpTimer.Interval = 10000;
            }
            if (ztTimer == 5)
            {
                refreshVpnIpTimer.Enabled = false;
            }
        }

        private void vpn_start_Click(object sender, EventArgs e)
        {
            disableVPNOnBtn = false;

            if (IsRunningOnWine()) return;

            if (File.Exists("generals.ctr") && CalculateMD5("generals.ctr") == "ee7d5e6c2d7fb66f5c27131f33da5fd3")
            {
                Process[] ztExeByName = Process.GetProcessesByName("zt-x" + Globals.userOS);
                if (ztExeByName.Length == 0)
                {
                    var instance = new ZT();
                    instance.CheckZTInstall("https://download.zerotier.com/dist/ZeroTier%20One.msi");
                }

                if (Globals.ZTReady == 4)
                {
                    try //check if zt is running or not
                    {

                        if (ztExeByName.Length > 0) //if zt is running but we are clicking button again to turn it off
                        {
                            Process[] vpnprocesses = Process.GetProcessesByName("zt-x" + Globals.userOS);
                            foreach (Process vpnprocess in vpnprocesses)
                            {
                                vpnprocess.Kill();
                                vpnprocess.WaitForExit();
                                vpnprocess.Dispose();

                                IP_LabelReset();

                                vpn_start.BackgroundImage = Properties.Resources.vpn_off;
                                return;
                            }
                        }
                        else
                        {
                            StartZT();
                        }
                    }
                    catch { }
                }
            }
            else
            {
                if (Globals.GB_Checked == true)
                {
                    MessageBox.Show("\"generals.ctr\" not found or checksum mismatch! Please, extract it from the \"Contra009FinalPatch2\" archive if you want camera height setting to work and be able to play online with ContraVPN.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (Globals.RU_Checked == true)
                {
                    MessageBox.Show("\"generals.ctr\" не найден или несоответствие контрольной суммы! Извлеките его из архива \"Contra009FinalPatch2\", если вы хотите, чтобы настройка высоты камеры работала и была возможность играть онлайн с ContraVPN.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (Globals.UA_Checked == true)
                {
                    MessageBox.Show("\"generals.ctr\" не знайден або невідповідність контрольної суми! Будь ласка, витягніть його з архіву \"Contra009FinalPatch2\", якщо ви хочете, щоб налаштування висоти камери працювало та матимете змогу грати в режимі он-лайн з ContraVPN.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (Globals.BG_Checked == true)
                {
                    MessageBox.Show("\"generals.ctr\" не е намерен или има несъответствие в контролната сума! Моля, извлечете го от архива \"Contra009FinalPatch2\", ако искате настройката на височината на камерата да работи и да можете да играете онлайн с ContraVPN.", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (Globals.DE_Checked == true)
                {
                    MessageBox.Show("\"generals.ctr\" nicht gefunden oder Prüfsummeninkongruenz! Bitte extrahieren Sie es aus dem Archiv \"Contra009FinalPatch2\", wenn die Einstellung der Kamerahöhe funktionieren soll und Sie mit ContraVPN online spielen können.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ZT_IP()
        {
            Process ztExe = new Process();
            ztExe.StartInfo.WorkingDirectory = contravpnPath;
            ztExe.StartInfo.FileName = @"C:\windows\system32\windowspowershell\v1.0\powershell.exe";
            ztExe.StartInfo.Arguments = "./zt-cli listnetworks";
            ztExe.StartInfo.UseShellExecute = false;
            ztExe.StartInfo.CreateNoWindow = true;
            ztExe.StartInfo.RedirectStandardOutput = true;

            try
            {
                ztExe.Start();
                ip = ztExe.StandardOutput.ReadToEnd();
                ip = Regex.Match(ip, "100.100.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-5][0-4]).([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-5][0-4])[^/]*").Value.Trim();
                ztExe.WaitForExit();
                //MessageBox.Show(ip);

                if (!string.IsNullOrWhiteSpace(ip))
                {
                    refreshVpnIpTimer.Enabled = false;
                    //Properties.Settings.Default.IP_Label = "IP: " + ip;
                    IP_Label.Text = "IP: " + ip;
                    void writeIPAddress(string path)
                    {
                        File.WriteAllText(path, Regex.Replace(File.ReadAllText(path), "^IPAddress.*\\S+", $"IPAddress = {ip}", RegexOptions.Multiline));
                    }
                    if (File.Exists(UserDataLeafName() + "Options.ini"))
                    {
                        writeIPAddress(UserDataLeafName() + "Options.ini");
                    }
                    else if (File.Exists(myDocPath + "Options.ini"))
                    {
                        writeIPAddress(myDocPath + "Options.ini");
                    }
                    //else
                    //{
                    //    var cannotsaveip_lang = new Dictionary<string, bool>
                    //    {
                    //        {"Options.ini not found!\nCannot write IPAddress.", Globals.GB_Checked},
                    //        {"Файл Options.ini не найден!\nНевозможно записать IPAddress.", Globals.RU_Checked},
                    //        {"Файл Options.ini не знайдений!\nНеможливо написати IPAddress.", Globals.UA_Checked},
                    //        {"Options.ini не беше намерен!\nНе може запише IPAddress.", Globals.BG_Checked},
                    //        {"Options.ini nicht gefunden!\nIPAddress kann nicht geschrieben werden.", Globals.DE_Checked},
                    //    };

                    //    //Too spammy
                    //    //MessageBox.Show(cannotsaveip_lang.Single(l => l.Value).Key, null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    //Console.Error.WriteLine(cannotsaveip_lang.Single(l => l.Value).Key);
                    //}
                }
                else
                {
                    //MessageBox.Show(ip);
                    //MessageBox.Show(Output);

                    IP_Label.Text = "IP: " + new Dictionary<string, bool>
                    {
                        {"not compatible", Globals.GB_Checked},
                        {"несовместимый", Globals.RU_Checked},
                        {"несумісні", Globals.UA_Checked},
                        {"несъвместим", Globals.BG_Checked},
                        {"Nicht Kompatibel", Globals.DE_Checked},
                    }.Single(l => l.Value).Key;
                }
            }
            catch {}// (Exception ex) { Console.Error.WriteLine(ex); }
        }

        private void RadioFlag_GB_MouseEnter(object sender, EventArgs e)
        {
            RadioFlag_GB.BackgroundImage = Properties.Resources.flag_gb_tr;
        }
        private void RadioFlag_GB_MouseLeave(object sender, EventArgs e)
        {
            RadioFlag_GB.BackgroundImage = Properties.Resources.flag_gb;
        }

        private void RadioFlag_RU_MouseEnter(object sender, EventArgs e)
        {
            RadioFlag_RU.BackgroundImage = Properties.Resources.flag_ru_tr;
        }
        private void RadioFlag_RU_MouseLeave(object sender, EventArgs e)
        {
            RadioFlag_RU.BackgroundImage = Properties.Resources.flag_ru;
        }

        private void RadioFlag_UA_MouseEnter(object sender, EventArgs e)
        {
            RadioFlag_UA.BackgroundImage = Properties.Resources.flag_ua_tr;
        }
        private void RadioFlag_UA_MouseLeave(object sender, EventArgs e)
        {
            RadioFlag_UA.BackgroundImage = Properties.Resources.flag_ua;
        }

        private void RadioFlag_BG_MouseEnter(object sender, EventArgs e)
        {
            RadioFlag_BG.BackgroundImage = Properties.Resources.flag_bg_tr;
        }
        private void RadioFlag_BG_MouseLeave(object sender, EventArgs e)
        {
            RadioFlag_BG.BackgroundImage = Properties.Resources.flag_bg;
        }

        private void RadioFlag_DE_MouseEnter(object sender, EventArgs e)
        {
            RadioFlag_DE.BackgroundImage = Properties.Resources.flag_de_tr;
        }

        private void RadioFlag_DE_MouseLeave(object sender, EventArgs e)
        {
            RadioFlag_DE.BackgroundImage = Properties.Resources.flag_de;
        }

        private void vpn_start_MouseDown(object sender, MouseEventArgs e)
        {
            vpn_start.BackgroundImage = Properties.Resources._button_vpn_highlight;
            vpn_start.ForeColor = SystemColors.ButtonHighlight;
            vpn_start.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }

        private void vpn_start_MouseEnter(object sender, EventArgs e)
        {
            Process[] ztExeByName = Process.GetProcessesByName("zt-x" + Globals.userOS);
            if (ztExeByName.Length > 0 && Globals.ZTReady == 4 && disableVPNOnBtn == false)
            {
                vpn_start.BackgroundImage = Properties.Resources.vpn_on_over;
                vpn_start.ForeColor = SystemColors.ButtonHighlight;
                vpn_start.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            }
            else
            {
                vpn_start.BackgroundImage = Properties.Resources.vpn_off_over;
                vpn_start.ForeColor = SystemColors.ButtonHighlight;
                vpn_start.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            }
        }

        private void vpn_start_MouseLeave(object sender, EventArgs e)
        {
            Process[] ztExeByName = Process.GetProcessesByName("zt-x" + Globals.userOS);
            if (ztExeByName.Length > 0 && Globals.ZTReady == 4 && disableVPNOnBtn == false)
            {
                vpn_start.BackgroundImage = Properties.Resources.vpn_on;
                vpn_start.ForeColor = SystemColors.ButtonHighlight;
                vpn_start.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            }
            else if (disableVPNBtnChangeTimer.Enabled == false)
            {
                vpn_start.BackgroundImage = Properties.Resources.vpn_off;
                vpn_start.ForeColor = SystemColors.ButtonHighlight;
                vpn_start.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            }
            //MessageBox.Show(Globals.ZTReady.ToString());
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //If updating has failed, clear the 0KB file
            if (File.Exists($"{launcherExecutingPath}\\Contra_Launcher_{newVersion}.exe") && (applyNewLauncher == false))
            {
                File.Delete($"{launcherExecutingPath}\\Contra_Launcher_{newVersion}.exe");
            }
            //This renames the original file so any shortcut works and names it accordingly after the update
            if (File.Exists($"{launcherExecutingPath}\\Contra_Launcher_{newVersion}.exe") && (applyNewLauncher == true))
            {
                File.Move($"{launcherExecutingPath}\\Contra_Launcher.exe", $"{launcherExecutingPath}\\Contra_Launcher_ToDelete.exe");
                File.Move($"{launcherExecutingPath}\\Contra_Launcher_{newVersion}.exe", $"{launcherExecutingPath}\\Contra_Launcher.exe");
                System.Diagnostics.Process.Start(Path.Combine(launcherExecutingPath, "Contra_Launcher.exe"));
            }
        }

        public static bool isGentoolInstalled(string gentoolPath)
        {
            try
            {
                var size = GetFileVersionInfoSize(gentoolPath, out _);
                if (size == 0) { throw new Win32Exception(); };
                var bytes = new byte[size];
                bool success = GetFileVersionInfo(gentoolPath, 0, size, bytes);
                if (!success) { throw new Win32Exception(); }

                VerQueryValue(bytes, @"\StringFileInfo\040904E4\ProductName", out IntPtr ptr, out _);
                return Marshal.PtrToStringUni(ptr) == "GenTool";
            }
            catch //(Exception ex)
            {
                //Console.Error.WriteLine(ex);
                return false;
            }
        }

        public static bool isGentoolOutdated(string gentoolPath, int minVersion)
        {
            try
            {
                var size = GetFileVersionInfoSize(gentoolPath, out _);
                if (size == 0) { throw new Win32Exception(); };
                var bytes = new byte[size];
                bool success = GetFileVersionInfo(gentoolPath, 0, size, bytes);
                if (!success) { throw new Win32Exception(); }

                // 040904E4 US English + CP_USASCII
                VerQueryValue(bytes, @"\StringFileInfo\040904E4\ProductVersion", out IntPtr ptr, out _);
                return int.Parse(Marshal.PtrToStringUni(ptr)) < minVersion;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        private void disableVPNBtnChangeTimer_Tick(object sender, EventArgs e)
        {
            disableVPNBtnChangeTimer.Enabled = false;
        }

        private void ZTConsoleBtn_Click(object sender, EventArgs e)
        {
            if (IsRunningOnWine()) return;

            try
            {
                //string strCmdText = Path.Combine(Directory.GetCurrentDirectory(), "Start.ps1");
                var process = new Process();
                //process.StartInfo.UseShellExecute = false;
                //process.StartInfo.RedirectStandardOutput = true;
                //process.StartInfo.Verb = "runas";
                process.StartInfo.WorkingDirectory = contravpnPath;
                process.StartInfo.FileName = @"C:\windows\system32\windowspowershell\v1.0\powershell.exe";
                process.StartInfo.Arguments = "-noexit $Host.UI.RawUI.WindowTitle = 'ZeroTier Console'; Write-Host 'Enter ./zt-cli help for available options.'";
                //process.StartInfo.Arguments = "-noexit cd 'C:\\Users\\Bat Vlado\\AppData\\Local\\Contra\\vpnconfig\\zt\\CommonAppDataFolder\\ZeroTier\\One'";
                //process.StartInfo.Arguments = "-NoProfile -ExecutionPolicy Bypass -Command \"Start-Process PowerShell -Verb runAs -ArgumentList '-NoExit','cd '\"C:\\Users\\Bat Vlado\\AppData\\Local\\Contra\\vpnconfig\\zt\\CommonAppDataFolder\\ZeroTier\\One\"''\"";

                process.Start();
                //string s = process.StandardOutput.ReadToEnd();
                //MessageBox.Show(s);

                //process.WaitForExit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void ZTConsoleBtn_MouseDown(object sender, MouseEventArgs e)
        {
            ZTConsoleBtn.BackgroundImage = Properties.Resources._button_console_highlight;
            ZTConsoleBtn.ForeColor = SystemColors.ButtonHighlight;
            ZTConsoleBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void ZTConsoleBtn_MouseEnter(object sender, EventArgs e)
        {
            ZTConsoleBtn.BackgroundImage = Properties.Resources._button_console_s_tr;
            ZTConsoleBtn.ForeColor = SystemColors.ButtonHighlight;
            ZTConsoleBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void ZTConsoleBtn_MouseLeave(object sender, EventArgs e)
        {
            ZTConsoleBtn.BackgroundImage = Properties.Resources._button_console_s;
            ZTConsoleBtn.ForeColor = SystemColors.ButtonHighlight;
            ZTConsoleBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }

        private void ZTConfigBtn_Click(object sender, EventArgs e)
        {
            if (IsRunningOnWine()) return;

            try
            {
                Process.Start("notepad.exe", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt\local.conf");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void ZTConfigBtn_MouseDown(object sender, MouseEventArgs e)
        {
            ZTConfigBtn.BackgroundImage = Properties.Resources._button_config_highlight;
            ZTConfigBtn.ForeColor = SystemColors.ButtonHighlight;
            ZTConfigBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void ZTConfigBtn_MouseEnter(object sender, EventArgs e)
        {
            ZTConfigBtn.BackgroundImage = Properties.Resources._button_config_s_tr;
            ZTConfigBtn.ForeColor = SystemColors.ButtonHighlight;
            ZTConfigBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void ZTConfigBtn_MouseLeave(object sender, EventArgs e)
        {
            ZTConfigBtn.BackgroundImage = Properties.Resources._button_config_s;
            ZTConfigBtn.ForeColor = SystemColors.ButtonHighlight;
            ZTConfigBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }

        private void ZTNukeBtn_Click(object sender, EventArgs e)
        {
            if (IsRunningOnWine()) return;

            string dialogMsg = null;
            string dialogTitle = null;

            if (Globals.GB_Checked == true)
            {
                dialogMsg = "Are you sure you want to uninstall ContraVPN?";
                dialogTitle = "Uninstall ContraVPN?";
            }
            else if (Globals.RU_Checked == true)
            {
                dialogMsg = "Вы уверены, что хотите удалить ContraVPN?";
                dialogTitle = "Удалить ContraVPN?";
            }
            else if (Globals.UA_Checked == true)
            {
                dialogMsg = "Ви впевнені, що хочете видалити ContraVPN?";
                dialogTitle = "Видалити ContraVPN??";
            }
            else if (Globals.BG_Checked == true)
            {
                dialogMsg = "Сигурни ли сте, че искате да деинсталирате ContraVPN?";
                dialogTitle = "Деинсталирай ContraVPN?";
            }
            else if (Globals.DE_Checked == true)
            {
                dialogMsg = "Möchten Sie ContraVPN wirklich deinstallieren?";
                dialogTitle = "Deinstallieren Sie ContraVPN?";
            }

            DialogResult dialogResult = MessageBox.Show(dialogMsg, dialogTitle, MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                disableVPNOnBtn = true;
                var instance = new ZT();

                try
                {
                    if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt\networks.d") ||
                    Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt\networks.d").Length > 1)
                    {
                        instance.LeaveZTNetwork();
                    }
                }
                catch { }

                instance.UninstallZTDriver();

                bool didZtFolderExist;
                try
                {
                    Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Contra\vpnconfig\zt", true);
                    didZtFolderExist = true;
                }
                catch
                {
                    didZtFolderExist = false;
                }

                if (Globals.ZTDriverUninstallSuccessful == true || didZtFolderExist == true)
                {
                    if (Globals.GB_Checked == true)
                    {
                        MessageBox.Show("ContraVPN was successfully uninstalled!", "VPN uninstalled!");
                    }
                    else if (Globals.RU_Checked == true)
                    {
                        MessageBox.Show("ContraVPN был успешно удален!", "VPN удален!");
                    }
                    else if (Globals.UA_Checked == true)
                    {
                        MessageBox.Show("ContraVPN успішно видалено!", "Видалено VPN!");
                    }
                    else if (Globals.BG_Checked == true)
                    {
                        MessageBox.Show("ContraVPN беше деинсталиран успешно!", "VPN деинсталиран!");
                    }
                    else if (Globals.DE_Checked == true)
                    {
                        MessageBox.Show("ContraVPN wurde erfolgreich deinstalliert!", "VPN deinstalliert!");
                    }
                }
                else
                {
                    if (Globals.GB_Checked == true)
                    {
                        MessageBox.Show("ContraVPN is already uninstalled.", "ContraVPN already uninstalled");
                    }
                    else if (Globals.RU_Checked == true)
                    {
                        MessageBox.Show("ContraVPN уже удален.", "ContraVPN уже удален");
                    }
                    else if (Globals.UA_Checked == true)
                    {
                        MessageBox.Show("ContraVPN вже видалено.", "ContraVPN вже видалено");
                    }
                    else if (Globals.BG_Checked == true)
                    {
                        MessageBox.Show("ContraVPN вече беше деинсталиран.", "ContraVPN вече беше деинсталиран");
                    }
                    else if (Globals.DE_Checked == true)
                    {
                        MessageBox.Show("ContraVPN ist bereits deinstalliert.", "ContraVPN ist bereits deinstalliert");
                    }
                }
            }
            //else if (dialogResult == DialogResult.No)
            //{
            //    //Properties.Settings.Default.adapterExists = false;
            //}
        }

        private void ZTNukeBtn_MouseDown(object sender, MouseEventArgs e)
        {
            ZTNukeBtn.BackgroundImage = Properties.Resources._button_sm_highlight_s;
            ZTNukeBtn.ForeColor = SystemColors.ButtonHighlight;
            ZTNukeBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void ZTNukeBtn_MouseEnter(object sender, EventArgs e)
        {
            ZTNukeBtn.BackgroundImage = Properties.Resources._button_trash_tr;
            ZTNukeBtn.ForeColor = SystemColors.ButtonHighlight;
            ZTNukeBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void ZTNukeBtn_MouseLeave(object sender, EventArgs e)
        {
            ZTNukeBtn.BackgroundImage = Properties.Resources._button_trash;
            ZTNukeBtn.ForeColor = SystemColors.ButtonHighlight;
            ZTNukeBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }

        private void DonateBtn_Click(object sender, EventArgs e)
        {
            Url_open("https://www.paypal.com/paypalme2/Contramod");
        }

        private void DonateBtn_MouseDown(object sender, MouseEventArgs e)
        {
            DonateBtn.BackgroundImage = Properties.Resources._button_vpn_highlight;
            DonateBtn.ForeColor = SystemColors.ButtonHighlight;
            DonateBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void DonateBtn_MouseEnter(object sender, EventArgs e)
        {
            DonateBtn.BackgroundImage = Properties.Resources._button_donate_tr;
            DonateBtn.ForeColor = SystemColors.ButtonHighlight;
            DonateBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void DonateBtn_MouseLeave(object sender, EventArgs e)
        {
            DonateBtn.BackgroundImage = Properties.Resources._button_donate;
            DonateBtn.ForeColor = SystemColors.ButtonHighlight;
            DonateBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }

        private void openPlayersListTimer_Tick(object sender, EventArgs e)
        {

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void CancelModDLBtn_Click(object sender, EventArgs e)
        {
            httpCancellationToken.Cancel();
            //PatchDLPanel.Hide();
            //wcMod.CancelAsync();
        }
    }
}
