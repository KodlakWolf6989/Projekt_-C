using System;
using System.Windows.Forms;

namespace Test_Licko
{
    internal static class Program
    {
        /// Hlavní vstupní bod aplikace.
        [STAThread]
        static void Main()
        {
            // Povolení vizuálních stylů aplikace (moderní vzhled).
            Application.EnableVisualStyles();

            // Nastavení kompatibilního vykreslování textu.
            Application.SetCompatibleTextRenderingDefault(false);

            // Spuštění hlavního okna aplikace (Form1).
            Application.Run(new Form1());
        }
    }
}

