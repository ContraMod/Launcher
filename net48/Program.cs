using System;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace Contra
{
    static class Program
    {
        private static Mutex mutex;

        private static bool IsLikelyUpdateRestart()
        {
            try
            {
                // Check if there are any Contra_Launcher processes that started recently (within last 10 seconds)
                Process[] processes = Process.GetProcessesByName("Contra_Launcher");
                foreach (Process proc in processes)
                {
                    if (proc.Id != Process.GetCurrentProcess().Id)
                    {
                        TimeSpan runtime = DateTime.Now - proc.StartTime;
                        if (runtime.TotalSeconds < 10)
                        {
                            return true; // Another launcher started recently, likely an update restart
                        }
                    }
                }
            }
            catch
            {
                // If we can't check processes, assume it's not an update restart
            }
            return false;
        }

        [STAThread]
        static void Main()
        {
            // Check if the config file is corrupt. If yes, reset it and continue.
            try
            {
                Properties.Settings.Default.LangEN = false;
            }
            catch (System.Configuration.ConfigurationErrorsException ex)
            {
                string filename = ((System.Configuration.ConfigurationErrorsException)ex.InnerException).Filename;
                //MessageBox.Show("Contra Launcher has detected that your user settings file has become corrupted. This may be due to a crash or improper exiting of the program. Contra Launcher will now reset your user settings in order to continue.");
                File.Delete(filename);
                //Properties.Settings.Default.Reload();
                System.Diagnostics.Process.Start("Contra_Launcher.exe");
                return;
            }
            Properties.Settings.Default.LangEN = true;

            mutex = new Mutex(false, "Contra_Launcher");

            try
            {
                // First, try a quick acquisition to detect if launcher is actually running
                bool mutexAcquired = mutex.WaitOne(100); // Quick 100ms check
                
                if (!mutexAcquired)
                {
                    // Check if this is likely an update restart scenario
                    bool isUpdateRestart = IsLikelyUpdateRestart();
                    
                    if (isUpdateRestart)
                    {
                        // This is likely an update restart, wait for previous instance to shut down
                        Thread.Sleep(500);
                        
                        int retryCount = 0;
                        const int maxRetries = 2;
                        const int timeoutMs = 1000; // 1 second timeout for retries

                        while (!mutexAcquired && retryCount < maxRetries)
                        {
                            try
                            {
                                mutexAcquired = mutex.WaitOne(timeoutMs);
                                if (!mutexAcquired)
                                {
                                    retryCount++;
                                    if (retryCount < maxRetries)
                                    {
                                        // Wait a bit before retrying
                                        Thread.Sleep(300);
                                    }
                                }
                            }
                            catch (AbandonedMutexException) 
                            { 
                                // Mutex was abandoned by previous instance, we can acquire it
                                mutexAcquired = true;
                            }
                            catch (Exception)
                            {
                                // If there's any other mutex-related exception, try to create a new mutex
                                if (retryCount == maxRetries - 1)
                                {
                                    try
                                    {
                                        mutex.Dispose();
                                        mutex = new Mutex(false, "Contra_Launcher");
                                        mutexAcquired = mutex.WaitOne(1000);
                                    }
                                    catch
                                    {
                                        // If all else fails, continue without mutex protection
                                        mutexAcquired = true;
                                    }
                                }
                            }
                        }
                    }
                    // If not an update restart, we'll show the "already running" message immediately
                }

                if (!mutexAcquired)
                {
                    mutex.Dispose();
                    mutex = null;

                    if (Properties.Settings.Default.Flag_GB == true)
                    {
                        MessageBox.Show("Contra Launcher is already running!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (Properties.Settings.Default.Flag_RU == true)
                    {
                        MessageBox.Show("Contra Launcher уже запущен!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (Properties.Settings.Default.Flag_UA == true)
                    {
                        MessageBox.Show("Contra Launcher вже працює!", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (Properties.Settings.Default.Flag_BG == true)
                    {
                        MessageBox.Show("Contra Launcher е вече стартиран!", "Известие", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (Properties.Settings.Default.Flag_DE == true)
                    {
                        MessageBox.Show("Contra Launcher läuft bereits!", "Beachten", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            finally
            {
                if (mutex != null)
                {
                    mutex.ReleaseMutex();
                    mutex.Dispose();
                }
            }
        }
    }
}
