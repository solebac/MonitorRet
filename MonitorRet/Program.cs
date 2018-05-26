using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MonitorRet
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string fileSecret = null;
            if (File.Exists("c:\\windows\\soberanu.ini"))
            {
                fileSecret = Path.Combine(Directory.GetCurrentDirectory(), "c:\\windows\\soberanu.ini");
            }
            else
            {
                fileSecret = Path.Combine(Directory.GetCurrentDirectory(), "soberanu.ini");
            }
            if (fileSecret != null)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Monitor(fileSecret));
            }
            else
            {
                MessageBox.Show("CONFIGURAÇÃO NÃO ENCONTRADA OU NÃO EXISTE.", "ERRO FATAL", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }
    }
}
