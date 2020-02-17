using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecsGemDriver
{
    static class Program
    {
        //Declare an instance for log4net
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Login fLogin = new Login();
            if (fLogin.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new Main());
            }
            else
            {
                Application.Exit();
            }
            //Application.Run(new Main());
        }
    }
}
