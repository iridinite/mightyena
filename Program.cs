/*
 * Mightyena - A Gen-III Pokémon Save Editor
 * (C) Mika Molenkamp, 2017.
*/

using System;
using System.Windows.Forms;

namespace Mightyena {

    internal static class Program {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }

    }

}
