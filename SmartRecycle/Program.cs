using System;
using System.Windows.Forms;

namespace SmartRecycle
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            using var login = new LoginForm();
            if (login.ShowDialog() != DialogResult.OK || string.IsNullOrWhiteSpace(login.LoggedInUser))
                return;

            Application.Run(new SmartRecycle.Forms.MainForm());
        }
    }
}
