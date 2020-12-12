using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SingleInstanceCore;

namespace EasySave_2._0
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, ISingleInstance
    {

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            bool isFirstInstance = SingleInstance<App>.InitializeAsFirstInstance("EasySave");
            if (!isFirstInstance)
            {
                Current.Shutdown();
            }

            MainWindow app = new MainWindow();
            app.Show();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            SingleInstance<App>.Cleanup();
        }
        public void OnInstanceInvoked(string[] args)
        {
            throw new NotImplementedException();
        }

    }
}
