using System;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace Contra
{
    static class Program
    {
        private static Mutex mutex = null;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            const string appName = "MyAppName";
            bool createdNew;

            mutex = new Mutex(true, appName, out createdNew);

            if (!File.Exists(Application.StartupPath + @"\Contra_Launcher_ToDelete.exe"))
            {
                if (!createdNew)
                {
                    //app is already running! Exiting the application
                    MessageBox.Show("Contra Launcher is already running!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
