using System;
using System.Windows.Forms;

namespace Return_Process
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Launch the ReturnForm instead of Form1
            Application.Run(new LibraryApp.ReturnForm());
        }
    }
}
