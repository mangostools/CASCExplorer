using CASCLib;
using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace CASCExplorer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            WDC2Reader reader = new WDC2Reader(@"f:\Dev\WoW\DBFilesClient_27481\ExpectedStat.db2");
            var row = reader.GetRow(14);
            var field = row.GetField<int>(0);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
