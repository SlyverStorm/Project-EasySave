using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Globalization;
using EasySave_2._0.Properties;

namespace EasySave_2._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class BaseWindow : Window
    {

        #region Methods

        /// <summary>
        /// Tells the user the source folder doesn't exist and that the save procedure has been cancelled
        /// </summary>
        /// <param name="o"></param>
        private void MessageBoxDirectorySingle(object o)
        {
            MessageBox.Show(Properties.Langs.Lang.ErrorBoxDirectorySingle, Properties.Langs.Lang.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        /// <summary>
        /// Tells the user the source folder doesn't exist and that the save procedure has been cancelled but the others will continue as usual
        /// </summary>
        /// <param name="o"></param>
        private void MessageBoxDirectoryAll(object o)
        {
            MessageBox.Show(Properties.Langs.Lang.ErrorBoxDirectoryAll, Properties.Langs.Lang.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        /// <summary>
        /// Tells the user the save procedure has been cancelled as the business software is running
        /// </summary>
        /// <param name="o"></param>
        private void MessageBoxSoftware(object o)
        {
            MessageBox.Show(Properties.Langs.Lang.ErrorBoxSoftware, Properties.Langs.Lang.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        #endregion

    }
}
