using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace bDisk
{
    public partial class App : Application
    {
        public static MainWindow mainWindow;

        private void ApplicationStartup(object sender, StartupEventArgs e)
        {
            mainWindow = new MainWindow();
            MainWindow.Show();
        }
    }
}
