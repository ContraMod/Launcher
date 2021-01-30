using System;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace Contra
{
    static class Program
    {
        private static Mutex mutex;

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
                try
                {
                    if (!mutex.WaitOne(5))
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
                }
                catch (AbandonedMutexException) { } // Mutex wasn't fully released previous instance

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
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
