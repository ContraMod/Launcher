using System;
using System.Windows.Forms;
using System.Threading;

namespace Contra
{
    static class Program
    {
        private static Mutex mutex;

        [STAThread]
        static void Main()
        {
            mutex = new Mutex(false, "Contra_Launcher");

            try
            {
                try
                {
                    if (!mutex.WaitOne(5))
                    {
                        mutex.Dispose();
                        mutex = null;
                        MessageBox.Show("Contra Launcher is already running!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                catch (AbandonedMutexException) {} // Mutex wasn't fully released previous instance

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
